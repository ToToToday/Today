let confirmBtnPhone = document.querySelectorAll(".TaiconfirmBtn");
confirmBtnPhone.forEach((btn) => {

    btn.addEventListener("click", () => {
        let choosed_city = document.querySelector("#choose-city").innerText;
        UrlSearch(choosed_city, 1);
    })
})