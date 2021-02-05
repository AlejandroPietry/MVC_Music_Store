using Microsoft.AspNetCore.Http;
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
        private MusicStoreEntities _context;
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public ShoppingCart() { }
        public ShoppingCart(MusicStoreEntities context)
        {
            _context = context;
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

        private string GetCartId(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
