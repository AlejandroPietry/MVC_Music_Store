using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusicStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UsuarioId { get; set; }
        public string Username { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Primeiro Nome")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Ultimo Nome")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Endereço")]
        [StringLength(300)]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Cidade")]
        [StringLength(100)]
        public string City { get; set; }

        public string State { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "CEP")]
        [StringLength(8,MinimumLength = 8, ErrorMessage = "Digite o Cep Corretamente!")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public string Country { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "N° Celular")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Numero Incorreto")]
        public string Phone { get; set; }

        public string Email { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
