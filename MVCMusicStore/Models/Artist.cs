using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusicStore.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        [Display(Name = "Artist Name")]
        public string Name { get; set; }
    }
}
