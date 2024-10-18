$(document).ready(function () {

    $(".image-link").magnificPopup({ type: "image" });

    $(".image-popup-vertical-fit").magnificPopup({
        type: "image",
        closeOnContentClick: true,
        mainClass: "mfp-img-mobile",
        image: {
            verticalFit: true
        }

    });

    $(".image-popup-fit-width").magnificPopup({
        type: "image",
        closeOnContentClick: true,
        image: {
            verticalFit: false
        }
    });

    $(".image-popup-no-margins").magnificPopup({
        type: "image",
        closeOnContentClick: true,
        closeBtnInside: false,
        fixedContentPos: true,
        mainClass: "mfp-no-margins mfp-with-zoom",
        image: {
            verticalFit: true
        },
        zoom: {
            enabled: true,
            duration: 300
        }
    });
});
