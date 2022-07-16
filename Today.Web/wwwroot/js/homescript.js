let eventdetails = document.querySelector(".event-details");
let eventimg = document.querySelector(".new-limit img");
let afterward = document.querySelectorAll(".afterward");
let groupcard = document.querySelectorAll(".group-card");
let citycard = document.querySelectorAll(".city-limit .group-card > div");
let other_dropdown = document.querySelectorAll(".other-selection-limit .title-icon");
let other_ul = document.querySelectorAll(".other-selection-limit ul");
let footer_dropdown = document.querySelectorAll(".footer-limit .title-icon");
let footer_ul = document.querySelectorAll(".footer-limit ul");
let city_moused = citycard[0];
let other_checked_index, footer_checked_index;

// eventimg.addEventListener("mouseenter", () => {
//     eventdetails.width = document.querySelector(".new-limit .pic").offsetWidth;
//     eventdetails.Height = document.querySelector(".new-limit .pic");
//     eventdetails.classList.toggle("d-none");
// })
// if (eventdetails != undefined){
//     eventdetails.addEventListenter("mouseleave", () => {
//         eventdetails.classList.toggle("d-none");
//     })
// }

// afterward.forEach((item, index) => {
//     item.addEventListener("click", () => {
//         groupcard[index].querySelectorAll("div").forEach((item, index) => {
//             item.setAttribute("style", "transform: translateX(-200px);");
//         })
//     })
// })

citycard.forEach((item, index) => {
    item.addEventListener("mouseenter", () => {
        if (item != city_moused){
            city_moused.classList.remove("flex-lg-grow-1");
            city_moused.querySelectorAll(".city-details span").forEach((item) => item.classList.add("d-lg-none"));
            item.classList.add("flex-lg-grow-1");
            item.querySelectorAll(".city-details span").forEach((item) => item.classList.remove("d-lg-none"));
        }
        city_moused = item;
    })
})



other_dropdown.forEach((item, index) => {
    item.addEventListener("click", ()=>{
        if (index != other_checked_index && other_checked_index != undefined){
            other_ul[other_checked_index].classList.add("d-none");
        }
        other_ul[index].classList.toggle("d-none");
        other_checked_index = index;
        alert("成功");
    });
})

footer_dropdown.forEach((item, index) => {
    item.addEventListener("click", ()=>{
        if (index != footer_checked_index && footer_checked_index != undefined){
            footer_ul[footer_checked_index].classList.add("d-none");
        }
        footer_ul[index].classList.toggle("d-none");
        footer_checked_index = index;
        alert("成功");
    });
})
