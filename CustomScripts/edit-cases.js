$(function () {

    $.ajaxSetup({
        async: false,
        cache: false
    });
    $('#ad2').click(function () {
        $('div#handdinfod').show('slide', { direction: 'down' }, 600);
    });
    $('input[name="cancel12"]').click(function () {
        $(this).parents('div.slide').hide('slide', { direction: 'down' }, 600);
    });
    var index = $('ul[name="HDInfos"]').length;

    //ck added
    var r = 0;
    $('#hsaved').click(function () {
        if (($('#Edithname').val() == '') && ($('#Editadds').val() == '') && ($('#Editphno').val() == '') && ($('#Editemail').val() == '')) {
            alert("Please enter hospital information");
            return false;
        }
        var x = document.getElementById("Editphno").value;
        if (x.length == "") {
        }
        else if (x.length < 10) {
            alert("please enter correct phone number");
            return false;
        }

        if ($('#Edithname').val() == "") {
            return false;
        }
        var pattern = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
        if (($('#Editemail').val() == '')) {
            var list1 = '<ul class="HspInfo" id="e' + r + '"><li>  Hospital Name</li> <li> <input type="text" name="HospitalInfos[' + r + '].HospitalName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="HospitalName"]').val() + '" /></li><li>Email</li> <li> <input type="text" name="HospitalInfos[' + r + '].HospEmail" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="HospEmail"]').val() + '" /></li><li>PhoneNo</li> <li> <input type="text" name="HospitalInfos[' + r + '].HospPhno" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="HospPhno"]').val() + '" /></li><li>Address</li> <li> <input type="text" name="HospitalInfos[' + r + '].HospAddress" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('textarea[name="HospAddress"]').val() + '" /> </li></ul>';

            $("<li/>").appendTo('#HospitalInfo').html(list1);
            $('div#handdinfod').hide('slide', { direction: 'down' }, 600);
            $("#ad1").hide();
            $("#label1").hide();
        }
        else {
            if (pattern.test($('#Editemail').val())) {
                var list1 = '<ul class="HspInfo" id="e' + r + '"><li>  Hospital Name</li> <li> <input type="text" name="HospitalInfos[' + r + '].HospitalName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="HospitalName"]').val() + '" /></li><li>Email</li> <li> <input type="text" name="HospitalInfos[' + r + '].HospEmail" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="HospEmail"]').val() + '" /></li><li>PhoneNo</li> <li> <input type="text" name="HospitalInfos[' + r + '].HospPhno" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="HospPhno"]').val() + '" /></li><li>Address</li> <li> <input type="text" name="HospitalInfos[' + r + '].HospAddress" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('textarea[name="HospAddress"]').val() + '" /> </li></ul>';
                $("<li/>").appendTo('#HospitalInfo').html(list1);
                $('div#handdinfod').hide('slide', { direction: 'down' }, 600);
                $("#ad2").hide();
                $("#label1").hide();
            }
            else {
                alert("Please Enter correct Email");
                return false;
            }
        }

    });
    //ck end
    $('input[name="cancelslide"]').click(function () {
        $(this).parents('div#handdinfod').hide('slide', { direction: 'down' }, 600);
    });
    // create Edit case sp
    $("#Editphno").keydown(function (event) {
        // Allow: backspace, delete, tab, escape, and enter
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
        // Allow: Ctrl+A
    (event.keyCode == 65 && event.ctrlKey === true) ||
        // Allow: home, end, left, right
    (event.keyCode >= 35 && event.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        else {
            // Ensure that it is a number and stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) || ($(this).val().length >= 10)) {
                event.preventDefault();
            }
        }
    });

    $('#editmail').blur(function () {
        var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        if (testEmail.test(this.value)) {
            return false;
        }
        else {
            alert('please enter valid mail id');
            $('input[tabindex=6]').focus();
        }
    });

    $("#editph").keydown(function (event) {
        // Allow: backspace, delete, tab, escape, and enter
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
        // Allow: Ctrl+A
(event.keyCode == 65 && event.ctrlKey === true) ||
        // Allow: home, end, left, right
(event.keyCode >= 35 && event.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        else {
            // Ensure that it is a number and stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) || ($(this).val().length >= 10)) {
                event.preventDefault();
            }
        }
    });
    var index = $('ul[name="HDInfos"]').length;

    //EditView Add Procedures
    $('#btnAddprocdiv').click(function () {
        $.ajax({
            url: '/iHealthUser/Cases/AddProcedures',
            type: 'GET',
            async: false,
            data: { caseId: $('#CaseId').val() },
            success: function (result) {
                $('#Modal-div').html(result).modal({
                    closeHTML: "",
                    onShow: function () {

                        $('#dateOfService3').datepicker({
                            changeYear: true,
                            changeMonth: true
                        }); // sandeep added

                        $('input[name="cancel"]').click(function () {
                            $.modal.close();
                        });
                        $('#btnProcSave').click(function () {
                            if ($("#procname").val() == "") {
                                alert("Please enter procedure name");
                                $('input[tabindex=1]').focus();
                                //  return false;
                            }
                            else if ($("#dateOfServiceprocedure1").val() == "") {
                                alert("Please enter procedure date");
                                $('input[tabindex=2]').focus();
                            }
                            else if ($("#provider").val() == "") {
                                alert("Please enter provider or facility name");
                                $('input[tabindex=3]').focus();
                            }
                            else if ($("#surgeon").val() == "") {
                                alert("Please enter surgeon name");
                                $('input[tabindex=4]').focus();
                            }
                            else if ($('#r1:checked').val() == 'true') {

                                if ($('#dateOfServiceprocedurenotify1').val() == "") {
                                    alert("pls enter date ");
                                    $('input[tabindex=5]').focus();
                                    return false;
                                }
                            }
                           
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
    // end of EditView Add Procedures

    //EditView Add MedicalTest
    $('#btnAddMedTestdiv').click(function () {
        $.ajax({
            url: '/iHealthUser/Cases/AddMedicalTests',
            type: 'GET',
            async: false,
            data: { caseId: $('#CaseId').val() },
            success: function (result) {
                $('#Modal-div').html(result).modal({

                    closeHTML: "",
                    onShow: function () {

                        $('#dateOfMtest3').datepicker({
                            changeYear: true,
                            changeMonth: true
                        }); // sandeep added

                        $('input[name="cancel"]').click(function () {
                            $.modal.close();
                        });
                        $('#btnMTSave').click(function () {
                            if ($("#tstnameid").val() == "") {
                                alert("Please medical test name");
                                $('input[tabindex=1]').focus();
                                //  return false;
                            }
                            else if ($("#dateOfMtest1").val() == "") {
                                alert("Please enter medical test date");
                                $('input[tabindex=2]').focus();
                            }
                            else {
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
                                            $('#medtstlst').html(data);
                                            $.modal.close();
                                        }
                                        else {
                                            alert("Creation Failed !!");
                                        }
                                    }
                                });
                            }
                            return false;
                        });

                    }
                });
            }
        });
    });
    //end of EditView Add MedicalTest

    //Edit View Add Chart
    $('#btnAddCharttdiv').click(function () {
        $.ajax({
            url: '/iHealthUser/Cases/AddCharts',
            type: 'GET',
            async: false,
            data: { caseId: $('#CaseId').val() },
            success: function (result) {
                $('#Modal-div').html(result).modal({

                    closeHTML: "",
                    onShow: function () {

                        $('#dateOfChart1').datepicker({
                            changeYear: true,
                            changeMonth: true
                        }); // chandrakanth added

                        $('input[name="cancel"]').click(function () {
                            $.modal.close();
                        });
                        $('#btnCTSave').click(function () {
                            if ($("#chrtnameid").val() == "") {
                                alert("Please enter chart name");
                                $('input[tabindex=1]').focus();
                                //  return false;
                            }
                            else if ($("#dateOfChart11").val() == "") {
                                alert("Please enter date of chart");
                                $('input[tabindex=3]').focus();
                            }
                            else if ($("#chrttypeid").val() == "") {
                                alert("Please enter chart type");
                                $('input[tabindex=2]').focus();
                            }
                            else if ($('#r1:checked').val() == 'true') {

                                if ($('#dateofmtestnotify1').val() == "") {
                                    alert("pls enter date ");
                                    $('input[tabindex=3]').focus();
                                    return false;
                                }
                            }
                          
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
    });
    //End of Chart Edit 



    //EditView Add MedicalSchedule
    $('#btnMedicationdiv').click(function () {
        $.ajax({
            url: '/iHealthUser/Cases/CreateMedicalSchedules',
            type: 'GET',
            async: false,
            cache: false,
            data: { caseId: $('#CaseId').val() },
            success: function (result) {
                $('#Modal-div').html(result).modal({

                    closeHTML: "",
                    onShow: function () {
                        var l = 0;
                        $("#pdate1").datepicker({
                            changeYear: true,
                            changeMonth: true

                        });
                        $("#pdate2").datepicker({
                            changeYear: true,
                            changeMonth: true

                        });

                        //sandeep added new code start here

                        //                        var med = 0;
                        //                        $('img#addmed').click(function () {
                        //                            $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" name="ScheduleInfo[' + med + '].MedicineName" title="Name of the Medicine" placeholder="Medicine" class="crt" style="width: 90px;  margin:0 5px 0 0;" /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" style="border: 1px solid #BED0DF; width: 48px;" placeholder="Quantity" /><select name="ScheduleInfo[' + med + '].TypeOfMedicine" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;"><option value="tablet(s)">tablet(s)</option><option value="drop(s)">drop(s)</option><option value="capsule(s)">capsule(s)</option><option value="tsp(s)">tsps(s)</option><option value="tbsp(s)">tbsp(s)</option><option value="puff(s)">puff(s)</option><option value="application(s)">application(s)</option></select><select name="ScheduleInfo[' + med + '].FrequencyTaken" id="frequencyTaken" style="border: 1px solid #BED0DF; width: 80px;"><option value="as needed">as needed</option><option value="Every 1hr">Every 1hr</option><option value="Every 2hrs">Every 2hrs</option><option value="Every 3hrs">Every 3hrs</option><option value="Every 4hrs">Every 4hrs</option><option value="Every morning">Every morning</option><option value="Every evening">Every evening</option><option value="once a day">once a day</option><option value="twice a day">twice a day</option><option value="thrice a day">thrice a day</option><option value="four times a day">four times a day</option><option value="once a week">once a week</option><option value="twice a week">twice a week</option><option value="thrice a week">thrice a week</option><option value="four times a week">four times a week</option><option value="before bed">before bed</option><option value="before meals">before meals</option><option value="after meals">after meals</option><option value="with meals">with meals</option></select></div><input type="text" name="ScheduleInfo[' + med + '].TotalQuantity" title="Total quantity" placeholder="Quantity" class="crt" style="width: 55px; margin:0 5px;" /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" title="Starting time" style="width: 62px; display:none;" class="crt" placeholder="Start Time" /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');


                        $('img#addmed').click(function () {
                            var pdate1 = document.getElementById("pdate1").value;
                            var pdate2 = document.getElementById("pdate2").value;
                            var startdate = new Date($('#pdate1').val());
                            var enddate = new Date($('#pdate2').val());

                            if (pdate1 == '') {
                                alert("Please select start date");
                                $('input[tabindex=1]').focus();
                                return false;
                            }
                            else if (pdate2 == '') {
                                alert("Please select end date");
                                $('input[tabindex=2]').focus();
                                return false;
                            }
                            else if (startdate > enddate) {
                                alert("Please select greater than start date");
                                $('#pdate2').val("");
                                $('input[tabindex=2]').focus();
                                return false;
                            }
                            else {
                                $("#idMedicineName,#iddosagetaken,#iddtaken,#idquantity,#idfrequencyTaken,#idquantity,#idreason").val("");
                                $("#date1,#date2,#date3,#date4,#date5,#date6,#date7,#date8,#date9,#date10,#date11,#date12,#date13,#date14,#date15,#date16,#date17,#date18,#date19,#date20").val("");
                                $("#FQ1,#FQ2,#FQ3,#FQ4,#FQ5,#FQ6,#FQ7,#FQ8,#lblstarttime").hide();

                                $('div#schedule').show('slide', { direction: 'down' }, 600);
                            }
                        });


                        med = 0;
                        $("#schedulsave").click(function () {

                            var dosagetaken = document.getElementById("iddosagetaken").value;
                            var MedicineName = document.getElementById("idMedicineName").value;
                            var quantity = document.getElementById("idquantity").value;
                            var frequencytaken = document.getElementById("idfrequencyTaken").value;
                            var tdate1 = document.getElementById("date1").value;
                            var tdate2 = document.getElementById("date2").value;
                            var tdate3 = document.getElementById("date3").value;
                            var tdate4 = document.getElementById("date4").value;
                            var tdate5 = document.getElementById("date5").value;
                            var tdate6 = document.getElementById("date6").value;
                            var tdate7 = document.getElementById("date7").value;
                            var tdate8 = document.getElementById("date8").value;
                            var tdate9 = document.getElementById("date9").value;
                            var tdate10 = document.getElementById("date10").value;
                            var tdate11 = document.getElementById("date11").value;
                            var tdate12 = document.getElementById("date12").value;
                            var tdate13 = document.getElementById("date13").value;
                            var tdate14 = document.getElementById("date14").value;
                            var tdate15 = document.getElementById("date15").value;
                            var tdate16 = document.getElementById("date16").value;
                            var tdate17 = document.getElementById("date17").value;
                            var tdate18 = document.getElementById("date18").value;
                            var tdate19 = document.getElementById("date19").value;
                            var tdate20 = document.getElementById("date20").value;
                            var startdate1 = new Date($('#pdate1').val());
                            var enddate1 = new Date($('#pdate2').val()).setHours(23, 59, 00, 00);
                            var selectdate11 = new Date($('#date11').val());
                            var selectdate12 = new Date($('#date12').val());
                            var selectdate13 = new Date($('#date13').val());
                            var selectdate14 = new Date($('#date14').val());
                            var selectdate15 = new Date($('#date15').val());
                            var selectdate16 = new Date($('#date16').val());
                            var selectdate17 = new Date($('#date17').val());
                            var selectdate18 = new Date($('#date18').val());
                            var selectdate19 = new Date($('#date19').val());
                            var selectdate20 = new Date($('#date20').val());





                            var timeregexp = /([01][0-9]|[02][0-3]):[0-5][0-9]/;

                            if (MedicineName == "") {
                                alert("Please enter MedicineName");
                                $('input[tabindex=3]').focus();
                                return false;
                            }
                            if (dosagetaken == "") {
                                alert("Please enter dosage taken");
                                $('input[tabindex=4]').focus();
                                return false;
                            }
                            if (quantity == "") {
                                //                                alert("Please enter total quantity");
                                //                                $('input[tabindex=5]').focus();
                                //                                return false;
                            }
                            else if (quantity > 1000) {
                                alert("Doesnot take morethan 1000 tables at a time");
                                $('input[tabindex=5]').focus();
                                return false;
                            }
                            if (frequencytaken == "0") {


                            }
                            else if (frequencytaken == "1") {
                                if (tdate1 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=6]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date1').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time formate");
                                        $('#date1').val("");
                                        $('input[tabindex=6]').focus();
                                        return false;
                                    }
                                }

                                if ($("#idfrequencyTaken option:selected").text() == "Every morning") {
                                    if (tdate1 < "12:00") {
                                    }
                                    else {
                                        alert("please select less then 12hrs");
                                        $('#date1').val("");
                                        $('input[tabindex=6]').focus();
                                        return false;
                                    }
                                }
                                else if ($("#idfrequencyTaken option:selected").text() == "Every evening") {
                                    if (tdate1 >= "18:00") {
                                    }
                                    else {
                                        alert("please select more then 18hrs");
                                        $('#date1').val("");
                                        $('input[tabindex=6]').focus();
                                        return false;
                                    }
                                }
                                else if ($("#idfrequencyTaken option:selected").text() == "before bed") {
                                    if (tdate1 >= "20:00") {
                                    }
                                    else {
                                        alert("please select greater then 20hrs");
                                        $('#date1').val("");
                                        $('input[tabindex=6]').focus();
                                        return false;
                                    }
                                }

                            } else if (frequencytaken == "2") {
                                if (tdate2 == "") {
                                    alert("Please enter time");
                                    $('input[tabindex=7]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date2').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time format");
                                        $('#date2').val("");
                                        $('input[tabindex=7]').focus();
                                        return false;
                                    }
                                }
                                if (tdate3 == "") {
                                    alert("Please enter time");
                                    $('input[tabindex=8]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date3').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time format");
                                        $('#date3').val("");
                                        $('input[tabindex=8]').focus();
                                        return false;
                                    }
                                }

                                if ($("#idfrequencyTaken option:selected").text() == "before meals") {
                                    if (tdate2 <= "12:00") {
                                    }
                                    else {
                                        alert("please enter before 12hrs and after 20hrs");
                                        $('#date2').val("");
                                        $('input[tabindex=7]').focus();
                                        return false;
                                    }
                                    if (tdate3 >= "20:00") {
                                    }
                                    else {
                                        alert("please enter  after 20hrs");
                                        $('#date3').val("");
                                        $('input[tabindex=8]').focus();
                                        return false;
                                    }
                                }
                                if ($("#idfrequencyTaken option:selected").text() == "after meals" || $("#idfrequencyTaken option:selected").text() == "with meals") {
                                    if (tdate2 >= "12:00" && tdate2 <= "15:00") {
                                    }
                                    else {
                                        alert("please give time between 12-15hrs");
                                        $('#date2').val("");
                                        $('input[tabindex=7]').focus();
                                        return false;
                                    }
                                    if (tdate3 >= "20:00" && tdate3 <= "22:00") {
                                    }
                                    else {
                                        alert("please give time between 20-22hrs");
                                        $('#date3').val("");
                                        $('input[tabindex=8]').focus();
                                        return false;
                                    }
                                }
                                if (tdate2 < tdate3) {
                                }
                                else {
                                    alert("Please enter greater than above time");
                                    $('#date3').val("");
                                    $('input[tabindex=8]').focus();
                                    return false;
                                }
                            }
                            else if (frequencytaken == "3") {
                                if (tdate4 == "") {
                                    alert("Please enter time");
                                    $('input[tabindex=9]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date4').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time format");
                                        $('#date4').val("");
                                        $('input[tabindex=9]').focus();
                                        return false;
                                    }
                                }
                                if (tdate5 == "") {
                                    alert("Please enter time");
                                    $('input[tabindex=10]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date5').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time format");
                                        $('#date5').val("");
                                        $('input[tabindex=10]').focus();
                                        return false;
                                    }
                                }
                                if (tdate6 == "") {
                                    alert("Please enter time");
                                    $('input[tabindex=11]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date6').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time format");
                                        $('#date6').val("");
                                        $('input[tabindex=11]').focus();
                                        return false;
                                    }
                                }

                                if (tdate4 < tdate5) {
                                }
                                else {
                                    alert("Please enter greater than above time");
                                    $('#date5').val("");
                                    $('input[tabindex=10]').focus();
                                    return false;
                                }
                                if (tdate5 < tdate6) {
                                }
                                else {
                                    alert("Please enter greater than above time");
                                    $('#date6').val("");
                                    $('input[tabindex=11]').focus();
                                    return false;
                                }


                            }
                            else if (frequencytaken == "4") {
                                if (tdate7 == "") {
                                    alert("Please enter time");
                                    $('input[tabindex=12]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date7').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time format");
                                        $('#date7').val("");
                                        $('input[tabindex=12]').focus();
                                        return false;
                                    }
                                }
                                if (tdate8 == "") {
                                    alert("Please enter time");
                                    $('input[tabindex=13]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date8').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time format");
                                        $('#date8').val("");
                                        $('input[tabindex=13]').focus();
                                        return false;
                                    }
                                }
                                if (tdate9 == "") {
                                    alert("Please enter time");
                                    $('input[tabindex=14]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date9').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time format");
                                        $('#date9').val("");
                                        $('input[tabindex=14]').focus();
                                        return false;
                                    }
                                }
                                if (tdate10 == "") {
                                    alert("Please enter time");
                                    $('input[tabindex=15]').focus();
                                    return false;
                                }
                                else {
                                    var correct = ($('#date10').val().search(timeregexp) >= 0) ? true : false;
                                    if (correct) {
                                    }
                                    else {
                                        alert("please enter correct time format");
                                        $('#date10').val("");
                                        $('input[tabindex=15]').focus();
                                        return false;
                                    }
                                }

                                if (tdate7 < tdate8) {
                                }
                                else {
                                    alert("Please enter greater than above time");
                                    $('#date8').val("");
                                    $('input[tabindex=13]').focus();
                                    return false;
                                }

                                if (tdate8 < tdate9) {
                                }
                                else {
                                    alert("Please enter greater than above time");
                                    $('#date9').val("");
                                    $('input[tabindex=14]').focus();
                                    return false;
                                }
                                if (tdate9 < tdate10) {
                                }
                                else {
                                    alert("Please enter greater than above time");
                                    $('#date10').val("");
                                    $('input[tabindex=15]').focus();
                                    return false;
                                }

                            }
                            else if (frequencytaken == "5") {
                                if (tdate11 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=16]').focus();
                                    return false;
                                }
                                else {
                                    alert("selectdate11 is: " + selectdate11);
                                    if ((selectdate11 >= startdate1) && (selectdate11 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date11').val("");
                                        $('input[tabindex=16]').focus();
                                        return false;
                                    }
                                }

                            } else if (frequencytaken == "6") {
                                if (tdate12 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=17]').focus();
                                    return false;
                                }
                                else {
                                    //alert("selectdate12 is: " + selectdate12);
                                    if ((selectdate12 >= startdate1) && (selectdate12 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date12').val("");
                                        $('input[tabindex=17]').focus();
                                        return false;
                                    }
                                }
                                if (tdate13 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=18]').focus();
                                    return false;
                                }
                                else {
                                    // alert("selectdate13 is: " + selectdate13);
                                    if ((selectdate13 >= startdate1) && (selectdate13 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date13').val("");
                                        $('input[tabindex=18]').focus();
                                        return false;
                                    }
                                }

                                if (selectdate12 < selectdate13) {

                                }
                                else if (selectdate12 > selectdate13) {
                                    alert("Please select greater than the above date");
                                    $('#date13').val("");
                                    $('input[tabindex=18]').focus();
                                    return false;

                                }
                                else {
                                    alert("Please dont select same date");
                                    $('#date13').val("");
                                    $('input[tabindex=18]').focus();
                                    return false;
                                }
                            }
                            else if (frequencytaken == "7") {
                                if (tdate14 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=19]').focus();
                                    return false;
                                }
                                else {
                                    //alert("selectdate14 is: " + selectdate14);
                                    if ((selectdate14 >= startdate1) && (selectdate14 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date14').val("");
                                        $('input[tabindex=19]').focus();
                                        return false;
                                    }
                                }
                                if (tdate15 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=20]').focus();
                                    return false;
                                }
                                else {
                                    //alert("selectdate15 is: " + selectdate15);
                                    if ((selectdate15 >= startdate1) && (selectdate15 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date15').val("");
                                        $('input[tabindex=20]').focus();
                                        return false;
                                    }
                                }
                                if (tdate16 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=21]').focus();
                                    return false;
                                }
                                else {
                                    //alert("selectdate16 is: " + selectdate16);
                                    if ((selectdate16 >= startdate1) && (selectdate16 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date16').val("");
                                        $('input[tabindex=21]').focus();
                                        return false;
                                    }
                                }
                                if (selectdate14 < selectdate15) {

                                }
                                else if (selectdate14 > selectdate15) {
                                    alert("Please select greater than the above date");
                                    $('#date15').val("");
                                    $('input[tabindex=20]').focus();
                                    return false;

                                }
                                else {
                                    alert("Please dont select same date");
                                    $('#date15').val("");
                                    $('input[tabindex=20]').focus();
                                    return false;
                                }

                                if (selectdate15 < selectdate16) {
                                }
                                else if (selectdate15 > selectdate16) {
                                    alert("Please select greater than the above date");
                                    $('#date16').val("");
                                    $('input[tabindex=21]').focus();
                                    return false;
                                }
                                else {
                                    alert("Please dont select same date");
                                    $('#date16').val("");
                                    $('input[tabindex=21]').focus();
                                    return false;
                                }
                            }
                            else if (frequencytaken == "8") {
                                if (tdate17 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=22]').focus();
                                    return false;
                                }
                                else {
                                    //alert("selectdate17 is: " + selectdate17);
                                    if ((selectdate17 >= startdate1) && (selectdate17 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date17').val("");
                                        $('input[tabindex=22]').focus();
                                        return false;
                                    }
                                }
                                if (tdate18 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=23]').focus();
                                    return false;
                                }
                                else {
                                    //alert("selectdate18 is: " + selectdate18);
                                    if ((selectdate18 >= startdate1) && (selectdate18 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date18').val("");
                                        $('input[tabindex=23]').focus();
                                        return false;
                                    }
                                }
                                if (tdate19 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=24]').focus();
                                    return false;
                                }
                                else {
                                    //  alert("selectdate19 is: " + selectdate19);
                                    if ((selectdate19 >= startdate1) && (selectdate19 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date19').val("");
                                        $('input[tabindex=24]').focus();
                                        return false;
                                    }
                                }
                                if (tdate20 == "") {
                                    alert("Please enter date");
                                    $('input[tabindex=25]').focus();
                                    return false;
                                }
                                else {
                                    // alert("selectdate20 is: " + selectdate11);
                                    if ((selectdate20 >= startdate1) && (selectdate20 <= enddate1)) {

                                    }
                                    else {
                                        alert("Please select date between start and end date");
                                        $('#date20').val("");
                                        $('input[tabindex=25]').focus();
                                        return false;
                                    }
                                }

                                if (selectdate17 < selectdate18) {

                                }
                                else if (selectdate17 > selectdate18) {
                                    alert("Please select greater than the above date");
                                    $('#date18').val("");
                                    $('input[tabindex=23]').focus();
                                    return false;

                                }
                                else {
                                    alert("Please dont select same date");
                                    $('#date18').val("");
                                    $('input[tabindex=23]').focus();
                                    return false;
                                }

                                if (selectdate18 < selectdate19) {

                                }
                                else if (selectdate18 > selectdate19) {
                                    alert("Please select greater than the above date");
                                    $('#date19').val("");
                                    $('input[tabindex=24]').focus();
                                    return false;

                                }
                                else {
                                    alert("Please dont select same date");
                                    $('#date19').val("");
                                    $('input[tabindex=24]').focus();
                                    return false;
                                }

                                if (selectdate19 < selectdate20) {

                                }
                                else if (selectdate19 > selectdate20) {
                                    alert("Please select greater than the above date");
                                    $('#date20').val("");
                                    $('input[tabindex=25]').focus();
                                    return false;

                                }
                                else {
                                    alert("Please dont select same date");
                                    $('#date20').val("");
                                    $('input[tabindex=25]').focus();
                                    return false;
                                }
                            }


                            //alert("selectdate11 is: " + selectdate11);




                            if (frequencytaken == "0") {

                                //$('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddosagetaken').val() + "  " + $('#dtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $('#frequencyTaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 55px; margin:0 5px;" readonly /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="" title="Starting time" style="width: 62px;display:none" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                                $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" id="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" value="' + $('#iddosagetaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;" readonly ></div><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $("#idfrequencyTaken option:selected").text() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 30px; margin:0 5px;" readonly /><input type="text" name="ScheduleInfo[' + med + '].Reason" value="' + $('#idreason').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;display:none;" readonly ><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);display:none"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;display:none">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="" title="Starting time" style="width: 62px;display:none" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');

                            }
                            else if (frequencytaken == "1") {

                                //$('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddosagetaken').val() + "  " + $('#dtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $('#frequencyTaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 55px; margin:0 5px;" readonly /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date1').val() + '" title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                                $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" value="' + $('#iddosagetaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;" readonly ></div><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $("#idfrequencyTaken option:selected").text() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 30px; margin:0 5px;" readonly /><input type="text" name="ScheduleInfo[' + med + '].Reason" value="' + $('#idreason').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;display:none;" readonly ><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date1').val() + '"  title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                            }
                            else if (frequencytaken == "2") {
                                //$('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddosagetaken').val() + "  " + $('#dtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $('#frequencyTaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 55px; margin:0 5px;" readonly /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date2').val() + "," + $('#date3').val() + '" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                                $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" value="' + $('#iddosagetaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;" readonly ></div><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $("#idfrequencyTaken option:selected").text() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 30px; margin:0 5px;" readonly /><input type="text" name="ScheduleInfo[' + med + '].Reason" value="' + $('#idreason').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;display:none;" readonly ><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date2').val() + "," + $('#date3').val() + '" title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                            }
                            else if (frequencytaken == "3") {
                                //$('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddosagetaken').val() + "  " + $('#dtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $('#frequencyTaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 55px; margin:0 5px;" readonly /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date4').val() + "," + $('#date5').val() + "," + $("#date6").val() + '" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                                $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" value="' + $('#iddosagetaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;" readonly ></div><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $("#idfrequencyTaken option:selected").text() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 30px; margin:0 5px;" readonly /><input type="text" name="ScheduleInfo[' + med + '].Reason" value="' + $('#idreason').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;display:none;" readonly ><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date4').val() + "," + $('#date5').val() + "," + $("#date6").val() + '" title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                            }
                            else if (frequencytaken == "4") {
                                //$('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddosagetaken').val() + "  " + $('#dtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $('#frequencyTaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 55px; margin:0 5px;" readonly /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date7').val() + "," + $('#date8').val() + "," + $("#date9").val() + "," + $("#date10").val() + '" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                                $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" value="' + $('#iddosagetaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;" readonly ></div><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $("#idfrequencyTaken option:selected").text() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 30px; margin:0 5px;" readonly /><input type="text" name="ScheduleInfo[' + med + '].Reason" value="' + $('#idreason').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;display:none;" readonly ><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date7').val() + "," + $('#date8').val() + "," + $("#date9").val() + "," + $("#date10").val() + '" title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                            }
                            else if (frequencytaken == "5") {

                                //$('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddosagetaken').val() + "  " + $('#dtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $('#frequencyTaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 55px; margin:0 5px;" readonly /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date11').val() + '" title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                                $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" value="' + $('#iddosagetaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;" readonly ></div><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $("#idfrequencyTaken option:selected").text() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 30px; margin:0 5px;" readonly /><input type="text" name="ScheduleInfo[' + med + '].Reason" value="' + $('#idreason').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;display:none;" readonly ><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date11').val() + '" title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                            }
                            else if (frequencytaken == "6") {
                                //$('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddosagetaken').val() + "  " + $('#dtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $('#frequencyTaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 55px; margin:0 5px;" readonly /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date12').val() + "," + $('#date13').val() + '" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                                $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" value="' + $('#iddosagetaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;" readonly ></div><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $("#idfrequencyTaken option:selected").text() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 30px; margin:0 5px;" readonly /><input type="text" name="ScheduleInfo[' + med + '].Reason" value="' + $('#idreason').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;display:none;" readonly ><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date12').val() + "," + $('#date13').val() + '" title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                            }
                            else if (frequencytaken == "7") {
                                //$('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddosagetaken').val() + "  " + $('#dtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $('#frequencyTaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 55px; margin:0 5px;" readonly /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date14').val() + "," + $('#date15').val() + "," + $("#date16").val() + '" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                                $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" value="' + $('#iddosagetaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;" readonly ></div><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $("#idfrequencyTaken option:selected").text() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 30px; margin:0 5px;" readonly /><input type="text" name="ScheduleInfo[' + med + '].Reason" value="' + $('#idreason').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;display:none;" readonly ><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date14').val() + "," + $('#date15').val() + "," + $("#date16").val() + '" title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                            }
                            else if (frequencytaken == "8") {
                                //$('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddosagetaken').val() + "  " + $('#dtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $('#frequencyTaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 55px; margin:0 5px;" readonly /><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date17').val() + "," + $('#date18').val() + "," + $("#date19").val() + "," + $("#date20").val() + '" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                                $('#medlist').append('<div class="med" style="padding: 3px; margin-top: 2px;"><input type="text" value="' + $('#idMedicineName').val() + '" name="ScheduleInfo[' + med + '].MedicineName" class="crt" style="width: 90px;  margin:0 5px 0 0;" readonly /><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].DosageTaken" value="' + $('#iddosagetaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;" readonly ></div><div style="padding-top: 2px" class="crt inline"><input type="text" name="ScheduleInfo[' + med + '].TypeOfMedicine" value="' + $('#iddtaken').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly ><input type="text" name="ScheduleInfo[' + med + '].FrequencyTaken" value="' + $("#idfrequencyTaken option:selected").text() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 80px;" readonly /></div><input type="text" value="' + $('#idquantity').val() + '" name="ScheduleInfo[' + med + '].TotalQuantity" class="crt" style="width: 30px; margin:0 5px;" readonly /><input type="text" name="ScheduleInfo[' + med + '].Reason" value="' + $('#idreason').val() + '" style="border: 1px solid #BED0DF; margin:0 5px 0 5px; width: 30px;display:none;" readonly ><label for="notify" title="Alert me!" style="padding-top: 3px; color: rgb(89,89,89);"><input name="ScheduleInfo[' + med + '].IsNotify" id="notify' + med + '" type="checkbox" value="true" />&nbsp;<h6 class="inline" style="margin:0 5px;">Notify</h6></label><input type="text" name="ScheduleInfo[' + med + '].Starttime" value="' + $('#date17').val() + "," + $('#date18').val() + "," + $("#date19").val() + "," + $("#date20").val() + '" title="Starting time" style="width: 62px;" class="crt" readonly /><div class="x inline" id="medi[' + med + ']"><img alt="x" src="/Images/delete.png" name="imgmed' + med + '"/></div></div>');
                            }

                            $('img[name*="imgmed' + med + '"]').bind('click', function () {
                                var response = confirm("Are you sure you want to delete?");
                                if (response) {
                                    if (!med <= 0) {
                                        med = med - 1;
                                    }

                                    var object = $(this).parents('div.med').nextAll('div.med');

                                    object.each(function (i, e) {
                                        var elements = $(e).find('[name*="ScheduleInfo"]');
                                        elements.each(function (j, f) {
                                            var ind = parseInt($(this).attr('name').replace(/\D/g, ''), 10) - 1;
                                            $(this).attr('name', $(this).attr('name').replace($(this).attr('name').replace(/\D/g, ''), ind));
                                        });
                                    });

                                    $(this).parents('div.med').remove();
                                    alert("Schedule deleted successfully");
                                }
                                else {
                                    return false;
                                }

                            });
                            alert("Schedule saved successfully");
                            med = med + 1;

                            //                            $("#idMedicineName,#iddosagetaken,#dtaken,#idquantity,#frequencyTaken,#idquantity,#hdnFrequenyTaken,#date1,#date2,#date3,#date4,#date5,#date6,#date7,#date8,#date9,#date10,#date11,#date12,#date13,#date14,#date15,#date16,#date17,#date18,#date19,#date20").val("");
                            //                            $("#FQ1,#FQ2,#FQ3,#FQ4,#FQ5,#FQ6,#FQ7,#FQ8").hide();

                            $(this).parents('div#schedule').hide('slide', { direction: 'down' }, 600);
                        });

                        $('input[name="slidecancel"]').click(function () {
                            $(this).parents('div#schedule').hide('slide', { direction: 'down' }, 600);
                        });



                        //sandeep added new code end here

                        $('#btnMsSave').click(function () {
                            var pdate1 = document.getElementById("pdate1").value;
                            var pdate2 = document.getElementById("pdate2").value;
                            var startdate = new Date($('#pdate1').val());
                            var enddate = new Date($('#pdate2').val());

                            if (pdate1 == '' && pdate2 == '') {
                                alert("Please select start date");
                                $('input[tabindex=1]').focus();
                                return false;
                            }
                            else if (pdate1 == '') {
                                alert("Please select start date");
                                $('input[tabindex=1]').focus();
                                return false;
                            }
                            else if (pdate2 == '') {
                                alert("Please select end date");
                                $('input[tabindex=2]').focus();
                                return false;
                            }
                            else if (startdate > enddate) {
                                alert("Please select greater than start date");
                                $('#pdate2').val("");
                                $('input[tabindex=2]').focus();
                                return false;
                            }
                            if (med == 0) {
                                alert("Please create atleast one medical schedule");
                                return false;
                            }


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
    });
    //end of EditView Add MedicalSchedule

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

                    closeHTML: "",
                    onShow: function () {

                        $('#cvisitDate3').datepicker({
                            changeYear: true,
                            changeMonth: true
                        }); // sandeep added

                        $('input[name="cancel"]').click(function () {
                            $.modal.close();
                        });
                        $('#btnVstSave').click(function () {

                            if ($("#datevisit1").val() == "") {
                                alert("Please enter date of visit");
                                $('input[tabindex=1]').focus();
                                //  return false;
                            }
                            else if ($("#reason").val() == "") {
                                alert("Please enter reason");
                                $('input[tabindex=2]').focus();
                            }
                            else {
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
                                            $('#vstlst').html(data);
                                            $.modal.close();
                                        }
                                        else {
                                            alert("Creation Failed !!");
                                        }
                                    }
                                });
                            }
                            return false;
                        });
                    }
                });
            }
        });
    });
    //end of EditView Add Visits

    //Case Edit save
    $("#btnCaseEditSave").click(function () {
        if ($("#editcasename").val() == "") {
            alert("Please enter case name");
        }
        else {

            $.ajax({
                url: "/iHealthUser/Cases/UpdateCase",
                type: 'POST',
                async: false,
                data: $(this).parents("#editCase").serialize(),
                cache: false,
                success: function (success) {
                    if (success == "1020") {

                        alert("Your modifications are updates successfully");
                        //location.reload();

                    }
                    else {
                        alert("Update failed");
                        location.reload();
                    }
                }
            });
            return false;
        }
    });
    //end of Case Edit save

    //log view in case creation
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
    //end of log view 

    //case view in case creation
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

                            $('#menus').each(function () {

                                var $active, $content, $links = $('#menus').find('a');

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
                        }
                    });
                }
            });

        }
    });



    //end of case view in case creation

    //HDinfo
    $('#hsave').click(function () {

        var hitem = '<a id="#e' + index + '" style="height:16px; padding:1px 1px"><span style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden; display: inline-block; width: 75px; font-size:10px;">' + $('input[name="HospitalName"]').val() + '</span></a><ul id="e' + index + '"><li><div class="tldiv" style="width: 35%"><h6>Hospital Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="HDInfos[' + index + '].HospitalName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="HospitalName"]').val() + '" /></div></li><li><div class="tldiv" style="width: 35%"><h6>Doctor Name</h6></div><div class="trdiv" style="width: 55%"><input type="text" name="HDInfos[' + index + '].DoctorName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#handdinfod').find('input[name="DoctorName"]').val() + '" /></div></li></ul>';

        $('<li/>').prependTo('#HDInfos').html(hitem);
        $('#HDInfos').accordion('destroy').accordion({ collapsible: true });
        $(this).parents("#handdinfod").find(':text').val('');
        index += 1;
    });
    //end of HDinfo

    $('#ad').click(function () {
        $('#handdinfod').show('slide', { direction: 'down' }, 600);
    });
    $('#handdinfod').find('#hsave').click(function () {
        $("#handdinfod").hide("slide", { direction: 'down' }, 600);
    });
    $('input[name="cancel"]').click(function () {
        $(this).parents('div.slide').hide('slide', { direction: 'down' }, 600);
    });

    //jagadeesh
    $("#s1,#s2").dropdownchecklist({ maxDropHeight: 150 }, { emptyText: "Please select ...", width: 150 });
    $('ul.accordion').accordion({ collapsible: true, heightStyle: "content" });
    //end of jagadeesh

});

