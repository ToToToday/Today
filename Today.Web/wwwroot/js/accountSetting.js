(function () {

    let forms = document.querySelectorAll('.needs-validation')

    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('click', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()


let btn_submit = document.querySelector('#btn-submit');
let nameValue, genderValue, ageValue, cityValue, phoneValue
let saveMessage

btn_submit.addEventListener('click', function () {
    nameValue = document.querySelector("#name").value;
    genderValue = document.querySelector("#gender").value;
    //let identityCardValue = document.querySelector("#identityCard").value;
    ageValue = document.querySelector("#age").value;
    cityValue = document.querySelector("#city").value;
    phoneValue = document.querySelector("#phone").value;

    let request = {
        //加入個人資料(詳細資訊)
        MemberName: nameValue,
        Age: ageValue != "" ? parseInt(ageValue) : "",
        Phone: phoneValue,
        CityId: cityValue,
        Gender: genderValue == "null" ? null : genderValue == 1,
        //IdentityCard: identityCardValue,
    }

    fetch("/api/MemberApi/CountSetting", {
        method: "Post",
        cache: "no-cache",
        // headers 加入 json 格式
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        // body 將 json 轉 字串送出
        body: JSON.stringify(request),
    })
        .then(res => {
            return res.json()
        }).then(res => {
            //console.log(res)
            toastr['success']('資料已更新')
        })
});





