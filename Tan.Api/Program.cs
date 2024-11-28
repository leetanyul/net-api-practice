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

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddTransient<ISampleFacade, SampleFacade>();
    builder.Services.AddTransient<ISampleService, SampleService>();
    builder.Services.AddTransient<ISampleRepository, SampleRepository>();
    builder.Services.AddAutoMapper(typeof(SampleProfile));

    builder.Services.AddDbContext<SampleContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"),
            a => { a.MigrationsAssembly("Tan.Migrations"); });
    });

    SetNLog(builder);

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    });

    await using var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "tan api"));
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    // 미들웨어 사용 등록
    app.UseEdgeHandlerMiddleware();

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