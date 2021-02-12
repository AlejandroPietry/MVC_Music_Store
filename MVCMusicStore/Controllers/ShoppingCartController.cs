using Microsoft.AspNetCore.Mvc;
using MVCMusicStore.Models;
using MVCMusicStore.ViewModels;
using System.Text.Encodings.Web;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ShoppingCartController(MusicStoreEntities context)
        {
            _contextDB = context;
        }

        //GET shoppingCart
        public IActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public IActionResult AddToCart(int id)
        {
            var addedAlbum = _contextDB.Tab_Album.Find(id);

            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);

            return RedirectToAction("Index");
        }

        //AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            //remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

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

    }
}
