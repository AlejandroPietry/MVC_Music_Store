using Microsoft.AspNetCore.Mvc;
using MVCMusicStore.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCMusicStore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly MusicStoreEntities _context;
        public PaymentController(MusicStoreEntities context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("IndexModal", new Order {Email = this.HttpContext.User.FindFirst(ClaimTypes.Email).Value});
        }

        public async Task<IActionResult> FinishPayment(Order order)
        {
            return Ok();
        }
    }
}
