using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using HomeApi;
using HomeApi.Configuration;
using HomeApi.Contracts.Devices;
using HomeApi.Contracts.Validators;
using HomeApi.Data;
using HomeApi.Data.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// === Конфигурация ===
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile("HomeOptions.json", optional: false, reloadOnChange: true);

// === AutoMapper ===
var assembly = Assembly.GetAssembly(typeof(MappingProfile));
builder.Services.AddAutoMapper(assembly);

// === FluentValidation ===
builder.Services.AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<AddDeviceRequestValidator>();
});
builder.Services.AddScoped<IValidator<AddDeviceRequest>, AddDeviceRequestValidator>();

// === Репозитории ===
builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();

// === База данных ===
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HomeApiContext>(options =>
    options.UseSqlServer(connection),
    ServiceLifetime.Singleton
);

// === Настройки ===
builder.Services.Configure<HomeOptions>(builder.Configuration);
builder.Services.Configure<Address>(builder.Configuration.GetSection("Address"));

// === Контроллеры и Swagger ===
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HomeApi",
        Version = "v1"
    });
});

var app = builder.Build();

// === Middleware ===
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
