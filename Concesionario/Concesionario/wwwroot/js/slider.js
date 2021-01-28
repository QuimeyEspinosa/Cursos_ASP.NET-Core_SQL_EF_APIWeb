window.addEventListener('load', function () {
    new Glider(document.querySelector('.slider-list'), {
        slidesToShow: 1,
        slidesToScroll: 1,
        dots: '.slider-indicators',
        arrows: {
            prev: '.slider-prev',
            next: '.slider-next'
        }
    });
})