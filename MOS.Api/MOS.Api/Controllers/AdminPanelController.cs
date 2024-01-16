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

namespace MOS.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly IMediator mediator;

        public AdminPanelController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //admin
        //getallExpense //getparameter //getbyid
        //Onaylabyid // reddetbyid


        [HttpGet("GetAllExpense")]
        public async Task<ApiResponse<List<ExpenseResponse>>> GetAllExpense()
        {
            var operation = new GetAllExpenseQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetExpenseById/{id}")]
        public async Task<ApiResponse<ExpenseResponse>> GetExpenseById(int id)
        {
            var operation = new GetExpenseByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetExpenseParameter")]
        public async Task<ApiResponse<List<ExpenseResponse>>> GetExpenseParameter(
            [FromQuery] string? ExpenseName,
            [FromQuery] string? ApprovalStatus)
        {
            var operation = new GetExpenseByParameterQuery(ExpenseName,int.Parse(ApprovalStatus));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost("ApproveById/{id}")]
        public async Task<ApiResponse> ApproveById(int id,[FromBody] AdminExpenceRequest request)
        {
            string AdminNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;

            var operation = new ApproveByIdCommand(AdminNumber:int.Parse(AdminNumber),id,request);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost("RejectedById/{id}")]
        public async Task<ApiResponse> RejectedById(int id,[FromBody] AdminExpenceRequest request)
        {
            string AdminNumber = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;

            var operation = new RejectedByIdCommand(AdminNumber:int.Parse(AdminNumber),id,request);
            var result = await mediator.Send(operation);
            return result;
        }

    }
}