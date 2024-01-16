using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MOS.Base.Response;
using MOS.Base.Token;
using MOS.Data;
using MOS.Data.Entity;
using MOS.Schema;

using Vb.Business.Cqrs;

namespace Vb.Business.Command;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
    private readonly MosDbContext dbContext;
    private readonly JwtConfig jwtConfig;

    public TokenCommandHandler(MosDbContext dbContext, IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig.CurrentValue;
    }

    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        string Password;
        string Email;
        string id;
        string UserName;
        string role;

        var admin = await dbContext.Set<Admin>().Where(x => x.UserName == request.Model.UserName)
            .FirstOrDefaultAsync(cancellationToken);
        var personal = await dbContext.Set<Personal>().Where(x => x.UserName == request.Model.UserName)
            .FirstOrDefaultAsync(cancellationToken);

        if (admin != null)
        {
            id = admin.AdminNumber.ToString();
            Email = admin.Email;
            Password = admin.Password;
            UserName = admin.UserName;
            role = admin.GetType().Name;
        }
        else if (personal != null)
        {
            id = personal.PersonalNumber.ToString();
            Email = personal.Email;
            Password = personal.Password;
            UserName = personal.UserName;
            role = personal.GetType().Name;
        }
        else{return new ApiResponse<TokenResponse>("Invalid user information");}


        //string hash = Md5Extension.GetHash(request.Model.Password.Trim());
        //if (hash != Password)
        if (request.Model.Password != Password)
        {
            return new ApiResponse<TokenResponse>("Invalid user information");
        }

        Claim[] claims =
        [
            new Claim("Id", id),
            new Claim("Email", Email),
            new Claim("UserName", UserName),
            new Claim(ClaimTypes.Role, role)
        ];
        string token = Token(claims);

        return new ApiResponse<TokenResponse>(new TokenResponse()
        {
            Role = role,
            Token = token,
            ExpireDate = DateTime.Now.AddDays(jwtConfig.AccessTokenExpiration)
        });
    }

    private string Token(Claim[] claims)
    {

        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddDays(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }

}