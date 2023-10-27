using ManufacturerVehicles.Communication.DataAccess;
using System.Reflection;
using ManufacturerVehicles.Communication.Business;
using ManufacturerVehicles.Communication.Business.Mappers;
using ManufacturerVehicles.Communication.Services;
using ManufacturerVehicles.Communication.ServiceClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddCommunicationHandlersModule();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IRabbitMQInterface, RabbitMQInterface>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
	app.UseSwagger();
	app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
