

$(function () {
    $('img[name="HandDinfo"]').click(function () {
        alert("clicking Hospital and Doctors view ");
        var CaseId = $('input[name="CaseId"]').val();
        $.ajax({
            url: "/iHealthUser/Cases/HandDinfo",
            type: "GET",
            async: false,
            cache: false,
            data: { CaseId: CaseId },
            success: function (result) {
                $('#modal-div').html(result).modal({

                    onShow: function (e) {

                    }
                });
            }
        });
    });

    $("#imgadd").click(function () {
        //var msId = $('#MedicalScheduleId').val();
        //var msId = $('input[name="MedicalScheduleId"]').val();
        var caseId2 = $(this).parents('li').find('input[name=CaseId2]').val();
        var mSheduleId = $(this).parents('li').find('input[name="MedicalScheduleId"]').val();
        $.ajax({
            url: "/iHealthUser/Cases/ScheduleInfo",
            type: "GET",
            async: false,
            cache: false,
            data: { mSheduleId: mSheduleId },
            success: function (result) {
                $('#modal-div').html(result).modal({
                    onShow: function (e) {

                    }
                });
            }
        });
    });
    //added by jagadeesh
    $('#printcase').click(function () {
        var CaseID = $('#caseid').val();
        $.ajax({
            url: '/iHealthUser/Cases/PrintSelection',
            type: 'GET',
            async: false,
            cache: false,
            data: { CaseID: CaseID },
            success: function (result) {
                $('#modal-div').html(result).modal({
                });
            }
        });
    });
    //end

    return false;
});
