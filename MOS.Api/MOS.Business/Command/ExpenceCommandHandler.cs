using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using MediatR;
using Microsoft.EntityFrameworkCore;
using MOS.Base.Enum;
using MOS.Base.Response;
using MOS.Business.Cqrs;
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

        public ExpenseCommandHandler(MosDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        //Expense
        public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            //public string ExpenseName { get; set; }
            //public string ExpenseCategory { get; set; }

            //ExpenseAmount  ExpenseDescription  InvoiceImageFilePath  ?Location //mapden gelir

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
            if (fromdb.ApprovalStatus==ApprovalStatus.Approved ||fromdb.ApprovalStatus==ApprovalStatus.Rejected )
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
            if (fromdb.ApprovalStatus==ApprovalStatus.Approved ||fromdb.ApprovalStatus==ApprovalStatus.Rejected )
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

            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
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
            
            fromdb.ApprovalStatus = ApprovalStatus.Rejected;
            fromdb.DeciderAdminNumber = request.AdminNumber;
            fromdb.DecisionDescription = $"Harcamanız Reddedilmiştir. Açıkama: {request.Model.DecisionDescription}";
            fromdb.DecisionDate = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}