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

public class ExpenseQueryHandler :
        IRequestHandler<GetAllOwnExpenseQuery, ApiResponse<List<ExpenseResponse>>>,
        IRequestHandler<GetOwnExpenseByIdQuery, ApiResponse<ExpenseResponse>>,
        IRequestHandler<GetOwnExpenseByParameterQuery, ApiResponse<List<ExpenseResponse>>>,
        IRequestHandler<GetAllExpenseQuery, ApiResponse<List<ExpenseResponse>>>,
        IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>,
        IRequestHandler<GetExpenseByParameterQuery, ApiResponse<List<ExpenseResponse>>>
{
    private readonly MosDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseQueryHandler(MosDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllOwnExpenseQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Expense>().Where(x => x.PersonalNumber == request.PersonalNumber)
        .ToListAsync(cancellationToken);
        if (list == null)
        {
            return new ApiResponse<List<ExpenseResponse>>("Record not found");
        }


        var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
        return new ApiResponse<List<ExpenseResponse>>(mappedList);
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(GetOwnExpenseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Expense>().Where(x => x.PersonalNumber == request.PersonalNumber)
            .FirstOrDefaultAsync(x => x.ExpenseId == request.ExpenseId, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<ExpenseResponse>("Record not found");
        }

        var mapped = mapper.Map<Expense, ExpenseResponse>(entity);
        return new ApiResponse<ExpenseResponse>(mapped);
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetOwnExpenseByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Expense>(true);
        if (!string.IsNullOrEmpty(request.ExpenseName))
            predicate.And(x => x.ExpenseName.ToUpper().Contains(request.ExpenseName.ToUpper()));

        if (request.ApprovalStatus != null)
            predicate.And(x => ((int)x.ApprovalStatus) == request.ApprovalStatus);

        var list = await dbContext.Set<Expense>().Where(x => x.PersonalNumber == request.PersonalNumber)
            .Where(predicate).ToListAsync(cancellationToken);

        if (list == null)
        {
            return new ApiResponse<List<ExpenseResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
        return new ApiResponse<List<ExpenseResponse>>(mappedList);
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpenseQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Expense>().ToListAsync(cancellationToken);
        if (list == null)
        {
            return new ApiResponse<List<ExpenseResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
        return new ApiResponse<List<ExpenseResponse>>(mappedList);
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Expense>()
            .FirstOrDefaultAsync(x => x.ExpenseId == request.ExpenseId, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<ExpenseResponse>("Record not found");
        }

        var mapped = mapper.Map<Expense, ExpenseResponse>(entity);
        return new ApiResponse<ExpenseResponse>(mapped);
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetExpenseByParameterQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Min != null && request.Max != null && request.Min > request.Max)
        {
            return new ApiResponse<List<ExpenseResponse>>($" {request.Min} - {request.Max} range is invalid. Please check");
        }
        if (request.afterdate != null && request.beforedate != null && request.afterdate > request.beforedate)
        {
            return new ApiResponse<List<ExpenseResponse>>($" {request.afterdate} - {request.beforedate} range is invalid. Please check");
        }
        var predicate = PredicateBuilder.New<Expense>(true);
        if (!string.IsNullOrEmpty(request.ExpenseName))
            predicate.And(x => x.ExpenseName.ToUpper().Contains(request.ExpenseName.ToUpper()));

        if (request.PersonalNumber != null)
            predicate.And(x => x.PersonalNumber == request.PersonalNumber);

        if (request.ApprovalStatus != null)
            predicate.And(x => ((int)x.ApprovalStatus) == request.ApprovalStatus);

        if (request.Min != null)
            predicate.And(x => x.ExpenseAmount > request.Min);

        if (request.Max != null)
            predicate.And(x => x.ExpenseAmount < request.Max);

        if (request.afterdate != null)
            predicate.And(x => x.ExpenseCreateDate > request.afterdate);

        if (request.beforedate != null)
            predicate.And(x => x.ExpenseCreateDate < request.beforedate);


        var list = await dbContext.Set<Expense>()
            .Where(predicate).ToListAsync(cancellationToken);

        if (list == null)
        {
            return new ApiResponse<List<ExpenseResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
        return new ApiResponse<List<ExpenseResponse>>(mappedList);
    }


}


