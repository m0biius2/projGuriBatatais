var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//-------------------------------------------------------------------
// Habilita o serviço de AUTENTICAÇÃO para COOKIE
//-------------------------------------------------------------------
builder.Services.AddAuthentication("CookieAuntenticacao").AddCookie("CookieAuntenticacao",
                                               options =>
                                               {
                                                   options.Cookie.Name = "CookieAuntenticacao";
                                                   options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                                                   options.LoginPath = "/Usuario/Cadastrar";
                                                   options.AccessDeniedPath = "/AcessoNegado/AcessoNegado";
                                               });

//-------------------------------------------------------------------
// Habilita o serviço de AUTORIZAÇÃO para COOKIE
// Cria POLÍTICAS de acesso
//-------------------------------------------------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AcessoAluno",
                      policy => policy.RequireClaim("Tipo", "Aluno"));
    options.AddPolicy("AcessoAdm",
                      policy => policy.RequireClaim("Tipo", "Professor"));
});

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
    pattern: "{controller=Agenda}/{action=ExibirAgendaPublico}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Agenda}/{action=ExibirAgendaAluno}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Agenda}/{action=ExibirAgendaAdm}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
