using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVCMusicStore.Models;
using System.Collections.Generic;
using System.Linq;

/*Essa contorller da suport e a tres cenarios:
 * 
 * Uma página de listagem dos gêneros musicais em nossa loja de música.
 * Uma página de navegação que lista todos os álbuns de música em um gênero específico.
 * Uma página de detalhes que mostra informações sobre um álbum de música específico.
 */


namespace MVCMusicStore.Controllers
{
    [Route("BuscarGeneros")]
    public class StoreController : Controller
    {
        private readonly MusicStoreEntities _storeDB;
        public StoreController(MusicStoreEntities storeDB)
        {
            _storeDB = storeDB;
        }
        //GET /Store
        public IActionResult Index()
        {
            var genres = _storeDB.Tab_Genre.ToList();

            return View(genres);
        }

        //GET /Store/buscaporgeneros
        [Route("DetalhesGenero")]
        public IActionResult Browse(string genreName)
        {
            Genre genreModel = _storeDB.Tab_Genre.Include("Albums")
                .Single(x => x.Name == genreName);
            return View(genreModel);
        }

        //GET /Store/Details/5
        [Route("DetalhesProduto")]
        public IActionResult Details(int id)
        {
            Album album = _storeDB.Tab_Album.Include("Artist").First(x=> x.AlbumId == id);
            return View(album);
        }
    }
}
