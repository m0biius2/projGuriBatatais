using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projGuriBatatais.DataAccess;
using projGuriBatatais.Models;
using System.Data;

namespace projGuriBatatais.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastrar ()
        {
            UsuarioViewModel o_UsuarioViewModel = new UsuarioViewModel ();

            return View("ViewCadastrar", o_UsuarioViewModel);
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
    }
}
