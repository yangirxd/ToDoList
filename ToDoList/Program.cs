using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDb>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ToDoList")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoList API", Description = "Notes To Do", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API V1");
    });
}

app.UseDeveloperExceptionPage();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

app.Run();