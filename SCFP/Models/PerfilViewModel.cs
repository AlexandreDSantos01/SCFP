using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SCFP.ViewModels
{
    public class PerfilViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome completo")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string? NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        public string? ConfirmarNovaSenha { get; set; }

        [Display(Name = "Foto de Perfil")]
        public IFormFile? FotoPerfilArquivo { get; set; }

        public string? FotoPerfilUrl { get; set; }
    }
}
