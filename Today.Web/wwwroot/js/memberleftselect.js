let pagemenu = document.querySelectorAll(".page-menu .list-item")
let pagemenutemp = document.querySelectorAll(".page-menu .list-item.active")

pagemenu.forEach((item, index) => {
    item.addEventListener("click", function (event) {
        if (pagemenutemp != null & pagemenutemp != item) {
            pagemenutemp.classList.toggle("active")
            item.classList.toggle("active")
        }
        pagemenutemp = item
    })
})