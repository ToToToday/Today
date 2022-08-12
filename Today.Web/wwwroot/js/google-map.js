let map_card_h5 = document.querySelectorAll(".map-h5");


let mapmodal = document.querySelector('#map-modal')
//let modal = bootstrap.Modal.getOrCreateInstance(modalEl)
//初始化地圖
let map_lat = 23.7625225;
let map_long = 121.0302279;
let map_zoom = 8
if (window.location.pathname.includes("City"))
{
    map_lat = json_city_location[0].City_Latitude;
    map_long = json_city_location[0].City_Longtitude;
    map_zoom = 10.5
}

let map = L.map('google-big-map',
    {
        center: [map_lat, map_long],
        zoom: map_zoom,
        wheelPxPerZoomLevel: 80,
        tapTolerance: 10
    })
// 設定圖資來源
var osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png'
var osm = new L.TileLayer(osmUrl, { minZoom: 4, maxZoom: 18 })
map.addLayer(osm)

mapmodal.addEventListener('shown.bs.modal', function () {
    map.invalidateSize()
})


var LeafIcon = L.Icon.extend({
    options: {
        iconSize: [30, 50],
    }
});
var orangeIcon = new LeafIcon({ iconUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/aa/Google_Maps_icon_%282020%29.svg/536px-Google_Maps_icon_%282020%29.svg.png' })


for (let i = 0; i < json_location.length; i++) {
    L.marker([json_location[i].Latitude, json_location[i].Longitude], { icon: orangeIcon, title: `${json_location[i].ProductName}` })
        .addTo(map)
        .bindPopup(`<a class='card flex-row rounded google-map-product-item w-100' .href = "/Product/ProductInfo/${json_location[i].ProductId}">\
                        <img src='${json_location[i].Path}' class='rounded-start w-50'>\
                        <div class='card-body  position-relative p-0 m-2'>\
                          <h5 class='card-title mb-2 map-h5 '>${json_location[i].ProductName}</h5>\
                          <p class='star text-info  m-0'><i class='fa-solid fa-star'></i> <span class='star-amount'>4.75</span></p>\
                          <p class='card-text bind-map-price '>TWD <span class='text-info'>358</span></p>\
                        </div></a>`
        )
}

let map_product_list = document.querySelector(".google-map-product-list");
let map_product_list_phone = document.querySelector(".map-list-product-phone");

let MapcardTemplate = document.querySelector("#MapcardTemplate");
let MapcardTemplatePhone = document.querySelector("#MapcardTemplatePhone");

function createCard(index)  //創造卡片
{
    let cloneContent = MapcardTemplate.content.cloneNode(true);
    let cloneContentPhone = MapcardTemplatePhone.content.cloneNode(true);

    cloneContent.querySelector('h5').innerText = json_location[index].ProductName;
    cloneContentPhone.querySelector('h5').innerText = json_location[index].ProductName;

    cloneContent.querySelector('.star-amount').innerText = json_location[index].RatingStar;
    cloneContent.querySelector('.star-amount').innerText = json_location[index].RatingStar;

    cloneContent.querySelector('.google-map-product-item').href = `/Product/ProductInfo/${json_location[index].ProductId}`
    cloneContentPhone.querySelector('.google-map-product-item').href = `/Product/ProductInfo/${json_location[index].ProductId}`

    cloneContent.querySelector('img').src = json_location[index].Path;
    cloneContentPhone.querySelector('img').src = json_location[index].Path;

    cloneContent.querySelector('.map-price').innerText = `TWD ${json_location[index].Price}`
    cloneContentPhone.querySelector('.map-price').innerText = `TWD ${json_location[index].Price}`

    map_product_list.append(cloneContent);
    map_product_list_phone.append(cloneContentPhone);
}

function clearCard() {
    let product_item = document.querySelectorAll(".google-map-product-item");
    let product_item_phone = document.querySelectorAll(".map-product-item-phone");
    product_item.forEach(Card => { Card.remove() });
    product_item_phone.forEach(Card => { Card.remove() });

}
//原本有 -> 亂留下卡
map.addEventListener("moveend", function () {
    let map_northEast_lat = map.getBounds()._northEast.lat; //目前地圖右邊 緯度
    let map_northEast_lng = map.getBounds()._northEast.lng; //目前地圖右邊 精度
    let map_southWest_lat = map.getBounds()._southWest.lat; //目前地圖左邊 緯度
    let map_southWest_lng = map.getBounds()._southWest.lng; //目前地圖左邊 精度

    clearCard();
    for (let i = 0; i < json_location.length; i++) {
        let site_local_lat = json_location[i].Latitude;
        let site_local_lng = json_location[i].Longitude;

        if (map_southWest_lat <= site_local_lat & site_local_lat <= map_northEast_lat & map_southWest_lng <= site_local_lng & site_local_lng <= map_northEast_lng) {
            createCard(i);
        }
    }
    var $owl = $(".owl-carousel.google-map-product-list-collapse").owlCarousel
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
            }
        });
    $owl.trigger('refresh.owl.carousel'); //重整owl-carousel 避免 當地圖重整的時候 ,會跑版

})
