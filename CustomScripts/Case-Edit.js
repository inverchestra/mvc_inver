$(function () {
    var i = 0;
    var s;
    var n = 0;
    var l = 0;


    //-----------------------------------------------------------------------
    //Added By Sandeep for Add in Edit

    //EditView Procedure When click on add button
    $('#AddProcedurediv').click(function () {
        $('#AddProceduressubdiv').show('slide', { direction: 'down' }, 600);
    });

    //EditView MedicalTest When click on Add Button
    $('#AddMedicalTestdiv').click(function () {
        $('#AddMedicalTestsubdiv').show('slide', { direction: 'down' }, 600);
    });

    //EditView Medication When click on Add Button
    $('#AddMedicationdiv').click(function () {
        $('#AddMedicationsubdiv').show('slide', { direction: 'down' }, 600);
    });


    //EditView visisting When click on add button
    $('#Addvisitdiv').click(function () {
        $('#Addvisitsubdiv').show('slide', { direction: 'down' }, 600);
    });

    //EditView MedicationSchedule when click on + Button
    $('#AddMedicationscheduleaddbtn').click(function () {
        $('#AddMedicationsubdivschedule').show('slide', { direction: 'down' }, 600);
    });


    //EditViewCase Add Visits
    $("#frmEditViewAddVisitsdiv").submit(function () {
        $.ajax({
            url: $(this).attr("action"),
            type: "POST",
            data: $(this).serialize(),
            success: function (CaseId) {
                if (CaseId == "1010") {
                    alert("Success");
                }

                else
                    alert("Failed");
            }
        });

        var visitslst = '<a href="#a' + i + '"><span style="text-overflow: ellipsis; white-space: nowrap;overflow: hidden; display: inline-block; width: 350px">' + $(this).find('input[name="Visits.VisitDate"]').val() + '</span></a><ul id="a' + i + '"><li><div class="tldiv" style="width: 35%"><h6>Provider Facility:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Visits.ProviderFacility"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Visit Date:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Visits.VisitDate1"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Reason:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Visits.Reason"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Visits.Documents"]').val() + '</span></div></li></ul>';

        $('<li/>').prependTo('#visitdi').html(visitslst);
        $('#visitdi').accordion('destroy').accordion({ collapsible: true });
        $(this).parent('div').hide('slide', { direction: 'down' }, 600);
        $(this).get(0).reset();
        i += 1;
        return false;
    });


    //EditViewCase Add Procedures
    $("#frmEditViewAddProcedurediv").submit(function () {
        $.ajax({
            url: $(this).attr("action"),
            type: "POST",
            data: $(this).serialize(),
            success: function (CaseId) {
                if (CaseId != 0) {
                    alert("Success");

                }
                else
                    alert("Failed");
            }
        });

        var procedurelst = '<a href="#a' + i + '"><span style="text-overflow: ellipsis; white-space: nowrap;overflow: hidden; display: inline-block; width: 350px">' + $(this).find('input[name="Procedures.ProcedureName"]').val() + '</span></a><ul id="a' + i + '"><li><div class="tldiv" style="width: 35%"><h6>Procedure Name:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Procedures.ProcedureName"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date of Service:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Procedures.DateOfService"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Provider or Facility:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Procedures.ProviderOrFacility"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Surgeon:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Procedures.Surgeon"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Notes:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('textarea[name="Procedures.Notes"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Procedures.Documents"]').val() + '</span></div></li></ul>';

        $('<li/>').prependTo('#Procedurediv').html(procedurelst);
        $('#Procedurediv').accordion('destroy').accordion({ collapsible: true });
        $(this).parent('div').hide('slide', { direction: 'down' }, 600);
        $(this).get(0).reset();
        i += 1;
        return false;
    });


    //EditViewCase Add MedicalTest
    $('#frmEditViewAddMedicalTestdiv').submit(function () {
        $.ajax({
            url: "/iHealthUser/HealthRecord/AddMedicalTest",
            type: "POST",
            data: $(this).serialize(),
            success: function (Result) {
                if (Result != 0) {
                    alert("success");
                }
                else
                    alert("Failed");
            }
        });

        var MedicalTestlst = '<a href="#a' + i + '"><span style="text-overflow: ellipsis; white-space: nowrap;overflow: hidden; display: inline-block; width: 350px">' + $(this).find('input[name="MedicalTests.TestName"]').val() + '</span></a><ul id="a' + i + '"><li><div class="tldiv" style="width: 35%"><h6>Test Name:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalTests.TestName"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date of Test:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalTests.DateOfTest"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalTests.Documents"]').val() + '</span></div></li></ul>';

        $('<li/>').prependTo('#MedicalTestdiv').html(MedicalTestlst);
        $('#MedicalTestdiv').accordion('destroy').accordion({ collapsible: true });
        $(this).parents('div.slide').hide('slide', { direction: 'down' }, 600);
        $(this).get(0).reset();
        i += 1;
        return false;
    });


    //EditViewCase Add Medications
    $("#frmEditViewAddMedicationdiv").submit(function () {
        $.ajax({
            url: "/iHealthUser/HealthRecord/AddMedicalSchduleTest",
            type: "POST",
            data: $(this).serialize(),
            success: function (CaseId) {
                if (CaseId != 0) {
                    alert("Success");
                    var medicalschedulelst = '<a href="#a' + i + '"><span style="text-overflow: ellipsis; white-space: nowrap;overflow: hidden; display: inline-block; width: 350px">' + $(this).find('input[name="MedicalSchdule.DatePrescribed"]').val() + '</span></a><ul id="a' + i + '"><li><div class="tldiv" style="width: 35%"><h6>Doctor Name:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalSchdule.DoctorName"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date Prescribed:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalSchdule.DatePrescribed"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date Dispensed:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalSchdule.Dispensed"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Pharmacy:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalSchdule.Pharmacy"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Reason:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalSchdule.Reason]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Send Notification:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalSchdule.SendNotification"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalSchdule.Documents"]').val() + '</span></div></li></ul>';
                    $('<li/>').prependTo('#MedicalSchedulediv').html(medicalschedulelst);
                    $('#MedicalSchedulediv').accordion('destroy').accordion({ collapsible: true });

                    i += 1;

                }
            }
        });
        $(this).get(0).reset();
        $(this).parents('div.slide').hide('slide', { direction: 'down' }, 600);


        return false;

    });



    //EditViewCase Add MedicationSchedule
    $('#BtnAddMedicationschedulsave').click(function () {

        var result = $("#FrequencyTaken").val();
        // var mlist = '<a href="#d' + l + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $(this).parents('div#AddMedicationsubdivschedule').find('input[name="ScheduleInfo.MedicineName"]').val() + '</span></a><ul id="d' + l + '"><li><div class="tldiv" style="width: 35%"><h6>Medicine Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="ScheduleInfo[' + l + '].MedicineName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('input[name="ScheduleInfo.MedicineName"]').val() + '" /></div></li>     <li><div class="tldiv" style="width: 35%"><h6>Type of Medicine</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].TypeOfMedicne" title="Enter type of Medicine" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('input[name="ScheduleInfo.TypeOfMedicine"]').val() + '"/></div></li>  <li><div class="tldiv" style="width: 35%"><h6>DosageTaken</h6></div><div class="trdiv" style="width: 55%"><input type="select" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DosageQuantity" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('#DosageTaken').val() + "," + $(this).parents('div#AddMedicationsubdivschedule').find('#DosageType').val() + '" /></div></li>   <li><div class="tldiv" style="width: 35%"><h6>Frequency Taken</h6></div><div class="trdiv" style="width: 55%"><input type="select" onfocus="this.blur();" name="ScheduleInfo.FrequencyTaken" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('#FrequencyTaken').val() + '" /></div></li>     <li><div class="tldiv" style="width: 35%"><h6>Quantity</h6></div><div class="trdiv" style="width: 55%"><input type="select" onfocus="this.blur();" name="ScheduleInfo[' + l + '].Quantity" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('#QuantityTime option:selected').text() + '" /></div></li>    <li><div class="tldiv" style="width: 35%"><h6> Days Supply</h6></div><div class="trdiv" style="width: 55%"><input type="select" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DaysSupply" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('#DaysSupply').text() + '" /></div></li>     <li><div class="tldiv" style="width: 35%"><h6>Notes</h6></div><div class="trdiv" style="width: 55%"><textarea onfocus="this.blur();" name="ScheduleInfo[' + l + ']." cols="15" rows="7" class="al" style="height: 40px">' + $(this).parents('div#AddMedicationsubdivschedule').find('textarea[name="ScheduleInfo.Notes"]').val() + '</textarea></div></li></ul>';

        var mlist = '<a href="#d' + l + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $(this).parents('div#AddMedicationsubdivschedule').find('input[name="MedicalSchdule.MedicineName"]').val() + '</span></a><ul id="d' + l + '"><li><div class="tldiv" style="width: 35%"><h6>Medicine Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="MedicalSchdule.MedicineName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('input[name="MedicalSchdule.MedicineName"]').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Type of Medicine</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].TypeOfMedicne" title="Enter type of Medicine" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('input[name="MedicalSchdule.MedicineName"]').val() + '"/></div></li><li><div class="tldiv" style="width: 35%"><h6>DosageTaken</h6></div><div class="trdiv" style="width: 55%"><input type="select" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DosageQuantity" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('#DosageTaken').val() + "," + $(this).parents('div#AddMedicationsubdivschedule').find('#DosageType').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Frequency Taken</h6></div><div class="trdiv" style="width: 55%"><input type="select" onfocus="this.blur();" name="ScheduleInfo.FrequencyTaken" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('#FrequencyTaken').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Quantity</h6></div><div class="trdiv" style="width: 55%"><input type="select" onfocus="this.blur();" name="ScheduleInfo[' + l + '].Quantity" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('#QuantityTime option:selected').text() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6> Days Supply</h6></div><div class="trdiv" style="width: 55%"><input type="select" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DaysSupply" class="al" value="' + $(this).parents('div#AddMedicationsubdivschedule').find('#DaysSupply option:selected').text() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Notes</h6></div><div class="trdiv" style="width: 55%"><textarea onfocus="this.blur();" name="ScheduleInfo[' + l + '].Notes" cols="15" rows="7" class="al" style="height: 40px">' + $(this).parents('div#AddMedicationsubdivschedule').find('textarea[name="MedicalSchdule.Notes"]').val() + '</textarea></div></li></ul>';


        $('<li/>').prependTo('#medsc').html(mlist);
        $('#medsc').accordion('destroy').accordion({ collapsible: true });
        $(this).parents('div#AddMedicationsubdivschedule').hide('slide', { direction: 'down' }, 600);
        $(this).parents('div#AddMedicationsubdivschedule').find(':text').val('');
        l += 1;
        return false;
    });


    //End by Sandeep for Add in Edit
    //-----------------------------------------------------------------------

    //For Update in Edit

    //By Kumar
    $("#btnUpDateCase").click(function () {
        $.ajax({
            url: "/iHealthUser/HealthRecord/UpDateCaseInfo",
            type: 'POST',
            async: false,
            data: $(this).parents("form").serialize(),
            cache: false,
            success: function (success) {
                if (success == "1020") {

                    location.reload();

                }
                else {
                    alert("Update failed");
                    location.reload();
                }
            }
        });
        return false;
    });

    //Kumar
    //Venkateswari

    $("img.editCaseProcedureImgBtn").click(function (e) {
        e.stopPropagation();
        var procId = $(this).parents('li').find('input[name="ProcedureId"]').val();
        s = $(this).parents('li');

        $.ajax({
            url: "/iHealthUser/HealthRecord/GetProcedure",
            type: 'GET',
            data: { procId: procId },
            success: function (result) {
                $('#cid2').val(result.CaseId);
                $('#pid2').val(result.ProcedureId);
                $("#EditPName").val(result.ProcedureName);
                $("#EditPDate").val(result.dateofservice1);
                $("#EditPProvider").val(result.ProviderOrFacility);
                $("#EditPSurgieon").val(result.Surgeon);
                $("#EditPNotes").val(result.Notes);
            }
        });
        $('#UpdateProcedures').show('slide', { direction: 'down' }, 600);
        return false; //Dileep
    });


    $('input[name="btnProcedureUpdate"]').click(function (e) {

        var obj = $(this).parents('li');
        var procId2 = $(this).parents('li').find('#pid2').val();
        var mc2 = $(this).parents('li').find('#cid2').val();
        // var procid2= $(this).find('input[name="ProcedureId2"]').val();
        $.ajax({
            url: "/iHealthUser/HealthRecord/UpdateProcedure",
            type: 'POST',
            async: false,
            cache: false,
            // dataType: 'json',
            data: new FormData($('#frmUprocedureId').get(0)),
            contentType: false,
            processData: false,
            success: function (success) {
                if (success == '1020') {
                    //location.reload();

                    alert("Updated Successfully");
                    $(s).remove();
                    var procedurelst = '<a href="#a' + n + '"><span style="text-overflow: ellipsis; white-space: nowrap;overflow: hidden; display: inline-block; width: 350px">' + $('#frmUprocedureId').find('input[name="lstProcedure[0].ProcedureName"]').val() + '</span></a><ul id="a' + n + '"><li><div class="tldiv" style="width: 35%"><h6>Procedure Name:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmUprocedureId').find('input[name="lstProcedure[0].ProcedureName"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date of Service:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmUprocedureId').find('input[name="lstProcedure[0].DateOfService"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Provider or Facility:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmUprocedureId').find('input[name="lstProcedure[0].ProviderOrFacility"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Surgeon:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmUprocedureId').find('input[name="lstProcedure[0].Surgeon"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Notes:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmUprocedureId').find('textarea[name="lstProcedure[0].Notes"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmUprocedureId').find('input[name="Procedures.Documents"]').val() + '</span></div></li></ul>';

                    $('<li/>').prependTo('#Procedurediv').html(procedurelst);
                    $('#Procedurediv').accordion('destroy').accordion({ collapsible: true });
                    $('#UpdateProcedures').hide('slide', { direction: 'down' }, 600);

                    n += 1;



                }
                else {
                    alert("Not Updated, Error in Server Code");
                }
            }
        });
        $('#frmUprocedureId').get(0).reset();
        return false;
    });


    //MedicalTestEdit
    $("img[name='editCaseMtImgBtn']").click(function (e) {
        e.stopPropagation();
        var mtId = $(this).parents('li').find('input[name="MedicalTestId"]').val();
        var mc2 = $(this).parents('li').find('#mcid2').val();
        s = $(this).parents('li');
        $.ajax({
            url: "/iHealthUser/HealthRecord/GetMedicalTest",
            type: 'GET',
            data: { mtId: mtId },
            success: function (result) {
                $('#mcid2').val(result.CaseId);
                $('#mtid2').val(result.MedicalTestId);
                $("#EditMTName").val(result.TestName);
                $("#EditMTDate").val(result.DateOfTest1);
            }
        });
        $('#UpdateMedicalTest').show('slide', { direction: 'down' }, 600);
        return false; //Dileep
    });



    $('input[name="btnMTUpdate"]').click(function (e) {

        var mtId2 = $(this).parents('li').find('#mtid2').val();
        var mc2 = $(this).parents('li').find('#mcid2').val();
        $.ajax({
            url: "/iHealthUser/HealthRecord/UpdateMedicalTest",
            type: 'POST',
            async: false,
            cache: false,
            // dataType: 'json',
            data: new FormData($('#frmMT').get(0)),
            contentType: false,
            processData: false,
            success: function (success) {
                if (success == '1020') {
                    //location.reload();
                    alert("Updated Successfully");
                    $(s).remove();
                    //var mlst = '<a href="#a' + n + '"><span style="text-overflow: ellipsis; white-space: nowrap;overflow: hidden; display: inline-block; width: 350px">' + $('#frmMT').find('input[name="lstMedicalTest[0].TestName"]').val() + '</span></a><ul id="a' + n + '"><li><div class="tldiv" style="width: 35%"><h6>MedicalTest Name:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMT').find('input[name="lstMedicalTest[0].TestName"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date of Test:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMT').find('input[name="lstMedicalTest[0].DateOfTest"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMT').find('input[name="MTfiles"]').val() + '</span></div></li></ul>';
                    var mlst = '<a href="#a' + n + '"><span style="text-overflow: ellipsis; white-space: nowrap;overflow: hidden; display: inline-block; width: 350px">' + $('#frmMT').find('input[name="lstMedicalTest[0].TestName"]').val() + '</span></a><ul id="a' + n + '"><li><div class="tldiv" style="width: 35%"><h6>MedicalTest Name:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMT').find('input[name="lstMedicalTest[0].TestName"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date of Test:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMT').find('input[name="lstMedicalTest[0].DateOfTest"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMT').find('input[name="MTfiles"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $.each($(this).find('input[name="medicalTest.lstDocuments"]'), function (I, obj) { '<a href= "/iHealthUser/HealthRecord/ViewDocument"?id =' + obj.Id + '>' + obj.OriginalName + '</a>' }); +'</span></div></li></ul>';


                    $('<li/>').prependTo('#MedicalTestdiv').html(mlst);
                    $('#MedicalTestdiv').accordion('destroy').accordion({ collapsible: true });
                    $('#UpdateMedicalTest').hide('slide', { direction: 'down' }, 600);

                    n += 1;
                }
                else {
                    alert("Not Updated, Error in Server Code");
                }
            }
        });
        $('#frmMT').get(0).reset();
        return false;
    });
    // End Venkateswari


    //Prashanth

    //for MedicalSchedule Update

    $("img[name='editCaseMScheduleImgBtn']").click(function (e) {
        e.stopPropagation();
        var mSheduleId = $(this).parents('li').find('input[name="MedicalScheduleId"]').val();

        s = $(this).parents('li');

        $.ajax({
            url: "/iHealthUser/HealthRecord/GetMedicationSchedule",
            type: 'GET',
            data: { mSheduleId: mSheduleId },
            success: function (result) {

                // $("#Visitsid2").val(result.visitId);
                // $("#EditVDate").val(result.VisitDate);
                // $("#EditVReason").val(result.Reason);
                // $("#EditVProvider").val(result.ProviderFacility);
                $('#mscid2').val(result.CaseId);
                $("#datePrescribed").val(result.DatePrescribed1);
                $("#dateDispensed").val(result.DateDispensed1);
                $("#doctorsName").val(result.DoctorName);
                $("#reason").val(result.Reason);
                $("#pharmacyName").val(result.Pharmacy);
                $("#MSId").val(result.MedicalScheduleId);
            }
        });

        $('#medicationUpdate').show('slide', { direction: 'down' }, 600);
        return false; //Dileep
    });


    $('input[name="BtnMschduleUpdate"]').click(function (e) {

        var msId2 = $(this).parents('li').find('#MSId').val();
        var mc2 = $(this).parents('li').find('#mscid2').val();
        $.ajax({
            url: "/iHealthUser/HealthRecord/UpdateMedicalSchedule",
            type: 'POST',
            async: false,
            cache: false,
            data: new FormData($('#frmMedicalSheduleId2').get(0)),
            contentType: false,
            processData: false,
            success: function (success) {
                if (success == "1020") {
                    alert("Updated Successfully");

                    $(s).remove();
                    var mlst = '<a href="#a' + n + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $('#frmMedicalSheduleId2').find('input[name="lstMedicalSchedule[0].DatePrescribed"]').val() + '</span></a><ul id="a' + n + '"><li><div class="tldiv" style="width: 35%"><h6>Date Prescribed :</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMedicalSheduleId2').find('input[name="lstMedicalSchedule[0].DatePrescribed"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date Dispenced :</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMedicalSheduleId2').find('input[name="lstMedicalSchedule[0].Dispensed"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>DoctorName:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMedicalSheduleId2').find('input[name="lstMedicalSchedule[0].DoctorName"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Reason:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMedicalSheduleId2').find('input[name="lstMedicalSchedule[0].Reason"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Pharmacy:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMedicalSheduleId2').find('input[name="lstMedicalSchedule[0].Pharmacy"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmMedicalSheduleId2').find('input[name="MTfiles"]').val() + '</span></div></li></ul>';


                    $('<li/>').prependTo('#MedicalSchedulediv').html(mlst);
                    $('#MedicalSchedulediv').accordion('destroy').accordion({ collapsible: true });
                    $('#medicationUpdate').hide('slide', { direction: 'down' }, 600);

                    n += 1;

                }
                else {
                    alert("Not Updated, Error in Server Code");

                }
            }
        });
        $('#frmMedicalSheduleId2').get(0).reset();
        return false;
    });


    //for Visits UpDate

    $("img[name='editCaseVisitsImgBtn']").click(function (e) {
        e.stopPropagation();
        var visitsId = $(this).parents('li').find('input[name="visitId"]').val();
        s = $(this).parents('li');
        $.ajax({
            url: "/iHealthUser/HealthRecord/GetMyVisits",
            type: 'GET',
            data: { visitsId: visitsId },
            success: function (result) {
                $("#Visitsid2").val(result.visitId);
                $("#EditVDate").val(result.VisitDate1);
                $("#EditVReason").val(result.Reason);
                $("#EditVProvider").val(result.ProviderFacility);
            }
        });
        $('#Updatevisit').show('slide', { direction: 'down' }, 600);
        return false; //Dileep
    });


    //Visit Update button
    $('input[name="btnVisitsUpdate"]').click(function (e) {

        var visitId2 = $(this).parents('li').find('#Visitsid2').val();
        $.ajax({
            url: "/iHealthUser/HealthRecord/UpdateVisit",
            type: 'POST',
            async: false,
            cache: false,
            // dataType: 'json',
            data: new FormData($('#frmUpdateVistis').get(0)),
            contentType: false,
            processData: false,
            success: function (success) {
                if (success == '1020') {
                    //location.reload();
                    alert("Updated Successfully");
                    $(s).remove();
                    var mlst = '<a href="#a' + n + '"><span style="text-overflow: ellipsis; white-space: nowrap;overflow: hidden; display: inline-block; width: 350px">' + $('#frmUpdateVistis').find('input[name="lstVisits[0].VisitDate"]').val() + '</span></a><ul id="a' + n + '"><li><div class="tldiv" style="width: 35%"><h6>MedicalTest Name:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmUpdateVistis').find('input[name="lstVisits[0].VisitDate"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date of Test:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('frmUpdateVistis').find('input[name="lstVisits[0].Reason"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Documents:</h6></div><div class="trdiv" style="width: 55%"><span>' + $('#frmUpdateVistis').find('input[name="lstVisits[0].ProviderFacility"]').val() + '</span></div></li></ul>';

                    $('<li/>').prependTo('#visitdi').html(mlst);
                    $('#visitdi').accordion('destroy').accordion({ collapsible: true });
                    $('#Updatevisit').hide('slide', { direction: 'down' }, 600);

                    n += 1;
                }
                else {
                    alert("Not Updated, Error in Server Code");


                }
            }
        });
        $('#frmUpdateVistis').get(0).reset();
        return false;
    });

    // End Prashanth 

});