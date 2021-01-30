using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCMusicStore.Models
{
    public class Album
    {
        [ScaffoldColumn(false)]
        public int AlbumId { get; set; }

        public int GenreId { get; set; }
        public int ArtistId { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "Titulo do album é obbrigatório!")]
        [StringLength(160)]
        public string Title { get; set; }

        [Display(Name = "Valor")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 200.00, ErrorMessage = "O preço tem que ser entre 0.01 e 200.00")]
        public decimal Price { get; set; }

        [Display(Name = "Url da imagem do album")]
        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }

        public Genre Genre { get; set; }
        public Artist Artist { get; set; }
    }
}