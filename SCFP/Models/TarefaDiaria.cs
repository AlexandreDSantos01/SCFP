using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCFP.Models
{
    public class TarefaDiaria
    {
        public int Id { get; set; }

        [Required]
        public string DiaSemana { get; set; } // segunda, terca, etc.

        public bool Concluida { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        // Chave estrangeira para a tarefa base
        public int TarefaId { get; set; }

        [ForeignKey("TarefaId")]
        public Tarefa Tarefa { get; set; }
    }
}
