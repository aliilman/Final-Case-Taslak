using MediatR;
using MOS.Base.Response;
using MOS.Schema;



namespace Vb.Business.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;


