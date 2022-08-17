let offIslandA = document.querySelectorAll(".offIsland-item");
offIslandA.forEach((btn, i) => {
    btn.addEventListener('click', () => {
        City = document.querySelectorAll(".offIsland-item>h4")[i].innerText;
        
    })
})

