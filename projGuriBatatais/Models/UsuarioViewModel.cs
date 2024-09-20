using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace projGuriBatatais.Models
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }

        public string NomeCompleto { get; set; }

        public string NomeUsuario { get; set;}

        [MaxLength(50)] // limite maximo de digitos
        public string Senha { get; set;}

        public List<string> Curso { get; set; }

        public string Tipo { get; set;}
    }
}
