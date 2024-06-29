using ApiGateway.behavior;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiGateway.extensions;

public static class AddJwtExtensions
{
    public static IServiceCollection AddJwtBehavior(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        var builder = new AuthenticationBuilder(services);
        JwtTokenBehavior.JwtBehavior(builder, configuration);

        return services;
    }
}