using ETicaretAPI.API.Configurations.ColumnWriters;
using ETicaretAPI.API.Extensions;
using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Infrastructure.Services.Storage.Azure;
using ETicaretAPI.Persistence;
using ETicaretAPI.SignalR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructreServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage(StorageType.Local);
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));
Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", 
        needAutoCreateTable: true, 
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter()},
            {"message_template", new MessageTemplateColumnWriter()},
            {"level", new LevelColumnWriter()},
            {"time_stamp", new TimestampColumnWriter()},
            {"exception", new ExceptionColumnWriter()},
            {"log_event", new LogEventSerializedColumnWriter()},
            {"user_name", new UsernameColumnWriter()}
        })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())//declare fluent validation
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);//custom filter
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin", options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true,//olusturulacak token degerini kimlerin hangi sitelerin originlerin kullanici belirledigimiz degerdir www.bilmemne.com
                        ValidateIssuer = true,//olusturulacak token degerini kimin dagittigini ifade edecegimiz alandir. www.myapi.com
                        ValidateLifetime = true,//olusturulan token degerinin suresini kontol edecek olan dogrulamadir
                        ValidateIssuerSigningKey = true,//uretilecek token degerinin uygulamamiza ait bir deger oldugunu ifade eden security key verisinin dogrulanmasidir

                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

                        NameClaimType = ClaimTypes.Name //Jwt uzerinde name claimne karsilik gelen degeri user.identity.name property uzerinden elde edebiliriz
                    };
                });
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});
app.MapControllers();
app.MapHubs();
app.Run();
