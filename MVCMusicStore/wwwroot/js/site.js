// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AddToCart(albumId) {
    console.log(albumId);
    $.ajax({
        url: "/ShoppingCart/AddToCart",
        method: "POST",
        data: { id : albumId }
    }).done(function (qtdItems) {
        document.getElementById("cartCount").innerText = qtdItems;
        })
}
