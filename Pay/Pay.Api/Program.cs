using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pay.OvetimePolicies.Application.IDatabaseContexts;
using Pay.OvetimePolicies.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pay.OvetimePolicies.Application.Services;
using Pay.OvetimePolicies.Api.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Pay.OvetimePolicies.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=> c.SchemaFilter<EnumSchemaFilter>());


string connection = builder.Configuration.GetSection("ConnectionStrings:PayConnection").Value;
builder.Services.AddTransient<IPayDbContext, PayDbContext>();
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<PayDbContext>(
     option => option.UseSqlServer(connection)
    );

//Register dapper in scope    
builder.Services.AddScoped<IPayDapperContext, PayDapperContext>();



builder.Services.AddTransient<ICalculator, CalculatorA>();
builder.Services.AddTransient<IPayService, PayService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Register the custom model binder with the desired separator
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new SeparatorModelBinderProvider('/'));

});

var app = builder.Build();

 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
