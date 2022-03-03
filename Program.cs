using System.Reflection;
using System.Runtime.CompilerServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PizzaStore.Infra;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

builder.Services.AddScoped<IRepository<Model.Product>, ProductRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo() {Title = "Pizza Store Api", Description = "Ask for your pizza", Version = "v1"});
});

var app = builder.Build();

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizza Store Api V1");
});


app.MapGet("get-products/take/{take:int}/skip/{skip:int}",
    async ([FromServices] IRepository<Model.Product> product, int skip, int take) => await product.Get(skip, take));


app.MapPost("insert-new-product",
    async ([FromServices] IRepository<Model.Product> productRepo, Model.Product productDto) =>
        await productRepo.Add(productDto));

app.MapPut("update-product",
    async ([FromServices] IRepository<Model.Product> productRepo, Model.Product productDto) => 
        await productRepo.Update(productDto));

app.MapDelete("delete-product/{skip:int}",
    async ([FromServices] IRepository<Model.Product> productRepo, int skip) =>
        await productRepo.Remove(skip));

app.Run();
