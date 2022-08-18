
const classifyCardVue = new Vue({
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
            offIslandcities:
                [
                    { CityId: 24, CityName: '馬祖', CityImage: '' },
                    { CityId: 22, CityName: '澎湖', CityImage: '' },
                    { CityId: 19, CityName: '綠島', CityImage: '' },
                    { CityId: 23, CityName: '金門', CityImage: '' },
                    { CityId: 20, CityName: '蘭嶼', CityImage: '' },
                ],
            bannerChooseCity:
                [
                    { CityName: '基隆' },
                    { CityName: '蘭嶼' },
                    { CityName: '屏東' },
                    { CityName: '馬祖' },
                ],
        },
        //排序區
        allsort: [
            //{ SortId: 1, SortIcon: "fa-regular fa-thumbs-up", SortName: 'Today推薦', checked: false },
            { SortId: 2, SortIcon: "fa-solid fa-fire", SortName: '熱門程度', checked: true },
            { SortId: 3, SortIcon: "fa-solid fa-star", SortName: '用戶評價', checked: false },
            { SortId: 4, SortIcon: "fa-solid fa-dollar-sign", SortName: '價格 : 低到高', checked: false },
        ],
        //卡列表區
        page: 1,
        isOffIsland: false,
        isParent: false,
        isRent: false,
        isHSR: false,
        isDIY: false,
        cardCount: 100,
        productCards: [
            {
                productId: '1',
                href: '',
                path: '/image/Classify/1.gif',
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
                path: '/image/Classify/1.gif',
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
            {
                productId: '1',
                href: '',
                path: '/image/Classify/1.gif',
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
        this.allFilter.cities = cityList
        this.allFilter.categories = categoryList
        if (window.location.pathname.includes("OffIsland"))
        {
            this.allFilter.offIslandcities = OffIslandCardList.map
            (x =>
                ({
                    CityId: x.CityId,
                    CityName: x.CityName,
                    CityImage: x.CityImage,
                })
            ),
                this.allFilter.isOffIsland = true 
        }
        if (window.location.pathname.includes("DIY")) {
                this.allFilter.isDIY= true
        }
        if (window.location.pathname.includes("ParentChild")) {
            this.allFilter.isParent = true
        }
        if (window.location.pathname.includes("Rent")) {
            this.allFilter.isRent = true
        }
        if (window.location.pathname.includes("HSR")) {
            this.allFilter.isHSR = true
        }
        this.filterPost(1)
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
        cancelAll() {
            this.allFilter.cities.forEach(c => c.Checked = false)
            this.allFilter.categories.forEach(ca => {
                ca.ChildCategory.forEach(child => { child.Checked = false })
            })
            this.filterPost(1)
        },
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
        changeSort(target) {
            this.allsort.forEach(
                     (item,index)=>{
                        item.checked = false
                    }
            )

            target.checked = true
            this.filterPost(1)
        },
        async filterPost(page) {
            //v 戴格停區
            var dateRange = null
            try {
                var dateSelectd_text_All = document.querySelector("span.drp-selected").innerText;
                //dateSelectd_text_All.forEach(item => {
                //    var dateSelectd_text = item.innerText
                //})
                if (dateSelectd_text_All == "")
                    throw new Error();

                dateRange = dateSelectd_text_All.split(' - ');
            }
            catch (err) {
                dateRange = null
            }
            try {
                var city_choosed = document.querySelector('.show-city').innerText;
                var btn_city = document.querySelectorAll("#choose-city");
                btn_city.forEach(btn => {
                    if (btn.innerText == city_choosed) {
                        city_choosed = btn.innerText
                    }
                })
                if (city_choosed == '') {
                    city_choosed =null
                }
            }
            catch (err){
                city_choosed=null
            }
            try {
                let params = new URLSearchParams(All_search);
                let get_Date = params.get("City");
                if (get_Date == null) {
                    offCityName= null
                }
                var offCityName = get_Date;
            }
            catch (err){
                offCityName = null;
            }
            try {
                var typeBanner = document.querySelector("#searchString").value;
                if (typeBanner == '') {
                    typeBanner = null
                }
            }
            catch (err) {
                typeBanner=null
            }
            // 轉圈啟動
            coverplate.classList.remove('d-none')
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
                    dateRange: dateRange,
                    IsOffIsland: this.allFilter.isOffIsland,
                    isParent: this.allFilter.isParent,
                    isRent: this.allFilter.isRent,
                    isHSR: this.allFilter.isHSR,
                    isDIY: this.allFilter.isDIY,
                    city_choosed: city_choosed,
                    offCityName: offCityName,
                    typeBanner: typeBanner,
                    // 排序
                    sortBy: this.allsort.find(x => x.checked).SortId,

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
                    classifyCardVue.page = page;

                    classifyCardVue.productCards =
                        JSobj.classifyCardList.map(x => {
                            x.href = `/Product/ProductInfo/${x.productId}`
                            return x;
                        })
                    document.querySelector('#destFilter').focus();
                    // 轉圈關掉
                    coverplate.classList.add('d-none')

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