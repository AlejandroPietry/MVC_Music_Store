using System.ComponentModel.DataAnnotations;

namespace MVCMusicStore.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "Precisa preencher o nome do artista.")]
        [Display(Name = "Nome do artista")]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "O nome precisa ter entre 4 e 200 caracteres.")]
        public string Name { get; set; }
    }
}
