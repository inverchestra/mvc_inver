
(function ($) {

    $(document).ready(function () {
        $.get('/tips/weektips', { week: $('#currentweek').val(), day: $('#weekcurrentday').val() }, function (data) {
            if (data != null) {
                $('#tabletips').find('p:nth-child(2)').text(data.Title);
            }
            //$('#tabletips').find('p:nth-child(3)>a').attr('href', '/ihealthuser/tips/tip/?week=' + $('#currentweek').val() + '&day=' + $('#weekcurrentday').val());
        }, 'json');

        $('#tabletips').hover(function () {
            $(this).find('p:nth-child(3)').fadeToggle(500, 'linear');
        });

        $('#tabletips p:nth-child(3) a').live('click', function (e) {
            e.preventDefault();
            var cnt = $('span[data-rel="cnt"]').text();
            if (cnt != "" && cnt != null && $.isNumeric(cnt)) {
                $.post('/ihealthuser/tips/UpdateTip', { week: $('#currentweek').val() }, function (data) {
                    location.href = '/ihealthuser/tips/tip/?week=' + $('#currentweek').val() + '&day=' + $('#weekcurrentday').val();
                }, 'json');
            } else {
                location.href = '/ihealthuser/tips/tip/?week=' + $('#currentweek').val() + '&day=' + $('#weekcurrentday').val();
            }
        });

    });

} (jQuery));

