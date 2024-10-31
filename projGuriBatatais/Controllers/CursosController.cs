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
        public IActionResult ArquivoUploadExibirCAgudas()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoUploadExibirCAgudas", o_ArquivoVM);
        }

        public IActionResult ArquivoUploadProcessarCAgudas(ArquivoViewModel o_ArquivoVM)
        {
            //UsuarioViewModel o_UsuarioVM = new UsuarioViewModel();

            //Usuario o_Usuario = new Usuario();

            Arquivo o_Arquivo = new Arquivo();

            int idUsuario = o_ArquivoVM.IdUsuario;

            string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CAgudas");

            // se a pasta não estiver criada ele a criará
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            FileInfo o_FileInfo = new FileInfo(o_ArquivoVM.File.FileName);
            string nomeArq = idUsuario.ToString() + "-" + o_FileInfo;

            // gravar foto no banco
            string arquivoBD = $"CAgudas/{nomeArq}";

            string arquivo = Path.Combine(diretorio, nomeArq);

            using (var stream = new FileStream(arquivo, FileMode.Create))
            {
                o_ArquivoVM.File.CopyTo(stream);
            }

            o_Arquivo.caminho = arquivoBD;
            o_Arquivo.idUsuario = idUsuario;
            o_Arquivo.data = o_ArquivoVM.Data;

            o_Arquivo.Inserir();

            return RedirectToAction("ArquivoUploadExibirCAgudas");
        }

        public IActionResult ExcluirProcessar(ArquivoViewModel o_ArquivoVM)
        {
            // objetos
            // objeto da class colaborador
            Arquivo o_Arquivo = new Arquivo();

            // preenche os atributos do banco com os dados inseridos
            o_Arquivo.idArquivo = o_ArquivoVM.IdArquivo;

            // executa o metodo deletar
            o_Arquivo.Deletar();

            // redireciona os dados para o metodo selecionar
            return RedirectToAction("ArquivoUploadExibirCAgudas");
        }
    }
}
