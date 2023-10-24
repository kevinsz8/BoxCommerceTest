using ManufacturerVehicles.Orchestration.Business;
using ManufacturerVehicles.Orchestration.Business.Mappers;
using ManufacturerVehicles.Orchestration.ServiceClients;
using ManufacturerVehicles.Orchestration.Services;
using ManufacturerVehicles.Orchestration.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orchestration API", Version = "v1" });

});

builder.Services.Configure<ServiceConfigOptions>(builder.Configuration.GetSection(ServiceConfigOptions.MicroserviceUrls));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddOrchestrationHandlersModule();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IItemInterface, ItemInterface>();
builder.Services.AddScoped<IOrderInterface, OrderInterface>();
builder.Services.AddScoped<ICustomerInterface, CustomerInterface>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<HttpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();


