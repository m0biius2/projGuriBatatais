using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace projGuriBatatais.Models
{
    // model da tabela Agenda
    public class AgendaViewModel
    {
        // propriedades das colunas da tabela
        public int IdAgenda { get; set; }

        public string Titulo { get; set; }

        public string Comunicado { get; set; }

        public int IdProfessor { get; set; }

        public DateTime Data { get; set; }

        public int IdCorComunicado { get; set; }
        public List<SelectListItem> Cores { get; set; } // lista para selecionar entre varios itens

        public DataTable tabSelect { get; set; } // formar uma datatable para exibir as informacoes da tabela
    }
}
