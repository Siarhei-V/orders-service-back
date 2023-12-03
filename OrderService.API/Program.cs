using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using OrderService.API.Dtos;
using OrderService.API.Filters;
using OrderService.BLL.BL;
using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.BLL.Services.Filters;
using OrderService.BLL.Services.OrderItems;
using OrderService.BLL.Services.Orders;
using OrderService.BLL.Services.Providers;
using OrderService.DAL.ApplicationLogs;
using OrderService.DAL.Filters;
using OrderService.DAL.Infrastructure;
using OrderService.DAL.OrderItems;
using OrderService.DAL.Orders;
using OrderService.DAL.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.Filters.Add<ExceptionsFilter>());
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(builder.Configuration["PostgresConnectionString"]));
builder.Services.AddSwaggerGen();
builder.Services.AddCors(setup => setup.AddPolicy("AllowAll", cfg => cfg.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// BLL
builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddTransient<IBackgroundDataHandler, BackgroundDataHandler>();
builder.Services.AddTransient<IOrderItemsService, OrderItemsService>();
builder.Services.AddTransient<IProvidersService, ProvidersService>();
builder.Services.AddTransient<IFiltersService, FiltersService>();
builder.Services.AddTransient<IOrderValidator, OrderValidator>();

// DAL
builder.Services.AddScoped<IOrdersRepository, PostgresEfOrdersRepository>();
builder.Services.AddScoped<ILogsRepository, PostgresEfLogsRepository>();
builder.Services.AddScoped<IRepository<Order>, PostgresEfCommonRepository<Order>>();
builder.Services.AddScoped<IOrderItemsRepository, PostgresEfOrderItemsRepository>();
builder.Services.AddScoped<IRepository<OrderItem>, PostgresEfCommonRepository<OrderItem>>();
builder.Services.AddScoped<IUoW, UoW>();
builder.Services.AddScoped<IProvidersRepository, PostgresEfProvidersRepository>();
builder.Services.AddScoped<IFiltersRepository, PostgresEfFiltersRepository>();

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
    
    var response = new BaseResponseDto { Status = StatusCodes.Status500InternalServerError, Message = "Ошибка сервера" };
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    await context.Response.WriteAsJsonAsync(response);
}));
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
