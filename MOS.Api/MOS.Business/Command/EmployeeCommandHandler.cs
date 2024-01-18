using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class EmployeeCommandHandler :
        IRequestHandler<CreatePersonalCommand, ApiResponse<PersonalResponse>>,
        IRequestHandler<UpdatePersonalCommand, ApiResponse>,
        IRequestHandler<DeletePersonalCommand, ApiResponse>,
        IRequestHandler<CreateAdminCommand, ApiResponse<AdminResponse>>,
        IRequestHandler<UpdateAdminCommand, ApiResponse>,
        IRequestHandler<DeleteAdminCommand, ApiResponse>
    {
        private readonly MosDbContext dbContext;
        private readonly IMapper mapper;

        public EmployeeCommandHandler(MosDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        //Personal
        public async Task<ApiResponse<PersonalResponse>> Handle(CreatePersonalCommand request, CancellationToken cancellationToken)
        {
            if (await CheckEmployeeIdentityEmail(request.Model.Email, cancellationToken))
            {
                return new ApiResponse<PersonalResponse>($"{request.Model.Email} is already used");
            }
            if (await  CheckEmployeeIdentityUserName(request.Model.UserName, cancellationToken))
            {
                return new ApiResponse<PersonalResponse>($"{request.Model.UserName} is already used");
            }

            var entity = mapper.Map<PersonalRequest, Personal>(request.Model);

            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<Personal, PersonalResponse>(entityResult.Entity);
            return new ApiResponse<PersonalResponse>(mapped);
        }

        public async Task<ApiResponse> Handle(UpdatePersonalCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Personal>().Where(x => x.PersonalNumber == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            fromdb.FirstName = request.Model.FirstName;
            fromdb.LastName = request.Model.LastName;
            fromdb.Email = request.Model.Email;
            fromdb.Password = request.Model.Password;
            fromdb.UserName = request.Model.UserName;
            fromdb.IBAN = request.Model.IBAN;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeletePersonalCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Personal>()
                .Join(
                    dbContext.Set<Expense>(),
                    personal => personal.PersonalNumber, // Personal tablosundaki ortak alan
                    expense => expense.PersonalNumber, // AnotherTable tablosundaki ortak alan
                    (personal, expense) => new
                    {
                        Personal = personal,
                        Expense = expense
                    }
                )
                .Where(x => x.Personal.PersonalNumber == request.Id)
                .ToListAsync(cancellationToken);

            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }
            foreach (var item in fromdb)
            {
                if (item.Expense.ApprovalStatus == Base.Enum.ApprovalStatus.Saved)
                {
                    return new ApiResponse("Record has a expense process. Cannot delete that personal");
                }
            }


            //fromdb.IsActive = false;

            dbContext.Personals.Remove(await dbContext.Set<Personal>().FirstOrDefaultAsync(x => x.PersonalNumber == request.Id)); //hard delate işlemi
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        //Admin 
        public async Task<ApiResponse<AdminResponse>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            if (await CheckEmployeeIdentityEmail(request.Model.Email, cancellationToken))
            {
                return new ApiResponse<AdminResponse>($"{request.Model.Email} is already used");
            }
            if (await  CheckEmployeeIdentityUserName(request.Model.UserName, cancellationToken))
            {
                return new ApiResponse<AdminResponse>($"{request.Model.UserName} is already used");
            }

            var entity = mapper.Map<AdminRequest, Admin>(request.Model);

            var entityResult = await dbContext.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<Admin, AdminResponse>(entityResult.Entity);
            return new ApiResponse<AdminResponse>(mapped);

        }

        public async Task<ApiResponse> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Personal>().Where(x => x.PersonalNumber == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            fromdb.FirstName = request.Model.FirstName;
            fromdb.LastName = request.Model.LastName;
            fromdb.Email = request.Model.Email;
            fromdb.Password = request.Model.Password;
            fromdb.UserName = request.Model.UserName;


            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Personal>().Where(x => x.PersonalNumber == request.Id)
                 .FirstOrDefaultAsync(cancellationToken);

            if (fromdb == null)
            {
                return new ApiResponse("Record not found");
            }

            //fromdb.IsActive = false;

            dbContext.Personals.Remove(fromdb); //hard delate işlemi
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }


        private async Task<bool> CheckEmployeeIdentityUserName(string UserName, CancellationToken cancellationToken)
        {
            var checkIdentity = await dbContext.Set<Admin>().Where(x => x.UserName == UserName)
            .FirstOrDefaultAsync(cancellationToken);
            if (checkIdentity != null)
            {
                return true;
            }
            var checkIdentitypersonal = await dbContext.Set<Personal>().Where(x => x.UserName == UserName)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkIdentity != null)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> CheckEmployeeIdentityEmail(string Email, CancellationToken cancellationToken)
        {
            var checkIdentity = await dbContext.Set<Admin>().Where(x => x.Email == Email)
            .FirstOrDefaultAsync(cancellationToken);
            if (checkIdentity != null)
            {
                return true;
            }
            var checkIdentitypersonal = await dbContext.Set<Personal>().Where(x => x.Email == Email)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkIdentity != null)
            {
                return true;
            }
            return false;
        }
    }
}