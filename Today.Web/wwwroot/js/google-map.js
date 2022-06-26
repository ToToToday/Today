$(".owl-carousel.google-map-product-list-collapse").owlCarousel
    ({
      loop: false,
      margin: 20,
      stagePadding: 32,
      responsive:
      {
        0: {
          items: 1
        },
        450: {
          items: 2
        },
        810:
        {
          item: 3
        }
      }
});

let mapmodal = document.querySelector('#map-modal')
        //let modal = bootstrap.Modal.getOrCreateInstance(modalEl)
        //初始化地圖
let map = L.map('google-big-map', 
{
    center: [23.7625225, 121.0302279],
    zoom: 8,
    wheelPxPerZoomLevel:80,
    tapTolerance:10
})
    // 設定圖資來源
    var osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png'
    var osm = new L.TileLayer(osmUrl, { minZoom: 4, maxZoom: 18 })
    map.addLayer(osm)

    mapmodal.addEventListener('shown.bs.modal', function() 
    {
        map.invalidateSize()
    })
//離島 東西 (經度) 北南 (緯度) 
//基隆嶼燈塔 25.156652,121.7313924
//基隆城市博覽會 基隆之星海上展覽(國門廣場附近)  25.1319577,121.7379821
//桃園戶外休閒體驗營 | OBT 台灣外展 | 高空繩索  X   落羽松露營  24.8418952,121.1881965
//屏東 東港住宿 | 綠芳水岸民宿 | 距離東港碼頭僅需5分  22.4695792,120.4461174
//碰到卡片 跳跳跳
//碰到 local 標誌 會顯示 整張 card 

/*
var site_location = 
{
    site:
    ["基隆嶼燈塔","基隆城市博覽會 基隆之星海上展覽(國門廣場附近)","桃園戶外休閒體驗營 | OBT 台灣外展 | 高空繩索  X   落羽松露營"]
    ,location:
    [[25.156652,121.7313924],[25.1319577,121.7379821],[24.8418952,121.1881965]]
}

console.log(site_location.location[1])
for(let i =0 ;i<site_location.site.length;i++)
{
    L.marker(site_location.location[i])
    .addTo(map)
    .bindPopup("<a class='card flex-row rounded google-map-product-item w-100 '><img src='https://picsum.photos/300/200/?random=10' class='rounded-start'><div class='card-body  position-relative'><h5 class='card-title mb-2'>【2022澎湖花火節】台灣澎湖租機車 | 騎車輕鬆環島遊澎湖</h5><p class='star text-info'>★<span class='star-amount'>4.75</span></p><p class='card-text map-price'>TWD <span class='text-info'>358</span></p></div></a>");
}
*/
