using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.behavior;

public class JwtTokenBehavior
{
    public static void JwtBehavior(AuthenticationBuilder authBuilder, IConfiguration configuration)
    {

        authBuilder.AddJwtBearer("Bearer", option =>
        {
            option.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Jwt_Key") ?? string.Empty)
                ),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}