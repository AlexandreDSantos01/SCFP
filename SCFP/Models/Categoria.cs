using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // no topo


namespace SCFP.Models
{


    public class Categoria 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(75, ErrorMessage = "Máximo de 75 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        public TipoCategoria Tipo { get; set; }

        [ValidateNever]
        public string UsuarioId { get; set; }
        [ValidateNever]
        public Usuario Usuario { get; set; }

        [ValidateNever]
        public ICollection<Transacao> Transacoes { get; set; }

    }
}