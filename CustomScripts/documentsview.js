$(function () {
    $('a.anchor').click(function (e) {
        var DocumentId = $(this).attr('id');
        $.ajax({
            url: "/iHealthUser/Cases/ViewDocument",
            cache: false,
            type: "GET",
            data: { DocumentId: DocumentId },
            success: function (data) {
                $('#tab').html(data).modal();
            }
        });
        return false;
    });
});
