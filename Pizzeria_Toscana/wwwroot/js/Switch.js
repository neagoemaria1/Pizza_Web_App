function toggleSearchGrids() {
    var productSearch = document.getElementById("productSearchContainer");
    var specSearch = document.getElementById("specSearchContainer");

    var productDisplay = window.getComputedStyle(productSearch).display;

    if (productDisplay === "none") {
        productSearch.style.display = "block";
        specSearch.style.display = "none";
    } else {
        productSearch.style.display = "none";
        specSearch.style.display = "block";
    }
}
