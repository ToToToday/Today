﻿const classifyCardVue = new Vue({
    // 掛載位置
    el: '#classifyCard',
    // 資料, state
    data: {
        //篩選區 
        allFilter: {
            cities: [
                { CityId: 1, CityName: '基隆', checked: false },
                { CityId: 2, CityName: '新北', checked: false },
            ],
            categories: [
                {
                    ProductCategoryId: 1,
                    CategoryName: "住宿",
                    ChildCategory: [
                        {
                            ProductCategoryId: 13,
                            CategoryName: "高鐵假期",
                            ChildCategory: null
                        },
                        {
                            ProductCategoryId: 14,
                            CategoryName: "飯店",
                            ChildCategory: null
                        },
                    ]
                },
            ],
        },
        //排序區
        allsort: [
            { SortId: 1, SortIcon: "fa-regular fa-thumbs-up", SortName: 'Today推薦', checked: false },
            { SortId: 2, SortIcon: "fa-solid fa-fire", SortName: '熱門程度', checked: false },
            { SortId: 3, SortIcon: "fa-solid fa-star", SortName: '用戶評價', checked: false },
            { SortId: 4, SortIcon: "fa-solid fa-dollar-sign", SortName: '價格 : 低到高', checked: false },
        ],
        //卡列表區
        page: 1,
        cardCount: 7991,
        productCards: [
            {
                productId: '1',
                href: '',
                path: '',
                productName: '',
                tagText: [],
                cityName: '',
                ratingStar: '',
                totalComment: '',
                prices: {
                    originalPrice: '',
                    price: '',
                },
                favorite: false,
            },
            {
                productId: '1',
                href: '',
                path: '',
                productName: '',
                tagText: [],
                cityName: '',
                ratingStar: '',
                totalComment: '',
                prices: {
                    originalPrice: '',
                    unitPrice: '',
                },
                favorite: false
            },
        ],
    },
    //從後端拉資料到前端 需要東西時到此發請求
    mounted() {
        //let cityList = @Html.Raw(cityJson);
        //let categoryList = @Html.Raw(categoryJson);
        //console.log(cityList)
        //console.log(categoryList)
        this.allFilter.cities = cityList.map(x =>
        ({
            CityId: x.CityId,
            CityName: x.CityName,
            Checked: false,
        })
        )
        this.allFilter.categories = categoryList.map(x => {
            //x['data-bs-target'] = `#collapseAttractiontickets${x.ProductCategoryId}`
            x.ChildCategory.forEach(child => child.Checked = false)
            return x;
        })
        this.filterPost(1)
        //this.productCards = JSON.parse(' Html.Raw( JsonConvert.SerializeObject(Model.ClassifyCardList) ) ')
    },
    // computed: {
    //     data() {
    //         return {
    //             guestShowProduct: [],
    //         }
    //     },
    // },
    
    // 可寫任何方法到vue實體裡
    methods: {
        // cancelAll() {
        //     this.allFilter.cities.forEach(c => c.Checked = false)
        //     this.allFilter.categories.forEach(ca => {
        //         ca.ChildCategory.forEach(child => { child.Checked = false })
        //     })
        //     this.filterPost(1)
        // },
        //pageRadio
        cancel(target) {
            console.log('取消', target)
            target.Checked = false
            this.filterPost(1)
        },
        // sortHightToLow() {
        //     this.guestShowProduct.sort((a, b) => {
        //         if (a.Price < a.OriginalPrice) {
        //             return b.Price - a.Price
        //         } else if (a.Price === a.OriginalPrice) {
        //             return b.Price - a.OriginalPrice
        //         }
        //     })
        // },
        //towNumber(val) {
        //    return val.toFixed(2)
        //},
        async filterPost(page) {
            fetch("/api/ClassifyApi/Classify", {
                method: 'post',
                headers: {
                    'content-type': 'application/json;charset=utf-8',
                },
                body: JSON.stringify({
                    //cities: array.from(citieschecked).map(x => x.value),
                    // 篩選  
                    cities: this.allFilter.cities.filter(x => x.Checked).map(x => x.CityId),
                    categories: this.allFilter.categories.map(ca => ca.ChildCategory).flatMap(x => x)
                        .filter(x => x.Checked).map(x => x.ProductCategoryId),
                    // 排序
                    allsort: this.allsort.filter(x => x.Checked).map(x => x.SortId,),
                    // todayrecommend:this.allsort.todayrecommend.filter(x => x.Checked)
                    // pricelowtohigh(){
                    //     this.
                    // }
                    // listMe: function (list) {
                    //     return list.filter(function (item) {
                    //         return item.n <= 33
                    //     })
                    // },

                    //categories: Array.from(selfformchecked).map(x => x.value), 
                    //citiescategories: Array.from(citiescategorieschecked).map(x => x.value),
                    // popularity:popularity,
                    page: page,
                })
            })
                .then(resp => resp.json())
                .then(JSobj => {
                    console.log(JSobj)
                    //重新處理DOM
                    if (classifyCardVue.cardCount != JSobj.cardCount)
                        classifyCardVue.cardCount = JSobj.cardCount
                    //document.querySelector('.experience-itinerary').innerHTML = JSobj.cardCount
                    classifyCardVue.page = page;

                    classifyCardVue.productCards =
                        JSobj.classifyCardList.map(x => {
                            x.href = `/Product/ProductInfo/${x.productId}`
                            return x;
                        })
                        // classifyCardVue.allsort = 
                    // classifyCardVue.popularity = popularity;
                    document.querySelector('#destFilter').focus();
                })
                .then(() => {
                    let OriginalPrice = document.querySelectorAll('.OriginalPrice')
                    let Price = document.querySelectorAll('.Price')
                    OriginalPrice.forEach((item, index) => {
                        if (item.innerText === Price[index].innerText) {
                            item.classList.add("d-none")
                        }
                    })
                })
        },

    },
})