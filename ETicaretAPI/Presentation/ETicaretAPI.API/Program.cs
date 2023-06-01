using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Enums;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Infrastructure.Services.Storage.Azure;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructreServices();
builder.Services.AddApplicationServices();
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage(StorageType.Local);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    //declare fluent validation
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    //custom filter
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
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
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
                    };
                });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
