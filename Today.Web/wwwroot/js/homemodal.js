let submit = document.querySelector("#signupSubmit")
let agreelabel = document.querySelector("#agree-label")
let agreebox = agreelabel.querySelector("#checkAgree")
let password = document.querySelector("#password")

//agreelabel.addEventListener("click", function () {
//    if (agreelabel.querySelector("#checkAgree").checked == true) {
//        //console.log(agreelabel.querySelector("#checkAgree").checked)
//        submit.disabled = false;
//    }
//    else {
//        //console.log(agreelabel.querySelector("#checkAgree").checked)
//        submit.disabled = true;
//    }
//})

password.addEventListener('enter') //(未寫完)


agreelabel.addEventListener("click", function () {
    submit.disabled = true;
    if (agreelabel.querySelector("#checkAgree").checked != true) {
        //console.log(agreelabel.querySelector("#checkAgree").checked)
        submit.disabled = true;
    }
    else {
        submit.disabled = false;
    }
})
