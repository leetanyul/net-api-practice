using NLog;
using NLog.Web;
using Tan.Domain.Services;
using Tan.Api.Middlewares;
using Tan.Domain.Repositories;
using Tan.Application.Facades;
using Tan.Infrastructure.Mappers;
using Tan.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Tan.Domain.Services.Interfaces;
using Tan.Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Tan.Application.Facades.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Tan.Api.Authorization;
using Microsoft.OpenApi.Models;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddTransient<ISampleFacade, SampleFacade>();
    builder.Services.AddTransient<ISampleService, SampleService>();
    builder.Services.AddTransient<ISampleRepository, SampleRepository>();
    builder.Services.AddAutoMapper(typeof(SampleProfile));

    builder.Services.AddTransient<IAccountFacade, AccountFacade>();
    builder.Services.AddTransient<IJwtService, JwtService>();
    builder.Services.AddAutoMapper(typeof(AccountProfile));


    builder.Services.AddDbContext<SampleContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"),
            a => { a.MigrationsAssembly("Tan.Migrations"); });
    });

    SetNLog(builder);

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "tan api", Version = "v1" });

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter 'Bearer {token}'",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        };

        c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new List<string>() }
                });
    });

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    });


    var secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]);
    builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });

    builder.Services.AddAuthorization(options =>
    {
        // 기본 권한 정책은 AuthorizationPolicyProvider 에서 동적 추가
        // none 정책 추가: 로그인상태인 경우 추가 권한 검사 없음
        options.AddPolicy("none", policy =>
            policy.RequireAuthenticatedUser());
    });

    builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
    builder.Services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();

    await using var app = builder.Build();

    app.UseHttpsRedirection();
    app.MapControllers();

    // 미들웨어 사용 등록
    app.UseEdgeHandlerMiddleware().UseJwtMiddleware();

    app.UseAuthentication();
    app.UseAuthorization();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "tan api"));
    }

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "expetion main");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

void SetNLog(WebApplicationBuilder builder)
{

    logger.Debug("init main");
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
}