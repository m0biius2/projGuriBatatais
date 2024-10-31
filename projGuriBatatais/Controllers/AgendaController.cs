using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using projGuriBatatais.DataAccess;
using projGuriBatatais.Models;
using System.Data;

namespace projGuriBatatais.Controllers
{
    public class AgendaController : Controller
    {
        // Anotação
        [Authorize]
        [Authorize(Policy = "AcessoAdm")]
        public IActionResult ExibirAgendaAdm()
        {
            Agenda o_Agenda = new Agenda();

            AgendaViewModel o_AgendaVM = new AgendaViewModel();

            o_AgendaVM.tabSelect = o_Agenda.SelecionarTodos();

            CorComunicado o_CorComunicado = new CorComunicado();

            DataTable tabCorComunicado = o_CorComunicado.SelecionarTodos();

            o_AgendaVM.Cores = (from DataRow dr in tabCorComunicado.Rows
                                       select new SelectListItem()
                                       {
                                           Value = dr["IdCorComunicado"].ToString(),
                                           Text = dr["NomeCor"].ToString(),
                                       }).ToList();

            return View("ViewExibirAgendaAdm", o_AgendaVM);
        }

        [Authorize]
        [Authorize(Policy = "AcessoAluno")]
        public IActionResult ExibirAgendaAluno()
        {
            Agenda o_Agenda = new Agenda();

            AgendaViewModel o_AgendaVM = new AgendaViewModel();

            o_AgendaVM.tabSelect = o_Agenda.SelecionarTodos();

            CorComunicado o_CorComunicado = new CorComunicado();

            DataTable tabCorComunicado = o_CorComunicado.SelecionarTodos();

            o_AgendaVM.Cores = (from DataRow dr in tabCorComunicado.Rows
                                select new SelectListItem()
                                {
                                    Value = dr["IdCorComunicado"].ToString(),
                                    Text = dr["NomeCor"].ToString(),
                                }).ToList();

            return View("ViewExibirAgendaAluno", o_AgendaVM);
        }

        public IActionResult ExibirAgendaPublico()
        {
            Agenda o_Agenda = new Agenda();

            AgendaViewModel o_AgendaVM = new AgendaViewModel();

            o_AgendaVM.tabSelect = o_Agenda.SelecionarTodos();

            CorComunicado o_CorComunicado = new CorComunicado();

            DataTable tabCorComunicado = o_CorComunicado.SelecionarTodos();

            o_AgendaVM.Cores = (from DataRow dr in tabCorComunicado.Rows
                                select new SelectListItem()
                                {
                                    Value = dr["IdCorComunicado"].ToString(),
                                    Text = dr["NomeCor"].ToString(),
                                }).ToList();

            return View("ViewExibirAgendaPublico", o_AgendaVM);
        }

        public IActionResult InserirProcessar(AgendaViewModel o_AgendaViewModel)
        {
            Agenda o_Agenda = new Agenda();

            o_Agenda.titulo = o_AgendaViewModel.Titulo;
            o_Agenda.comunicado = o_AgendaViewModel.Comunicado;
            o_Agenda.idUsuario = o_AgendaViewModel.IdUsuario.ToString();
            o_Agenda.data = o_AgendaViewModel.Data;
            o_Agenda.idCorComunicado = o_AgendaViewModel.IdCorComunicado;

            o_Agenda.Inserir();

            return RedirectToAction("ExibirAgendaAdm");
        }

        public IActionResult AlterarExibir(int IdAgenda)
        {
            AgendaViewModel o_AgendaViewModel = new AgendaViewModel();

            Agenda o_Agenda = new Agenda();

            DataTable dtBusca;

            o_Agenda.idAgenda = IdAgenda;
            dtBusca = o_Agenda.SelecionarPorId();

            o_AgendaViewModel.IdAgenda = int.Parse(dtBusca.Rows[0]["IdAgenda"].ToString());
            o_AgendaViewModel.Titulo = dtBusca.Rows[0]["Titulo"].ToString();
            o_AgendaViewModel.Data = DateTime.Parse(dtBusca.Rows[0]["Data"].ToString());
            o_AgendaViewModel.Comunicado = dtBusca.Rows[0]["Comunicado"].ToString();
            o_AgendaViewModel.IdCorComunicado = int.Parse(dtBusca.Rows[0]["IdCorComunicado"].ToString());

            CorComunicado o_CorComunicado = new CorComunicado();

            DataTable tabCorComunicado = o_CorComunicado.SelecionarTodos();

            o_AgendaViewModel.Cores = (from DataRow dr in tabCorComunicado.Rows
                                select new SelectListItem()
                                {
                                    Value = dr["IdCorComunicado"].ToString(),
                                    Text = dr["NomeCor"].ToString(),
                                }).ToList();

            // retorna a view alterarExibir que vai estar preenchida com os dados da model colaborador para serem alterados
            return View("ViewExibirAgendaAdm", o_AgendaViewModel);
        }

