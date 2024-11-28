using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projGuriBatatais.DataAccess;
using projGuriBatatais.Models;
using System.Data;
using System.Security.Claims;

namespace projGuriBatatais.Controllers
{
    public class UsuarioController : Controller
    {
        private int idUsuario;

        private readonly IHttpContextAccessor o_Contexto;

        // construtor
        public UsuarioController(IHttpContextAccessor httpContextAccessor)
        {
            o_Contexto = httpContextAccessor;
        }

        public IActionResult IniciarSessao()
        {


            return View("ViewExibirPerfil");
        }

        public IActionResult Cadastrar()
        {
            UsuarioViewModel o_UsuarioViewModel = new UsuarioViewModel();

            return View("ViewEntrar", o_UsuarioViewModel);
        }

        public IActionResult CadastrarProcessar(UsuarioViewModel o_UsuarioVM, string Cursos)
        {
            Usuario o_Usuario = new Usuario();

            o_Usuario.nomeCompleto = o_UsuarioVM.NomeCompleto;
            o_Usuario.nomeUsuario = o_UsuarioVM.NomeUsuario;
            o_Usuario.senha = o_UsuarioVM.Senha;
            o_Usuario.curso = Cursos;
            o_Usuario.tipo = o_UsuarioVM.Tipo;

            o_Usuario.Inserir();

            return RedirectToAction("Cadastrar");
        }

        public async Task<IActionResult> LoginProcessar(UsuarioViewModel o_UsuarioVM)
        {
            Usuario o_Usuario = new Usuario();
            DataTable dtPesquisa;

            // Atribuindo os valores
            o_Usuario.idUsuario = o_UsuarioVM.IdUsuario;
            o_Usuario.nomeUsuario = o_UsuarioVM.NomeUsuario;
            o_Usuario.senha = o_UsuarioVM.Senha;



            // Validar login no banco de dados
            dtPesquisa = o_Usuario.ValidarLogin();

            if (dtPesquisa != null)
            {
                // Login bem-sucedido
                int idUsuario = int.Parse(dtPesquisa.Rows[0]["IdUsuario"].ToString());
                string nomeCompleto = dtPesquisa.Rows[0]["NomeCompleto"].ToString();
                string nomeUsuario = dtPesquisa.Rows[0]["NomeUsuario"].ToString();
                string senha = dtPesquisa.Rows[0]["Senha"].ToString();
                string tipo = dtPesquisa.Rows[0]["Tipo"].ToString();

                o_Contexto.HttpContext.Session.SetInt32("IdUsuario", idUsuario);


                //-------------------------------------------------
                //          Declarações ::  Claim
                //-------------------------------------------------
                List<Claim> usuarioDeclaracoes = new List<Claim>{
                            new Claim("IdUsuario", idUsuario.ToString()),
                            new Claim("NomeCompleto", nomeCompleto),
                            new Claim("NomeUsuario", nomeUsuario),
                            new Claim("Senha", senha),
                            new Claim("Tipo", tipo)
                        };

                //-------------------------------------------------
                //               I D E N T I T Y
                //-------------------------------------------------
                ClaimsIdentity o_Identidade = new ClaimsIdentity(usuarioDeclaracoes,
                                                                 "CookieAuntenticacao");

                //-------------------------------------------------
                //               P R I N C I P A L
                //-------------------------------------------------
                ClaimsPrincipal o_Principal = new ClaimsPrincipal(o_Identidade);

                //-------------------------------------------------
                //        CRIA O CONTEXTO DE SEGURANÇA
                //-------------------------------------------------
                await HttpContext.SignInAsync(o_Principal);

                // Redirecionar o usuário com base no tipo
                string redirectUrl;
                if (tipo == "Aluno")
                {
                    redirectUrl = Url.Action("ExibirAgendaAluno", "Agenda");
                }
                else
                {
                    redirectUrl = Url.Action("ExibirAgendaAdm", "Agenda");
                }

                // Retornando um objeto JSON de sucesso
                return Json(new { success = true, redirectUrl });
            }
            else
            {
                // Retornando um objeto JSON de falha
                return Json(new { success = false, message = "Nome de usuário ou senha incorretos" });
            }
        }

        public IActionResult Alterar(int IdUsuario)
        {
            if (IdUsuario == 0)
            {
                IdUsuario = o_Contexto.HttpContext.Session.GetInt32("IdUsuario") ?? 0;
            }

            if (IdUsuario == 0)
            {
                return RedirectToAction("ExibirPerfil"); // Ou outra lógica de fallback
            }

            UsuarioViewModel o_UsuarioVM = new UsuarioViewModel();
            Usuario o_Usuario = new Usuario();
            DataTable dtBusca;

            o_Usuario.idUsuario = IdUsuario;
            dtBusca = o_Usuario.SelecionarPorId();

            if (dtBusca != null && dtBusca.Rows.Count > 0)
            {
                o_UsuarioVM.IdUsuario = int.Parse(dtBusca.Rows[0]["IdUsuario"].ToString());
                o_UsuarioVM.NomeCompleto = dtBusca.Rows[0]["NomeCompleto"].ToString();
                o_UsuarioVM.NomeUsuario = dtBusca.Rows[0]["NomeUsuario"].ToString();
            }

            ViewBag.IdUsuario = IdUsuario;

            return View("ViewExibirPerfil", o_UsuarioVM);
        }

        public IActionResult AlterarProcessar(UsuarioViewModel o_UsuarioVM)
        {
            Usuario o_Usuario = new Usuario();

            idUsuario = o_Contexto.HttpContext.Session.GetInt32("IdUsuario").Value;

            o_Usuario.idUsuario = idUsuario;
            o_Usuario.nomeCompleto = o_UsuarioVM.NomeCompleto;
            o_Usuario.nomeUsuario = o_UsuarioVM.NomeUsuario;

            o_Usuario.Alterar();

            return RedirectToAction("Alterar", new { IdUsuario = o_UsuarioVM.IdUsuario });
        }

        public JsonResult ValidarNomeUsuario(string nomeUsuario)
        {
            Usuario o_Usuario = new Usuario();

            var usuarioExiste = o_Usuario.ValidarNomeUsuario(nomeUsuario);

            return Json(new { usuarioExiste = usuarioExiste != null });
        }

        public async Task<IActionResult> Logout()
        {
            //int idUsuario = int.Parse(User.FindFirst("IdUsuario").Value);
            //string nome = User.FindFirst("Nome").Value;
            //string permissao = User.FindFirst("Permissao").Value;

            await HttpContext.SignOutAsync("CookieAuntenticacao");

            // Redireciona para a página Index
            return RedirectToRoute(new { controller = "Inicial", action = "Index" });
        }

        public IActionResult ExibirPerfil()
        {
            int idUsuario = o_Contexto.HttpContext.Session.GetInt32("IdUsuario") ?? 0;

            UsuarioViewModel o_UsuarioVM = new UsuarioViewModel();
            Usuario o_Usuario = new Usuario { idUsuario = idUsuario };

            var dt = o_Usuario.SelecionarPorId();
            if (dt != null && dt.Rows.Count > 0)
            {
                o_UsuarioVM.IdUsuario = Convert.ToInt32(dt.Rows[0]["IdUsuario"]);
                o_UsuarioVM.NomeCompleto = dt.Rows[0]["NomeCompleto"].ToString();
                o_UsuarioVM.NomeUsuario = dt.Rows[0]["NomeUsuario"].ToString();
            }

            return View("ViewExibirPerfil", o_UsuarioVM);
        }
    }
}
