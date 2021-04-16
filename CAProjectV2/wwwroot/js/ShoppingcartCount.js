//add js file to get shopping cart ccount through ajax
window.onload = function () {
    let itemcount = document.getElementById("shoppingcartcount");
    itemcount.innerText = shoppingcartcount();
}
function shoppingcartcount() {
    $.ajax({
        type: "POST",
        url: "/ShoppingCartItems/shoppingcartcount",
        async: false,
        data: "",
        encode: true,
        success: function (data) {
            result = data.success;
        }
    });
    return result;
}