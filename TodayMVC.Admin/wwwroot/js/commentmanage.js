let Data = []
let url = "/api/apicommentmanage/GetAllComment"
fetch(url, {
    headers: {
        'Content-Type': 'application/json;'
    },
    method: "get",
})
    .then(resp => {
        if (resp.ok) {
            Promise.resolve(resp.json())
                .then(function (result) {
                    result.forEach(function (vaule, key) {
                        Data.push(vaule)
                    })
                    app.Detail = Data
                })
        }
        else {
            alert("失敗")
        }
    })

let app = new Vue({
    el: '#app',
    data: {
        Detail: [],
    },
    methods: {
        collapse: function (e) {
            let parent = $(e.target).parent();
            let eachComment = parent.children(".each-comment");
            eachComment.slideToggle("slow");
        },
        deleteComment: function (e, card, comment, j) {
            let url = "/api/APICommentManage/DeleteComment"
            fetch(url, {
                headers: {
                    'Content-Type': 'application/json;'
                },
                method: "post",
                body: JSON.stringify({
                    //加入產品詳細資訊
                    CommentId: comment.CommentId
                }),
            })
                .then(resp => {
                    if (resp.ok) {

                        card.Comments.splice(j, 1)
                        if (card.Comments.length == 0) {
                            var card_index = app.Detail.indexOf(card)
                            app.Detail.splice(card_index, 1)
                        }
                        alert("成功刪除")
                    }
                    else {
                        alert("刪除失敗")
                    }
                })

        }
    }
})