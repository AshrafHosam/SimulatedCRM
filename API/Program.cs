using Application.Contracts.Repos;
using Application.Contracts.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Implementation.Repos;
using Persistence.Implementation.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Libraries Registration

builder.Services.AddDbContext<SimulatedCRMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnectionString")));

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
#endregion

#region Contracts Registration

builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));

builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerValidator, CustomerValidator>();

builder.Services.AddScoped<ICustomerAddressRepo, CustomerAddressRepo>();
builder.Services.AddScoped<ICustomerAddressValidator, CustomerAddressValidator>();

builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IProductValidator, ProductValidator>();

builder.Services.AddScoped<ISalesOrderRepo, SalesOrderRepo>();
builder.Services.AddScoped<ISalesOrderValidator, SalesOrderValidator>();

builder.Services.AddScoped<ISalesOrderDetailRepo, SalesOrderDetailRepo>();
builder.Services.AddScoped<ISalesOrderDetailValidator, SalesOrderDetailValidator>();

#endregion
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
