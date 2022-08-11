//template
let index = 0;
let orderdetailid;
let productid;
let template_card = document.querySelector("#template-card")
let page_text_dynamic = document.querySelector(".page-text-dynamic")
let template_list = @Html.Raw(ViewData["OrderManageCard"])
template_list.OrderDtailList.forEach((x, index) => {
    //先判斷尚無購買任何商品的情況
    if (template_list.OrderDtailList.length == 0) {
        document.querySelector('.page-text').classList.remove("d-none")
        return;
    }
    let clone_card = template_card.content.cloneNode(true);
    clone_card.querySelector(".commm-productname").innerText = x.ProductName
    clone_card.querySelector(".comm-title").innerText = x.Title
    clone_card.querySelector(".comm-orderid").innerText = "訂單編號：#" + x.OrderId
    clone_card.querySelector(".day").innerText = x.DepartureDate.slice(0, 10)
    clone_card.querySelector(".TWD").innerText = "TWD" + x.UnitPrice
    clone_card.querySelector(".comm-path").src = x.Path
    //template:判斷已出發或未出發
    let date = new Date();
    let date1 = new Date(date.toISOString().split('T')[0]);
    let date2 = new Date(clone_card.querySelector(".day").innerText);
    if (date1 < date2) {
        clone_card.querySelector(".togo>span").innerHTML = "未出發"
    }
    else {
        clone_card.querySelector(".togo>span").innerHTML = "已出發"
    }
    //template:給予評論按鈕
    clone_card.querySelector("#commitBtn").addEventListener("click", () => {
        document.querySelector('.alert-danger').classList.add("d-none")
        console.log(x)
        setComment(x);
        //template:判斷客戶留言標題、富文本有無資料
        if (x.comment.CommentTitle == null) {
            document.querySelector("#comment-title").value = ""
            editor.setData("");
            document.querySelector('.alert-danger+p').innerHTML = "你的評論將提供給其他旅客參考"
            document.querySelector('#btn-edit').classList.add("d-none")
            document.querySelector('#btn-submit').classList.remove("d-none")
        }
        else {
            document.querySelector("#comment-title").value = x.comment.CommentTitle
            editor.setData(x.comment.CommentText);
            document.querySelector('.alert-danger+p').innerHTML = "上次留言時間: " + x.comment.CommentDate.substring(0, 10) + " " + x.comment.CommentDate.substring(11, 16)
            document.querySelector('#btn-edit').classList.remove("d-none")
            document.querySelector('#btn-submit').classList.add("d-none")
        }
        //template:客戶留言星星評價
        //let starList = document.querySelectorAll('input[name="rating"]');
        //starList.forEach(radio => { radio.checked = false; })
        //starList.forEach(starRadio => {
        //    if (x.comment.RatingStar === parseInt(starRadio.value)) {
        //        starRadio.checked = true;
        //    }
        //})
        switch (x.comment.RatingStar) {
            case 1:
                document.querySelector('#star1').checked = true;
                break;
            case 2:
                document.querySelector('#star2').checked = true;
                break;
            case 3:
                document.querySelector('#star3').checked = true;
                break;
            case 4:
                document.querySelector('#star4').checked = true;
                break;
            case 5:
                document.querySelector('#star5').checked = true;
                break;
            default:
                for (let i = 1; i <= 5; i++) {
                    document.querySelector('#star' + i).checked = false
                }
        }
        //template:客戶留言旅伴類型
        switch (x.comment.PartnerType) {
            case 1:
                document.querySelector('#family').checked = true;
                break;
            case 2:
                document.querySelector('#couple').checked = true;
                break;
            case 3:
                document.querySelector('#personal').checked = true;
                break;
            case 4:
                document.querySelector('#friend').checked = true;
                break;
            case 5:
                document.querySelector('#business').checked = true;
                break;
            default:
                let array = ['family', 'couple', 'personal', 'friend', 'business']
                for (let i = 0; i < array.length; i++) {
                    document.querySelector('#' + array[i]).checked = false
                }
        }
    })
    page_text_dynamic.appendChild(clone_card)
})
function setComment(x) {
    //改Modal (R)
    orderdetailid = x.comment.OrderDetailId;
    productid = x.comment.ProductId
    console.log(orderdetailid)
}
//CKeditor設定
ClassicEditor
    .create(document.querySelector('.editor'), {
        licenseKey: '',
        ckfinder: {
            // 使用 CKFinder QuickUpload 命令將圖像上傳到服務器
            uploadUrl: '/api/upload',
            // 定義 CKFinder 配置（如有必要
            options: {
                resourceType: 'Images'
            }
        }
    })
    .then(editor2 => {
        window.editor = editor2;

    })
    .catch(error => {
        console.error('Oops, something went wrong!');
        console.error('Please, report the following error on https://github.com/ckeditor/ckeditor5/issues with the build id and the error stack trace:');
        console.warn('Build id: 8cnzoumsu8xz-nohdljl880ze');
        console.error(error);
    });
//按出送出鍵
document.querySelector("#btn-submit").addEventListener("click", function () {
    let html = editor.getData();
    let star_value = document.querySelector('.star:checked');
    let partner_type = document.querySelector('input[name="btnradio"]:checked');
    let comment_title = document.querySelector("#comment-title").value;
    if (html == "" || star_value == null || comment_title == "" || partner_type == null) {
        document.querySelector('.alert-danger').classList.remove("d-none")
        return;
    }
    let url = "/api/apicomment/CreateMemberComment";
    fetch(url, {
        headers: {
            'Content-Type': 'application/json;'
        },
        method: "post",
        body: JSON.stringify({
            //加入產品詳細資訊
            RatingStar: star_value.value,
            Partnertype: parseInt(partner_type.value, 10),
            CommentTitle: comment_title,
            CommentText: html,
            MemberId: @Model.MemberId,
    OrderDetailId: orderdetailid,
        ProductId: productid,
                }),
            })
            .then(resp => {
            if (resp.ok) {
                window.location.reload();
            }
            else {
                toastr['error']('新增留言失敗:有什麼錯誤了！');
            }
        })
        });
//按出修改紐
let submit_edit = document.querySelector('#btn-edit');
submit_edit.addEventListener("click", function () {
    let html = editor.getData();
    let star_value = document.querySelector('.star:checked');
    let partner_type = document.querySelector('input[name="btnradio"]:checked');
    let comment_title = document.querySelector("#comment-title").value;
    if (html == "" || star_value == null || comment_title == "" || partner_type == null) {
        document.querySelector('.alert-danger').classList.remove("d-none")
        return;
    }
    let url = "/api/apicomment/UpdateMemberComment";
    fetch(url, {
        headers: {
            'Content-Type': 'application/json;'
        },
        method: "post",
        body: JSON.stringify({
            //加入產品詳細資訊
            RatingStar: star_value.value,
            Partnertype: parseInt(partner_type.value, 10),
            CommentTitle: comment_title,
            CommentText: html,
            MemberId: @Model.MemberId,
    OrderDetailId: orderdetailid,
        ProductId: productid,
                }),
            })
            .then(resp => {
            if (resp.ok) {
                window.location.reload();
            }
            else {
                toastr['error']('修改留言失敗:有什麼錯誤了！');
            }
        })
        })