using dotenv.net;
using ProjectDroit.Core;
using ProjectDroit.Infrastructure;
using ProjectDroit.Server;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Exceptions;

DotEnv.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithExceptionDetails()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithProperty("ApplicationName", "ProjectDroit")
    .Enrich.WithCorrelationIdHeader("correlationId")
    .Enrich.With(new RemovePropertiesEnricher())
    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
    .MinimumLevel.Override("System", LogEventLevel.Error)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .WriteTo.Async(writeTo => writeTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{@Exception}{@Properties}{NewLine}"
    ));

if (builder.Environment.IsDevelopment())
    loggerConfig.WriteTo.Async(writeTo => writeTo.Seq(builder.Configuration["Seq:Url"]));

if (builder.Environment.IsProduction())
    loggerConfig.WriteTo.Async(writeTo =>
        writeTo.Seq(
            builder.Configuration["Seq:Url"],
            apiKey: builder.Configuration["Seq:Apikey"]));


var logger = loggerConfig.CreateLogger();
Log.Logger = logger;
SelfLog.Enable(Console.Error);
builder.Host.UseSerilog(logger);
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// DI Injection

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCore();


var app = builder.Build();

app.UseExceptionHandler();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
