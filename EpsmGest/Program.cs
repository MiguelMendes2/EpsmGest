using EpsmGest.Data;
using EpsmGest.Helpers;
using EpsmGest.Services.Users;
using EPSMGest.Services.Purchase;
using EPSMGest.Services.Invoice;
using EPSMGest.Services.Supplier;
using EPSMGest.Services.Requisition;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EpsmGest.Services.Vehicle;
using EpsmGest.Services.Department;
using EpsmGest.Services.Stock;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EpsmGestDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(o =>
{
    o.SignIn.RequireConfirmedAccount = false;
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 6;
}).AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<EpsmGestDbContext>();
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IRequisitionService, RequisitionService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IStockService, StockService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
StartupIdentity.SeedRoles(app.Services).Wait();
app.Run();


