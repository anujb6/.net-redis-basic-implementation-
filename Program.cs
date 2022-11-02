using Microsoft.EntityFrameworkCore;
using net_redisAPI.Models;
using net_redisAPI.Service;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<redis_testContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("redisDbConnection"), serverVersion));

builder.Services.AddScoped<CacheService>();
builder.Services.AddTransient<CacheService>();
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

app.Run();
