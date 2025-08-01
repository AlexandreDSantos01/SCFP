using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCFP.Models
{
    public class PlanejamentoDia
    {
        public int Id { get; set; }

        [Required]
        public string DiaSemana { get; set; } = string.Empty;

        public string? GifUrl { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}
