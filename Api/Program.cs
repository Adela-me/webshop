using Api.Middleware;
using Application;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("serilog.json", optional: false, reloadOnChange: true);

var configuration = configurationBuilder.Build();

builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(configuration));

// Add services to the container.
builder.Services
    .RegisterApplication()
    .RegisterInfrastructure(builder.Configuration)
    .RegisterIdentity(builder.Configuration)
    .RegisterAuthentication(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
