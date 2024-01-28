
using Excellerent.APIModularization;
using Excellerent.ApplicantTracking.Presentation.AccoutService;
using Excellerent.UserManagement.Presentaion.AuthTokenServices;
using Excellerent.UserManagement.Presentation.AccoutService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
namespace excellerent_epp_be
{
    public partial class Startup
    {
        private void ConfigureAuth(IServiceCollection services)
        {


            //Token
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = GetTokenValidationParameters();
                options.RequireHttpsMetadata = false;
            });
            var jwtConfig = Configuration.GetSection("JwtOptions");
            services.AddSingleton<IAuthentService>(new AuthentService(jwtConfig));
            services.AddSingleton<IAuthTokenService>(new AuthTokenService(jwtConfig));

            /////////////////////////////////////////////////////////////////
            var jwtConfigs = Configuration.GetSection("JwtOptions");
            services.AddSingleton<IAuthService>(new AuthService(jwtConfigs));
           // services.AddSingleton<IAuthTokenService>(new AuthTokenService(jwtConfigs));
        }

        private TokenProviderOptions GetTokenProviderOptions()
        {
            //JWT Configuration
            var jwtConfig = Configuration.GetSection("JwtOptions");

            var jwtOptions = new TokenProviderOptions
            {
                Audience = jwtConfig["Audience"],
                Issuer = jwtConfig["Issuer"],
                SecretKey = jwtConfig["SecretKey"]
            };
            return jwtOptions;
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {

            var jwtOptions = GetTokenProviderOptions();

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtOptions.SymmetricSecurityKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = System.TimeSpan.Zero
            };

            return tokenValidationParameters;
        }

        private void ConfigurePolicies(IServiceCollection services)
        {
            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });


            //Authorization Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireElevatedRights", policy => policy.RequireRole("Super Admin"));
                options.AddPolicy("RequireAdminRights", policy => policy.RequireRole("Super Admin", "Administrator"));
            });
        }


    }
}
