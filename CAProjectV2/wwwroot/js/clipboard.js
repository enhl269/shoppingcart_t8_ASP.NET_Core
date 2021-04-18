window.onload = function () {

    var elemlist = document.getElementsByClassName("actCode");

    for (let j = 0; j < elemlist.length; j++) {
        if (elemlist[j].options.length === 1)
            elemlist[j].addEventListener("click", toCopy);
        else
            elemlist[j].addEventListener("change", toCopy);
    }
}
function toCopy(event) {
    let selected = event.currentTarget;
    let code = selected.options[selected.selectedIndex].innerText;
    navigator.clipboard.writeText(code)
    alert("You have copied the activation code :\n\n" + code +"\n\nEnjoy using our software :)");

}