        // metodo que processa os dados alterados acima
        public IActionResult AlterarProcessar(AgendaViewModel o_AgendaVM)
        {
            // objetos
            // objeto da class Agenda
            Agenda o_Agenda = new Agenda();

            // preenche os atributos do banco com os dados alterados
            o_Agenda.idAgenda = o_AgendaVM.IdAgenda;
            o_Agenda.titulo = o_AgendaVM.Titulo;
            o_Agenda.data = o_AgendaVM.Data;
            o_Agenda.idCorComunicado = o_AgendaVM.IdCorComunicado;
            o_Agenda.comunicado = o_AgendaVM.Comunicado;

            // executa o metodo alterar
            o_Agenda.Alterar();

            // redireciona os dados para o metodo selecionar
            return RedirectToAction("ExibirAgendaAdm");
        }

        public IActionResult ExibirComunicadoAdm(int IdAgenda)
        {
            AgendaViewModel o_AgendaViewModel = new AgendaViewModel();

            Agenda o_Agenda = new Agenda();

            DataTable dtBusca;

            o_Agenda.idAgenda = IdAgenda;
            dtBusca = o_Agenda.SelecionarPorId();

            o_AgendaViewModel.IdAgenda = int.Parse(dtBusca.Rows[0]["IdAgenda"].ToString());
            o_AgendaViewModel.Titulo = dtBusca.Rows[0]["Titulo"].ToString();
            o_AgendaViewModel.Comunicado = dtBusca.Rows[0]["Comunicado"].ToString();
            o_AgendaViewModel.IdUsuario = int.Parse(dtBusca.Rows[0]["IdUsuario"].ToString());
            o_AgendaViewModel.Data = DateTime.Parse(dtBusca.Rows[0]["Data"].ToString());
            o_AgendaViewModel.IdCorComunicado = int.Parse(dtBusca.Rows[0]["IdCorComunicado"].ToString());

            CorComunicado o_CorComunicado = new CorComunicado();

            DataTable tabCorComunicado = o_CorComunicado.SelecionarTodos();

            o_AgendaViewModel.Cores = (from DataRow dr in tabCorComunicado.Rows
                                                    select new SelectListItem()
                                                    {
                                                        Value = dr["IdCorComunicado"].ToString(),
                                                        Text = dr["NomeCor"].ToString(),
                                                    }).ToList();

            return View("ViewExibirAgendaAdm", o_AgendaViewModel);
        }

        public IActionResult ExibirComunicadoAluno(int IdAgenda)
        {
            AgendaViewModel o_AgendaViewModel = new AgendaViewModel();

            Agenda o_Agenda = new Agenda();

            DataTable dtBusca;

            o_Agenda.idAgenda = IdAgenda;
            dtBusca = o_Agenda.SelecionarPorId();

            o_AgendaViewModel.IdAgenda = int.Parse(dtBusca.Rows[0]["IdAgenda"].ToString());
            o_AgendaViewModel.Titulo = dtBusca.Rows[0]["Titulo"].ToString();
            o_AgendaViewModel.Comunicado = dtBusca.Rows[0]["Comunicado"].ToString();
            o_AgendaViewModel.IdUsuario = int.Parse(dtBusca.Rows[0]["IdUsuario"].ToString());
            o_AgendaViewModel.Data = DateTime.Parse(dtBusca.Rows[0]["Data"].ToString());
            o_AgendaViewModel.IdCorComunicado = int.Parse(dtBusca.Rows[0]["IdCorComunicado"].ToString());

            CorComunicado o_CorComunicado = new CorComunicado();

            DataTable tabCorComunicado = o_CorComunicado.SelecionarTodos();

            o_AgendaViewModel.Cores = (from DataRow dr in tabCorComunicado.Rows
                                       select new SelectListItem()
                                       {
                                           Value = dr["IdCorComunicado"].ToString(),
                                           Text = dr["NomeCor"].ToString(),
                                       }).ToList();

            return View("ViewExibirAgendaAluno", o_AgendaViewModel);
        }

        public IActionResult ExibirComunicadoPublico(int IdAgenda)
        {
            AgendaViewModel o_AgendaViewModel = new AgendaViewModel();

            Agenda o_Agenda = new Agenda();

            DataTable dtBusca;

            o_Agenda.idAgenda = IdAgenda;
            dtBusca = o_Agenda.SelecionarPorId();

            o_AgendaViewModel.IdAgenda = int.Parse(dtBusca.Rows[0]["IdAgenda"].ToString());
            o_AgendaViewModel.Titulo = dtBusca.Rows[0]["Titulo"].ToString();
            o_AgendaViewModel.Comunicado = dtBusca.Rows[0]["Comunicado"].ToString();
            o_AgendaViewModel.IdUsuario = int.Parse(dtBusca.Rows[0]["IdUsuario"].ToString());
            o_AgendaViewModel.Data = DateTime.Parse(dtBusca.Rows[0]["Data"].ToString());
            o_AgendaViewModel.IdCorComunicado = int.Parse(dtBusca.Rows[0]["IdCorComunicado"].ToString());

            CorComunicado o_CorComunicado = new CorComunicado();

            DataTable tabCorComunicado = o_CorComunicado.SelecionarTodos();

            o_AgendaViewModel.Cores = (from DataRow dr in tabCorComunicado.Rows
                                       select new SelectListItem()
                                       {
                                           Value = dr["IdCorComunicado"].ToString(),
                                           Text = dr["NomeCor"].ToString(),
                                       }).ToList();

            return View("ViewExibirAgendaPublico", o_AgendaViewModel);
        }
    }
}
