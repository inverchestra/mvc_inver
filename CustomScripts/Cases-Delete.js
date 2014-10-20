$(function () {

    // AD Code for Delete Case Items Ends here
    $('img[name="delMTest"]').click(function (e) {
        e.stopPropagation();
        // alert("Are you sure you Want to delete ?");
        var id = $(this).parents('li').find('input[name="MedicalTestId"]').val();
        var d = $(this).parents('li');
        $.ajax({
            url: "/iHealthUser/HealthRecord/DeleteMTest",
            type: 'POST',
            async: false,
            cache: false,
            //            dataType: 'json',
            data: { id: id },
            success: function (success) {
                if (success == '1030') {
                    alert("Deleted Successfully");
                    $(d).remove();
                    $('ul.accordion').accordion('destroy').accordion({
                        collapsable: true
                    });
                }
                else {
                    alert("Not Deleted, Error in Server Code");
                }
            }
        });
        return false; //Dileep
    });


    $('img[name="delProcedure"]').click(function (e) {
        e.stopPropagation();
        // alert("Are you sure you Want to delete ?");
        var id = $(this).parents('li').find('input[name="ProcedureId"]').val(); //Modified AswinCOde By V

        var d = $(this).parents('li');
        $.ajax({
            url: "/iHealthUser/HealthRecord/DeleteProcedure",
            type: 'POST',
            async: false,
            cache: false,
            //            dataType: 'json',
            data: { id: id },
            success: function (success) {
                if (success == '1030') {
                    alert("Deleted Successfully");
                    $(d).remove();
                    $('ul.accordion').accordion('destroy').accordion({
                        collapsable: true
                    });
                }
                else {
                    alert("Not Deleted, Error in Server Code");
                }
            }

        });
        return false; //Dileep
    });


    $('img[name="delVisitsimg"]').click(function (e) {
        e.stopPropagation();
        // alert("Are you sure you Want to delete ?");
        var id = $(this).parents('li').find('input[name="delVisits"]').val();
        var d = $(this).parents('li');
//        alert(id);
        $.ajax({
            url: "/iHealthUser/HealthRecord/DeleteVisits",
            type: 'POST',
            async: false,
            cache: false,
            //            dataType: 'json',
            data: { id: id },
            success: function (success) {
                if (success == '1030') {
                    alert("Deleted Successfully");
                    $(d).remove();
                    $('ul.accordion').accordion('destroy').accordion({
                        collapsable: true
                    });
                }
                else {
                    alert("Not Deleted, Error in Server Code");
                }
            }

        });
        return false; //Dileep
    });


    $('img[name="delMedSched"]').click(function (e) {
        e.stopPropagation();
        // alert("Are you sure you Want to delete ?");
        var id = $(this).parents('li').find('input[name="MedicalScheduleId"]').val();
        var d = $(this).parents('li');
////        alert(id);
        $.ajax({
            url: "/iHealthUser/HealthRecord/DeleteMedSchedule",
            type: 'POST',
            async: false,
            cache: false,
            //            dataType: 'json',
            data: { id: id },
            success: function (success) {
                if (success == '1030') {
                    alert("Deleted Successfully");
                    $(d).remove();
                    $('ul.accordion').accordion('destroy').accordion({
                        collapsable: true
                    });
                }
                else {
                    alert("Not Deleted, Error in Server Code");
                }
            }

        });
        return false; //Dileep

    });
    // AD Code for Delete Case Items Ends here
});