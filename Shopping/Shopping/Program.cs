using Microsoft.EntityFrameworkCore;
using Shopping.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//Inyectamos la Clase SeeDb 
builder.Services.AddTransient<SeedDb>(); 

var app = builder.Build();

//Metodo para hacer la inyeccion manual!
seedData();
void seedData()
{
    IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopeFactory.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
        service.SeedAsync().Wait();
    }
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
