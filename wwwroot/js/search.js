function search() {
    var input = document.getElementById("search-box");
    var filter = input.value.toUpperCase();
    var images = document.getElementsByClassName("image");
    for (var i = 0; i < images.length; i++) {
        var title = images[i].getAttribute("data-title");
        if (title.toUpperCase().indexOf(filter) > -1) {
            images[i].style.display = "";
        } else {
            images[i].style.display = "none";
        }
    }
}