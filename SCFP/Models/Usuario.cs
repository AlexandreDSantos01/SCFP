using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SCFP.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public bool Autenticacao2F { get; set; }
        public string? FotoPerfil { get; set; }


        public ICollection<Categoria> Categorias { get; set; }
        public ICollection<Transacao> Transacoes { get; set; }
    }
}