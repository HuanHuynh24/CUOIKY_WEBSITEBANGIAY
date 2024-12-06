using BE.Interface;
using BE.Models;
using BE.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepositoryK, UserRepositoryK>();
builder.Services.AddDbContext<db_websitebanhangContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddScoped<SanphamH>();

builder.Services.AddScoped<DanhMucRepository>();

builder.Services.AddScoped<IChitietsanphamDH, ChitietsanphamDH>();

builder.Services.AddScoped<IColor, ColorDH>();

builder.Services.AddScoped<IBrand, Brand>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
