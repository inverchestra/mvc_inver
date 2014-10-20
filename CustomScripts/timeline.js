(function ($) {

    $.fn.timeline = function () {

        //get all the elements of the control
        var tlweeks = $('div#tlweeks'),
             weeks = $('ul#weeks'),
             weekslist = weeks.find('li'),
             weekslistanchor = $('ul#weeks a'),
             days = $('#days'),
             dayslist = days.find('li'),
             dayslistanchor = $('#days>li>a'),
             leftcap = $('div.leftcap'),
             rightcap = $('div.rightcap'),
             weekhtm = $('#currentweek'),
             dayhtm = $('#weekcurrentday'),
             trimesterheader = $('div.trimester>h4'),
             postdiv = $('#share'),
             alertdiv = $('div.alert'),
             alertspan = alertdiv.find('span'),
             image = $('img.week-pic');


        //get week and day
        var week = parseInt(weekhtm.val()),
         day = parseInt(dayhtm.val());
        var cweek = week,
        tempday = day,
        cday = day;

        console.log(tempday + " : " + cday);

        //set and intializing variables
        var margin = 0.00,
                 x = 0.00,
                 d = tlweeks.width(),
                 lix = $('ul#weeks>li').eq(0).width(),
                 error = (75 / 19),
                 max = (d / 2) - (lix / 2) + error,
                 min = (d / 2) - ((40 + 0.5) * lix) - (40 * error),
                 interval;

        var trimester = function (offset) {
            switch (true) {
                case offset >= (d / 2) - ((12 + 0.5) * lix) - (12 * error):
                    {
                        trimesterheader.text("First Trimester");
                        break;
                    }
                case offset <= (d / 2) - ((13 + 0.5) * lix) - (13 * error) && offset >= (d / 2) - ((27 + 0.5) * lix) - (27 * error):
                    {
                        trimesterheader.text("Second Trimester");
                        break;
                    }
                case offset <= (d / 2) - ((28 + 0.5) * lix) - (28 * error):
                    {
                        trimesterheader.text("Third Trimester");
                        break;
                    }
            }
        };

        var alertfn = function (message) {
            alertdiv.hide();
            alertspan.html(message);
            alertdiv.fadeIn(1000, function () { $(this).delay(3000).fadeOut(1000); });
        }

        //calculate margin based on week value
        margin = ((week - 1) == 0 ? ((d / 2) - (lix / 2)) : ((d / 2) - ((week - 0.5) * lix)) - ((week - 1) * error));

        //set the margin
        margin = (margin > max ? max : margin);
        margin = (margin <= min ? min + lix : margin);

        //apply css for the initial data
        weeks.animate({ 'margin-left': margin + 'px' }, 1000);
        weekslist.eq(week - 1).find('a').addClass('pink pink-pale');
        dayslist.eq(day - 1).find('a').addClass('green');
        image.attr('src', "https://www.bumpdocs.com/weeks/" + cweek + ".jpg");

        //set trimester
        trimester(margin);
        alertdiv.hide();

        weekslistanchor.click(function (e) {
            e.preventDefault();
            var li = $(this).parent('li'),
                 l = li.width(),
                 index = li.index();

            if (index > cweek - 1) {
                alertfn("<strong>Week Warning:</strong> You cannot enter the future data now !");
                weekslistanchor.eq(week - 1).trigger('click');
                weekoffset = weekslistanchor.eq(week - 1).offset().left;
                trimester(weekoffset);
                return;
            }

            var data = $(this).html();
            document.getElementById('prevweek').value = data;
            $('#contentposts').empty();
            $('div#contentposts').load('/iHealthUser/Medwall/ShowPosts?data=' + encodeURIComponent($('#prevweek').val()));
            

            weekhtm.val($(this).attr('data-rel'));
            week = parseInt($(this).attr('data-rel'));
            image.attr('src', "https://www.bumpdocs.com/weeks/" + (week) + ".jpg");

            if (week < cweek) {
                cday = 7;
                postdiv.hide();
                dayslistanchor.eq(cday - 1).trigger('click');
            } else {
                cday = tempday;
                postdiv.show();
                dayslistanchor.eq(tempday - 1).trigger('click');
            }

            li.find('a').addClass('pink');
            li.siblings().find('a').removeClass('pink');
            if (index > 0) {
                x = -(d / 2) + ((index + 0.5) * l);
                margin = -x - (index * error);

                trimester(margin);

                weeks.animate({ 'margin-left': margin + 'px' }, 1000);
            } else {
                x = (d / 2) - (l / 2) + (index * error);
                margin = x;

                trimester(margin);

                weeks.animate({ 'margin-left': x + 'px' }, 1000);
            }
        });

        tlweeks.mousewheel(function (event, delta) {
            event.preventDefault();
            if (margin <= max && margin >= min) {
                margin = margin + (delta > 0 ? lix : -lix);
                margin = (margin > max ? max : margin);
                margin = (margin <= min ? min + lix : margin);

                trimester(margin);

                weeks.animate({ 'margin-left': margin + 'px' }, 20);
            }
        });

        dayslistanchor.click(function (e) {
            e.preventDefault();

            var index = $(this).parent('li').index();

            if (week == cweek) {
                dayslist.eq(tempday - 1).find('a').addClass('green-pale');
            } else {
                dayslist.eq(tempday - 1).find('a').removeClass('green-pale');
            }

            if (index > cday - 1) {
                alertfn("<strong>Date Warning:</strong> You cannot enter the future data now !");
                dayslistanchor.eq(day - 1).trigger('click');
                return;
            }

            (index < cday - 1 ||( cday == 7 && week < cweek)) ? postdiv.hide() : postdiv.show();

            var data = $(this).html();
            $('#contentposts').empty();
            $('div#contentposts').load('/iHealthUser/Medwall/ShowPosts1?previous=' + encodeURIComponent($('#prevweek').val() + ',' + data));

            dayhtm.val($(this).attr('data-rel'));
            day = parseInt($(this).attr('data-rel'));

            $(this).addClass('green');
            $(this).parent('li').siblings().find('a').removeClass('green');
        });

        leftcap.hover(function (event) {
            if (event.type == 'mouseenter') {
                interval = setInterval(function () {
                    if (margin <= max) {
                        margin = margin + lix;
                        margin = (margin > max ? max : margin);

                        trimester(margin);

                        weeks.animate({ 'margin-left': margin + 'px' }, 1000, 'linear', function () { });
                    }
                }, 1000);
            } else {
                clearInterval(interval);
            }
        });

        rightcap.hover(function (event) {
            if (event.type == 'mouseenter') {
                interval = setInterval(function () {
                    if (margin >= min) {
                        margin = margin - lix;
                        margin = (margin <= min ? min + lix : margin);

                        trimester(margin);

                        weeks.animate({ 'margin-left': margin + 'px' }, 1000, 'linear', function () { });
                    }
                }, 1000);
            } else {
                clearInterval(interval);
            }
        });

        return this;

    }

} (jQuery));