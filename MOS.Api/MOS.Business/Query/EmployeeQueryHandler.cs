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

public class EmployeeQueryHandler :
        IRequestHandler<GetAllAdminQuery, ApiResponse<List<AdminResponse>>>,
        IRequestHandler<GetAdminByIdQuery, ApiResponse<AdminResponse>>,
        IRequestHandler<GetAdminByParameterQuery, ApiResponse<List<AdminResponse>>>,
        IRequestHandler<GetAllPersonalQuery, ApiResponse<List<PersonalResponse>>>,
        IRequestHandler<GetPersonalByIdQuery, ApiResponse<PersonalResponse>>,
        IRequestHandler<GetPersonalByParameterQuery, ApiResponse<List<PersonalResponse>>>
{
    private readonly MosDbContext dbContext;
    private readonly IMapper mapper;

    public EmployeeQueryHandler(MosDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AdminResponse>>> Handle(GetAllAdminQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Admin>().ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Admin>, List<AdminResponse>>(list);
        return new ApiResponse<List<AdminResponse>>(mappedList);
    }

    public async Task<ApiResponse<AdminResponse>> Handle(GetAdminByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Admin>()
            .FirstOrDefaultAsync(x => x.AdminNumber == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<AdminResponse>("Record not found");
        }

        var mapped = mapper.Map<Admin, AdminResponse>(entity);
        return new ApiResponse<AdminResponse>(mapped);
    }

    public async Task<ApiResponse<List<AdminResponse>>> Handle(GetAdminByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Admin>(true);
        if (!string.IsNullOrEmpty(request.FirstName))
            predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));

        if (!string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));

        if (!string.IsNullOrEmpty(request.UserName))
            predicate.And(x => x.UserName.ToUpper().Contains(request.UserName.ToUpper()));

        if (!string.IsNullOrEmpty(request.Email))
            predicate.And(x => x.Email.ToUpper().Contains(request.Email.ToUpper()));

        var list = await dbContext.Set<Admin>()
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Admin>, List<AdminResponse>>(list);
        return new ApiResponse<List<AdminResponse>>(mappedList);
    }

    public async Task<ApiResponse<List<PersonalResponse>>> Handle(GetAllPersonalQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Personal>().ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Personal>, List<PersonalResponse>>(list);
        return new ApiResponse<List<PersonalResponse>>(mappedList);
    }

    public async Task<ApiResponse<PersonalResponse>> Handle(GetPersonalByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Personal>()
            .FirstOrDefaultAsync(x => x.PersonalNumber == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<PersonalResponse>("Record not found");
        }

        var mapped = mapper.Map<Personal, PersonalResponse>(entity);
        return new ApiResponse<PersonalResponse>(mapped);
    }

    public async Task<ApiResponse<List<PersonalResponse>>> Handle(GetPersonalByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Personal>(true);
        if (!string.IsNullOrEmpty(request.FirstName))
            predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));

        if (!string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));

        if (!string.IsNullOrEmpty(request.UserName))
            predicate.And(x => x.UserName.ToUpper().Contains(request.UserName.ToUpper()));

        if (!string.IsNullOrEmpty(request.Email))
            predicate.And(x => x.Email.ToUpper().Contains(request.Email.ToUpper()));

        var list = await dbContext.Set<Personal>()
            .Where(predicate).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Personal>, List<PersonalResponse>>(list);
        return new ApiResponse<List<PersonalResponse>>(mappedList);
    }
}


