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
        public IActionResult Cadastrar ()
        {
            UsuarioViewModel o_UsuarioViewModel = new UsuarioViewModel ();

            return View("ViewEntrar", o_UsuarioViewModel);
        }

        public IActionResult CadastrarProcessar (UsuarioViewModel o_UsuarioVM, string Cursos)
        {
            Usuario o_Usuario = new Usuario ();

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

            o_Usuario.nomeUsuario = o_UsuarioVM.NomeUsuario;
            o_Usuario.senha = o_UsuarioVM.Senha;
            dtPesquisa = o_Usuario.ValidarLogin();

            //if(dtPesquisa != null)
            //{
            //    int idUsuario = int.Parse(dtPesquisa.Rows[0]["IdUsuario"].ToString());
            //    string nomeCompleto = dtPesquisa.Rows[0]["NomeCompleto"].ToString();
            //    string chaveAcesso = dtPesquisa.Rows[0]["ChaveAcesso"].ToString();

            //    //-------------------------------------------------
            //    //          Declarações ::  Claim
            //    //-------------------------------------------------
            //    List<Claim> usuarioDeclaracoes = new List<Claim>{
            //                new Claim("IdUsuario", idUsuario.ToString()),
            //                new Claim("Nome", nome),
            //                new Claim("Permissao", permissao)
            //            };

            //    //-------------------------------------------------
            //    //               I D E N T I T Y
            //    //-------------------------------------------------
            //    ClaimsIdentity o_Identidade = new ClaimsIdentity(usuarioDeclaracoes,
            //                                                     "CookieAuntenticacao");

            //    //-------------------------------------------------
            //    //               P R I N C I P A L
            //    //-------------------------------------------------
            //    ClaimsPrincipal o_Principal = new ClaimsPrincipal(o_Identidade);

            //    //-------------------------------------------------
            //    //        CRIA O CONTEXTO DE SEGURANÇA
            //    //-------------------------------------------------
            //    await HttpContext.SignInAsync(o_Principal);
            //}
            //else
            //{
            //    Redirect("LoginExibir");
            //}

            return Redirect("LoginExibir");
        }

        public IActionResult Alterar (int IdUsuario)
        {
            IdUsuario = 8;

            UsuarioViewModel o_UsuarioVM = new UsuarioViewModel ();

            Usuario o_Usuario = new Usuario();

            DataTable dtBusca;

            o_Usuario.idUsuario = IdUsuario;

            dtBusca = o_Usuario.SelecionarPorId();

            o_UsuarioVM.IdUsuario = int.Parse(dtBusca.Rows[0]["IdUsuario"].ToString());
            o_UsuarioVM.NomeCompleto = dtBusca.Rows[0]["NomeCompleto"].ToString();
            o_UsuarioVM.NomeUsuario = dtBusca.Rows[0]["NomeUsuario"].ToString();

            // Converter a string de cursos armazenada em uma lista
            string cursosString = dtBusca.Rows[0]["Curso"].ToString();

            o_UsuarioVM.Curso = cursosString.Split(",").Select(c => c.Trim()).ToList();

            return View("ViewAlterar", o_UsuarioVM);
        }

        public IActionResult AlterarProcessar(UsuarioViewModel o_UsuarioVM, string Cursos)
        {
            Usuario o_Usuario = new Usuario();

            o_Usuario.idUsuario = o_UsuarioVM.IdUsuario;
            o_Usuario.nomeCompleto = o_UsuarioVM.NomeCompleto;
            o_Usuario.nomeUsuario = o_UsuarioVM.NomeUsuario;
            o_Usuario.curso = Cursos;

            o_Usuario.Alterar();

            return RedirectToAction("Alterar");
        }

        public JsonResult ValidarNomeUsuario(string nomeUsuario)
        {
            Usuario o_Usuario = new Usuario();

            var usuarioExiste = o_Usuario.ValidarNomeUsuario(nomeUsuario);

            return Json(new { usuarioExiste = usuarioExiste != null });
        }
    }
}
