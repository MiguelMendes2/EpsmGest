using EpsmGest.Data;
using EpsmGest.Helpers;
using EpsmGest.Services.Users;
using EpsmGest.Services.Utilizadores;
using EPSMGest.Services;
using EPSMGest.Services.Compras;
using EPSMGest.Services.Faturas;
using EPSMGest.Services.Fornecedores;
using EPSMGest.Services.Requisicao;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IComprasService, ComprasService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IFaturasService, FaturasService>();
builder.Services.AddScoped<IFornecedoresService, FornecedoresService>();
builder.Services.AddScoped<IRequisicaoService, RequisicaoService>();
builder.Services.AddScoped<IUsersService, UsersService>();

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


