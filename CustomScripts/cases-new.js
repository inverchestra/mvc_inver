//OLD Script File

$(function () {

    $.ajaxSetup({
        async: false,
        cache: false
    });

    var index = $('ul[name="HDInfos"]').length;

    $('#btnCreateProc').click(function () {
        if ($('#caseId').val() != "") {
            $.ajax({
                url: '/iHealthUser/Cases/CreateProcedures',
                type: 'GET',
                async: false,
                data: { caseId: $('#caseId').val() },
                success: function (result) {
                    $('#tab').html(result).modal({
                        closeHTML: "",
                        onShow: function () {
                            $('#dateOfService1').datepicker({
                                changeYear: true,
                                changeMonth: true
                            }); // sandeep added

                            $('input[name="cancel"]').click(function () {
                                $.modal.close();
                            });
                            $('#btnProcSave').click(function () {

                                $.ajax({
                                    url: '/iHealthUser/Cases/SaveProcedure',
                                    type: "POST",
                                    aync: false,
                                    cache: false,
                                    data: new FormData($(this).parents('#frmproc').get(0)),
                                    contentType: false,
                                    processData: false,
                                    success: function (data) {
                                        if (data != "0") {
                                            alert("Procedure information saved successfully");
                                            $('#proclst').html(data);
                                            $.modal.close();
                                        }
                                        else {
                                            alert("Creation Failed !!");
                                        }
                                    }
                                });
                                return false;
                            });
                        }
                    });
                }
            });
        }
        else {
            alert("Please Create Case First");
        }
    });

    $('#btnCreateMtest').click(function () {
        if ($('#caseId').val() != "") {
            $.ajax({
                url: '/iHealthUser/Cases/CreateMedicalTests',
                type: 'GET',
                async: false,
                data: { caseId: $('#caseId').val() },
                success: function (result) {
                    $('#tab').html(result).modal({
                        onShow: function () {

                            $('#dateOfMtest1').datepicker({
                                changeYear: true,
                                changeMonth: true
                            }); // sandeep added

                            $('input[name="cancel"]').click(function () {
                                $.modal.close();
                            });
                            $('#btnMTSave').click(function () {
                                $.ajax({
                                    url: '/iHealthUser/Cases/SaveMedicalTest',
                                    type: "POST",
                                    aync: false,
                                    data: new FormData($(this).parents('#frmmt').get(0)),
                                    contentType: false,
                                    processData: false,
                                    success: function (data) {
                                        if (data != "0") {
                                            alert("Medical test information saved successfully");
                                            $('#medtstlst').html(data);
                                            $.modal.close();
                                        }
                                        else {
                                            alert("Creation Failed !!");
                                        }
                                    }

                                });
                                return false;
                            });
                        }
                    });
                }
            });
        }
        else {
            alert("Please Create Case First");
        }
    });

    $('#btnCreateMSchedule').click(function () {
        if ($('#caseId').val() != "") {
            $.ajax({
                url: '/iHealthUser/Cases/CreateMedicalSchedules',
                type: 'GET',
                async: false,
                cache: false,
                data: { caseId: $('#caseId').val() },
                success: function (result) {
                    $('#tab').html(result).modal({
                        onShow: function () {
                            var l = 0;
                            $('#datePrescribed1').datepicker({
                                changeYear: true,
                                changeMonth: true
                            });

                            $('#dateDispensed1').datepicker({
                                changeYear: true,
                                changeMonth: true
                            });

                            $('#medsc').accordion();

                            $('input[name="cancel"]').click(function () {
                                $.modal.close();
                            });

                            $('img#imgadd').click(function () {
                                $('div#crtschedule').show('slide', { direction: 'down' }, 600);
                                alert($('#r').length);
                                if ($('#r').is(':checked')) {
                                    alert("checked");
                                    $('#txt').show();
                                    $('#frequencyTaken').hide();
                                } else {
                                    alert("not checked");
                                    $('#txt').hide();
                                    $('#frequencyTaken').show();
                                }
                            });

                            var num = 1;

                            $('#imgaddfrq').click(function () {
                                if (num < 4) {
                                    $('#texts').append('<input type="text" name="time[' + num + ']" placeholder="HH:MM" style="margin-bottom:5px;" />');
                                    num += 1;
                                    $.each($('input[name*="time"]'), function () {
                                        $(this).timepicker();
                                    });
                                } else {
                                    alert("Only four are allowed");
                                }
                            });

                            $('input[name*="time"]').timepicker();

                            $('input[name="slidecancel"]').click(function () {
                                $('div#crtschedule').hide('slide', { direction: 'down' }, 600);
                            });

                            $('#schedulsave').click(function () {
                                var x = 0;

                                if ($('#r').is(':checked')) {//no select

                                    var values = $('input[name*="time"]');
                                    var string = null;
                                    alert(values.length);
                                    $.each($('input[name*="time"]'), function (index, key) {
                                        if (string == null) {
                                            string = $(this).val();
                                        } else {
                                            string += "," + $(this).val();
                                        }
                                    });

                                    var mlist = '<a href="#d' + x + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $('#MedicineName').val() + '</span></a><ul id="d' + x + '"><li><div class="tldiv" style="width: 35%"><h6>Medicine Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="ScheduleInfo[' + x + '].MedicineName" onfocus="this.blur();" class="al" value="' + $('#MedicineName').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Type of Medicine</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + x + '].TypeOfMedicine" title="Enter type of Medicine" class="al" value="' + $('#TypeofMedicine').val() + '"/></div></li><li><div class="tldiv" style="width: 35%"><h6>DosageTaken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + x + '].DosageQuantity" class="al" value="' + $('#dosagetaken').val() + " " + $('#dtaken').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Frequency Taken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + x + '].FrequencyTaken" class="al" value="' + string + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Quantity</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + x + '].Quantity" class="al" value="' + $('#quantity').val() + '" /></div></li></ul>';
                                    $('<li/>').prependTo('#medsc').html(mlist);
                                    $('#medsc').accordion('destroy').accordion({ collapsible: true });
                                    $('div.slide').hide('slide', { direction: 'down' }, 600);
                                    x += 1;
                                } else {// select
                                    var mlist = '<a href="#d' + l + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $('#MedicineName').val() + '</span></a><ul id="d' + l + '"><li><div class="tldiv" style="width: 35%"><h6>Medicine Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="ScheduleInfo[' + l + '].MedicineName" onfocus="this.blur();" class="al" value="' + $('#MedicineName').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Type of Medicine</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].TypeOfMedicine" title="Enter type of Medicine" class="al" value="' + $('#TypeofMedicine').val() + '"/></div></li><li><div class="tldiv" style="width: 35%"><h6>DosageTaken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DosageQuantity" class="al" value="' + $('#dosagetaken').val() + " " + $('#dtaken').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Frequency Taken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].FrequencyTaken" class="al" value="' + $('#frequencyTaken').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Quantity</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].Quantity" class="al" value="' + $('#quantity').val() + '" /></div></li></ul>';
                                    $('<li/>').prependTo('#medsc').html(mlist);
                                    $('#medsc').accordion('destroy').accordion({ collapsible: true });
                                    $('div.slide').hide('slide', { direction: 'down' }, 600);
                                    l += 1;
                                }
                            });

                            $('#btnMsSave').click(function () {
                                $.ajax({
                                    url: '/iHealthUser/Cases/SaveMedicalSchedule',
                                    type: "POST",
                                    aync: false,
                                    data: new FormData($(this).parents('#frmmed').get(0)),
                                    contentType: false,
                                    processData: false,
                                    success: function (data) {
                                        if (data != "0") {
                                            alert("Medication information saved successfully");
                                            $('#mslst').html(data);
                                            $.modal.close();
                                        }
                                        else {
                                            alert("Creation Failed !!");
                                        }
                                    }
                                });
                                return false;
                            });
                        }
                    });
                }
            });
        }
        else {
            alert("Please Create Case First");
        }
    });

    $('#btnCreateVisit').click(function () {
        if ($('#caseId').val() != "") {
            $.ajax({
                url: '/iHealthUser/Cases/CreateVisits',
                type: 'GET',
                async: false,
                cache: false,
                data: { caseId: $('#caseId').val() },
                success: function (result) {
                    $('#tab').html(result).modal({
                        onShow: function () {

                            $('#cvisitDate1').datepicker({
                                changeYear: true,
                                changeMonth: true
                            }); // sandeep added

                            $('input[name="cancel"]').click(function () {
                                $.modal.close();
                            });
                            $('#btnVstSave').click(function () {
                                $.ajax({
                                    url: '/iHealthUser/Cases/SaveVisit',
                                    type: "POST",
                                    aync: false,
                                    data: new FormData($(this).parents('#frmvst').get(0)),
                                    contentType: false,
                                    processData: false,
                                    success: function (data) {
                                        if (data != "0") {
                                            alert("Your doctor visit information saved successfully");
                                            $('#vstlst').html(data);
                                            $.modal.close();
                                        }
                                        else {
                                            alert("Creation Failed !!");
                                        }
                                    }
                                });
                                return false;
                            });
                        }
                    });
                }
            });
        }
        else {
            alert("Please Create Case First");
        }
    });

    //ck code for ChartsStart
    $('#btnCreateCharts').click(function () {
        if ($('#caseId').val() != "") {
            $.ajax({
                url: '/iHealthUser/Cases/CreateCharts',
                type: 'GET',
                async: false,
                data: { caseId: $('#caseId').val() },
                success: function (result) {
                    $('#tab').html(result).modal({
                        onShow: function () {

                            $('#dateOfChart1').datepicker({
                                changeYear: true,
                                changeMonth: true
                            }); // chandrakanth added

                            $('input[name="cancel"]').click(function () {
                                $.modal.close();
                            });
                            $('#btnCTSave').click(function () {
                                $.ajax({
                                    url: '/iHealthUser/Cases/SaveChart',
                                    type: "POST",
                                    aync: false,
                                    data: new FormData($(this).parents('#frmcts').get(0)),
                                    contentType: false,
                                    processData: false,
                                    success: function (data) {
                                        if (data != "0") {
                                            alert("Charts information saved successfully");
                                            $('#chartlst').html(data);
                                            $.modal.close();
                                        }
                                        else {
                                            alert("Creation Failed !!");
                                        }
                                    }

                                });
                                return false;
                            });
                        }
                    });
                }
            });
        }
        else {
            alert("Please Create Case First");
        }
    });
    //ck code for ChartsEnd


    $('#btnCaseLogView').click(function () {
        var logIds = $('#s2').val();
        var Caseids = $('#s1').val();
        if (logIds.length > 1) {
            alert("you have selected more than one log,please select only one log");
        }
        else {
            var logId = logIds[0].toString();
            $.ajax({
                url: '/iHealthUser/HealthLog/ViewLog',
                type: 'GET',
                async: false,
                cache: false,
                data: { logId: logId },
                success: function (result) {
                    $('#tab').html(result).modal({
                        closeHTML: '',
                        escClose: true,
                        onShow: function (e) {

                            e.container.css({ 'height': 'auto', 'top': '90px' });

                            $('ul.accordion').accordion({
                                collapsible: true
                            });

                            $('#btnCancel').click(function () {
                                $.modal.close();
                            });
                        }
                    });
                }
            });

        }
    });

    $('#btnCaseView').click(function () {
        var logIds = $('#s2').val();
        var Caseids = $('#s1').val();
        if (Caseids.length > 1) {
            alert("you have selected more than one case,please select only one case");
        }
        else {
            var logId = Caseids[0].toString();
            $.ajax({
                url: '/iHealthUser/HealthRecord/ViewCase',
                type: 'GET',
                async: false,
                cache: false,
                data: { CaseId: logId },
                success: function (result) {
                    $('#tab').html(result).modal({
                        closeHTML: '',
                        escClose: true,
                        onShow: function (e) {

                            e.container.css({ 'height': 'auto', 'top': '90px' });

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

                            $('ul.accordion').accordion({
                                collapsible: true
                            });

                            $('#btnCancel').click(function () {
                                $.modal.close();
                            });
                        }
                    });
                }
            });

        }
    });

    //sandeep added new functions start here

    //EditView Add Procedures
    $('#btnAddprocdiv').click(function () {
        $.ajax({
            url: '/iHealthUser/Cases/AddProcedures',
            type: 'GET',
            async: false,

            data: { caseId: $('#CaseId').val() },
            success: function (result) {
                $('#Modal-div').html(result).modal({
                    onShow: function () {
                        CloseHTML: "",
                        $('#dateOfService3').datepicker({
                            changeYear: true,
                            changeMonth: true
                        }); // sandeep added

                        $('input[name="cancel"]').click(function () {
                            $.modal.close();
                        });
                        $('#btnProcSave').click(function () {
                            $.ajax({
                                url: '/iHealthUser/Cases/SaveEditAddProcedure',
                                type: "POST",
                                aync: false,
                                cache: false,
                                data: new FormData($(this).parents('#frmproc').get(0)),
                                contentType: false,
                                processData: false,
                                success: function (data) {
                                    if (data != "0") {
                                        alert("Procedure information saved successfully");
                                        $('#proclst').html(data);

                                        $.modal.close();
                                    }
                                    else {
                                        alert("Creation Failed !!");
                                    }
                                }
                            });
                            return false;
                        });
                    }
                });
            }
        });

    });

    //EditView Add MedicalTest
    $('#btnAddMedTestdiv').click(function () {
        $.ajax({
            url: '/iHealthUser/Cases/AddMedicalTests',
            type: 'GET',
            async: false,
            data: { caseId: $('#CaseId').val() },
            success: function (result) {
                $('#Modal-div').html(result).modal({
                    onShow: function () {

                        $('#dateOfMtest3').datepicker({
                            changeYear: true,
                            changeMonth: true
                        }); // sandeep added

                        $('input[name="cancel"]').click(function () {
                            $.modal.close();
                        });
                        $('#btnMTSave').click(function () {
                            $.ajax({
                                url: '/iHealthUser/Cases/SaveEditAddMedicalTest',
                                type: "POST",
                                aync: false,
                                data: new FormData($(this).parents('#frmmt').get(0)),
                                contentType: false,
                                processData: false,
                                success: function (data) {
                                    if (data != "0") {
                                        alert("Medical test information saved successfully");
                                        $('#mtlst').html(data);
                                        $.modal.close();
                                    }
                                    else {
                                        alert("Creation Failed !!");
                                    }
                                }

                            });
                            return false;
                        });
                    }
                });
            }
        });
    });

    //EditView Add Charts

    $('#btnAddCharttdiv').click(function () {
        $.ajax({
            url: '/iHealthUser/Cases/AddCharts',
            type: 'GET',
            async: false,
            data: { caseId: $('#CaseId').val() },
            success: function (result) {
                $('#Modal-div').html(result).modal({
                    onShow: function () {

                        $('#dateOfChart3').datepicker({
                            changeYear: true,
                            changeMonth: true
                        }); // chandrakanth added

                        $('input[name="cancel"]').click(function () {
                            $.modal.close();
                        });
                        $('#btnCTSave').click(function () {
                            $.ajax({
                                url: '/iHealthUser/Cases/SaveEditAddChart',
                                type: "POST",
                                aync: false,
                                data: new FormData($(this).parents('#frmcts').get(0)),
                                contentType: false,
                                processData: false,
                                success: function (data) {
                                    if (data != "0") {
                                        alert("Charts information saved successfully");
                                        $('#ctlst').html(data);
                                        $.modal.close();
                                    }
                                    else {
                                        alert("Creation Failed !!");
                                    }
                                }

                            });
                            return false;
                        });
                    }
                });
            }
        });
    });



    //EditView Add MedicalSchedule
    $('#btnMedicationdiv').click(function () {
        $.ajax({
            url: '/iHealthUser/Cases/AddMedicalSchedules',
            type: 'GET',
            async: false,
            cache: false,
            data: { caseId: $('#CaseId').val() },
            success: function (result) {
                $('#Modal-div').html(result).modal({
                    onShow: function () {
                        var l = 0;
                        $('#datePrescribed1').datepicker({
                            changeYear: true,
                            changeMonth: true
                        });

                        $('#dateDispensed1').datepicker({
                            changeYear: true,
                            changeMonth: true
                        });

                        $('#medsc').accordion();

                        $('input[name="cancel"]').click(function () {
                            $.modal.close();
                        });

                        $('img#imgadd').click(function () {

                            $('div#schedule').show('slide', { direction: 'down' }, 600);

                            alert($('#r').length);
                            if ($('#r').is(':checked')) {
                                alert("checked");
                                $('#txt').show();
                                $('#frequencyTaken').hide();
                            } else {
                                alert("not checked");
                                $('#txt').hide();
                                $('#frequencyTaken').show();
                            }
                        });

                        var num = 1;

                        $('#imgaddfrq').click(function () {
                            if (num < 4) {
                                $('#texts').append('<input type="text" name="time[' + num + ']" placeholder="HH:MM" style="margin-bottom:5px;" />');
                                num += 1;
                                $.each($('input[name*="time"]'), function () {
                                    $(this).timepicker();
                                });
                            } else {
                                alert("Only four are allowed");
                            }
                        });

                        $('input[name*="time"]').timepicker();

                        $('input[name="slidecancel"]').click(function () {
                            $('div#schedule').hide('slide', { direction: 'down' }, 600);
                        });

                        $('#schedulsave').click(function () {
                            var x = 0;

                            if ($('#r').is(':checked')) {//no select

                                var values = $('input[name*="time"]');
                                var string = null;
                                alert(values.length);
                                $.each($('input[name*="time"]'), function (index, key) {
                                    if (string == null) {
                                        string = $(this).val();
                                    } else {
                                        string += "," + $(this).val();
                                    }
                                });

                                var mlist = '<a href="#d' + x + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $('#MedicineName').val() + '</span></a><ul id="d' + x + '"><li><div class="tldiv" style="width: 35%"><h6>Medicine Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="ScheduleInfo[' + x + '].MedicineName" onfocus="this.blur();" class="al" value="' + $('#MedicineName').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Type of Medicine</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + x + '].TypeOfMedicine" title="Enter type of Medicine" class="al" value="' + $('#TypeofMedicine').val() + '"/></div></li><li><div class="tldiv" style="width: 35%"><h6>DosageTaken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + x + '].DosageQuantity" class="al" value="' + $('#dosagetaken').val() + " " + $('#dtaken').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Frequency Taken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + x + '].FrequencyTaken" class="al" value="' + string + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Quantity</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + x + '].Quantity" class="al" value="' + $('#quantity').val() + '" /></div></li></ul>';
                                $('<li/>').prependTo('#medsc').html(mlist);
                                $('#medsc').accordion('destroy').accordion({ collapsible: true });
                                $('div.slide').hide('slide', { direction: 'down' }, 600);
                                x += 1;
                            } else {// select
                                var mlist = '<a href="#d' + l + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $('#MedicineName').val() + '</span></a><ul id="d' + l + '"><li><div class="tldiv" style="width: 35%"><h6>Medicine Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="ScheduleInfo[' + l + '].MedicineName" onfocus="this.blur();" class="al" value="' + $('#MedicineName').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Type of Medicine</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].TypeOfMedicine" title="Enter type of Medicine" class="al" value="' + $('#TypeofMedicine').val() + '"/></div></li><li><div class="tldiv" style="width: 35%"><h6>DosageTaken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DosageQuantity" class="al" value="' + $('#dosagetaken').val() + " " + $('#dtaken').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Frequency Taken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].FrequencyTaken" class="al" value="' + $('#frequencyTaken').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Quantity</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].Quantity" class="al" value="' + $('#quantity').val() + '" /></div></li></ul>';
                                $('<li/>').prependTo('#medsc').html(mlist);
                                $('#medsc').accordion('destroy').accordion({ collapsible: true });
                                $('div.slide').hide('slide', { direction: 'down' }, 600);
                                l += 1;
                            }
                        });

                        $('#btnMsSave').click(function () {
                            $.ajax({
                                url: '/iHealthUser/Cases/SaveEditAddMedicalSchedule',
                                type: "POST",
                                aync: false,
                                data: new FormData($(this).parents('#frmmed').get(0)),
                                contentType: false,
                                processData: false,
                                success: function (data) {
                                    if (data != "0") {
                                        alert("Medication information saved successfully");
                                        $('#mslst').html(data);
                                        $.modal.close();
                                    }
                                    else {
                                        alert("Creation Failed !!");
                                    }
                                }
                            });
                            return false;
                        });
                    }
                });
            }
        });


        //        $.ajax({
        //            url: '/iHealthUser/Cases/AddMedicalSchedules',
        //            type: 'GET',
        //            async: false,
        //            cache: false,
        //            data: { caseId: $('#CaseId').val() },
        //            success: function (result) {
        //                $('#Modal-div').html(result).modal({
        //                    onShow: function () {
        //                        var l = 0;

        //                        $('#datePrescribed3').datepicker({
        //                            changeYear: true,
        //                            changeMonth: true
        //                        }); // sandeep added

        //                        $('#dateDispensed3').datepicker({
        //                            changeYear: true,
        //                            changeMonth: true
        //                        }); // sandeep added

        //                        $('#medsc').accordion();

        //                        $('input[name="cancel"]').click(function () {
        //                            $.modal.close();
        //                        });

        //                        $('img#imgadd').click(function () {
        //                            $('div#schedule').show('slide', { direction: 'down' }, 600);
        //                        });

        //                        $('input[name="slidecancel"]').click(function () {
        //                            $('div#schedule').hide('slide', { direction: 'down' }, 600);
        //                        });

        //                        $('#schedulsave').click(function () {
        //                            alert("medical Schedule");
        //                            var mlist = '<a href="#d' + l + '"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 350px">' + $('#MedicineName').val() + '</span></a><ul id="d' + l + '"><li><div class="tldiv" style="width: 35%"><h6>Medicine Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="ScheduleInfo[' + l + '].MedicineName" onfocus="this.blur();" class="al" value="' + $('#MedicineName').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Type of Medicine</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].TypeOfMedicine" title="Enter type of Medicine" class="al" value="' + $('#TypeofMedicine').val() + '"/></div></li><li><div class="tldiv" style="width: 35%"><h6>DosageTaken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DosageQuantity" class="al" value="' + $('#dosagetaken').val() + " " + $('#dtaken').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Frequency Taken</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].FrequencyTaken" class="al" value="' + $('#frequencyTaken').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Quantity</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].Quantity" class="al" value="' + $('#quantity').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Days Supply</h6></div><div class="trdiv" style="width: 55%"><input type="text" onfocus="this.blur();" name="ScheduleInfo[' + l + '].DaysSupply" class="al" value="' + $('#dayssupply').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Notes</h6></div><div class="trdiv" style="width: 55%"><textarea onfocus="this.blur();" name="ScheduleInfo[' + l + '].Notes" cols="15" rows="7" class="al" style="height: 40px">' + $('#schedulenotes').val() + '</textarea></div></li></ul>';
        //                            $('<li/>').prependTo('#medsc').html(mlist);
        //                            $('#medsc').accordion('destroy').accordion({ collapsible: true });
        //                            $('div.slide').hide('slide', { direction: 'down' }, 600);
        //                            l += 1;
        //                        });

        //                        $('#btnMsSave').click(function () {
        //                            $.ajax({
        //                                url: '/iHealthUser/Cases/SaveEditAddMedicalSchedule',
        //                                type: "POST",
        //                                aync: false,
        //                                data: new FormData($(this).parents('#frmmed').get(0)),
        //                                contentType: false,
        //                                processData: false,
        //                                success: function (data) {
        //                                    if (data != "0") {
        //                                        alert("Medical Schedule Created Successfully");
        //                                        $('#mslst').html(data);
        //                                        $.modal.close();
        //                                    }
        //                                    else {
        //                                        alert("Creation Failed !!");
        //                                    }
        //                                }
        //                            });
        //                            return false;
        //                        });
        //                    }
        //                });
        //            }
        //        });
    });



    //EditView Add Visits
    $('#btnAddvisitsdiv').click(function () {
        $.ajax({
            url: '/iHealthUser/Cases/AddVisits',
            type: 'GET',
            async: false,
            cache: false,
            data: { caseId: $('#CaseId').val() },
            success: function (result) {
                $('#Modal-div').html(result).modal({
                    onShow: function () {

                        $('#cvisitDate3').datepicker({
                            changeYear: true,
                            changeMonth: true
                        }); // sandeep added

                        $('input[name="cancel"]').click(function () {
                            $.modal.close();
                        });
                        $('#btnVstSave').click(function () {
                            $.ajax({
                                url: '/iHealthUser/Cases/SaveEditAddVisit',
                                type: "POST",
                                aync: false,
                                data: new FormData($(this).parents('#frmvst').get(0)),
                                contentType: false,
                                processData: false,
                                success: function (data) {
                                    if (data != "0") {
                                        alert("Your doctor visit information saved successfully");
                                        $('#visitn').html(data);
                                        $.modal.close();
                                    }
                                    else {
                                        alert("Creation Failed !!");
                                    }
                                }
                            });
                            return false;
                        });
                    }
                });
            }
        });
    });





    //sandeep added new functions end here

    //Dileep
    $("#btnCaseEditSave").click(function () {

        $.ajax({
            url: "/iHealthUser/Cases/UpdateCase",
            type: 'POST',
            async: false,
            data: $(this).parents("#editCase").serialize(),
            cache: false,
            success: function (success) {
                if (success == "1020") {

                    alert("Your modifications are updates successfully");
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
    //for HDinfo
    $('#hsave').click(function () {

        var hitem = '<a id="#e' + index + '" style="height:16px; padding:1px 1px"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 75px; font-size:10px;">' + $('input[name="HospitalName"]').val() + '</span></a><ul id="e' + index + '"><li><div class="tldiv" style="width: 35%"><h6>Hospital Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="HDInfos[' + index + '].HospitalName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="HospitalName"]').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Doctor Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="HDInfos[' + index + '].DoctorName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="DoctorName"]').val() + '" /></div></li></ul>';

        $('<li/>').prependTo('#HDInfos').html(hitem);
        $('#HDInfos').accordion('destroy').accordion({ collapsible: true });
        $(this).parents("#handdinfod").find(':text').val('');
        index += 1;
    });

    $('#ad').click(function () {
        $('#handdinfod').show('slide', { direction: 'down' }, 600);
    });
    $('#handdinfod').find('#hsave').click(function () {
        $("#handdinfod").hide("slide", { direction: 'down' }, 600);
    });
    $('input[name="cancel"]').click(function () {
        $(this).parents('div.slide').hide('slide', { direction: 'down' }, 600);
    });

});