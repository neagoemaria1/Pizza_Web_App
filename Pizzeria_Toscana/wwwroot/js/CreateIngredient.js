document.addEventListener("DOMContentLoaded", function () {
    const inputField = document.getElementById("denumireIngredient");
    const submitBtn = document.getElementById("submitBtn");

    function checkInput() {
        if (inputField.value.trim().length > 0) {
            submitBtn.removeAttribute("disabled");
        } else {
            submitBtn.setAttribute("disabled", "true");
        }
    }

    inputField.addEventListener("input", checkInput);
    checkInput(); 
});
