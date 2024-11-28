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
        // cordas agudas
        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAluno")]
        public IActionResult ArquivoExibirCAgudas()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoExibirCAgudas", o_ArquivoVM);
        }

        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAdm")]
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

            string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CAgudas");

            // se a pasta não estiver criada ele a criará
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            FileInfo o_FileInfo = new FileInfo(o_ArquivoVM.File.FileName);
            string nomeArq = o_FileInfo.ToString();

            // gravar foto no banco
            string arquivoBD = $"CAgudas/{nomeArq}";

            string arquivo = Path.Combine(diretorio, nomeArq);

            using (var stream = new FileStream(arquivo, FileMode.Create))
            {
                o_ArquivoVM.File.CopyTo(stream);
            }

            o_Arquivo.caminho = arquivoBD;

            o_Arquivo.Inserir();

            return RedirectToAction("ArquivoUploadExibirCAgudas");
        }

        // metodo que exibe os dados de uma linha a ser excluida
        public IActionResult ExcluirExibirCAgudas(int IdArquivo)
        {
            // objetos
            // objeto da model
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            // objeto da class
            Arquivo o_Arquivo = new Arquivo();

            // tabela de busca
            DataTable dtBusca;

            o_Arquivo.idArquivo = IdArquivo;
            dtBusca = o_Arquivo.SelecionarPorId();

            o_ArquivoVM.IdArquivo = int.Parse(dtBusca.Rows[0]["IdArquivo"].ToString());
            o_ArquivoVM.Caminho = dtBusca.Rows[0]["Caminho"].ToString();

            // retorna a view excluirExibir que exibe os dados antes de serem excluidos a partir da model colaborador
            return View("ViewArquivoUploadExibirCAgudas", o_ArquivoVM);
        }

        public IActionResult ExcluirProcessarCAgudas(ArquivoViewModel o_ArquivoVM)
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

        // cordas graves
        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAluno")]
        public IActionResult ArquivoExibirCGraves()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoExibirCGraves", o_ArquivoVM);
        }

        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAdm")]
        public IActionResult ArquivoUploadExibirCGraves()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoUploadExibirGraves", o_ArquivoVM);
        }

        public IActionResult ArquivoUploadProcessarCGraves(ArquivoViewModel o_ArquivoVM)
        {
            //UsuarioViewModel o_UsuarioVM = new UsuarioViewModel();

            //Usuario o_Usuario = new Usuario();

            Arquivo o_Arquivo = new Arquivo();

            string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CGraves");

            // se a pasta não estiver criada ele a criará
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            FileInfo o_FileInfo = new FileInfo(o_ArquivoVM.File.FileName);
            string nomeArq = o_FileInfo.ToString();

            // gravar foto no banco
            string arquivoBD = $"CGraves/{nomeArq}";

            string arquivo = Path.Combine(diretorio, nomeArq);

            using (var stream = new FileStream(arquivo, FileMode.Create))
            {
                o_ArquivoVM.File.CopyTo(stream);
            }

            o_Arquivo.caminho = arquivoBD;

            o_Arquivo.Inserir();

            return RedirectToAction("ArquivoUploadExibirCGraves");
        }

        // metodo que exibe os dados de uma linha a ser excluida
        public IActionResult ExcluirExibirCGraves(int IdArquivo)
        {
            // objetos
            // objeto da model
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            // objeto da class
            Arquivo o_Arquivo = new Arquivo();

            // tabela de busca
            DataTable dtBusca;

            o_Arquivo.idArquivo = IdArquivo;
            dtBusca = o_Arquivo.SelecionarPorId();

            o_ArquivoVM.IdArquivo = int.Parse(dtBusca.Rows[0]["IdArquivo"].ToString());
            o_ArquivoVM.Caminho = dtBusca.Rows[0]["Caminho"].ToString();

            // retorna a view excluirExibir que exibe os dados antes de serem excluidos a partir da model colaborador
            return View("ViewArquivoUploadExibirGraves", o_ArquivoVM);
        }

        public IActionResult ExcluirProcessarCGraves(ArquivoViewModel o_ArquivoVM)
        {
            // objetos
            // objeto da class colaborador
            Arquivo o_Arquivo = new Arquivo();

            // preenche os atributos do banco com os dados inseridos
            o_Arquivo.idArquivo = o_ArquivoVM.IdArquivo;

            // executa o metodo deletar
            o_Arquivo.Deletar();

            // redireciona os dados para o metodo selecionar
            return RedirectToAction("ArquivoUploadExibirCGraves");
        }

        // metais
        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAluno")]
        public IActionResult ArquivoExibirMetais()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoExibirMetais", o_ArquivoVM);
        }

        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAdm")]
        public IActionResult ArquivoUploadExibirMetais()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoUploadExibirMetais", o_ArquivoVM);
        }

        public IActionResult ArquivoUploadProcessarMetais(ArquivoViewModel o_ArquivoVM)
        {
            //UsuarioViewModel o_UsuarioVM = new UsuarioViewModel();

            //Usuario o_Usuario = new Usuario();

            Arquivo o_Arquivo = new Arquivo();

            string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Metais");

            // se a pasta não estiver criada ele a criará
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            FileInfo o_FileInfo = new FileInfo(o_ArquivoVM.File.FileName);
            string nomeArq = o_FileInfo.ToString();

            // gravar foto no banco
            string arquivoBD = $"Metais/{nomeArq}";

            string arquivo = Path.Combine(diretorio, nomeArq);

            using (var stream = new FileStream(arquivo, FileMode.Create))
            {
                o_ArquivoVM.File.CopyTo(stream);
            }

            o_Arquivo.caminho = arquivoBD;

            o_Arquivo.Inserir();

            return RedirectToAction("ArquivoUploadExibirMetais");
        }

        // metodo que exibe os dados de uma linha a ser excluida
        public IActionResult ExcluirExibirMetais(int IdArquivo)
        {
            // objetos
            // objeto da model
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            // objeto da class
            Arquivo o_Arquivo = new Arquivo();

            // tabela de busca
            DataTable dtBusca;

            o_Arquivo.idArquivo = IdArquivo;
            dtBusca = o_Arquivo.SelecionarPorId();

            o_ArquivoVM.IdArquivo = int.Parse(dtBusca.Rows[0]["IdArquivo"].ToString());
            o_ArquivoVM.Caminho = dtBusca.Rows[0]["Caminho"].ToString();

            // retorna a view excluirExibir que exibe os dados antes de serem excluidos a partir da model colaborador
            return View("ViewArquivoUploadExibirMetais", o_ArquivoVM);
        }

        public IActionResult ExcluirProcessarMetais(ArquivoViewModel o_ArquivoVM)
        {
            // objetos
            // objeto da class colaborador
            Arquivo o_Arquivo = new Arquivo();

            // preenche os atributos do banco com os dados inseridos
            o_Arquivo.idArquivo = o_ArquivoVM.IdArquivo;

            // executa o metodo deletar
            o_Arquivo.Deletar();

            // redireciona os dados para o metodo selecionar
            return RedirectToAction("ArquivoUploadExibirMetais");
        }

        // madeiras
        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAluno")]
        public IActionResult ArquivoExibirMadeiras()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoExibirMadeiras", o_ArquivoVM);
        }

        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAdm")]
        public IActionResult ArquivoUploadExibirMadeiras()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoUploadExibirMadeiras", o_ArquivoVM);
        }

        public IActionResult ArquivoUploadProcessarMadeiras(ArquivoViewModel o_ArquivoVM)
        {
            //UsuarioViewModel o_UsuarioVM = new UsuarioViewModel();

            //Usuario o_Usuario = new Usuario();

            Arquivo o_Arquivo = new Arquivo();

            string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Madeiras");

            // se a pasta não estiver criada ele a criará
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            FileInfo o_FileInfo = new FileInfo(o_ArquivoVM.File.FileName);
            string nomeArq = o_FileInfo.ToString();

            // gravar foto no banco
            string arquivoBD = $"Madeiras/{nomeArq}";

            string arquivo = Path.Combine(diretorio, nomeArq);

            using (var stream = new FileStream(arquivo, FileMode.Create))
            {
                o_ArquivoVM.File.CopyTo(stream);
            }

            o_Arquivo.caminho = arquivoBD;

            o_Arquivo.Inserir();

            return RedirectToAction("ArquivoUploadExibirMadeiras");
        }

        // metodo que exibe os dados de uma linha a ser excluida
        public IActionResult ExcluirExibirMadeiras(int IdArquivo)
        {
            // objetos
            // objeto da model
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            // objeto da class
            Arquivo o_Arquivo = new Arquivo();

            // tabela de busca
            DataTable dtBusca;

            o_Arquivo.idArquivo = IdArquivo;
            dtBusca = o_Arquivo.SelecionarPorId();

            o_ArquivoVM.IdArquivo = int.Parse(dtBusca.Rows[0]["IdArquivo"].ToString());
            o_ArquivoVM.Caminho = dtBusca.Rows[0]["Caminho"].ToString();

            // retorna a view excluirExibir que exibe os dados antes de serem excluidos a partir da model colaborador
            return View("ViewArquivoUploadExibirMadeiras", o_ArquivoVM);
        }

        public IActionResult ExcluirProcessarMadeiras(ArquivoViewModel o_ArquivoVM)
        {
            // objetos
            // objeto da class colaborador
            Arquivo o_Arquivo = new Arquivo();

            // preenche os atributos do banco com os dados inseridos
            o_Arquivo.idArquivo = o_ArquivoVM.IdArquivo;

            // executa o metodo deletar
            o_Arquivo.Deletar();

            // redireciona os dados para o metodo selecionar
            return RedirectToAction("ArquivoUploadExibirMadeiras");
        }

        // percussao
        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAluno")]
        public IActionResult ArquivoExibirPercussao()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoExibirPercussao", o_ArquivoVM);
        }

        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAdm")]
        public IActionResult ArquivoUploadExibirPercussao()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoUploadExibirPercussao", o_ArquivoVM);
        }

        public IActionResult ArquivoUploadProcessarPercussao(ArquivoViewModel o_ArquivoVM)
        {
            //UsuarioViewModel o_UsuarioVM = new UsuarioViewModel();

            //Usuario o_Usuario = new Usuario();

            Arquivo o_Arquivo = new Arquivo();

            string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Percussao");

            // se a pasta não estiver criada ele a criará
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            FileInfo o_FileInfo = new FileInfo(o_ArquivoVM.File.FileName);
            string nomeArq = o_FileInfo.ToString();

            // gravar foto no banco
            string arquivoBD = $"Percussao/{nomeArq}";

            string arquivo = Path.Combine(diretorio, nomeArq);

            using (var stream = new FileStream(arquivo, FileMode.Create))
            {
                o_ArquivoVM.File.CopyTo(stream);
            }

            o_Arquivo.caminho = arquivoBD;

            o_Arquivo.Inserir();

            return RedirectToAction("ArquivoUploadExibirPercussao");
        }

        // metodo que exibe os dados de uma linha a ser excluida
        public IActionResult ExcluirExibirPercussao(int IdArquivo)
        {
            // objetos
            // objeto da model
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            // objeto da class
            Arquivo o_Arquivo = new Arquivo();

            // tabela de busca
            DataTable dtBusca;

            o_Arquivo.idArquivo = IdArquivo;
            dtBusca = o_Arquivo.SelecionarPorId();

            o_ArquivoVM.IdArquivo = int.Parse(dtBusca.Rows[0]["IdArquivo"].ToString());
            o_ArquivoVM.Caminho = dtBusca.Rows[0]["Caminho"].ToString();

            // retorna a view excluirExibir que exibe os dados antes de serem excluidos a partir da model colaborador
            return View("ViewArquivoUploadExibirPercussao", o_ArquivoVM);
        }

        public IActionResult ExcluirProcessarPercussao(ArquivoViewModel o_ArquivoVM)
        {
            // objetos
            // objeto da class colaborador
            Arquivo o_Arquivo = new Arquivo();

            // preenche os atributos do banco com os dados inseridos
            o_Arquivo.idArquivo = o_ArquivoVM.IdArquivo;

            // executa o metodo deletar
            o_Arquivo.Deletar();

            // redireciona os dados para o metodo selecionar
            return RedirectToAction("ArquivoUploadExibirPercussao");
        }

        // coral
        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAluno")]
        public IActionResult ArquivoExibirCoral()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoExibirCoral", o_ArquivoVM);
        }

        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAdm")]
        public IActionResult ArquivoUploadExibirCoral()
        {
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            Arquivo o_Arquivo = new Arquivo();

            o_ArquivoVM.tabSelect = o_Arquivo.SelecionarTodos();

            return View("ViewArquivoUploadExibirCoral", o_ArquivoVM);
        }

        public IActionResult ArquivoUploadProcessarCoral(ArquivoViewModel o_ArquivoVM)
        {
            //UsuarioViewModel o_UsuarioVM = new UsuarioViewModel();

            //Usuario o_Usuario = new Usuario();

            Arquivo o_Arquivo = new Arquivo();

            string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Coral");

            // se a pasta não estiver criada ele a criará
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            FileInfo o_FileInfo = new FileInfo(o_ArquivoVM.File.FileName);
            string nomeArq = o_FileInfo.ToString();

            // gravar foto no banco
            string arquivoBD = $"Coral/{nomeArq}";

            string arquivo = Path.Combine(diretorio, nomeArq);

            using (var stream = new FileStream(arquivo, FileMode.Create))
            {
                o_ArquivoVM.File.CopyTo(stream);
            }

            o_Arquivo.caminho = arquivoBD;

            o_Arquivo.Inserir();

            return RedirectToAction("ArquivoUploadExibirCoral");
        }

        // metodo que exibe os dados de uma linha a ser excluida
        public IActionResult ExcluirExibirCoral(int IdArquivo)
        {
            // objetos
            // objeto da model
            ArquivoViewModel o_ArquivoVM = new ArquivoViewModel();

            // objeto da class
            Arquivo o_Arquivo = new Arquivo();

            // tabela de busca
            DataTable dtBusca;

            o_Arquivo.idArquivo = IdArquivo;
            dtBusca = o_Arquivo.SelecionarPorId();

            o_ArquivoVM.IdArquivo = int.Parse(dtBusca.Rows[0]["IdArquivo"].ToString());
            o_ArquivoVM.Caminho = dtBusca.Rows[0]["Caminho"].ToString();

            // retorna a view excluirExibir que exibe os dados antes de serem excluidos a partir da model colaborador
            return View("ViewArquivoUploadExibirCoral", o_ArquivoVM);
        }

        public IActionResult ExcluirProcessarCoral(ArquivoViewModel o_ArquivoVM)
        {
            // objetos
            // objeto da class colaborador
            Arquivo o_Arquivo = new Arquivo();

            // preenche os atributos do banco com os dados inseridos
            o_Arquivo.idArquivo = o_ArquivoVM.IdArquivo;

            // executa o metodo deletar
            o_Arquivo.Deletar();

            // redireciona os dados para o metodo selecionar
            return RedirectToAction("ArquivoUploadExibirCoral");
        }
    }
}
