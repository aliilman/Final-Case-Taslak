using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using MediatR;
using Microsoft.EntityFrameworkCore;
using MOS.Base.Response;
using MOS.Business.Cqrs;
using MOS.Data;
using MOS.Data.Entity;
using MOS.Schema;


namespace MOS.Business.Command
{
    public class PaymentCommandHandler :
        IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>,
        IRequestHandler<UpdatePaymentCommand, ApiResponse>,
        IRequestHandler<DeletePaymentCommand, ApiResponse>

    {
        private readonly MosDbContext dbContext;
        private readonly IMapper mapper;

        public PaymentCommandHandler(MosDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        //Payment
        public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            //her Expense için bir Payment olmalıdır. Hali hazırda bir ödeme tanımlı ise başkası tanımlanamaz
            var checkIdentity = await dbContext.Set<Payment>().Where(x => x.ExpenseId == request.Model.ExpenseId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkIdentity != null)
            {
                return new ApiResponse<PaymentResponse>($"{request.Model.ExpenseId} already have a Payment");
            }

            var entity = mapper.Map<PaymentRequest, Payment>(request.Model);

            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<Payment, PaymentResponse>(entityResult.Entity);
            return new ApiResponse<PaymentResponse>(mapped);
        }

        public async Task<ApiResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Payment>().Where(x => x.PaymentId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            fromdb.IBAN = request.Model.IBAN;
            fromdb.PaymentAmount = request.Model.PaymentAmount;
            fromdb.Description = request.Model.Description;
            fromdb.PaymentType = request.Model.PaymentType;
            fromdb.ExpenseDate = request.Model.ExpenseDate;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Payment>().Where(x => x.PaymentId == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            //fromdb.IsActive = false;

            dbContext.Payments.Remove(fromdb); //hard delate işlemi
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

    }
}