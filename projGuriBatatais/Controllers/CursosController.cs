using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projGuriBatatais.DataAccess;
using projGuriBatatais.Models;
using System.Data;

namespace projGuriBatatais.Controllers
{
    public class CursosController : Controller
    {
        public IActionResult ArquivoUploadExibir()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_Arquivo.idUsuario = 60;

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoUploadExibir", o_ArquivoVM);
        }

        public IActionResult ArquivoUploadProcessar(ArquivoViewModel o_ArquivoVM)
        {
            //UsuarioViewModel o_UsuarioVM = new UsuarioViewModel();

            //Usuario o_Usuario = new Usuario();

            Arquivo o_Arquivo = new Arquivo();

            int idUsuario = 60;

            string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Arquivos");

            // se a pasta não estiver criada ele a criará
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            FileInfo o_FileInfo = new FileInfo(o_ArquivoVM.File.FileName);
            string nomeArq = idUsuario.ToString() + "-" + o_FileInfo;

            // gravar foto no banco
            string arquivoBD = $"Arquivos/{nomeArq}";

            string arquivo = Path.Combine(diretorio, nomeArq);

            using (var stream = new FileStream(arquivo, FileMode.Create))
            {
                o_ArquivoVM.File.CopyTo(stream);
            }

            o_Arquivo.caminho = arquivoBD;
            o_Arquivo.idUsuario = idUsuario;
            o_Arquivo.data = o_ArquivoVM.Data;

            o_Arquivo.Inserir();

            return RedirectToAction("ArquivoUploadExibir");
        }
    }
}
