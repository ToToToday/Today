let carda, cardcollectheart

window.onload = function() {
    carda = document.querySelectorAll(".card_detail a")
    cardcollectheart = document.querySelectorAll(".card_detail .fa-heart")

    cardcollectheart.forEach((item, index) => {
        item.addEventListener("click", function (event) {
            item.animate(heartscale, heartTiming)

            if (item.classList.contains("fa-solid")) {
                url = "/api/Collection/RemoveCollect"
                isfavorite = false
            }
            else {
                url = "/api/Collection/AddCollect"
                isfavorite = true
            }

            fetch(url, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    ProductId: carda[index].href.substring(carda[index].href.lastIndexOf('/') + 1, carda[index].href.length),
                    Favorite: isfavorite,
                })
            })
                .then(res => {
                    if (res.status === 200) {
                        console.log('OK')
                        item.classList.toggle("fa-solid")
                        item.classList.toggle("fa-regular")
                    }
                    else {
                        console.log('Fail')
                    }
                })
        })
    })
}

const heartscale = [
    { transform: 'scale(1)' },
    { transform: 'scale(1.5)' },
    { transform: 'scale(1)' },
];

const heartTiming = {
    duration: 1000,
    iterations: 1
}