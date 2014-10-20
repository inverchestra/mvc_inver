$(function () {

    var i = 0; var l = 0; var m = 0;
    var j = 0; var k = 0;

    $.ajaxSetup({
        async: false,
        cache: false
    });

    //For CaseSave

    $("#btncasesave").click(function () {
        var logIds = $('#s2').val();
        var caseIds = $('#s1').val();
        $.ajax({
            url: $('#crtCase').attr("action"),
            type: 'POST',
            async: false,
            data: $(this).parents("form#crtCase").serialize(),
            cache: false,
            success: function (name) {
                
                   
                    $("#CaseId,#CaseId1,#CaseId2,#CaseId3").val(name);
                    $(this).parents('#procs').find(":text, textarea,input[type='radio']").attr("readOnly", true);

                    $('#btncasesave').button({ disabled: true });
                    
                


            }

        });

        return false;
    });


    //For Cases>Hospital and Doctor

    $('#hsave').click(function () {
        var hitem = '<a id="#e' + k + '" style="height:16px; padding:1px 1px"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 75px; font-size:10px;">' + $(this).parents('div#handdinfod').find('input[name="HDInfos.HospitalName"]').val() + '</span></a><ul id="e' + k + '"><li><input type="text" name="HDInfos[' + k + '].HospitalName" title="Hospital Name" class="al" onfocus="this.blur();" value="' + $(this).parents('div#handdinfod').find('input[name="HDInfos.HospitalName"]').val() + '" /></li><li><input type="text" name="HDInfos[' + k + '].DoctorName" title="Hospital Name" class="al" onfocus="this.blur();" value="' + $(this).parents('div#handdinfod').find('input[name="HDInfos.DoctorName"]').val() + '" /></li></ul>';
        $('<li/>').prependTo('#hdinfo').html(hitem);
        $('#hdinfo').accordion('destroy').accordion({ collapsible: true });
        $(this).parents('div#handdinfod').hide('slide', { direction: 'down' }, 600);
        $(this).parents('div#handdinfod').find(':text').val('');
        k += 1;
    });

    //For Procedures
    $('#procs').submit(function () {
        $.ajax({
            url: '/iHealthUser/HealthRecord/CreateNewProcedure',
            type: 'POST',
            data: new FormData($(this).get(0)),
            contentType: false,
            processData: false,
            success: function (data) {
            }
        });

        var plst = '<a href="#b' + j + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px; ">' + $(this).find('input[name="Procedures.ProcedureName"]').val() + '</span></a><ul id="b' + j + '"><li><div class="tldiv" style="width: 35%"><h6>Procedures Name:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Procedures.ProcedureName"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>DateofService:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Procedures.DateOfService"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Provider(or)Facility</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Procedures.ProviderOrFacility"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Surgeon:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="Procedures.Surgeon"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Notes:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('textarea[name="Procedures.Notes"]').val() + '</span></div></li></ul>';

        $('<li/>').prependTo('#proclist').html(plst);
        $('#proclist').accordion('destroy').accordion({ collapsible: true });
        $(this).parent('div').hide('slide', { direction: 'down' }, 600);
        $(this).get(0).reset();
        j += 1;

        return false;

    })

    //Medical Tests
    $('#medtests').submit(function (e) {
        $.ajax({
            type: 'POST',
            url: '/iHealthUser/HealthRecord/CreateNewMedicalTest',
            data: new FormData($(this).get(0)),
            contentType: false,
            processData: false,
            success: function (data) {
            }
        });

        var mtlst = '<a href="#a' + i + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $(this).find('input[name="MedicalTests.TestName"]').val() + '</span></a><ul id="a' + i + '"><li><div class="tldiv" style="width: 35%"><h6>Test Name:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalTests.TestName"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Date of Test:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalTests.DateOfTest"]').val() + '</span></div></li><li><div class="tldiv" style="width: 35%"><h6>Diagnostic Center:</h6></div><div class="trdiv" style="width: 55%"><span>' + $(this).find('input[name="MedicalTests.DC"]').val() + '</span></div></li></ul>';

        $('<li/>').prependTo('#medtestlst').html(mtlst);
        $('#medtestlst').accordion('destroy').accordion({ collapsible: true });
        $(this).parent('div').hide('slide', { direction: 'down' }, 600);
        $(this).get(0).reset();
        i += 1;
        return false;
    });

    //For Medications
    //ForMs(Dil)
    $('#medform').submit(function (e) {
        $.ajax({
            type: 'POST',
            url: '/iHealthUser/HealthRecord/CreateNewMedicalSchedule',
            data: new FormData($(this).get(0)),
            contentType: false,
            processData: false,
            success: function (data) {
                
            }
        });
        return false;
    });
    //End

    $('#imgadd').click(function () {
        $('#schedule').show('slide', { direction: 'down' }, 600);
    });

    $('#schedulsave').click(function () {
        var mlist = '<a href="#d' + l + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $(this).parents('div#schedule').find('input[name="ScheduleInfo.MedicineName"]').val() + '</span></a><ul id="d' + l + '"><li><div class="tldiv" style="width: 35%"><h6>Medicine Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="ScheduleInfo[' + l + '].MedicineName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#schedule').find('input[name="ScheduleInfo.MedicineName"]').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Type of Medicine</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].TypeOfMedicne" title="Enter type of Medicine" class="al" value="' + $(this).parents('div#schedule').find('input[name="ScheduleInfo.TypeOfMedicne"]').val() + '"/></div></li><li><div class="tldiv" style="width: 35%"><h6>DosageTaken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DosageQuantity" class="al" value="' + $(this).parents('div#schedule').find('input[name="ScheduleInfo.DosageQuantity"]').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Frequency Taken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].FrequencyTaken" class="al" value="' + $(this).parents('div#schedule').find('input[name="ScheduleInfo.FrequencyTaken"]').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Quantity</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].Quantity" class="al" value="' + $(this).parents('div#schedule').find('input[name="ScheduleInfo.Quantity"]').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Days Supply</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DaysSupply" class="al" value="' + $(this).parents('div#schedule').find('input[name="ScheduleInfo.DaysSupply"]').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Notes</h6></div><div class="trdiv" style="width: 55%"><textarea onfocus="this.blur();" name="ScheduleInfo[' + l + '].Notes" cols="15" rows="7" class="al" style="height: 40px">' + $(this).parents('div#schedule').find('textarea[name="ScheduleInfo.Notes"]').val() + '</textarea></div></li></ul>';

        $('<li/>').prependTo('#medsc').html(mlist);
        $('#medsc').accordion('destroy').accordion({ collapsible: true });
        $(this).parents('div#schedule').hide('slide', { direction: 'down' }, 600);
        $(this).parents('div#schedule').find(':text').val('');
        l += 1;
        return false;
    });

    //For Visits

    $('#visitdiv').click(function () {
        $('#visit').show('slide', { direction: 'down' }, 600);
    });


    $('#visits').submit(function (e) {

        $.ajax({
            type: 'POST',
            url: '/iHealthUser/HealthRecord/CreateNewVisit',
            data: new FormData($(this).get(0)),
            contentType: false,
            processData: false,
            success: function (data) {
            }
        });


        var g = $(this).find(':checkbox:checked').val();

        if (g == 'true' || g == true)
            var v = 'Yes';
        else
            var v = 'No';

        var vlist = '<a href="#m' + m + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $(this).find('input[name="lstVisits[0].Reason"]').val() + '</span></a> <ul id="m' + m + '" ><li><div class="tldiv" style="width: 35%"><h6>Visit date</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="lstVisits[0].VisitDate" onfocus="this.blur();" value="' + $(this).find('input[name="lstVisits[0].VisitDate"]').val() + '" class="al" /></div></li> <li><div class="tldiv" style="width: 35%"><h6>Reason</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="lstVisits[0].Reason" onfocus="this.blur();" value="' + $(this).find('input[name="lstVisits[0].Reason"]').val() + '" class="al" /></div></li><li><div class="tldiv" style="width: 35%"><h6> Provider(or)Facility</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="lstVisits[0].ProviderFacility" class="al" onfocus="this.blur();" value="' + $(this).find('input[name="lstVisits[0].ProviderFacility"]').val() + '" /></div></li><li><div class="tldiv" style="width:55%"><h6>Send Notification</h6></div><input type="text" name="IsNotify[0].IsNotify" value="' + v + '" class="al" onfocus="this.blur();"/></li></ul>';
        $('<li/>').prependTo('#viewdiv').html(vlist);
        $('#viewdiv').accordion('destroy').accordion({ collapsible: true });
        $('#visit').hide('slide', { direction: 'down' }, 600);
        $(this).get(0).reset();
        m += 1;
        return false;
    });
});
