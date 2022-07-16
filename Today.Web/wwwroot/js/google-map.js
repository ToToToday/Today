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
      }
});
let map_card_h5 = document.querySelectorAll(".map-h5");


let mapmodal = document.querySelector('#map-modal')
//let modal = bootstrap.Modal.getOrCreateInstance(modalEl)
//初始化地圖
let map = L.map('google-big-map',
    {
        center: [23.7625225, 121.0302279],
        zoom: 8,
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

for (let i = 0; i < json_location.length; i++) {
    L.marker([json_location[i].latitude, json_location[i].longitude])
        .addTo(map)
        .bindPopup(`<a class='card flex-row rounded google-map-product-item w-100'>\
                        <img src='${json_photo[i].PhotoPath}' class='rounded-start w-50'>\
                        <div class='card-body  position-relative p-0 m-2'>\
                          <h5 class='card-title mb-2 map-h5 '>${json_Name[i].ProductName}</h5>\
                          <p class='star text-info  m-0'><i class='fa-solid fa-star'></i> <span class='star-amount'>4.75</span></p>\
                          <p class='card-text bind-map-price '>TWD <span class='text-info'>358</span></p>\
                        </div></a>`
        )
} 
/*let saveArray = []; //存放locationId 當卡片還在範圍時 , 還在裡面*/
let map_product_list = document.querySelector(".google-map-product-list");
let map_product_list_phone = document.querySelector("#map-list-product-phone");

let MapcardTemplate = document.querySelector("#MapcardTemplate");
let MapcardTemplatePhone = document.querySelector("#MapcardTemplatePhone");

   function createCard(index)  //創造卡片
   {
       let cloneContent = MapcardTemplate.content.cloneNode(true);
       let cloneContentPhone = MapcardTemplatePhone.content.cloneNode(true);
       cloneContent.querySelector('h5').innerText = json_Name[index].ProductName;
       cloneContentPhone.querySelector('h5').innerText = json_Name[index].ProductName;
       cloneContent.querySelector('img').src = json_photo[index].PhotoPath;
       cloneContentPhone.querySelector('img').src = json_photo[index].PhotoPath;
       // cloneContent.querySelector('.map-price').innerText = `TWD ${site_location.price[index]}`;

       map_product_list.append(cloneContent);
       map_product_list_phone.append(cloneContentPhone);
   }
   
 
function clearCard()
{
    let product_item = document.querySelectorAll(".google-map-product-item");
    product_item.forEach(Card => { Card.remove() })

}
//原本有 -> 亂留下卡
map.on("moveend", function()
{
    let map_northEast_lat = map.getBounds()._northEast.lat; //目前地圖右邊 緯度
    let map_northEast_lng = map.getBounds()._northEast.lng; //目前地圖右邊 精度
    let map_southWest_lat = map.getBounds()._southWest.lat; //目前地圖左邊 緯度
    let map_southWest_lng = map.getBounds()._southWest.lng; //目前地圖左邊 精度

    clearCard();
    for (let i = 0; i < json_location.length; i++)
    {
        let site_local_lat = json_location[i].latitude;
        let site_local_lng = json_location[i].longitude;
        let site_local_id =  json_location[i].Id;


        if (map_southWest_lat <= site_local_lat & site_local_lat <= map_northEast_lat & map_southWest_lng <= site_local_lng & site_local_lng <= map_northEast_lng)
        {

            //if (saveArray.includes(site_local_id))//當重複出現在視野裡
            //{
            //    continue;
            //}
/*            else {*/
                //判斷視窗顯示時候,是否有show出景點
                createCard(i);
/*                saveArray.splice(i, 1, site_local_id);*/
/*            }*/
        }
        //else
        //{   
        //    //console.log(`${site_local_id}在沒再裡面`); 
        //    if (saveArray.includes(site_local_id))//要是不在可看見 地圖裡,且儲存矩陣有locationId 時候
        //    {
        //        clearCard(i);
        //        /*                product_items[i].remove();*/
        //        console.log(`delete ${site_local_id}`)
        //        saveArray.splice(i, 1,-1);  //-1表示不在裡面
        //    }
        //    else {
        //        saveArray.splice(i, 1,-1);
        //    }
        //}
    }
    //console.log(saveArray);
})
