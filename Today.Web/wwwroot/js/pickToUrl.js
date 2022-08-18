let offCity = document.querySelectorAll(".offIsland-item");
offCity.forEach(item => {
    item.addEventListener("click", function () {
        UrlSearch(item.innerText, 0);
        filterPost(1);
    })
})