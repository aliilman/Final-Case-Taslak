using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Base.Response;
using MOS.Business.Cqrs;
using MOS.Schema;


namespace MOS.Api.Controllers;

[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class EmployeeController : Controller
{
    private readonly IMediator mediator;

    public EmployeeController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet("GetAllAdmin")]
    public async Task<ApiResponse<List<AdminResponse>>> GetAllAdmin()
    {
        var operation = new GetAllAdminQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetAdminById/{id}")]
    public async Task<ApiResponse<AdminResponse>> GetAdminById(int id)
    {
        var operation = new GetAdminByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetAdminParameter")]
    public async Task<ApiResponse<List<AdminResponse>>> GetAdminParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName,
        [FromQuery] string? UserName,
        [FromQuery] string? Email)
    {
        var operation = new GetAdminByParameterQuery(FirstName, LastName, UserName,Email);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost("CreateAdmin")]
    public async Task<ApiResponse<AdminResponse>> CreateAdmin([FromBody] AdminRequest Admin)
    {
        var operation = new CreateAdminCommand(Admin);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("UpdateAdmin/{id}")]
    public async Task<ApiResponse> UpdateAdmin(int id, [FromBody] AdminRequest Admin)
    {
        var operation = new UpdateAdminCommand(id, Admin);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("DeleteAdmin/{id}")]
    public async Task<ApiResponse> DeleteAdmin(int id)
    {
        var operation = new DeleteAdminCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

    //Personal
    [HttpGet("GetAllPersonal")]
    public async Task<ApiResponse<List<PersonalResponse>>> GetAllPersonal()
    {
        var operation = new GetAllPersonalQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetPersonalById/{id}")]
    public async Task<ApiResponse<PersonalResponse>> GetPersonalById(int id)
    {
        var operation = new GetPersonalByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetPersonaByParameter")]
    public async Task<ApiResponse<List<PersonalResponse>>> GetPersonaByParameter(
        [FromQuery] string? FirstName,
        [FromQuery] string? LastName,
        [FromQuery] string? UserName,
        [FromQuery] string? Email)
    {
        var operation = new GetPersonalByParameterQuery(FirstName, LastName, UserName,Email);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost("CreatePersonal")]
    public async Task<ApiResponse<PersonalResponse>> CreatePersonal([FromBody] PersonalRequest Personal)
    {
        var operation = new CreatePersonalCommand(Personal);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("UpdatePersonal/{id}")]
    public async Task<ApiResponse> UpdatePersonal(int id, [FromBody] PersonalRequest Personal)
    {
        var operation = new UpdatePersonalCommand(id, Personal);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("DeletePersonal/{id}")]
    public async Task<ApiResponse> DeletePersonal(int id)
    {
        var operation = new DeletePersonalCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}
