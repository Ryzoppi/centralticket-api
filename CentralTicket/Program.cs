using CentralTicket.Contexts.Auth;
using CentralTicket.Contexts.Auth.Interfaces.IRepositories;
using CentralTicket.Contexts.Auth.Interfaces.IUseCases;
using CentralTicket.Contexts.Auth.Repositories;
using CentralTicket.Contexts.Auth.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

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
                    policy.WithOrigins("http://localhost:3000") // Permite explicitamente o seu front
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add services to the container.

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection")!;

            builder.Services.AddDbContext<Contexts.Auth.Data.Context>(options =>
                options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

            builder.Services.AddScoped<ICreateTokenResponseUseCase, CreateTokenResponseUseCase>();
            builder.Services.AddScoped<ICreateTokenUseCase, CreateTokenUseCase>();
            builder.Services.AddScoped<IGenerateAndSaveRefreshTokenUseCase, GenerateAndSaveRefreshTokenUseCase>();
            builder.Services.AddScoped<IGenerateRefreshTokenUseCase, GenerateRefreshTokenUseCase>();
            builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
            builder.Services.AddScoped<IRefreshTokensUseCase, RefreshTokensUseCase>();
            builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();
            builder.Services.AddScoped<IValidateRefreshTokenUseCase, ValidateRefreshTokenUseCase>();
            builder.Services.AddScoped<IValidateTokenUseCase, ValidateTokenUseCase>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.RegisterUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.LoginUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.CreateTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.ValidateTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.GenerateAndSaveRefreshTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.GenerateRefreshTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.ValidateRefreshTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.CreateTokenResponseUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.RefreshTokensUseCase>();

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
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
                        ValidateIssuerSigningKey = true

                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
