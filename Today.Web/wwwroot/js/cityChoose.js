

        let choose_city = document.querySelectorAll("#choose-city");
        choose_city.forEach((item,i) => {
            item.addEventListener("click", () => {
                let show_city = document.querySelector(".show-city")
                show_city.innerHTML = item.innerText +'<span class="ms-2 fa-solid fa-caret-down opacity-25"></span>'
            })
        })


let confirmBtnPhone = document.querySelectorAll(".TaiconfirmBtn");
confirmBtnPhone.forEach((btn) => {
    btn.addEventListener("click", () => {
        let city_choosed = document.querySelector('.show-city').innerText;
        classifyCardVue.filterPost(1);
    })
})
