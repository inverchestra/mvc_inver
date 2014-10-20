
$(function () {
    //        $('#datevisit1').datepicker({
    //            changeYear: true,
    //            changeMonth: true
    //        });
    $('#datevisit1').datetimepicker();
    $('#nextdatevisit1').datepicker({
        changeYear: true,
        changeMonth: true,
        minDate: new Date()
    });


    //cs
    $("#Heightfeet,#Weight,#Temperature").keydown(function (event) {
        // Allow: backspace, delete, tab, escape, and enter
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 190 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
        // Allow: Ctrl+A
    (event.keyCode == 65 && event.ctrlKey == true) ||
        // Allow: home, end, left, right
    (event.keyCode >= 35 && event.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        else {
            // Ensure that it is a number and stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) || ($(this).val().length >= 5)) {
                event.preventDefault();
            }
        }
    });
    $("#Sistole,#Diastole,#Respiratoryrate,#PulseRate").keydown(function (event) {
        // Allow: backspace, delete, tab, escape, and enter
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
        // Allow: Ctrl+A
    (event.keyCode == 65 && event.ctrlKey == true) ||
        // Allow: home, end, left, right
    (event.keyCode >= 35 && event.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        else {
            // Ensure that it is a number and stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) || ($(this).val().length >= 3)) {
                event.preventDefault();
            }
        }
    });
    $("#doctorphno").keydown(function (event) {
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
    $('#doctorname,#reason,#DietPrescription').keydown(function (e) {
        if (e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 32) || (key == 9) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                e.preventDefault();
            }
        }
    });
    $('img#imgad2').click(function () {
        $('div#DoctorInfo').show('slide', { direction: 'down' }, 600);
    });
    $('input[name="cancel1"]').click(function () {
        $(this).parents('div.slide').hide('slide', { direction: 'down' }, 600);
    });
    var l = 0;
    $('#DoctorInfoSave').click(function () {

        if (($('#doctorname').val() == '') && ($('#doctorphno').val() == '') && ($('#demail').val() == '')) {
            alert("Please enter doctor information");
            return false;
        }

        var x = document.getElementById("doctorphno").value;
        if (x.length == "") {
        }
        else if (x.length < 10) {
            alert("please enter valid phone number");
            return false;
        }


        var pattern = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
        if (($('#demail').val() == '')) {
            var list1 = '<ul id="e' + l + '" class="nurse"><li>Doctor Name</li><li><input type="text" name="Visits.DoctorName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#DoctorInfo').find('input[name="DoctorName"]').val() + '" /></li><li>Doctor Phno</li><li><input type="text" name="Visits.DoctorPhoneNo" onfocus="this.blur();" class="al" value="' + $(this).parents('div#DoctorInfo').find('input[name="DoctorPhno"]').val() + '" /></li><li>Doctor Email</li><li><input type="text" name="Visits.DoctorEmail" onfocus="this.blur();" class="al" value="' + $(this).parents('div#DoctorInfo').find('input[name="DoctorEmail"]').val() + '" /></li></ul>';
            $('#DoctorInfos').append(list1);
            $('div#DoctorInfo').hide('slide', { direction: 'down' }, 600);
            $("#imgad2").hide();
        }
        else {
            if (pattern.test($('#demail').val())) {
                var list1 = '<ul id="e' + l + '" class="nurse"><li>Doctor Name</li><li><input type="text" name="Visits.DoctorName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#DoctorInfo').find('input[name="DoctorName"]').val() + '" /></li><li>Doctor Phno</li><li><input type="text" name="Visits.DoctorPhoneNo" onfocus="this.blur();" class="al" value="' + $(this).parents('div#DoctorInfo').find('input[name="DoctorPhno"]').val() + '" /></li><li>Doctor Email</li><li><input type="text" name="Visits.DoctorEmail" onfocus="this.blur();" class="al" value="' + $(this).parents('div#DoctorInfo').find('input[name="DoctorEmail"]').val() + '" /></li></ul>';
                $('#DoctorInfos').append(list1);
                $('div#DoctorInfo').hide('slide', { direction: 'down' }, 600);
                $("#imgad2").hide();
                return false;
            }
            else {
                alert("Please enter correct Email");
                return false;
            }
        }
    });




    $('img#imgadd1').click(function () {
        $('div#NRInfo').show('slide', { direction: 'down' }, 600);
    });
    $('input[name="cancel1"]').click(function () {
        $(this).parents('div.slide').hide('slide', { direction: 'down' }, 600);
    });
    var k = 0;
    $('#NRInfoSave').click(function () {

        if (($('#Heightfeet').val() == '') && ($('#Weight').val() == '') && ($('#Temperature').val() == '') && ($('#Sistole').val() == '') && ($('#Respiratoryrate').val() == '') && ($('#PulseRate').val() == '')) {
            alert("Please enter any nurse readings");
            return false;
        }
        else {
            var list1 = '<ul id="e' + k + '" class="nurse"><li>Height</li><li><input type="text" name="visits.NurseReadingsInfo.Height" onfocus="this.blur();" class="al" value="' + $(this).parents('div#NRInfo').find('input[name="Height"]').val() + '" /></li><li>Weight</li><li><input type="text" name="visits.NurseReadingsInfo.Weight" onfocus="this.blur();" class="al" value="' + $(this).parents('div#NRInfo').find('input[name="Weight"]').val() + '" /></li><li>Temparature</li><li><input type="text" name="visits.NurseReadingsInfo.Temparature" onfocus="this.blur();" class="al" value="' + $(this).parents('div#NRInfo').find('input[name="Temparature"]').val() + '" /></li><li>BloodPressure</li><li><input type="text" name="visits.NurseReadingsInfo.Weight" onfocus="this.blur();" class="al" value="' + $(this).parents('div#NRInfo').find('input[name="Sistole"]').val() + "/" + $('#Diastole').val() + '"/></li><li>Respiratoryrate</li><li><input type="text" name="visits.NurseReadingsInfo.Respiratoryrate" onfocus="this.blur();" class="al" value="' + $(this).parents('div#NRInfo').find('input[name="Respiratoryrate"]').val() + '" /></li><li>PulseRate</li><li><input type="text" name="visits.NurseReadingsInfo.PulseRate" onfocus="this.blur();" class="al" value="' + $(this).parents('div#NRInfo').find('input[name="PulseRate"]').val() + '" /><input type="hidden" name="visits.NurseReadingsInfo.Sistole" value="' + $(this).parents('div#NRInfo').find('input[name="Sistole"]').val() + '"/> <input type="hidden" value="' + $(this).parents('div#NRInfo').find('input[name="Diastole"]').val() + '" name="visits.NurseReadingsInfo.Diastole"/> </li></ul>';
            $('#NRInfos').append(list1);
            $('div#NRInfo').hide('slide', { direction: 'down' }, 600);
            $("#imgadd1").hide();
        }
    });
    $('img#imgad2').click(function () {
        $('div#doctorinfo').show('slide', { direction: 'down' }, 600);
    });
});
