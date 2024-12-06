using BE.Models;
using BE.Model;
using Microsoft.EntityFrameworkCore;
using BE.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký DbContextFactory với phạm vi (Scoped)
builder.Services.AddDbContextFactory<db_websitebanhangContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Đăng ký repository với Dependency Injection
builder.Services.AddScoped<ThongkedoanhthuRepositoryADONET>();
//Ngọc Huân
builder.Services.AddScoped<SanphamH>();
//Vĩ Khương
builder.Services.AddScoped<IUserRepositoryK, UserRepositoryK>();
//Bá Huân
builder.Services.AddScoped<DanhMucRepository>();
builder.Services.AddScoped<ThuongHieuRepository>();
builder.Services.AddScoped<KhuyenMaiRepository>();
builder.Services.AddScoped<TrangThaiRepository>();
builder.Services.AddScoped<ColorRepository>();
builder.Services.AddScoped<SizeRepository>();
builder.Services.AddScoped<SanPhamRepository>();
// Cấu hình CORS để cho phép tất cả các nguồn gốc
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Cho phép tất cả các nguồn gốc
              .AllowAnyMethod()  // Cho phép tất cả các phương thức HTTP
              .AllowAnyHeader(); // Cho phép tất cả các header
    });
});

var app = builder.Build();

// Cấu hình môi trường phát triển và sản xuất cho Swagger
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sử dụng CORS trước khi các middleware khác
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
