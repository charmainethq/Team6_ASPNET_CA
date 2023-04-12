using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Team6.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add our database context (using AddScoped() under the hood).
builder.Services.AddDbContext<ShopContext>(options => {
    var conn_str = builder.Configuration.GetConnectionString("conn_str");
    options.UseLazyLoadingProxies().UseSqlServer(conn_str);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Game/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

InitDB(app.Services);

app.Run();

void InitDB(IServiceProvider serviceProvider) {
    using var scope = serviceProvider.CreateScope();
    ShopContext db = scope.ServiceProvider.GetRequiredService<ShopContext>();

    // for our debugging, we just start off by removing our old 
    // database (if there is one).
    db.Database.EnsureDeleted();

    // create a new database.
    db.Database.EnsureCreated();
}

