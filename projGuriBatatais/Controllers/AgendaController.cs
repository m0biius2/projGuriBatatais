using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projGuriBatatais.DataAccess;
using projGuriBatatais.Models;
using System.Data;

namespace projGuriBatatais.Controllers
{
    public class AgendaController : Controller
    {
        // metodos de acao
        // metodo Selecionar que seleciona os dados do banco e exibe na tabela da view
        public IActionResult TesteProfessor()
        {
            // objetos
            // objeto da class Agenda
            Agenda o_Agenda = new Agenda();

            // chama a datatable para coletar os dados
            //DataTable dtAgenda;

            AgendaViewModel o_AgendaVM = new AgendaViewModel();

            // executa o metodo SelecionarTodos que seleciona os dados do banco e coloca na datatable
            o_AgendaVM.tabSelect = o_Agenda.SelecionarTodos();

            // objeto da class CorComunicado
            CorComunicado o_CorComunicado = new CorComunicado();

            // datatable com os dados vindos do banco pelo metodo SelecionarTodos
            DataTable tabCorComunicado = o_CorComunicado.SelecionarTodos();

            // preenche a list do model com as informacoes do banco
            o_AgendaVM.Cores = (from DataRow dr in tabCorComunicado.Rows
                                       select new SelectListItem()
                                       {
                                           Value = dr["IdCorComunicado"].ToString(),
                                           Text = dr["NomeCor"].ToString(),
                                       }).ToList();

            // retorna a view e manda a datatable com os dados para a view
            return View("ViewTesteProfessor", o_AgendaVM);
        }

        // metodo inserirprocessar que processa os dados do metodo acima e envia para o banco
        public IActionResult InserirProcessar(AgendaViewModel o_AgendaViewModel)
        {
            // objetos
            // objeto da class colaborador
            Agenda o_Agenda = new Agenda();

            // preenche os atributos do banco com os dados inseridos
            o_Agenda.titulo = o_AgendaViewModel.Titulo;
            o_Agenda.comunicado = o_AgendaViewModel.Comunicado;
            o_Agenda.idProfessor = o_AgendaViewModel.IdProfessor.ToString();
            o_Agenda.data = o_AgendaViewModel.Data;
            o_Agenda.idCorComunicado = o_AgendaViewModel.IdCorComunicado;

            // executa o metodo inserir
            o_Agenda.Inserir();

            // redireciona os dados para o metodo selecionar
            return RedirectToAction("TesteProfessor");
        }

        // metodo ExibirComunicado que exibe os dados do banco de dados de acordo com o id selecionado
        public IActionResult ExibirComunicado(int IdAgenda)
        {
            // objetos
            // objeto da model Agenda
            AgendaViewModel o_AgendaViewModel = new AgendaViewModel();

            // objeto da class Agenda
            Agenda o_Agenda = new Agenda();

            // chama a datatable dtBusca
            DataTable dtBusca;

            // declara que o id do objeto o_Agenda vem do parametro
            o_Agenda.idAgenda = IdAgenda;
            // executa a partir de dtBusca o metodo SelecionarPorId do objeto o_Agenda com o id passado anteriormente
            dtBusca = o_Agenda.SelecionarPorId();

            // preenche as propriedades da model colaborador a partir do objeto o_AgendaViewModel com as linhas de dtBusca
            o_AgendaViewModel.IdAgenda = int.Parse(dtBusca.Rows[0]["IdAgenda"].ToString());
            o_AgendaViewModel.Titulo = dtBusca.Rows[0]["Titulo"].ToString();
            o_AgendaViewModel.Comunicado = dtBusca.Rows[0]["Comunicado"].ToString();
            o_AgendaViewModel.IdProfessor = int.Parse(dtBusca.Rows[0]["IdProfessor"].ToString());
            o_AgendaViewModel.Data = DateTime.Parse(dtBusca.Rows[0]["Data"].ToString());
            o_AgendaViewModel.IdCorComunicado = int.Parse(dtBusca.Rows[0]["IdCorComunicado"].ToString());

            // objeto da class CorComunicado
            CorComunicado o_CorComunicado = new CorComunicado();

            // datatable com os dados vindos do banco pelo metodo SelecionarTodos
            DataTable tabCorComunicado = o_CorComunicado.SelecionarTodos();

            // preenche a list do model com as informacoes do banco
            o_AgendaViewModel.Cores = (from DataRow dr in tabCorComunicado.Rows
                                                    select new SelectListItem()
                                                    {
                                                        Value = dr["IdCorComunicado"].ToString(),
                                                        Text = dr["NomeCor"].ToString(),
                                                    }).ToList();

            // retorna a view TesteProfessor que vai estar preenchida com os dados da model Agenda para serem alterados
            return View("ViewTesteProfessor", o_AgendaViewModel);
        }
    }
}
