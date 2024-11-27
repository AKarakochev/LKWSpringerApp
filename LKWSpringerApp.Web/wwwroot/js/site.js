document.addEventListener("DOMContentLoaded", function () {
    const aboutUsButton = document.getElementById("about-us-button");
    const aboutUsText = document.getElementById("about-us-text");

    if (aboutUsButton) {
        aboutUsButton.addEventListener("click", function () {
            if (aboutUsText.style.display === "none" || aboutUsText.style.display === "") {
                aboutUsText.style.display = "block";
            } else {
                aboutUsText.style.display = "none";
            }
        });
    }
});