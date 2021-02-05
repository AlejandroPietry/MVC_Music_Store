using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCMusicStore.Models
{
    public class Genre
    {
        /*
         Observação: caso você esteja imaginando, a notação {Get; Set;} está usando o recurso C#
        de propriedades implementadas automaticamente. Isso nos dá os benefícios de uma propriedade
        sem exigir que declaremos um campo de apoio.
         */
        public int GenreId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} não pode ser vazio.")]
        [Display(Name = "Genêro")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} não pode ser vazio.")]
        [StringLength(5000, MinimumLength = 50, ErrorMessage = "{0} precisar ter entre {2} e {1}")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public List<Album> Albums { get; set; }
    }
}