using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMusicStore.Interfaces;
using MVCMusicStore.Models;
using MVCMusicStore.ViewModels;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MVCMusicStore.Controllers
{
    /*O controlador do carrinho de compras tem três objetivos principais: 
     * adicionar itens a um carrinho, remover itens do carrinho e exibir itens no carrinho. 
     * Ele fará uso das três classes que acabamos de criar: ShoppingCartViewModel, 
     * ShoppingCartRemoveViewModel e ShoppingCart.
     */
    public class ShoppingCartController : Controller
    {
        private MusicStoreEntities _contextDB;
        private IShoppingCart _shoppingCart;
        public ShoppingCartController(MusicStoreEntities context, IShoppingCart shoppingCart)
        {
            _contextDB = context;
            _shoppingCart = shoppingCart;
        }

        //GET shoppingCart
        public IActionResult Index()
        {
            _shoppingCart = _shoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = _shoppingCart.GetCartItems(),
                CartTotal = _shoppingCart.GetTotal()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            if (this.HttpContext.User.Identity.IsAuthenticated)
            {
                var addedAlbum = _contextDB.Tab_Album.Find(id);

                _shoppingCart = _shoppingCart.GetCart(this.HttpContext);

                _shoppingCart.AddToCart(addedAlbum);

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Login");

        }

        public int GetCount()
        {
            _shoppingCart = _shoppingCart.GetCart(this.HttpContext);
            return _shoppingCart.GetCount();
        }

        //AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            //remove the item from the cart
            _shoppingCart = _shoppingCart.GetCart(this.HttpContext);

            //get the name of the album to display confimation
            string albumName = _contextDB.Tab_Cart.Include("Album")
                .FirstOrDefaultAsync(x => x.RecordId == id).Result.Album.Title;

            //remove from the cart
            int itemCount = _shoppingCart.RemoveFromCart(id);

            //Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = HtmlEncoder.Default.Encode(albumName) + "Foi removido do seu carrinho de compras!",
                CartTotal = _shoppingCart.GetTotal(),
                CartCount = _shoppingCart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        /*Um conceito novo que o ASP.NET MVC 6 traz é o View Components, cuja ideia principal é ser algo parecido com uma partial view,
         * porém com muito mais recursos como testabilidade, isolamento de conceitos.
         * Basicamente é possível fazer uma comparação como um mini controller, que é responsável pela renderização de um bloco só da pagina.
         * Exemplos de uso: dados do usuário, menus customizados, informações de últimos produtos, promoções, algo que possua uma lógica
         * e implementação um pouco mais complexa que uma Partial View.
         */
        public ActionResult CartSummary()
        {
            _shoppingCart = _shoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = _shoppingCart.GetCount();
            return View("CartSumary");
        }
    }
}
