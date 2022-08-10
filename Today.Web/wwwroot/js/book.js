let bookbtn = document.querySelector("button#book")
let bookinput = document.querySelector(".book .input-group input")

bookbtn.addEventListener("click", function (event) {
    let url = "/api/Book/AddBook"

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    if (VerifyEmail(bookinput.value)) {
        fetch(url, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ Email: bookinput.value })
        })
            .then(res => {
                if (res.status === 200) {
                    toastr["success"]("訂閱成功", "成功")
                    bookinput.value = ""
                }
                else {
                    toastr["error"]("訂閱失敗", "錯誤")
                    bookinput.value = ""
                }
            })
    }
})

function VerifyEmail(target) {
    var reg = new RegExp("^\\w+@\\w{2,6}(\\.\\w{2,3})+$", "i");
    if (reg.test(target)) {
        return true
    }
    else {
        target = ""
        toastr["warning"](`請輸入 "正確的" Email !`, "警告")
        return false
    }
}