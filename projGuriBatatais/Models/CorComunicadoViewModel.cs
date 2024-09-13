using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace projGuriBatatais.Models
{
    // model da tabela CorComunicado
    public class CorComunicadoViewModel
    {
        // propriedades das colunas da tabela
        public int IdCorComunicado { get; set; }

        public string NomeCor { get; set; }

    }
}
