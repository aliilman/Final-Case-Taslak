using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Base.Response;
using MOS.Business.Cqrs;
using MOS.Business.Validator;
using MOS.Schema;


namespace MOS.Api.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class PaymentController : ControllerBase
{
    private readonly IMediator mediator;

    public PaymentController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    //Payment
    [HttpGet("GetAllPayment")]
    public async Task<ApiResponse<List<PaymentResponse>>> GetAllPayment()
    {
        var operation = new GetAllPaymentQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetPaymentById/{id}")]
    public async Task<ApiResponse<PaymentResponse>> GetPaymentById(int id)
    {
        var operation = new GetPaymentByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("GetPersonaByParameter")]
    public async Task<ApiResponse<List<PaymentResponse>>> GetPersonaByParameter(
        [FromQuery] string? IBAN,
        [FromQuery] int? Min,
        [FromQuery] int? Max)
    {
        var operation = new GetPaymentByParameterQuery(
            IBAN: IBAN,
            Min: Min,
            Max: Max
        );

        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost("CreatePayment")]
    public async Task<ApiResponse<PaymentResponse>> CreatePayment([FromBody] PaymentRequest Payment)
    {
        PaymentValidator validations = new();
        validations.ValidateAndThrow(Payment);

        var operation = new CreatePaymentCommand(Payment);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("UpdatePayment/{id}")]
    public async Task<ApiResponse> UpdatePayment(int id, [FromBody] PaymentRequest Payment)
    {
        PaymentValidator validations = new();
        validations.ValidateAndThrow(Payment);

        var operation = new UpdatePaymentCommand(id, Payment);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("DeletePayment/{id}")]
    public async Task<ApiResponse> DeletePayment(int id)
    {
        var operation = new DeletePaymentCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }


}
