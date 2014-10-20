$(function () {

//    $.ajaxSetup({ cache: false });

    var maxRows = 10;
    var cTable = $('#tbody');
    var cRows = cTable.find('tr');
    var cRowCount = cRows.size();

    if (cRowCount <= maxRows) {
        $('#btnPrevious,#btnNext').attr("disabled", true);
        return;
    }

    var frows = cRows.filter(':gt(' + (maxRows - 1) + ')');

    frows.hide();
    $('#btnPrevious').attr("disabled", true);

    $('#btnNext').click(function () {
        
        var cFirstVisible = cRows.index(cRows.filter(':visible'));
        
        cRows.hide();

        cRows.filter(':lt(' + (cFirstVisible + 2 * maxRows) + '):gt(' + (cFirstVisible + maxRows - 1) + ')').show();
        $('table#tbody tbody tr:nth-child(2n)').hide();
        
        if (cFirstVisible + 2 * maxRows >= cRows.size()) {
            $('#btnNext').attr("disabled", true);
        }

        $('#btnPrevious').attr("disabled", false);
        return false;

    });

    $('#btnPrevious').click(function () {
        
        var cFirstVisible = cRows.index(cRows.filter(':visible'));

        cRows.hide();

        if (cFirstVisible - maxRows - 1 > 0) {
            cRows.filter(':lt(' + cFirstVisible + '):gt(' + (cFirstVisible - maxRows - 1) + ')').show();
            $('table#tbody tbody tr:nth-child(2n)').hide();
        }
        else {
            cRows.filter(':lt(' + cFirstVisible + ')').show();
            $('table#tbody tbody tr:nth-child(2n)').hide();
            $('#btnPrevious').attr("disabled", true);
        }

        $('#btnNext').attr("disabled", false);
        return false;

    });


});

$(function () {
    $('table tbody tr:nth-child(2n+1)').filter(':has(:checkbox:checked)').addClass('selected').end().click(function (event) {
        $(this).next('tr').toggle();
        $(this).toggleClass('selected');
        if (event.target.type !== 'checkbox') {
            $(':checkbox', this).attr('checked', function () {
                return !this.checked;
            });
        }
    });
});