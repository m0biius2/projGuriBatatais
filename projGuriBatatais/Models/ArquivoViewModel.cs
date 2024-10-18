using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace projGuriBatatais.Models
{
    public class ArquivoViewModel
    {
        public int IdArquivo { get; set; }

        public string Caminho { get; set; }

        public IFormFile File { get; set; }

        public int IdUsuario { get; set; }

        public DateTime Data { get; set; }

        public DataTable tabSelect { get; set; }
    }
}
