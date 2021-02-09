using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MVCMusicStore.Models
{
    /*O modelo ShoppingCart manipula o acesso a dados para a tabela de carrinhos.
     * Além disso, ele tratará a lógica de negócios para a adição e remoção de itens do carrinho de compras.
     * 
     * Como não queremos exigir que os usuários se inscrevam para uma conta apenas para adicionar itens ao carrinho de compras, 
     * atribuíremos aos usuários um identificador exclusivo temporário (usando um GUID ou um identificador global exclusivo) 
     * quando acessarem o carrinho de compras. Armazenaremos essa ID usando a classe de sessão ASP.NET.
     */
    
    public partial class ShoppingCart
    {
        private MusicStoreEntities _contextDB;
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public ShoppingCart() { }
        public ShoppingCart(MusicStoreEntities context)
        {
            _contextDB = context;
        }
        /// <summary>
        /// é um método estático que permite que nossos controladores obtenham um objeto de carrinho. 
        /// Ele usa o método Getcarrinhoid para lidar com a leitura de cartid a partir da sessão do usuário. 
        /// O método getcarrinhoid requer o HttpContextBase para que ele possa ler o Carrinhoid do usuário a partir da sessão do usuário.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        //Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        //nos vamos usar o HttpContext para permitir os acessos aos cookies
        private string GetCartId(HttpContext context)
        {
            if (context.Items[CartSessionKey] is null)
            {
                if (!string.IsNullOrEmpty(context.User.Identity.Name))
                {
                    context.Items[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    //Send tempCartId back to client as a cookie
                    context.Items[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Items[CartSessionKey].ToString();
        }

        public void AddToCart(Album album)
        {
            //get the matching cart and album instances
            var cartItem = _contextDB.Tab_Cart.SingleOrDefault(
                c => c.CartId == ShoppingCartId 
                && c.AlbumId == album.AlbumId);

            if(cartItem is null)
            {
                //create a new cart item if no cart item exist 
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _contextDB.Add(cartItem);
            }
            else
            {
                //If the item does exist in cart ,
                //then add one to the quantity
                cartItem.Count++;
            }
            _contextDB.SaveChangesAsync();
        }

        public int RemoveCart(int id)
        {
            //Get the cart
            var cartItem = _contextDB.Tab_Cart.Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem is not null)
            {
                if(cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _contextDB.Tab_Cart.Remove(cartItem);
                }
                //Salvar as mudanças
                _contextDB.SaveChangesAsync();
            }
            return itemCount;
        }

    }
}
