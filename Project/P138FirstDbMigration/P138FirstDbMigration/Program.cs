using Microsoft.EntityFrameworkCore;
using P138FirstDbMigration.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseSqlServer("Server=CASATR0\\SQLEXPRESS01;Database=P138FirstMigrationDb;Trusted_Connection=true;");
});

var app = builder.Build();

app.MapControllerRoute("default", "{controller=Home}/{action=index}/{id?}");

app.Run();
