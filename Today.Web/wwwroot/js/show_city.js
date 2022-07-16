let choose_city = document.querySelectorAll("#choose-city");
let show_city = document.querySelector(".show-city");


choose_city.forEach((element,index)=>
    {
        element.addEventListener('click',function()
        {
            for(let i =0 ; i<choose_city.length;i++)
            {
                if(index==i)
                {
                    let city_name = element.innerText; 
                    show_city.innerHTML = city_name +"<span class='ms-2 fa-solid fa-caret-down opacity-25'></span>";
                    element.removeAttribute("class","btn-outline-info")
                    element.setAttribute("class","btn btn-info rounded-pill w-100 ps-0 pe-0 text-white");
                }
                else{
                    choose_city[i].setAttribute("class","btn btn-outline-info rounded-pill w-100 ps-0 pe-0");
                }
            }
        });

    });
//抓取 縣市! city_name


let choose_city_phone = document.querySelectorAll("#choose-city-phone");

choose_city_phone.forEach((element2,index)=>
    {
        element2.addEventListener('click',function()
        {
            for(let i =0 ; i<choose_city_phone.length;i++)
            {
                if(index==i)
                {
                    element2.removeAttribute("class","btn-light")
                    element2.setAttribute("class","btn btn-info rounded-pill w-100 p-0 text-white ");
                }
                else{
                    choose_city_phone[i].setAttribute("class","btn btn-light rounded-pill w-100 text-black-50 p-0");
                }
            }
        });
    })