using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios MVC
builder.Services.AddControllersWithViews();

// Ruta del archivo JSON — se guarda en la carpeta "data" del proyecto
var jsonPath = Path.Combine(
    builder.Environment.ContentRootPath,
    "Data",
    "items.json"
);

var usersJsonPath = Path.Combine(
    builder.Environment.ContentRootPath,
    "Data",
    "users.json"
);

var reviewsJsonPath = Path.Combine(
    builder.Environment.ContentRootPath,
    "Data",
    "reviews.json"
);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Registrar el repositorio JSON como implementación de IItemRepository
builder.Services.AddSingleton<IItemRepository>(
    new JsonItemRepository(jsonPath)
);

builder.Services.AddSingleton<IUsuarioRepository>(
    new JsonUsuarioRepository(usersJsonPath)
);

builder.Services.AddSingleton<IReviewRepository>(
    new JsonReviewRepository(reviewsJsonPath)
);

// Registrar el servicio de Application
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ReviewService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var usuarioService = scope.ServiceProvider.GetRequiredService<UsuarioService>();
    usuarioService.CrearAdminInicialSiNoExiste("Edwuard Chay", "admin@catalogo.local", "Admin123!");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
