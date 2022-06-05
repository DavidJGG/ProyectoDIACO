using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProyectoDIACO.Data;
using ProyectoDIACO.Services;
using ProyectoDIACO.Herramientas;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ProyectoDIACOContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProyectoDIACOContext")/* + "Password=" + builder.Configuration["DbPassword"] + ";" */ ?? throw new InvalidOperationException("Connection string 'ProyectoDIACOContext' not found."));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<AutenticacionService>();

builder.Services.AddScoped<CreateUserService>();

builder.Services.AddScoped<UpdateUserService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(15000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
