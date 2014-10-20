(function ($) {

    $.fn.tipnav = function () {
        var rightnav = $(this).find('div.tipnav').find('a:nth-child(2)'),
            leftnav = $(this).find('div.tipnav').find('a:nth-child(1)'),
            spanweek = $(this).find('span[data-sp="week"]'),
            spanday = $(this).find('span[data-sp="day"]'),
            spantitle = $(this).find('span[data-sp="title"]'),
            spand = $(this).find('span[data-sp="d"]'),
            ul = $(this).find('ul');

        var cweek = parseInt($(this).find('input[data-rel="week"]').val(), 10),
            cday = parseInt($(this).find('input[data-rel="day"]').val(), 10);

        var ckweek = tweek = cweek,
            tday = cday;

        var getTips = function (w, d) {
            $.get('/tips/weektips', { week: w, day: d }, function (data) {

                spanweek.text(data.Week); spanday.text(data.Day);
                spantitle.text(data.Title); spand.show();

                ul.empty();
                $.each(data.STip, function (index, obj) {
                    var li = $('<li/>');
                    li.html(obj);
                    ul.append(li);
                });

            }, 'json');
        }

        var getWeekTips = function (w) {
            $.get('/tips/PrevWeekTip', { week: w }, function (data) {

                spanweek.text(data.Week);
                spantitle.text((data.Title == null || data.Title == "") ? "" : data.Title);
                spand.hide();

                ul.empty();
                $.each(data.STip, function (index, obj) {
                    var li = $('<li/>');
                    li.html(obj);
                    ul.append(li);
                });

            }, 'json');
        }

        rightnav.on('click', function () {

            if (tweek == cweek && tday <= cday) {
                tday += 1;
                tday = tday > cday ? cday : tday;

                getTips(tweek, tday);
            } else {
                tweek += 1;
                (tweek == cweek) ? getTips(tweek, 1) : getWeekTips(tweek);
            }

            (tweek == cweek && tday == cday) ? rightnav.fadeOut(500) : rightnav.fadeIn(500);
            (tweek == 1) ? leftnav.fadeOut(500) : leftnav.fadeIn(500);

        });

        leftnav.on('click', function () {

            if (tweek == cweek && tday > 1) {
                tday -= 1;
                tday = tday == 0 ? 1 : tday;

                getTips(tweek, tday);
            } else {
                tweek -= 1;
                tweek = tweek == 0 ? 1 : tweek;
                getWeekTips(tweek);
            }
            
            (tweek <= cweek || tday < cday) ? rightnav.fadeIn(500) : rightnav.fadeOut(500);
            (tweek == 1) ? leftnav.fadeOut(500) : leftnav.fadeIn(500);

        });

    };

    $(document).ready(function () {
        $('#tipcontainer').tipnav();
    });

}(jQuery));