using MediatR;
using MOS.Schema;
using Vb.Base.Response;


namespace Vb.Business.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;


