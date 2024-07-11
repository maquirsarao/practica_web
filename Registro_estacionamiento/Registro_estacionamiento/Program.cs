using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Registro_estacionamiento.BR;
using Registro_estacionamiento.Data;
using Registro_estacionamiento.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuarios/Login";
        options.LogoutPath = "/Usuarios/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(VerificarAutenticacionAttribute));
});
builder.Services.AddControllers();
builder.Services.AddTransient<DbConnectionFactory>();
builder.Services.AddTransient<DataAccess>();
builder.Services.AddTransient<VehiculosDAO>();
builder.Services.AddTransient<RegistrosDAO>();
builder.Services.AddTransient<ParametrosDAO>();
builder.Services.AddTransient<UsuariosDAO>();
builder.Services.AddTransient<VehiculosBR>();
builder.Services.AddTransient<RegistrosBR>();
builder.Services.AddTransient<UsuariosBR>();

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