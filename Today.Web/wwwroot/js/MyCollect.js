let mainblock = document.querySelector(".collection-card.main-block")
let nocollectiontemplate = document.querySelector("#no-collection")
let hascollectiontemplate = document.querySelector("#has-collection")
let collectioncardtemplate = document.querySelector("#collection-card")
let producttagtemplate = document.querySelector("#product-tag")
let temp, cardheart, url, isfavorite, carddetail, count

if (collectionlist.length > 0) {
    mainblock.append(getblock(collectionlist.length))
}
else{
    mainblock.append(nocard())
}

function nocard() {
    let nocollection = nocollectiontemplate.content.cloneNode(true)

    return nocollection
}

function getblock(length) {
    let block = hascollectiontemplate.content.cloneNode(true)

    block.querySelector(".quantity .text-info").innerText = length
    collectionlist.forEach((item, index) => {
        block.querySelector(".page-text-dynamic").append(getcard(item))
    })

    return block
}

function getcard(target){
    let collectioncard = collectioncardtemplate.content.cloneNode(true)

    collectioncard.querySelector("a").href = `../Product/ProductInfo/${target.Id}`
    collectioncard.querySelector("img").src = target.ProductPhoto
    collectioncard.querySelector(".card-body .card-title").innerText = target.ProductName
    target.Tags.forEach((item, index) => {
        collectioncard.querySelector(".card-point").append(gettag(item))
    })
    collectioncard.querySelector(".product-palce .card-text small").innerText = target.CityName
    collectioncard.querySelector(".footer .number.comment-number").innerText = `(${target.TotalGiveComment})`
    collectioncard.querySelector(".footer .subscription.ordered").innerText = `${target.TotalOrder} 個已訂購`
    if (target.OriginalPrice !== null){
        collectioncard.querySelector(".doller .TWD-line").innerText = `TWD ${target.OriginalPrice}`
    }
    else{
        collectioncard.querySelector(".doller .TWD-line").classList.toggle("d-none")
    }
    collectioncard.querySelector(".doller .TWD").innerText = `TWD ${target.Price}`

    return collectioncard;
}

function gettag(target) {
    let clonetag = producttagtemplate.content.cloneNode(true)

    clonetag.querySelector(".label").innerText = target

    return clonetag;
}

window.onload = function(){
    carda = document.querySelectorAll(".card_detail a")
    cardheart = document.querySelectorAll(".card_detail .fa-heart")
    carddetail = document.querySelectorAll(".card_detail")
    count = document.querySelector(".page-text-dynamic .text-info")
    let cardheartcount = cardheart.length

    cardheart.forEach((item, index) => {
        item.addEventListener("click", function(event) {
            item.animate(heartscale, heartTiming)

            item.classList.toggle("fa-solid")
            item.classList.toggle("fa-regular")

            url = "/api/Collection/RemoveCollect"
            isfavorite = false

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
                    }
                    else {
                        console.log('Fail')
                    }
                })

            if (isfavorite === false){
                carddetail[index].remove()
                cardheartcount--
                if (cardheartcount > 0){
                    count.innerText = cardheartcount
                }
                else{
                    location.reload()
                }
            }
        })
    })
}

const heartscale = [
    { transform: 'scale(1)' },
    { transform: 'scale(1.5)' },
    { transform: 'scale(1)' },
]

const heartTiming = {
    duration: 1000,
    iterations: 1
}