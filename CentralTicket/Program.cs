using CentralTicket.Contexts.Auth.Data;
using CentralTicket.Contexts.Billing.Data;
using CentralTicket.Contexts.Profile.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Scalar.AspNetCore;
using System.Text;

namespace CentralTicket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var conn = builder.Configuration.GetConnectionString("DefaultConnection")!;
            var serverVersion = ServerVersion.AutoDetect(conn);

            builder.Services.AddDbContext<AuthDbContext>(opt =>
                opt.UseMySql(conn, serverVersion, x => x.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            builder.Services.AddDbContext<BillingDbContext>(opt =>
                opt.UseMySql(conn, serverVersion, x => x.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            builder.Services.AddDbContext<ProfileDbContext>(opt =>
                opt.UseMySql(conn, serverVersion, x => x.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            // Auth
            builder.Services.AddScoped<Contexts.Auth.Interfaces.IRepositories.IUserRepository, Contexts.Auth.Repositories.UserRepository>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.RegisterUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.LoginUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.CreateTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.ValidateTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.GenerateAndSaveRefreshTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.GenerateRefreshTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.ValidateRefreshToken>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.CreateTokenResponseUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.RefreshTokensUseCase>();

            // Billing
            builder.Services.AddScoped<Contexts.Billing.Interfaces.IRepositories.IUserRepository, Contexts.Billing.Repositories.UserRepository>();
            builder.Services.AddScoped<Contexts.Billing.Interfaces.IRepositories.ITicketRepository, Contexts.Billing.Repositories.TicketRepository>();
            builder.Services.AddScoped<Contexts.Billing.Interfaces.IRepositories.ISaleRepository, Contexts.Billing.Repositories.SaleRepository>();
            builder.Services.AddScoped<Contexts.Billing.Interfaces.IUseCases.ICancelSaleUseCase, Contexts.Billing.UseCases.CancelSaleUseCase>();
            builder.Services.AddScoped<Contexts.Billing.Interfaces.IUseCases.IConfirmSaleUseCase, Contexts.Billing.UseCases.ConfirmSaleUseCase>();
            builder.Services.AddScoped<Contexts.Billing.Interfaces.IUseCases.ICreateSaleUseCase, Contexts.Billing.UseCases.CreateSaleUseCase>();
            builder.Services.AddScoped<Contexts.Billing.Interfaces.IUseCases.IGetSaleByIdUseCase, Contexts.Billing.UseCases.GetSaleByIdUseCase>();
            builder.Services.AddScoped<Contexts.Billing.Interfaces.IUseCases.IListSalesUseCase, Contexts.Billing.UseCases.ListSalesUseCase>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["AppSettings:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
                        ValidateIssuerSigningKey = true
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseCors("AllowFrontend");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}