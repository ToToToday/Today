var explorecityswiper = new Swiper(".explorecity-Swiper", {
    preloadImages: false,
    lazy: true,
    slidesPerView: 1,
    breakpoints: {
        768: {
            slidesPerView: 3
        },
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev"
    }
});
var recentlyswiper = new Swiper(".recently-Swiper", {
    preloadImages: false,
    lazy: true,
    slidesPerView: 1,
    breakpoints: {
        320: {
            slidesPerView: 1
        },
        768: {
            slidesPerView: 2
        },
        992: {
            slidesPerView: 3
        },
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev"
    }
});
var swiper = new Swiper(".mySwiper", {
    preloadImages: false,
    lazy: true,
    slidesPerView: 1,
    breakpoints: {
        320: {
            slidesPerView: 1
        },
        425: {
            slidesPerView: 1.5
        },
        768: {
            slidesPerView: 3
        },
        992: {
            slidesPerView: 4
        },
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev"
    }
});
var nearbycityswiper = new Swiper(".nearbycity-Swiper", {
    preloadImages: false,
    lazy: true,
    slidesPerView: 1,
    breakpoints: {
        320: {
            slidesPerView: 2
        },
        768: {
            slidesPerView: 4
        },
        992: {
            slidesPerView: 6
        },
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev"
    }
});
