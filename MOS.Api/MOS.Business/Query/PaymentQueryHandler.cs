using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MOS.Base.Response;
using MOS.Business.Cqrs;
using MOS.Data;
using MOS.Data.Entity;
using MOS.Schema;

namespace MOS.Business.Query;

public class PaymentQueryHandler :
        IRequestHandler<GetAllPaymentQuery, ApiResponse<List<PaymentResponse>>>,
        IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>,
        IRequestHandler<GetPaymentByParameterQuery, ApiResponse<List<PaymentResponse>>>
{
    private readonly MosDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentQueryHandler(MosDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetAllPaymentQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Payment>().ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Payment>, List<PaymentResponse>>(list);
        return new ApiResponse<List<PaymentResponse>>(mappedList);
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Payment>()
            .FirstOrDefaultAsync(x => x.PaymentId == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<PaymentResponse>("Record not found");
        }

        var mapped = mapper.Map<Payment, PaymentResponse>(entity);
        return new ApiResponse<PaymentResponse>(mapped);
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetPaymentByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Payment>(true);
        if (!string.IsNullOrEmpty(request.IBAN))
            predicate.And(x => x.IBAN.ToUpper().Contains(request.IBAN.ToUpper()));

        if (request.Min != null)
            predicate.And(x => x.PaymentAmount > request.Min);

        if (request.Max != null)
            predicate.And(x => x.PaymentAmount < request.Max);

        var list = await dbContext.Set<Payment>()
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Payment>, List<PaymentResponse>>(list);
        return new ApiResponse<List<PaymentResponse>>(mappedList);
    }


}


