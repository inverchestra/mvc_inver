/* File Created: July 19, 2013 */

$(function () {

    var x = document.getElementById("contactnumber").value;
    if (x.length == "") {
    }
    else if (x.length < 10) {
        alert("please enter valid phone number");
        return false;
    }
    var pattern = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
    if (($('#email').val() == '')) {
        var list1 = '<ul id="e' + l + '" class="nurse"><li>Doctor Name</li><li><input type="text" name="Visits.DoctorName" onfocus="this.blur();" class="al" value="' + $(this).parents('div#DoctorInfo').find('input[name="DoctorName"]').val() + '" /></li><li>Doctor Phno</li><li><input type="text" name="Visits.DoctorPhoneNo" onfocus="this.blur();" class="al" value="' + $(this).parents('div#DoctorInfo').find('input[name="DoctorPhno"]').val() + '" /></li><li>Doctor Email</li><li><input type="text" name="Visits.DoctorEmail" onfocus="this.blur();" class="al" value="' + $(this).parents('div#DoctorInfo').find('input[name="DoctorEmail"]').val() + '" /></li></ul>';
        $('#DoctorInfos').append(list1);
        $('div#DoctorInfo').hide('slide', { direction: 'down' }, 600);
        $("#imgad2").hide();
    }
    else {
        if (pattern.test($('#email').val())) {
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