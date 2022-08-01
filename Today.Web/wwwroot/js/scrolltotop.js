let scrolltopbtn = document.querySelector(".scroll-to-top")

window.onscroll = function(){scrollFunction()} ;

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.querySelector(".scroll-to-top").style.display = "block";
    }
    else {
        document.querySelector(".scroll-to-top").style.display = "none";
    } 
}

scrolltopbtn.addEventListener("click", topFunction)

function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}