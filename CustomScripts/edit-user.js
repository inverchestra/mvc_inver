$(function () {
    //Start of Edit

    $('#editu').click(function () {

        
        $('#txt1,#txt2,#txt3,#txt4,#btnClose, #btnUpdate').show();
        $('#txt1').val($('#lblFirstName').text());
        $('#txt2').val($('#lblLastName').text());
        $('#txt3').val($('#lblLoginName').text());
        $('#txt4').val($('#lblEmailId').text());
        $('#lblFirstName,#lblLastName,#lblLoginName,#lblEmailId,#editu').hide();
        return false;
    });

    $('#btnClose').click(function () {
        $('#txt1,#txt2,#txt3,#txt4,#btnClose, #btnUpdate').hide();
        $('#lblFirstName,#lblLastName,#lblLoginName,#lblEmailId,#editu').show();
        return false;
    });

    //End of Edit

    // start of personal account edit code added by kumar
    $('#editu1').click(function () {
     
        $('#txt11,#txt22,#txt33,#txt44,#btnClose1,#btnUpdate1').show();
        $('#txt11').val($('#lblFirstName1').text());
        $('#txt22').val($('#lblLastName1').text());
        $('#txt33').val($('#lblLoginName1').text());
        $('#txt44').val($('#lblEmailId1').text());
        $('#lblFirstName1,#lblLastName1,#lblLoginName1,#lblEmailId1,#editu1').hide();
        return false;

    });
    $('#btnClose1').click(function () {
        $('#txt11,#txt22,#txt33,#txt44,#btnClose1, #btnUpdate1').hide();
        $('#lblFirstName1,#lblLastName1,#lblLoginName1,#lblEmailId1,#editu1').show();
        return false;
    });


    //end
});