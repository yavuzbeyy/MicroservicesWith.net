using AutoMapper;
using FreeCourseServicesCatalog.API.Dtos.Mapping;
using FreeCourseServicesCatalog.API.Settings;
using Microsoft.Extensions.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Yapýlandýrmayý yükle
builder.Configuration.AddJsonFile("appsettings.json");
// Yapýlandýrma ile nesne tanýt
var configuration = builder.Configuration;
builder.Services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
    {
        return sp.GetRequiredService<IOptions<IDatabaseSettings>>().Value;
    });

// AutoMapper'ý yapýlandýrýn
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new GeneralMapping());
});

builder.Services.AddAutoMapper(typeof(GeneralMapping)); // GeneralMapping sýnýfýný ekleyin
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper); builder.Services.AddAutoMapper(typeof(StartupBase));

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
