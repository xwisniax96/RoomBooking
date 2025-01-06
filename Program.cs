using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;

var builder = WebApplication.CreateBuilder(args);

// Pobierz connection string z konfiguracji
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Dodaj kontekst bazy danych do kontenera us³ug
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dodaj Identity do aplikacji
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Dodaj obs³ugê kontrolerów i widoków
builder.Services.AddControllersWithViews();

// Buduj aplikacjê
var app = builder.Build();

// Konfiguracja zapytañ HTTP
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Mapowanie tras
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
builder.WebHost.ConfigureKestrel(options =>
{
    // Konfiguracja dla certyfikatu deweloperskiego (jeœli nie masz certyfikatu .pfx)
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});
