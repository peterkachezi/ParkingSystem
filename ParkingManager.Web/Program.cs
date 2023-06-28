using ParkingManager.BLL.Repositories.ApplicationUserModule;
using ParkingManager.BLL.Repositories.MpesaStkModule;
using ParkingManager.BLL.Repositories.TimeSlotModule;
using ParkingManager.DAL.MapperProfiles;
using ParkingManager.DAL.Modules;
using ParkingManager.Web.Extensions;
using ParkingManager.SeedAppUsers;
using ParkingManager.Services.EmailModule;
using ParkingManager.Services.SMSModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParkingManager.Web.Helpers;
using ParkingManager.BLL.Repositories.ParkingSlotModule;
using ParkingManager.BLL.Repositories.BookingModule;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddTransient<IUserClaimsPrincipalFactory<AppUser>, ApplicationUserClaimsPrincipalFactory>();

builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();

builder.Services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();

builder.Services.AddTransient<ITimeSlotRepository, TimeSlotRepository>();

builder.Services.AddTransient<IMessagingService, MessagingService>();

builder.Services.AddTransient<IParkingSlotRepository, ParkingSlotRepository>();

builder.Services.AddTransient<IBookingRepository, BookingRepository>();


//Cross-origin policy to accept request from localhost:8084.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",

        x => x.AllowAnyOrigin()

            .AllowAnyMethod()

            .AllowAnyHeader());
});

builder.Services.AddSignalR();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseFileServer();

app.UseCors("CorsPolicy");

app.MapHub<SignalrServer>("/signalrServer");

app.UseEndpoints(endpoints =>
{


    
    endpoints.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    SeedUsers.Seed(userManager, roleManager);
}

app.Run();
