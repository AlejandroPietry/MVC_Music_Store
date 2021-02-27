using Microsoft.AspNetCore.Mvc;
using MVCMusicStore.Interfaces;
using MVCMusicStore.Models;
using MVCMusicStore.ViewModels;
using System.Text.Encodings.Web;

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

        public IActionResult AddToCart(int id)
        {
            var addedAlbum = _contextDB.Tab_Album.Find(id);

            var cart = _shoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);

            return RedirectToAction("Index");
        }

        //AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            //remove the item from the cart
            var cart = _shoppingCart.GetCart(this.HttpContext);

            //get the name of the album to display confimation
            string albumName = _contextDB.Tab_Cart.Find(id).Album.Title;

            //remove from the cart
            int itemCount = cart.RemoveFromCart(id);

            //Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = HtmlEncoder.Default.Encode(albumName) + "Foi removido do seu carrinho de compras!",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
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
            var cart = _shoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return View("CartSumary");
        }
    }
}
