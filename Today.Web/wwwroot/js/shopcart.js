
        let createItem = document.querySelectorAll(".list .item")
        let emptyCart = document.querySelector(".empty-cart")
        let checkout = document.querySelector(".checkout")

        let reconfirmation = document.querySelector(".reconfirmation")
        let shoppingGroup = document.querySelector(".shopping-group")
        let deleteCard = document.querySelector("#deleteCard .btn-primary")
        
        let CheckItem = document.querySelectorAll(".item input[type='checkbox']")
        let Check
        let CheckAll = document.querySelector(".checkout .check-control input[type='checkbox']")
        let label = document.querySelector(".checkout [for='checkout-control']")
        let totalNum = document.querySelector(".total > span")
        let price = document.querySelectorAll(".price")
        let calculate = 0
        let sumPrice = document.querySelector(".total > span:last-child")
        let deletebtnlist = document.querySelectorAll(".text-end>button")
        let deleteAllCard = document.querySelector("#deleteAllCard .btn-primary")

        let itemCard = document.querySelectorAll(".list .item")
        let deleteAll = document.querySelector(".footer-allDelete")
        

        deletebtnlist.forEach((d, index) => {
            console.log(d)
            d.addEventListener("click", () => {
                document.querySelector(".footer-delete").value = deletebtnlist[index].value
            })
        })

        if (createItem.length == 0) {
            emptyCart.classList.remove("d-none")
            reconfirmation.classList.add("d-none")
            //if (time.contains(getdata.getFullYear())) {
            //    shoppingGroup.classList.remove(reconfirmation)
            //}
        }
        else {
            reconfirmation.classList.add("d-none")
            checkout.classList.remove("d-none")
        }

        
        //點全選按鈕
        CheckAll.addEventListener("click", () => {
            

            Check = document.querySelectorAll(".item input[type='checkbox']:checked")
            if (CheckAll.checked){
                CheckItem.forEach((item, index) => {
                    item.checked = true
                    label.innerHTML = `全選(${CheckItem.length})`
                    totalNum.innerHTML = `${CheckItem.length}件商品合計`
                    
                    calculate = 0
                    itemCard.forEach(i => {

                        itemPrice =  i.querySelector(".price").innerHTML
                        calculate += parseInt(itemPrice)
                    
                    })
                    sumPrice.innerText = `TWD ${calculate}`
                    
                    //label.value = CheckItem.length
                    
                })  
                
            }
            else{
                CheckItem.forEach((item, index) => {
                    item.checked = false
                    
                    
                })
  
                if (!CheckAll.checked) {
                    calculate = 0
                    label.innerHTML = `全選(${CheckItem.length - Check.length})`
                    totalNum.innerHTML = `${CheckItem.length - Check.length}件商品合計`
                    sumPrice.innerText = `TWD ${calculate}`
                    
                }
            }
        


            


            //for (var i = 0; i <= CheckItem.length; i++) {
            //    if (CheckItem[i].checked) {
            //        CheckItem[i].checked = false
                    
            //    }
            //    else{
            //        CheckItem[i].checked = true
                    
            //    }

            //    //if (CheckItem.l) {

            //    //}

            //}


        })
                

        label.innerHTML = `全選(${CheckItem.length})`
        totalNum.innerHTML = `${CheckItem.length}件商品合計`


        
        let itemPrice
        
        //點單項商品卡
        CheckItem.forEach(c => {
            
            
            c.addEventListener("click", () => {
                
                calculate = 0
                itemCard.forEach(i => {

                    if (i.querySelector("input[type='checkbox']").checked == true) {
                        itemPrice = i.querySelector(".price").innerHTML
                        calculate += parseInt(itemPrice)

                        
                    }
                    
                })
                sumPrice.innerText = `TWD ${calculate}`
                
               
                Check = document.querySelectorAll(".item input[type='checkbox']:checked")
                if (c.checked == false)
                {

                    CheckAll.checked = false
                    
                    
                        
                }
                else if(Check.length == CheckItem.length){

                    CheckAll.checked = true
                    
                }
                
                 
                label.innerHTML = `全選(${Check.length})`
                totalNum.innerHTML = `${Check.length}件商品合計`          
                
            })
            

            
        })
        
            
        //Array.from(document.querySelectorAll(".item input[type='checkbox']:checked")).map(x => x.value)

        
        let modalNone = document.querySelector("#deleteAllCard")

        //deleteAllCard.addEventListener("click", () => {
        //    createItem.forEach((item, index) => {
        //        item.remove()
        //        emptyCart.classList.remove("d-none")
        //        checkout.classList.remove("d-none")
        //        modalNone.classList.add("d-none")
        //    })
        //})

        


            
        deleteCard.addEventListener("click", function() {

            
            let shoppingCardId = document.querySelector(".footer-delete").value
            console.log(shoppingCardId)
            toastr.options = {
                "positionClass": "toast-top-center",
                "timeOut": "3000"
            }

            let url = "/api/Shop/DeleteCard";
            fetch(url, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({

                    ShoppingCartId: parseInt(shoppingCardId),
                    //allshoppingCard:allshoppingCard
                })
            })
                .then(res => {
                    if (res.status === 200) {
                        //toastr.success("刪除成功");
                        window.location.reload();
                    }
                    else {
                        toastr.error("刪除失敗");
                        //return res.text()
                    }

                })
            
            
            
        })

        deleteAllCard.addEventListener("click", () => {
            toastr.options = {
                "positionClass": "toast-top-center",
                "timeOut": "3000"
            }

            let url = "/api/Shop/DeleteCard";
            fetch(url, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({

                    ShoppingCartIdList: Array.from(document.querySelectorAll(".item input[type='checkbox']:checked")).map(x => x.value),
                    //allshoppingCard:allshoppingCard
                })
            })
                .then(res => {
                    if (res.status === 200) {
                        //toastr.success("刪除成功");
                        window.location.reload();
                    }
                    else {
                        toastr.error("刪除失敗");
                        //return res.text()
                    }

                })
        })

    //order    
    let create_order_btn = document.querySelector(".caculate>a")
    create_order_btn.addEventListener("click", () => {
        let List = document.querySelectorAll("input.commodity-control:checked")
        fetch("/api/Order/CreateOrder", {
            method: 'Post',
            cache: 'no-cache',
            headers: new Headers({ 'Content-Type': 'application/json' }),
            body: JSON.stringify({
                //加入產品詳細資訊
                ShoppingCradIdList: Array.from(List).map(x => parseInt(x.value))
            })
        })
            .then(res => {
                if (res.status == 200) {
                    toastr.success("新增成功");
                    return res.json()
                }
                else {
                    toastr.error("新增失敗");
                    //return res.text()
                }
            })
            .then(obj => {
                //console.log(obj)
                //Status
                //Msg
                //Result
                window.location.href = `/Member/Checkout/${obj.result.id}`

            })
    })