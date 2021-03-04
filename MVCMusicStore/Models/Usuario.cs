using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusicStore.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public string Login { get; set; }
        
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha deve ser inserida!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Tem que ter no minimo 8 caracteres!")]
        public string Password { get; set; }
        
        [Display(Name = "Nome do Usuário")]
        [Required(ErrorMessage = "Nome é obrigatorio!", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Nome { get; set; }

        [Display(Name = "Email do usuário")]
        [Required(ErrorMessage = "Email é obrigatorio!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email no formato inválido")]
        [StringLength(200)]
        public string Email { get; set; }

    }
}
