using MediatR;
using Microsoft.AspNetCore.Mvc;
using MOS.Base.Response;
using MOS.Schema;

using Mos.Business.Cqrs;
using MOS.Business.Validator;
using FluentValidation;


namespace MOS.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IMediator mediator;

    public TokenController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpPost("TakeToken")]
    public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
    {
        TokenValidator validations = new();
        validations.ValidateAndThrow(request);
        
        var operation = new CreateTokenCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
}