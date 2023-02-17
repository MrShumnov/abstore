using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Serilog;
using Repository.Context;
using Service.Helper;
using Repository.IRepository;
using Repository.Repository;
using Service.IService;
using Service.Service;
using Microsoft.EntityFrameworkCore;
using Controller.Helper;
using Controller.Controllers;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

//var corsPolicy = "customCorsPolicy";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: corsPolicy,
//                      builder =>
//                      {
//                          builder.AllowAnyOrigin();
//                          builder.AllowAnyMethod();
//                          builder.AllowAnyHeader();
//                      });
//});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});



builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
// var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<BaseContext, PostgreSQLContext>((serviceProvider, dbContextBuilder) =>
{
    var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
    var user = httpContextAccessor.HttpContext.User;

    dbContextBuilder.UseNpgsql(connectionString);

    //if (user.IsInRole("Member"))
    //    dbContextBuilder.UseNpgsql(builder.Configuration["ConnectionStrings:Member"]);
    //else if (user.IsInRole("Moderator"))
    //    dbContextBuilder.UseNpgsql(builder.Configuration["ConnectionStrings:Moderator"]);
    //else if (user.IsInRole("Admin"))
    //    dbContextBuilder.UseNpgsql(builder.Configuration["ConnectionStrings:Admin"]);
    //else
    //    dbContextBuilder.UseNpgsql(builder.Configuration["ConnectionStrings:Anonymous"]);
});

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IOrdersProductsRepository, OrdersProductsRepository>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();

var listenPort = Environment.GetEnvironmentVariable("LISTENPORT");
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(listenPort)); // , listenOptions =>
                                                // {
                                                // listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                                                // listenOptions.UseHttps();
                                                // }); 
});

var app = builder.Build();

app.Logger.LogInformation(connectionString);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
//}

// app.UseCors(corsPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.Run();
