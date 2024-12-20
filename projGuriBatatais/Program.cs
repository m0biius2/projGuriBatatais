var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// habilita autenticação por cookie
builder.Services.AddAuthentication("CookieAuntenticacao").AddCookie("CookieAuntenticacao",
                                               options =>
                                               {
                                                   options.Cookie.Name = "CookieAuntenticacao";
                                                   options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                                                   options.LoginPath = "/Usuario/Cadastrar";
                                                   options.AccessDeniedPath = "/AcessoNegado/AcessoNegado";
                                               });

// cria politicas de acesso
builder.Services.AddAuthorization(options =>
{       options.AddPolicy("AcessoAluno",
                  policy => policy.RequireClaim("Tipo", "Aluno"));
        options.AddPolicy("AcessoAdm",
                      policy => policy.RequireClaim("Tipo", "Professor", "Coordenacao"));
});

builder.Services.AddControllersWithViews();

// Habilita o uso do Contexto - Texto
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
                        {
                            options.IdleTimeout = TimeSpan.FromMinutes(30);
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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicial}/{action=Index}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=ExibirPerfil}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cursos}/{action=ArquivoUploadExibirCAgudas}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Agenda}/{action=ExibirAgendaAdm}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Agenda}/{action=ExibirAgendaAdm}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicial}/{action=IndexPublico}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Agenda}/{action=ExibirAgendaAdm}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Jogo}/{action=Jogar}");

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
