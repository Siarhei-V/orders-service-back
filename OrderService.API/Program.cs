using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using OrderService.API.Dtos;
using OrderService.API.Filters;
using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.BLL.Services.OrderItems;
using OrderService.BLL.Services.Orders;
using OrderService.DAL.Infrastructure;
using OrderService.DAL.Infrastructure.ApplicationLogs;
using OrderService.DAL.Orders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.Filters.Add<ExceptionsFilter>());
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(builder.Configuration["PostgresConnectionString"]));
builder.Services.AddSwaggerGen();

// BLL
builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddTransient<IBackgroundDataHandler, BackgroundDataHandler>();
builder.Services.AddTransient<IOrderItemsService, OrderItemsService>();

// DAL
builder.Services.AddScoped<IOrdersRepository, PostgresEfOrdersRepository>();
builder.Services.AddScoped<ILogsRepository, PostgresEfLogsRepository>();
builder.Services.AddScoped<IRepository<Order>, PostgresEfCommonRepository<Order>>();
builder.Services.AddScoped<IOrderItemsRepository, PostgresEfOrderItemsRepository>();
builder.Services.AddScoped<IRepository<OrderItem>, PostgresEfCommonRepository<OrderItem>>();

var app = builder.Build();

// TODO: hide swagger
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseExceptionHandler(c => c.Run(async context => 
{
    var exeption = context.Features.Get<IExceptionHandlerFeature>().Error;
    var backgroundHandler = context.RequestServices.GetRequiredService<IBackgroundDataHandler>();

    backgroundHandler.HandleLog(exeption, 3);
    
    var response = new ResponseDto { Status = StatusCodes.Status500InternalServerError, Message = "Ошибка сервера" };
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    await context.Response.WriteAsJsonAsync(response);
}));
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
