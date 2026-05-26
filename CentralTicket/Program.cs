using Microsoft.EntityFrameworkCore;
using CentralTicket.Contexts.Auth.Data;
using CentralTicket.Contexts.Profile.Data;
using CentralTicket.Contexts.Billing.Data;
using CentralTicket.Contexts.Auth;
using CentralTicket.Contexts.Auth.Interfaces.IRepositories;
using CentralTicket.Contexts.Auth.Repositories;
using CentralTicket.Contexts.Auth.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.RegisterUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.LoginUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.CreateTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.ValidateTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.GenerateAndSaveRefreshTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.GenerateRefreshTokenUseCase>();
            builder.Services.AddScoped<Contexts.Auth.UseCases.ValidateRefreshToken>();
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

            var conn = builder.Configuration.GetConnectionString("Default");
            var serverVersion = ServerVersion.AutoDetect(conn);

            builder.Services.AddDbContext<AuthDbContext>(opt =>
                opt.UseMySql(conn, serverVersion));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            builder.Services.AddDbContext<ProfileDbContext>(opt =>
                opt.UseMySql(conn, serverVersion));

            builder.Services.AddDbContext<BillingDbContext>(opt =>
                opt.UseMySql(conn, serverVersion));

            app.UseCors("AllowFrontend");

            app.UseAuthentication();
            app.UseAuthorization();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}