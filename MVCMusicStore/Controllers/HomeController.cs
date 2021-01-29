using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private MusicStoreEntities _storeDB;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MusicStoreEntities storeDb)
        {
            _logger = logger;
            _storeDB = storeDb;
        }

        public async Task<IActionResult> Index()
        {
            var albums = _storeDB.Tab_Album.Include(a => a.Artist);
            return View( await albums.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
