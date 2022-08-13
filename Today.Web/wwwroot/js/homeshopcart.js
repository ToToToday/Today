let shopcart = document.querySelectorAll("nav .fa-cart-shopping")

shopcart.forEach((item, index) => {
    item.addEventListener("click", function(evnet){
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
                            location.href="/Member/ShopCart"
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