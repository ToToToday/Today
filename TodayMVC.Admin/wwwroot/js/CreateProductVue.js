/*const { ajax } = require("jquery")*/

//import { json } from "stream/consumers"

//import { push } from "core-js/core/array"

let array = []
const app = new Vue({
    el: '#app',
    data: {
        todoItem: '',
        todoList: [],
        AboutProgramOptionList:[],
        Prdouctname: '',
        Category: '',
        CategoryList: [],
        //CategoryList: ['Apple', 'Orange', 'Banana', 'Lime', 'Peach', 'Chocolate', 'Strawberry'],
        options: [],
        //options: ['Apple', 'Orange', 'Banana', 'Lime', 'Peach', 'Chocolate', 'Strawberry'],
        Supplier: {
            CompanyName: '',
            ContactName: '',
            ContactTitle: '',
            Address: '',
            Phon: '',
            City: '',
        },
        location: {
            locationTitle: '',
            locationtext: '',
            locationAddress: '',
            Longitude: '',
            Latitude: '',
            Type: '',
        },
        ProgarmList: [],
        Progarm: {
            PrgramName: '',
            PrgarmText: '',
            ProgramIncludeList: [],
            ProgramSpecificationList: [],
            //DateList: [],
            //ScreeningList:'',
        },
        ProgramSpecification: {
            Itemtext: '',
            UnitText: '',
            PorgarmUnitPrice: '',
            OriginalUnitPrice: '',
            //Status: '',
        },
        TagList: [],
        cityid: '',
        Tag: '',

        asd:[],
        search: '',
        //DateList: [],
        Screening: '',
        ProgramIncludeTrueListvalue: [],
        ProgramIncludeFalsListvalue: [],
        value: [],
        isShow: '',
        text: '',
        selected: [],
        Cityoptions: [],
        //{ value: 'A', text: 'Option A (from options prop)' },
        //{ value: 'B', text: 'Option B (from options prop)' }
    },
    mounted() {
        this.getSQL()
    },
    computed: {
        state() {
            return this.Prdouctname.length >= 4
        },
        invalidFeedback() {
            if (this.Prdouctname.length > 0) {
                return 'Enter at least 4 characters.'
            }
            return 'Please enter something.'
        },
        criteria() {
            // Compute the search criteria
            return this.search.trim().toLowerCase()
        },
        availableOptions() {
            const criteria = this.criteria
            // Filter out already selected options
            const options = this.options.filter(opt => this.value.indexOf(opt) === -1)
            if (criteria) {
                // Show only options that match criteria
                return options.filter(opt => opt.toLowerCase().indexOf(criteria) > -1);
            }
            // Show all options available
            //options.forEach(




            //)
            return options
        },
        searchDesc() {
            if (this.criteria && this.availableOptions.length === 0) {
                return 'There are no tags matching your search criteria'
            }
            return ''
        }
    },
    methods: {
        //availableOptions() {
        //    return this.options.filter(opt => this.value.indexOf(opt) === -1)
        //},
        addTodoItem() {
            if (this.todoItem != '') {
                this.todoList.push(this.todoItem)
                this.todoItem = ''
            }
        },
        onOptionClick({ option, addTag }) {
            console.log(option)
            addTag(option)
            this.search = ''
        },
        onReomveTagClick({ removeTag, tag }) {
            console.log(tag)
            removeTag(tag)
        },
        getSQL() {
            let url = "/api/ProdcutApi/get"
            fetch(url, {
                method: 'Get',
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(res => { return res.json() })
                .then(res => {

                    console.log(res)
                    //console.log('OK')
                    //console.log(this.Cityoptions)
                    ////let obj = cityList.map(c => { value = c.CityId, text =  c.CityName })
                    ////console.log(obj)
                    //this.Cityoptions = obj

                    //console.log(res.json())
                    //console.log(Promise.resolve)

                    //Promise.resolve(res.json()).then(function (result) {
                    res.cityList.forEach(c => {
                        let obj = { value: c.CityId, text: c.CityName }
                        this.Cityoptions.push(obj)
                        console.log(obj)

                    })
                    res.catecorieList.forEach(c => {
                        let obj = { value: c.CatecoryId, text: c.CatecoryName }
                        console.log(obj)
                        this.CategoryList.push(obj)
                    })
                    res.TagList.forEach(t => {
                        let obj = { value: t.Tagid, text: t.Tagname }
                        //console.log(obj)
                        this.asd.push(obj)
                        this.options.push(t.Tagname)
                    })
                    //console.log(this.CategoryList, this.Cityoptions,this.options)

                    console.log(this.asd)
                    //})
                    //this.selected.push(res.json())


                })
                .catch(res => {
                    console.log('Fail')

                })

        },
        CreateProduct() {
            //console.log(this.Prdouctname, this.ProgarmList)
            //console.log(array)
            //方案包含與不包含
            let tagidlist = [] 

            this.asd.forEach(t => {
                this.TagList.forEach(tg => {

                    if (tg == t.text) {
                        tagidlist.push(t.value)
                    }
                })
            })
            console.log(parseInt(this.cityid))
            console.log(this.Category)
            console.log(tagidlist)




            //alert(asd)
            this.ProgramIncludeTrueListvalue.forEach(ps => {
                let obj = { Inciudetext: ps, IsInclude: true }
                this.Progarm.ProgramIncludeList.push(obj)
            })
            this.ProgramIncludeFalsListvalue.forEach(ps => {
                let obj = { Inciudetext: ps, IsInclude: false }
                this.Progarm.ProgramIncludeList.push(obj)
            })
            this.Progarm.ProgramSpecificationList.push(this.ProgramSpecification)//方案歸規格
            let ProductText = array[0].getData()//商品說明
            let CancellationPolicy = array[1].getData()//取消政策
            let HowUse = array[2].getData()//如何購買
            let ShoppingNotice = array[3].getData()//購買須知

            let AboutProgramOptions = []

            this.AboutProgramOptionList.forEach(ap => {
                let obj = { Context: ap, IconClass: "<i class=\"icons icon-paper-plane\"></i>"}
                AboutProgramOptions.push(obj)

            })


            this.ProgarmList.push(this.Progarm)//方案push進ProgarmList回傳
            let url = "/api/ProdcutApi/Create"
            fetch(url, {
                headers: {
                    'Content-Type': 'application/json;charset=utf-8'
                },
                method: 'post',
                body: JSON.stringify({
                    //加入產品詳細資訊
                    ProductName: this.Prdouctname,
                    CategoryList: [this.Category],
                    TagList: tagidlist,
                    ProductText: ProductText,
                    CancellationPolicy: CancellationPolicy,
                    HowUse: HowUse,
                    ShoppingNotice: ShoppingNotice,
                    location: this.location,
                    Supplier: this.Supplier,
                    ProgarmList: this.ProgarmList,
                    City: parseInt(this.cityid),
                    AboutProgramOptionList: AboutProgramOptions,
                    PhtotList: this.todoList,
                }),
            })
            //.then(resp => {
            //    if (resp.ok) {
            //        return resp.text()
            //    }
            //}).then(text => {
            //    alert(text);
            //})
        }
    }

})


document.querySelectorAll('.introEditor').forEach((u, index) => {
    ClassicEditor
        .create(document.querySelectorAll('.introEditor')[index], {
            licenseKey: '',
            simpleUpload: {
                // The URL that the images are uploaded to.
                uploadUrl: '/api/APICKEditor/CKEditorMedia',

                // Enable the XMLHttpRequest.withCredentials property.
                // withCredentials: true,

                // Headers sent along with the XMLHttpRequest to the upload server.
                headers: {
                    //'X-CSRF-TOKEN': 'CSRF-Token',
                    //Authorization: 'Bearer <JSON Web Token>'
                },
            }
        })
        .then(editor => {
            window.introEditor = editor; //這句目的是 定義全域變數
            array.push(editor)
            //CKEditor重要的兩個內建方法：
            /*introEditor.setData("<p>預先放好的內容</p>");*/
            //introEditor.getData();  這是要儲存時用的，要把整段html字串拿到，寫入DB
        })
        .catch(error => {
            console.error('Oops, something went wrong!');
            console.error('Please, report the following error on https://github.com/ckeditor/ckeditor5/issues with the build id and the error stack trace:');
            console.warn('Build id: zdeudx7xcec-a1ii48rgiwrp');
            console.error(error);
        });
})


drag()
function drag() {
    $("#sortable").sortable(
        {
            items: "li", //目標是裡面的li
            opacity: 0.6,
            cursor: "move",
        })
    var Sortable_arr = $("#sortable").sortable('toArray');

    $("#sortable").sortable({
        stop: function () {
            var Sortable_arr = $("#sortable").sortable('toArray');
            console.log(Sortable_arr);
        }
    })
    //當sortable改變的時候 --> 更新 sortable_arr 值
}
//let btn = document.querySelector("button")
//console.log(btn)
//btn.addEventListener("click", () => {
//    console.log("123")
//    let url = "/api/ProdcutApi/Create"
//    fetch(url, {
//        headers: {
//            'Content-Type': 'application/json;charset=utf-8'
//        },
//        method: 'post',
//        body: JSON.stringify({
//            //加入產品詳細資訊
//            ProductId: 1,
//        }),
//    })
//    //.then(resp => {
//    //    if (resp.ok) {
//    //        return resp.text()
//    //    }
//    //}).then(text => {
//    //    alert(text);
//    //})
//})
