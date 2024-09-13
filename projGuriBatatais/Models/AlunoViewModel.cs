using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace projGuriBatatais.Models
{
    // model da tabela Aluno
    public class AlunoViewModel
    {
        // propriedades das colunas da tabela
        public int IdAluno { get; set; }

        public string NomeCompleto { get; set; }

        public string NomeUsuario { get; set; }

        [MaxLength(8)] // limite maximo de digitos
        public string Senha { get; set; }

        public bool CGraves { get; set; }

        public bool CAgudas { get; set; }

        public bool Metais { get; set; }

        public bool Madeiras { get; set; }

        public bool Percussao { get; set; }

        public bool Coral { get; set; }
    }
}
