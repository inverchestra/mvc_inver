(function ($) {
    $.fn.mytab = function () {

        var lis = $('div#vitalsDiv ul li');

        var anchs = lis.find('a');

        var divs = $('div#tab-body');

        divs.children('div').hide();
        // lis.eq(0).find('a').addClass('tabaselected');
        lis.eq(0).addClass('ui-state-active');
        $('div#tabs-1').show();

        anchs.click(function () {

            // $(this).parent('li').siblings().find('a').removeClass('tabaselected');
            $(this).parent('li').siblings().removeClass('ui-state-active');
            // $(this).addClass('tabliselected');
            $(this).parent('li').addClass('ui-state-active');
            var divid = "div" + $(this).attr('data-rel');

            divs.children('div').fadeOut(200);
            $(divid).delay(200).fadeIn(500);
        });
    }

} (jQuery));
