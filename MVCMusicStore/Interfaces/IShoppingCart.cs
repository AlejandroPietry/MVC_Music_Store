using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCMusicStore.Models;
using System.Collections.Generic;

namespace MVCMusicStore.Interfaces
{
    public interface IShoppingCart
    {
        ShoppingCart GetCart(HttpContext httpContext);
        ShoppingCart GetCart(Controller controller);
        string GetCartId(HttpContext httpContext);
        void AddToCart(Album album);
        int RemoveFromCart(int id);
        void EmptyCart();
        List<Cart> GetCartItems();
        int GetCount();
        decimal GetTotal();
        int CreateOrder(Order order);
    }
}
