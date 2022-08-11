let offIslandA = document.querySelectorAll(".offIsland-item");
offIslandA.forEach((btn, i) => {
    btn.addEventListener('click', () => {
        if (i == 0) {
            var City = "澎湖,金門,馬祖"
        }
        else {
            City = document.querySelectorAll(".offIsland-item>h4")[i].innerText;
        }
        UrlSearch(City, 1)

    })
})
