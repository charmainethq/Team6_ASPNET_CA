/*dynamic search*/
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

/*add to cart*/
function addToCart(productId) {
    var quantityInput = document.getElementById('quantity-' + productId);
    var quantity = parseInt(quantityInput.value) || 0;

    var form = document.getElementById('addToCartForm-' + productId);
    var formData = new FormData(form);

    fetch(form.action, {
        method: form.method,
        body: formData
    })
        .then(response => {
            if (response.ok) {
                // show success message
                alert('Product added to cart successfully.');
            } else {
                throw new Error('Error adding product to cart.');
            }
        })
        .catch(error => {
            // show error message
            alert(error.message);
        });
}