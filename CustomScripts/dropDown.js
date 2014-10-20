//for case
var value = $('#txtlstcases').val();
if (value != null) {
    ChechedText = value;
}
$('input[type=checkbox]').live('click', function () {
    $('#txtlstcases').val('');
    var checkedVal = $(this).val();
     
    var checkedFlag = $(this).attr('checked');
    if (checkedFlag) {
        if (ChechedText != null)
            ChechedText = ChechedText + ", " + checkedVal;
        else
            ChechedText = checkedVal;
    }
    else {
        ChechedText = null;
        var values = new Array();
        $.each($("input[name='chkbox']:checked"), function () {
            if (ChechedText != null) {
                ChechedText = ChechedText + ", " + $(this).val();
                 
            }
            else {
                ChechedText = $(this).val();
                 
            }

        });

    }

    $('#txtlstcases').val(ChechedText);
});

//end of case

///for log
var logvalue = $('#txtlstLogs').val();
if (logvalue != null) {
    ChechedText = logvalue;
}
$('input[type=checkbox]').live('click', function () {
    $('#txtlstLogs').val('');
    var checkedVal = $(this).val();
    
    var checkedFlag = $(this).attr('checked');
    if (checkedFlag) {
        if (ChechedText != null)
            ChechedText = ChechedText + ", " + checkedVal;
        else
            ChechedText = checkedVal;
    }
    else {
        ChechedText = null;
        var values = new Array();
        $.each($("input[name='chkbox']:checked"), function () {
            if (ChechedText != null) {
                ChechedText = ChechedText + ", " + $(this).val();
                 
            }
            else {
                ChechedText = $(this).val();
                 
            }

        });

    }

    $('#txtlstLogs').val(ChechedText);
});

///end of log