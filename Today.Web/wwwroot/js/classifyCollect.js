let carda, cardcollectheart

window.onload = function() {
    carda = document.querySelectorAll(".card_detail a")
    cardcollectheart = document.querySelectorAll(".card_detail .fa-heart")

    cardcollectheart.forEach((item, index) => {
        item.addEventListener("click", function (event) {
            item.animate(heartscale, heartTiming)

            let url = "/api/Collection/CheckMemberLoginStatus"
            fetch(url, {
                method: 'Get',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(res => {
                    if (res.ok) {
                        console.log('OK')
                        Promise.resolve(res.json()).then(function (result) {
                            if (Object.values(result)[0]) {
                                CRCollect(item, index)
                            }
                            else {
                                var myModal = new bootstrap.Modal(document.getElementById('staticBackdrop'), {})
                                myModal.show()
                            }
                        })

                    }
                    else {
                        console.log('Fail')
                    }
                })
        })
    })
}
function CRCollect(target, idx) {
    let CRurl
    if (target.classList.contains("fa-solid")) {
        CRurl = "/api/Collection/RemoveCollect"
        isfavorite = false
    }
    else {
        CRurl = "/api/Collection/AddCollect"
        isfavorite = true
    }

    fetch(CRurl, {
        method: 'post',
        headers: {
            'Content-Type': 'application/json;charset=utf - 8',
        },
        body: JSON.stringify({
            ProductId: carda[idx].href.substring(carda[idx].href.lastIndexOf('/') + 1, carda[idx].href.length),
            Favorite: isfavorite,
        })
    })
        .then(res => {
            if (res.status === 200) {
                console.log('OK')
                target.classList.toggle("fa-solid")
                target.classList.toggle("fa-regular")
            }
            else {
                console.log('Fail')
            }
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