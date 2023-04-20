using Backend_aflevering_2.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Backend_aflevering_2.Data;
using Mapster;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Backend_aflevering_2Context>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.

builder.Services.AddSignalR();
builder.Services.AddRazorPages();

builder.Services.AddControllers().AddJsonOptions(x =>
    //fix
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();





app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.MapHub<ExpenseHub>("/expensehub");

app.UseStaticFiles();
app.Run();
