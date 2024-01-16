using MediatR;
using MOS.Base.Response;
using MOS.Schema;



namespace Mos.Business.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;


