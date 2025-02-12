using OrderService.Clients;
using OrderService.Configurations;
using OrderService.Mappings;
using OrderService.Repositories;
using OrderService.Services;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ConfigService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8888/");
});

var serviceProvider = builder.Services.BuildServiceProvider();
var configService = serviceProvider.GetRequiredService<ConfigService>();

var mongoSettings = await configService.GetMongoSettingsAsync("order-service");


builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = mongoSettings.ConnectionString;
    options.DatabaseName = mongoSettings.DatabaseName;
});


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();


builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>();
builder.Services.AddHttpClient<ICustomerServiceClient, CustomerServiceClient>();

// Configuration AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
