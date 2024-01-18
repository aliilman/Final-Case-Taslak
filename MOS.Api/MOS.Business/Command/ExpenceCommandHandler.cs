using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using MOS.Base.DTO;
using MOS.Base.Enum;
using MOS.Base.Response;
using MOS.Business.Cqrs;
using MOS.Business.Service;
using MOS.Data;
using MOS.Data.Entity;
using MOS.Schema;

namespace MOS.Business.Command
{
    public class ExpenseCommandHandler :
        IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
        IRequestHandler<UpdateExpenseCommand, ApiResponse>,
        IRequestHandler<DeleteExpenseCommand, ApiResponse>,
        IRequestHandler<ApproveByIdCommand, ApiResponse>,
        IRequestHandler<RejectedByIdCommand, ApiResponse>

    {
        private readonly MosDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IRabbitMQService rabbitMQService;

        public ExpenseCommandHandler(MosDbContext dbContext, IMapper mapper,IRabbitMQService rabbitMQService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.rabbitMQService=rabbitMQService;
        }
        //Expense
        public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            // ExpenseName ExpenseCategory ExpenseAmount  ExpenseDescription  InvoiceImageFilePath  ?Location //mapden gelir

            var entity = mapper.Map<PersonalExpenseRequest, Expense>(request.Model);
            entity.PersonalNumber = request.PersonalNumber;

            entity.ApprovalStatus = ApprovalStatus.Saved;
            entity.ExpenseCreateDate = DateTime.Now;

            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<Expense, ExpenseResponse>(entityResult.Entity);
            return new ApiResponse<ExpenseResponse>(mapped);
        }

        public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Expense>().Where(x => x.PersonalNumber == request.PersonalNumber)
                .FirstOrDefaultAsync(a => a.ExpenseId == request.ExpenseId, cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }
            if (fromdb.ApprovalStatus == ApprovalStatus.Approved || fromdb.ApprovalStatus == ApprovalStatus.Rejected)
            {
                return new ApiResponse("Onaylanmış veya Reddedilmiş kayıtlar üzerinde değişiklik yapılamaz");
            }

            fromdb.ExpenseAmount = request.Model.ExpenseAmount;
            fromdb.ExpenseDescription = request.Model.ExpenseDescription;
            fromdb.InvoiceImageFilePath = request.Model.InvoiceImageFilePath;
            fromdb.Location = request.Model.Location;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {

            var fromdb = await dbContext.Set<Expense>().Where(x => x.PersonalNumber == request.PersonalNumber)
                .FirstOrDefaultAsync(a => a.ExpenseId == request.ExpenseId, cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            if (fromdb.ApprovalStatus == ApprovalStatus.Approved || fromdb.ApprovalStatus == ApprovalStatus.Rejected)
            {
                return new ApiResponse("Onaylanmış veya Reddedilmiş kayıtlar silinemez");
            }
            //fromdb.IsActive = false;

            dbContext.Expenses.Remove(fromdb); //hard delate işlemi
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(ApproveByIdCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Expense>().Where(a => a.ExpenseId == request.ExpenseId)
                 .FirstOrDefaultAsync(cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }
            var personal = await dbContext.Set<Personal>().Where(a => a.PersonalNumber == fromdb.PersonalNumber)
                .FirstOrDefaultAsync(cancellationToken);

            if (personal == null)
            {
                return new ApiResponse("Personal not found");
            }
            if (fromdb.ApprovalStatus != ApprovalStatus.Saved)
            {
                return new ApiResponse("The decision for this Expense has already been made.");
            }

            fromdb.ApprovalStatus = ApprovalStatus.Approved;
            fromdb.DeciderAdminNumber = request.AdminNumber;
            fromdb.DecisionDescription = $"Harcamanız onaylanmış ve ödeme emri tanımlanmıştır. Onay açılaması: {request.Model.DecisionDescription}";
            fromdb.DecisionDate = DateTime.Now;

            fromdb.Payment = new Payment
            {
                ExpenseId = fromdb.ExpenseId,
                IBAN = personal.IBAN,
                PaymentAmount = fromdb.ExpenseAmount,
                Description = $"{fromdb.ExpenseId} nolu şirket harcamanızın ücreti. Onay açıklaması:{request.Model.DecisionDescription}",
                PaymentType = "EFT",
                ExpenseDate = DateTime.Now
            };
            await dbContext.SaveChangesAsync(cancellationToken);

            NotificationCrateAndSend(true,fromdb, personal.GetType().Name);


            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(RejectedByIdCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Expense>().Where(a => a.ExpenseId == request.ExpenseId)
                 .FirstOrDefaultAsync(cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }
            var personal = await dbContext.Set<Personal>().Where(a => a.PersonalNumber == fromdb.PersonalNumber)
                    .FirstOrDefaultAsync(cancellationToken);

            if (personal == null)
            {
                return new ApiResponse("Personal not found");
            }

            if (fromdb.ApprovalStatus != ApprovalStatus.Saved)
            {
                return new ApiResponse("The decision for this Expense has already been made.");
            }

            fromdb.ApprovalStatus = ApprovalStatus.Rejected;
            fromdb.DeciderAdminNumber = request.AdminNumber;
            fromdb.DecisionDescription = $"Harcamanız Reddedilmiştir. Açıkama: {request.Model.DecisionDescription}";
            fromdb.DecisionDate = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);

            NotificationCrateAndSend(false,fromdb, personal.GetType().Name);

            return new ApiResponse();
        }

        private async Task NotificationCrateAndSend(bool Approved, Expense expense, string role)
        {
            NotificationDTO notificationDTO = new();
            if (Approved)
            {
                notificationDTO.Title = "Harcamanız onaylandı";
                notificationDTO.Content = $"'{expense.ExpenseName}' isimli harcamanız oanylandı: Ödeme emriniz tanımlanmıştır. ";
                notificationDTO.EmployeNumber = expense.PersonalNumber;
                notificationDTO.EmployeRole = role;
                rabbitMQService.SendPaymentQueue(expense.Payment.PaymentId.ToString());
            }
            else
            {
                notificationDTO.Title = "Harcamanız reddedildi";
                notificationDTO.Content = $"'{expense.ExpenseName}' isimli harcamanız reddedildi. Açıklama:{expense.DecisionDescription} ";
                notificationDTO.EmployeNumber = expense.PersonalNumber;
                notificationDTO.EmployeRole = role;
            }
            rabbitMQService.SendNotificationQueue(notificationDTO);

        }
    }
}