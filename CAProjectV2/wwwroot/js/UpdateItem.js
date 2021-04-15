window.onload = function () {
    let elemList = document.getElementsByClassName("updating");

    for (let i = 0; i < elemList.length; i++)
        elemList[i].addEventListener("change", changed);


}

function changed(event)
{
    let elem = event.currentTarget;
    let idOfProduct = elem.getAttribute("product_name");
    let idOfShoppingCartItem = elem.getAttribute("shoppingcartitem_id");
    let userid = "hello"

    prepareData(elem, idOfProduct, idOfShoppingCartItem, userid)

}

function prepareData(elem, idOfProduct, idOfShoppingCartItem,userid) {

    let formData = {

        Id: idOfShoppingCartItem,
        ShoppingCartId: "",
        ShoppingCartItemEachProductId: "",
        UserId: userid,
        ProductId: idOfProduct,
        Quantity: elem.value
    };

    $.ajax({
        type: "POST",
        url: "ShoppingCartItems/Updating",
        data: formData,
        dataType: "json",
        encode: true,
    })

} 

    //let xhr = new XMLHttpRequest();

    //xhr.open("POST", "ShoppingCartItems/Updating");
    //xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    //xhr.onreadystatechange = function () {
    //    if (this.readyState === XMLHttpRequest.DONE) {

    //        if (this.status == 200) {
    //            let data = JSON.parse(this.responseText);
    //            console.log("Operation Status: " + data.success);
    //        }
    //    }
    //};


    //xhr.send(JSON.stringify({
        
    //    Id: idOfShoppingCartItem,
    //    ShoppingCartId: "",
    //    ShoppingCartItemEachProductId: "",
    //    UserId: "",
    //    ProductId: idOfProduct,
    //    Quantity: elem.value
    //}));


