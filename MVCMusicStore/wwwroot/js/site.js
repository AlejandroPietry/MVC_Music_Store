// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function () {
    $.ajax({
        url: "/ShoppingCart/GetCount",
        method: "GET",
    }).done(function (qtdItems) {
        document.getElementById("cartCount").innerText = qtdItems;
    })
}

$(function () {
    // Document.ready -> link up remove event handler
    $(".RemoveLink").click(function () {
        // Get the id from the link
        var recordToDelete = $(this).attr("data-id");
        if (recordToDelete != '') {
            // Perform the ajax post
            $.ajax({
                url: "/ShoppingCart/RemoveFromCart",
                method: "POST",
                data: { id: recordToDelete }
            }).done(function (data) {
                if (data.ItemCount == 0) {
                    $('#row-' + data.DeleteId).fadeOut('slow');
                } else {
                    $('#item-count-' + data.DeleteId).text(data.ItemCount);
                }
                $('#cart-total').text(data.CartTotal);
                $('#update-message').text(data.Message);
                $('#cart-status').text('Cart (' + data.CartCount + ')');
            })
        }
    });
});


function AbrirModalCompra() {
    $.ajax({
        url: "/Payment/Index",
        method: "GET"
    }).done(function (htmlModal) {
        document.getElementById("modalHtml").innerHTML = htmlModal;
        $("#modalHtml").show();
    })
}


function BuscarDadosCep(numCep) {
    console.log(numCep)
    fetch("https://viacep.com.br/ws/"+  numCep +"/json/")
        .then(response => response.json())
        .then(json => (
            console.log("json", json),
            document.getElementById("Adress").value = json.logradouro + ", " + json.bairro + ", " + json.complemento,
            document.getElementById("City").value = json.localidade + "/" + json.uf,
            document.getElementById("PostalCode").value = json.cep
        ));
}

function MascaraCpf(cpf) {
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3-\$4");
}