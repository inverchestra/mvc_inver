$(function () {

    $('#menucases').each(function () {

        var $active, $content, $links = $(this).find('a');

        $active = $($links.filter('[href="' + location.hash + '"]')[0] || $links[0]);
        $active.addClass('active-tab');
        $content = $($active.attr('href'));

        //Hide the remaining content
        $links.not($active).each(function () {
            $($(this).attr('href')).hide();
        });

        //Bind the click event handler
        $(this).on('click', 'a', function (e) {

            //Make old tags inactive
            $active.removeClass('active-tab');
            $content.hide();

            //Update the variables with new links and content
            $active = $(this);
            $content = $($(this).attr('href'));

            //Make the tab active
            $active.addClass('active-tab');
            $content.show();

            //prevent anchors default click actions
            e.preventDefault();

        });

    });
});
