using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MOS.Base.Response;
using MOS.Business.Cqrs;
using MOS.Business.Validator;
using MOS.Schema;


namespace MOS.Api.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Personal")]
public class ExpenseController : ControllerBase
{
    private readonly IMediator mediator;

    public ExpenseController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    //personal
    //create, update, delate, getbyid,  getallExpense(kedine ait), getparameter(kedindeait),
    //onaylananları getir// beklemede olanları getir // onaylanmayanları getir

    [HttpGet("GetAllOwnExpense")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetAllExpense()
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetAllOwnExpenseQuery(PersonalNumber: int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetOwnExpenseById/{id}")]
    public async Task<ApiResponse<ExpenseResponse>> GetExpenseById(int id)
    {
        string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetOwnExpenseByIdQuery(PersonalNumber: int.Parse(PersonalNumber), id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetOwnExpenseParameter")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetExpenseParameter(
        [FromQuery] string? Name,
        [FromQuery] int? ApprovalStatus)
    {
        string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new GetOwnExpenseByParameterQuery(
            PersonalNumber: int.Parse(PersonalNumber),
            ExpenseName: Name,
            ApprovalStatus: ApprovalStatus);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost("CreateExpense")]
    public async Task<ApiResponse<ExpenseResponse>> CreateExpense([FromBody] PersonalExpenseRequest Expense)
    {
        PersonalExpenseValidator validations =new();
        validations.ValidateAndThrow(Expense);

        string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new CreateExpenseCommand(PersonalNumber: int.Parse(PersonalNumber), Expense);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("UpdateExpense/{id}")]
    public async Task<ApiResponse> UpdateExpense(int id, [FromBody] PersonalExpenseRequest Expense)
    {
        PersonalExpenseValidator validations =new();
        validations.ValidateAndThrow(Expense);
        
        string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new UpdateExpenseCommand(PersonalNumber: int.Parse(PersonalNumber), id, Expense);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("DeleteExpense/{id}")]
    public async Task<ApiResponse> DeleteExpense(int id)
    {
        string PersonalNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        var operation = new DeleteExpenseCommand(PersonalNumber: int.Parse(PersonalNumber), id);
        var result = await mediator.Send(operation);
        return result;
    }


}
