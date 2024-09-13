var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// rota
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicial}/{action=Index}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Agenda}/{action=TesteProfessor}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
