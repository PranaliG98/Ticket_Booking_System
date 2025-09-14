using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Web.Repositories.Implementations;
using TicketBooking.Web.Repositories.Interfaces;
using TicketBooking.Repositories;
using TicketBooking.Repositories.Implementations;
using TicketBooking.Repositories.Interfaces;
using TicketBooking.Web.Mappers;
using Microsoft.AspNetCore.Identity.UI.Services;
using TicketBooking.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//set false reference loop handling
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("TicketBooking.Web")));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IBusRepo, BusRepo>();
builder.Services.AddScoped<ISeatDetailsRepo, SeatDetailsRepo>();
builder.Services.AddScoped<IUtilityRepo, UtilityRepo>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IDbInitial, DbInitial>();
builder.Services.AddRazorPages();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var config = new AutoMapper.MapperConfiguration(options =>
{
    options.AddProfile(new AutoMapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
DataSeed();
void DataSeed()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbService = scope.ServiceProvider.GetRequiredService<IDbInitial>();
        dbService.DataSeed();
    }
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
