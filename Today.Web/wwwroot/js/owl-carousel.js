$(".Parent-child-carousel>.owl-carousel").owlCarousel({
    loop: false,
    margin: 10,
    stagePadding: 32,

    responsive:
    {
        0: {
            items: 1
        },
        768: {
            items: 2
        },
        992: {
            items: 4
        }
    }
});
