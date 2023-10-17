using DataHandler.API.ErrorHandling;
using DataHandler.CdrDbContext;
using DataHandler.Repositories;
using DataHandler.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SetDBContext(builder);

#region Service Dependencies
builder.Services.AddScoped<ICallDetailRecordService, CallDetailRecordService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddScoped<ICallDetailRecordRepository, CallDetailRecordRepository>();
#endregion Service Dependencies

var app = builder.Build();


app.AddGlobalErrorHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SetDBContext(WebApplicationBuilder builder)
{
    var mysqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    {
        options.UseLazyLoadingProxies().UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString));
    });
    builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>    {

        options.UseLazyLoadingProxies().UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString));
    });
}