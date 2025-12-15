using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pizzeria_Toscana.Data;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories;
using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Services;
using Pizzeria_Toscana.Services.Interfaces;
using Stripe;


var builder = WebApplication.CreateBuilder(args);


// Configurare conexiune la baza de date
builder.Services.AddDbContext<PizzerieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PizzerieCE")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProdusRepository, ProdusRepository>();
builder.Services.AddScoped<IProdusService, ProdusService>();

builder.Services.AddScoped<ICosRepository, CosRepository>();
builder.Services.AddScoped<ICosService, CosService>();

builder.Services.AddScoped<ICos_ProdusRepository, Cos_ProdusRepository>();
builder.Services.AddScoped<ICos_ProdusService, Cos_ProdusService>();

builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IIngredientService, IngredientService>();

builder.Services.AddScoped<IProdus_IngredientRepository, Produs_IngredientRepository>();
builder.Services.AddScoped<IProdus_IngredientService, Produs_IngredientService>();

builder.Services.AddScoped<IComandaRepository, ComandaRepository>();
builder.Services.AddScoped<IComandaService, ComandaService>();

builder.Services.AddScoped<ICategorie_Repository, Categorie_Repository>();
builder.Services.AddScoped<ICategorieService, CategorieService>();

builder.Services.AddScoped<IComanda_ProdusRepository, Comanda_ProdusRepository>();
builder.Services.AddScoped<IComanda_ProdusService, Comanda_ProdusService>();

builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<ITFIDFService, TFIDFService>();

builder.Services.AddScoped<LuceneIndexService, LuceneIndexService>();
builder.Services.AddScoped<ILuceneIndexService, LuceneIndexService>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<PizzerieContext>();
builder.Services.AddDbContext<PizzerieContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PizzerieCE")));

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.Stores.MaxLengthForKeys = 128;
});
var app = builder.Buildff();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Meniu}/{action=Index}/{id?}");

app.MapRazorPages();
await SeedRoles(app);
app.Run();
async Task SeedRoles(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await ContextSeed.SeedRolesAsync(roleManager);
    }
}