using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Registro_estacionamiento.BR;
using Registro_estacionamiento.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
builder.Services.AddTransient<DbConnectionFactory>();
builder.Services.AddTransient<DataAccess>();
builder.Services.AddTransient<VehiculosDAO>();
builder.Services.AddTransient<RegistrosDAO>();
builder.Services.AddTransient<ParametrosDAO>();
builder.Services.AddTransient<VehiculosBR>();
builder.Services.AddTransient<RegistrosBR>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();