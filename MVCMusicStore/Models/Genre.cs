using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        [Display(Name = "Genre Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Album> Albums { get; set; }
    }
}
