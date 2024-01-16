using MediatR;
using MOS.Base.Response;
using MOS.Schema;


namespace MOS.Business.Cqrs;

//Admin CQRS
public record CreateAdminCommand(AdminRequest Model) : IRequest<ApiResponse<AdminResponse>>;
public record UpdateAdminCommand(int Id,AdminRequest Model) : IRequest<ApiResponse>;
public record DeleteAdminCommand(int Id) : IRequest<ApiResponse>;

public record GetAllAdminQuery() : IRequest<ApiResponse<List<AdminResponse>>>;
public record GetAdminByIdQuery(int Id) : IRequest<ApiResponse<AdminResponse>>;
public record GetAdminByParameterQuery(string FirstName,string LastName,string UserName, string Email) : IRequest<ApiResponse<List<AdminResponse>>>;

//Personal CQRS
public record CreatePersonalCommand(PersonalRequest Model) : IRequest<ApiResponse<PersonalResponse>>;
public record UpdatePersonalCommand(int Id,PersonalRequest Model) : IRequest<ApiResponse>;
public record DeletePersonalCommand(int Id) : IRequest<ApiResponse>;

public record GetAllPersonalQuery() : IRequest<ApiResponse<List<PersonalResponse>>>;
public record GetPersonalByIdQuery(int Id) : IRequest<ApiResponse<PersonalResponse>>;
public record GetPersonalByParameterQuery(string FirstName,string LastName,string UserName, string Email) : IRequest<ApiResponse<List<PersonalResponse>>>;

