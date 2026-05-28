using CentralTicket.Contexts.Auth.Data;
using CentralTicket.Contexts.Billing.Data;
using CentralTicket.Contexts.Profile.Data;
using CentralTicket.Contexts.Auth;
using CentralTicket.Contexts.Auth.Interfaces.IRepositories;
using CentralTicket.Contexts.Auth.Interfaces.IUseCases;
using CentralTicket.Contexts.Auth.Repositories;
using CentralTicket.Contexts.Auth.UseCases;
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
            builder.Services.AddScoped<ICreateTokenResponseUseCase, CreateTokenResponseUseCase>();
            builder.Services.AddScoped<ICreateTokenUseCase, CreateTokenUseCase>();
            builder.Services.AddScoped<IGenerateAndSaveRefreshTokenUseCase, GenerateAndSaveRefreshTokenUseCase>();
            builder.Services.AddScoped<IGenerateRefreshTokenUseCase, GenerateRefreshTokenUseCase>();
            builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
            builder.Services.AddScoped<IRefreshTokensUseCase, RefreshTokensUseCase>();
            builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();
            builder.Services.AddScoped<IValidateRefreshTokenUseCase, ValidateRefreshTokenUseCase>();
            builder.Services.AddScoped<IValidateTokenUseCase, ValidateTokenUseCase>();

            builder.Services.AddScoped<Contexts.Auth.Interfaces.IRepositories.IUserRepository, Contexts.Auth.Repositories.UserRepository>();

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