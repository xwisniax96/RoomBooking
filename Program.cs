using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;
using RoomBooking.Services;
using RoomBooking.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Pobierz connection string z konfiguracji
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Dodaj kontekst bazy danych do kontenera us�ug
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dodaj Identity do aplikacji
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Dodanie kontroler�w i widok�w
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();



// Buduj aplikacj�
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

// Konfiguracja zapyta� HTTP
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Mapowanie tras
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

//app.MapRoomEndpoints();

app.Run();
builder.WebHost.ConfigureKestrel(options =>
{
    // Konfiguracja dla certyfikatu deweloperskiego (je�li nie masz certyfikatu .pfx)
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});
