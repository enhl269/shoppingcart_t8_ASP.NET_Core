window.onload = function () {
    var elemlist = document.getElementsByClassName("wishbutton");

    var dataupdate = sendwish().split(" ");
    for (let i = 0; i < elemlist.length; i++) {
        for (var k = 0; k < dataupdate.length; k++) {
            if (dataupdate[k] == elemlist[i].value && dataupdate[k] != "")
                elemlist[i].innerHTML = "added to wishlist";
        }
    }

    for (let j = 0; j < elemlist.length; j++) {
        elemlist[j].addEventListener("click", wishit);

    }
}

function Getdata(wishelem) {
    let data = {
        ProductId: wishelem.value,
    }
    return data;
}

function sendwish() {
    $.ajax({
        type: "POST",
        url: "/WishItem/UserWishList",
        async: false,
        data: "",
        encode: true,
        success: function (data) {
            result = data.success;
        }
    });
    return result;
}

function wishit(event) {
    var urlstring = "/WishItem/WishIt?ProductId=" + event.currentTarget.value;
    var details = window.location.toString();
    if (details.indexOf("details") > 0) {
        urlstring = urlstring + "&details=" + event.currentTarget.value;
    }
    window.location.href = urlstring;
}




