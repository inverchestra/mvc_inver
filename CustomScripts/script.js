var imgorgH, imgorgW;
var modalON = false;
var imgTitle;

$(document).ready(function () {

    $(window).resize(function () {
        $('.loader').css({
            'top': ($(window).height() - $('.loader').outerHeight()) / 2 + $(document).scrollTop(),
            'left': ($(window).width() - $('.loader').outerWidth()) / 2 + $(document).scrollLeft()
        });

        if (modalON) {
            $('.modal').css({
                'top': ($(window).height() - $('.modal').outerHeight()) / 2 + $(document).scrollTop(),
                'left': ($(window).width() - $('.modal').outerWidth()) / 2 + $(document).scrollLeft()
            });
        }
    });

    $(".image").click(function () {

        var clickedImg = $(this).find('img');
        imgTitle = $(this).next().text();

        var imgName = "/CommentImages/" + clickedImg.attr("alt");

        $(".loader").show();
        $(window).resize();

        if ($('.enlarged-img').attr('src')) { //if <img> has src attr remove it
            $('.enlarged-img').removeAttr('src');
        }
        $(".enlarged-img").attr("src", imgName);

        return false;
    });

    $(".enlarged-img").load(function () {

        $(".loader").hide();

        modalOpen(500);
        positionImg();

        var w, h;
        w = $(this).width();
        h = $(this).height();

        $('.modal .image-title').text(imgTitle);
        $(".modal").css({ 'height': h, 'width': w });
        $(window).resize();

    }).error(function () {
        /*alert("Can't Load image.");*/
    });

    function realSize() {
        var orgImg = $('.enlarged-img');
        var newImg = new Image();
        newImg.src = orgImg.attr('src');
        imgorgH = newImg.height;
        imgorgW = newImg.width;
    }

    function positionImg() {
        var windowH, windowW, minHdiff, minVdiff, diffH, diffV;
        var newW, newH;

        //        realSize();

        //        windowW = $(window).innerWidth();
        //        windowH = $(window).innerHeight();

        //        minHdiff = (windowW / 10) * 2;
        //        minVdiff = (windowH / 10) * 2;

        //        diffH = windowW - imgorgW;
        //        diffV = windowH - imgorgH;

        ////        calculate new width
        //        if ((windowW - minHdiff) >= imgorgW)
        //            newW = imgorgW;
        //        else
        //            newW = 900;


        //        if ((windowH - minVdiff) >= imgorgH)
        //            newH = imgorgH;
        //        else
        //            newH = 800;

        //        $(".enlarged-img").css({ "width": "auto", "height": "auto" });
        //        //auto adjust width or height if other changes in below code

        //        if (diffH <= minHdiff && diffV <= minVdiff) {
        //            if (diffH <= diffV)
        //                $(".enlarged-img").css("width", newW);
        //            else
        //                $(".enlarged-img").css("height", newH);
        //        }
        //        else if (diffH <= minHdiff) //recalculate image width
        //        {
        //            $(".enlarged-img").css("width", newW);
        //        }
        //        else if (diffV <= minVdiff) //recalculate image height
        //        {
        //            $(".enlarged-img").css("height", newH);
        //        }

    }

    $(".modal").hover(function () {
        $(".close").stop(true, true).fadeIn('fast');
    }, function () {
        $(".close").stop(true, true).fadeOut('fast');
    });

    $('.modal-bg').click(function () {
        modalClose(400);
    });

    $('.close').click(function () {
        modalClose(400);
        return false;
    });

    function modalOpen(delay) {
        modalON = true;
        $('.modal-bg').stop().fadeIn(delay / 2);
        $('.modal').css({ 'opacity': 0, 'display': 'block' });
        $('.modal').stop().delay(delay / 2).animate({ 'opacity': 1 }, delay);
    }

    function modalClose(delay) {
        $('.modal').stop().fadeOut(delay / 2);
        $('.modal-bg').stop().delay(delay / 2).fadeOut(delay, function () {
            $('.modal').removeAttr('style');
            $('.modal-bg').removeAttr('style');
            modalON = false;
        });
    }
});