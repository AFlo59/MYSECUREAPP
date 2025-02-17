using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySecureApp.Data;
using MySecureApp.Models;   // 🔥 Ajoute cette ligne pour importer `UserEntity` et `Role`


var builder = WebApplication.CreateBuilder(args);

// 🔹 Charger les variables d'environnement
try
{
    Env.Load();
    Console.WriteLine("✅ .env chargé avec succès !");
}
catch (Exception ex)
{
    Console.WriteLine($"⚠️ Erreur lors du chargement de .env : {ex.Message}");
}

// 🔹 Déterminer le mode de connexion (DEV ou PROD)
var modeConnection = Env.GetString("MODE_CONNECTION", "DEV").ToUpper();
var env = modeConnection == "PROD" ? "ProductionConnection" : "DevelopmentConnection";
Console.WriteLine($"🔹 Utilisation de la connexion : {env}");

// 🔹 Charger et configurer la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString(env)
    .Replace("${SQLSERVER_HOST}", Env.GetString("SQLSERVER_HOST", "localhost"))
    .Replace("${SQLSERVER_PORT}", Env.GetString("SQLSERVER_PORT", "1433"))
    .Replace("${SQLSERVER_DB}", Env.GetString("SQLSERVER_DB", "MySecureAppDb"))
    .Replace("${SQLSERVER_USER}", Env.GetString("SQLSERVER_USER", "sa"))
    .Replace("${SQLSERVER_PASSWORD}", Env.GetString("SQLSERVER_PASSWORD", "YourStrong!Passw0rd"));

Console.WriteLine("✅ ConnectionString générée avec succès !");

// 🔹 Ajouter DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Ajout d'Identity avec UserEntity et Role
builder.Services.AddIdentity<UserEntity, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()  // 🔥 Ajoute cette ligne pour éviter l'erreur CS1061
    .AddDefaultTokenProviders();

// Configuration de Razor Pages et authentification
builder.Services.AddRazorPages();
builder.Services.AddAuthentication();

// 🔹 Configuration des ports HTTP/HTTPS
var modeHttp = Env.GetString("MODE_HTTP", "HTTP").ToUpper();
var httpPort = int.Parse(Env.GetString("PORT_HTTP", "5000"));
var httpsPort = int.Parse(Env.GetString("PORT_HTTPS", "5001"));

builder.WebHost.ConfigureKestrel(options =>
{
    if (modeHttp == "HTTPS")
    {
        options.ListenAnyIP(httpsPort, listenOptions =>
        {
            listenOptions.UseHttps("certificate.pfx", "SecurePassword123");
        });
        Console.WriteLine($"🌍 Serveur démarré en mode HTTPS (port {httpsPort})");
    }
    else
    {
        options.ListenAnyIP(httpPort);
        Console.WriteLine($"🌍 Serveur démarré en mode HTTP (port {httpPort})");
    }
});

var app = builder.Build();

// 🔹 Configurations en mode Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// 🔹 Middleware ASP.NET Core
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
