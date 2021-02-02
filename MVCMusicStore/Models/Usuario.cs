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
        
        [Required(ErrorMessage = "Login deve ser inserido!", AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Senha deve ser inserida!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string Senha { get; set; }
        
        [Display(Name = "Nome do Usuário")]
        //[Required(ErrorMessage = "Nome é obrigatorio!", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Nome { get; set; }

        [Display(Name = "Email do usuário")]
        //[Required(ErrorMessage = "Email é obrigatorio!", AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Email { get; set; }

    }
}
