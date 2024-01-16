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
[Authorize(Roles = "Personal")]
public class ExpenseController : Controller
{
    private readonly IMediator mediator;

    public ExpenseController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    //personal
    //create, update, delate, getbyid,  getallExpense(kedine ait), getparameter(kedindeait),
    //onaylananları getir// beklemede olanları getir // onaylanmayanları getir

    [HttpGet("GetAllExpense")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetAllExpense()
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetAllOwnExpenseQuery(PersonalNumber:int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetExpenseById/{id}")]
    public async Task<ApiResponse<ExpenseResponse>> GetExpenseById(int id)
    {
        string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetOwnExpenseByIdQuery(PersonalNumber:int.Parse(PersonalNumber),id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetExpenseParameter")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetExpenseParameter(
        [FromQuery] string? Name,
        [FromQuery] string? ApprovalStatus)
    {
        string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetOwnExpenseByParameterQuery(
            PersonalNumber:int.Parse(PersonalNumber),
            ExpenseName:Name,
            ApprovalStatus:int.Parse(ApprovalStatus));
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost("CreateExpense")]
    public async Task<ApiResponse<ExpenseResponse>> CreateExpense([FromBody] PersonalExpenseRequest Expense)
    {
         string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new CreateExpenseCommand(PersonalNumber:int.Parse(PersonalNumber),Expense);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("UpdateExpense/{id}")]
    public async Task<ApiResponse> UpdateExpense(int id, [FromBody] PersonalExpenseRequest Expense)
    {
        string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new UpdateExpenseCommand(PersonalNumber:int.Parse(PersonalNumber),id, Expense);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("DeleteExpense/{id}")]
    public async Task<ApiResponse> DeleteExpense(int id)
    {
        string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new DeleteExpenseCommand(PersonalNumber:int.Parse(PersonalNumber),id);
        var result = await mediator.Send(operation);
        return result;
    }

    
}
