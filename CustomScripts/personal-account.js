$(function () {

    $.ajaxSetup({
        cache: false,
        async: false
    });

    //For Change password

    $('table#thead>tbody>tr>td>input[name="chpwd"]').click(function () {
        var UserId = $(this).parent('td').find('input[type="hidden"]').val();
        $.ajax({
            url: '/iHealthUser/AccountSettings/ChangePassword',
            data: { UserId: UserId },
            success: function (data1) {
                $('#some').html(data1).modal({
                    closeHTML: "",
                    onShow: function (k) {
                        k.container.css({ 'height': 'auto' });
                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });

                        $('#btnChange').click(function () {
                            var UserId = $('#UserId').val();
                            var oldPassword = $('#txtOldPassword').val();
                            var newPassword = $('#txtNewPassword').val();
                            $.ajax({
                                url: '/iHealthUser/AccountSettings/ChangePassword',
                                async: false,
                                cache: false,
                                type: 'POST',
                                dataType: 'json',
                                data: { UserId: UserId, oldPassword: oldPassword, newPassword: newPassword },
                                success: function (changed) {
                                    $('#some').html('<h2 style="padding:5px;">' + changed + '</h2>').modal({
                                        onShow: function (e) { e.container.css({ 'height': 'auto' }); }
                                    });
                                    setTimeout(function () {
                                        $.modal.close();
                                    }, 3000);
                                }
                            });
                            return false;
                        });

                    }
                });
            }

        });
        return false;
    });

    //End of change password

    //Start of Edit User

    $('input[name="editUser"]').click(function () {
        //alert("1");
        var UserId = $(this).parent('td').find('input[type="hidden"]').val();
        $.ajax({
            url: '/iHealthUser/AccountSettings/EditUser',
            data: { UserId: UserId },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });

                        //For Checkbox
                        if ($('#rbtusngModerator').attr('checked')) {
                            $("#pwd").hide();
                        }
                        else {
                            $("#pwd").show();
                        }

                        $("#rbtusngModerator").click(function () {
                            $("#EmailId").val = "";
                            $("#Password").val = "";
                            $("#pwd").toggle(this.unchecked);

                        });
                        //End of Checkbox

                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });

                        $('#EditForm').submit(function () {
                            $.ajax({
                                url: $(this).attr('action'),
                                type: 'POST',
                                data: $(this).serialize(),
                                success: function (edited) {
                                    $('#some').html('<h2 style="padding:5px;">' + edited + '</h2>').modal();
                                    setTimeout(function () {
                                        $.modal.close();
                                        location.reload();
                                    }, 3000);
                                }
                            });
                            return false;
                        });

                    }
                });
            }
        });
        return false;
    });

    //End of Edit
    //Added by AD
    $('#Disable').click(function () {
        alert("You have Exceeded doula Creation");
    });

    //End
    //Start of Create User

    $('#mod').click(function () {
        var id = $('#hdnUserID').val();

        $.ajax({
            url: '/iHealthUser/AccountSettings/CreateSubUser',
            data: { id: id },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });

                        //                        $("#rbtusngModerator").click(function () {
                        //                            $("#EmailId").val("");
                        //                            $("#Password").val("");
                        //                            $("#pwd").toggle();
                        //                        });
                        $("#btnSubmit").click(function () {
                            // debugger;
                            if (!("#chkusngModerator").checked) {
                                if ($("#Password").val() == $("#txtConfirmPassword").val()) {
                                    return true;
                                }
                                else {
                                    //                                    alert("Please Enter Correct Password");
                                    return false;
                                }
                            }
                        });

                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });

                    }
                });
            }
        });

        return false;
    });

    //End of Create User

    // sandeep added new code start here on 18-09-2013

    $('#btnVerify').click(function () {
        // $.modal.close();
        var UserId = $('#hdnUserID').val();
        // alert("User Id: " + UserId);
        //$('#ajaxtab').empty();
        $.ajax({
            url: '/iHealthUser/AccountSettings/VerifyPhone',
            //type: 'POST',
            data: { UserId: UserId },
            success: function (result) {
                //$.modal.close();
                $('#ajaxtab').html(result).modal({
                    //escClose: true,
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto', 'clear': 'both' });

                        $("#btnSubmitCode").click(function () {

                            var otacode = document.getElementById("txtOTACode").value;
                            if (otacode == "") {
                                alert("Please enter confirmation code");
                                $('input[tabindex=3]').focus();
                                return false;
                            }

                            var a = $("#frmVerifyOTACode").serialize();
                            $.ajax({
                                url: "/iHealthUser/AccountSettings/SaveVerifyPhone",
                                type: "POST",
                                async: false,
                                cache: false,
                                data: a,
                                success: function (result) {
                                    $.modal.close();
                                    $('#ajaxtab').html("<h3 style='padding: 30px;width:200px;word-wrap:break-word'>" + result + "</h3>").modal({
                                        escclose: true,
                                        onShow: function (e) {
                                            closeHTML: "",
                                            e.container.css({ 'height': 'auto', 'width': 'auto' });
                                        }
                                    });

                                    setTimeout(function () {
                                        // window.location = "/";
                                        location.reload();
                                    }, 3000);
                                }
                            });
                        });
                    }
                });
            }
        });
    });


    $("#btnEmailVerify").click(function () {
        var UserId = $("#hdnUserID").val();
        // alert(UserId);
        $.ajax({
            url: '/iHealthUser/AccountSettings/VerifyEmail',
            data: { UserId: UserId },
            success: function (result) {
                $('#ajaxtab').html(result).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto', 'clear': 'both' });
                        $("#btnSubmitEmail").click(function () {
                            //  alert("verify click");
                            var a = $("#frmVerifyEmail").serialize();
                            $.ajax({
                                url: '/iHealthUser/AccountSettings/SaveVerifyEmail',
                                type: 'POST',
                                data: a,
                                success: function (result) {
                                    $.modal.close();
                                    $('#ajaxtab').html("<h3 style='padding: 30px;width:200px;word-wrap:break-word'>" + result + "</h3>").modal({
                                        escclose: true,
                                        onShow: function (e) {
                                            closeHTML: "",
                                            e.container.css({ 'height': 'auto', 'width': 'auto' });
                                        }
                                    });

                                    setTimeout(function () {
                                        // window.location = "/";
                                        location.reload();
                                    }, 3000);
                                }
                            });
                        });

                    }
                });

            }
        });

    });



    $("#tipsendingmail").change(function () {
        var Userid = $('#hdnUserID').val();
        var cvalue = $(this).is(':checked');
        // alert(cvalue);
        $.ajax({
            url: '/iHealthUser/AccountSettings/TipSendinGmail',
            data: { UserId: Userid, cvalue: cvalue },
            success: function (result) {
            }
        });
    });


    $("#dailyendingmail").change(function () {
        var Userid = $('#hdnUserID').val();
        var cvalue = $(this).is(':checked');
        $.ajax({
            url: '/iHealthUser/AccountSettings/DailySendinGmail',
            data: { UserId: Userid, cvalue: cvalue },
            success: function (result) {
            }
        });
    });


    $("#dailyendingsms").change(function () {
        var Userid = $('#hdnUserID').val();
        var cvalue = $(this).is(':checked');
        $.ajax({
            url: '/iHealthUser/AccountSettings/DailySendinSms',
            data: { UserId: Userid, cvalue: cvalue },
            success: function (result) {
            }
        });
    });

    $("#emailcheck").change(function () {
        var Userid = $('#hdnUserID').val();
        var cvalue = $(this).is(':checked');
        var type = "mail";
        $.ajax({
            url: '/iHealthUser/AccountSettings/ChangePreferedby',
            data: { UserId: Userid, cvalue: cvalue, type: type },
            success: function (result) {
            }
        });
    });



    $("#emailcheck").change(function () {
        var Userid = $('#hdnUserID').val();
        var cvalue = $(this).is(':checked');
        var type = "mail";
        $.ajax({
            url: '/iHealthUser/AccountSettings/ChangePreferedby',
            data: { UserId: Userid, cvalue: cvalue, type: type },
            success: function (result) {
            }
        });
    });
    $("#phonecheck").change(function () {
        var UserId = $('#hdnUserID').val();
        var cvalue = $(this).is(':checked');
        var type = "phone";
        //  alert(type);
        $.ajax({
            url: '/iHealthUser/AccountSettings/ChangePreferedby',
            data: { UserId: UserId, cvalue: cvalue, type: type },
            success: function (result) {
            }
        });

    });
    window.onload = function () {


        var tipmail = $('#hdtipmail').attr("checked", true).val().toLowerCase();
        if (tipmail == "true") {
            $('#tipsendingmail').prop('checked', true);
        }
        else {
            $('#tipsendingmail').prop('checked', false);
        }

        var dailymail = $('#hddailymail').attr("checked", true).val().toLowerCase();
        if (dailymail == "true") {
            $('#dailyendingmail').prop('checked', true);
        }
        else {
            $('#dailyendingmail').prop('checked', false);
        }

        var dailysms = $("#hddailysms").attr("checked", true).val().toLowerCase();
        if (dailysms == "true") {
            $("#dailyendingsms").prop('checked', true);
        }
        else {
            $("#dailyendingsms").prop('checked', false);
        }


        //alert("Hi");
        // alert($("#hdpreferedBy").val());
        var p = $("#hdpreferedBy").val();
        if (p == 1) {
            $('#emailcheck').prop('checked', true);
        }
        else if (p == 2) {
            $('#phonecheck').prop('checked', true);
        }
        else if (p == 3) {
            $('#emailcheck').prop('checked', true);
            $('#phonecheck').prop('checked', true);
        }
        else {
            $('#emailcheck').prop('checked', false);
            $('#phonecheck').prop('checked', false);
        }

    }

    $('#btnResendOTA').click(function () {
        // $.modal.close();
        var UserId = $('#hdnUserID').val();

        $.ajax({
            url: '/iHealthUser/AccountSettings/ResendOtaCode',
            type: 'POST',
            data: { UserId: UserId },
            success: function (result) {
                $('#ajaxtab').html("<h3 style='padding: 30px;width:200px;word-wrap:break-word'>" + result + "</h3>").modal({
                    escclose: true,
                    onShow: function (e) {
                        closeHTML: "",
                    e.container.css({ 'height': 'auto', 'width': 'auto' });
                    }
                });

                setTimeout(function () {
                    // window.location = "/";
                    location.reload();
                }, 3000);
            }
        });
    });

    $("#btnEditModerator").click(function () {

        var UserId = $('#hdnUserID').val();

        $.ajax({
            url: '/iHealthUser/AccountSettings/EditUserInfo',
            data: { UserId: UserId },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });

                        $("#btnSubmitCode").click(function () {

                            var chkvalue = $('#checkBox').is(':checked');



                            var a = $("#frmEditUser").serialize();
                            $.ajax({
                                url: '/iHealthUser/AccountSettings/SaveEditUserInfo',
                                type: "POST",
                                async: false,
                                cache: false,
                                data: a,
                                success: function (data) {
                                    //$('#some').html(data).modal({
                                    //closeHTML: "",
                                    // onShow: function (e) {
                                    //e.container.css({ 'height': 'auto' });
                                    $.modal.close();
                                    $('#ajaxtab').html("<h3 style='padding: 30px;width:200px;word-wrap:break-word'>" + data + "</h3>").modal({
                                        escclose: true,
                                        onShow: function (e) {
                                            closeHTML: "",
                                            e.container.css({ 'height': 'auto', 'width': 'auto' });
                                        }
                                    });

                                    setTimeout(function () {
                                        // window.location = "/";
                                        location.reload();
                                    }, 3000);

                                    //}
                                    //});
                                }
                            });
                        });
                    }
                });
            }
        });

    });

    $("#btnSubmitCode").click(function () {

        var UserId = $('#hdnUserID').val();

        $.ajax({
            url: '/iHealthUser/AccountSettings/EditUserInfo',
            data: { UserId: UserId },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });
                    }
                });
            }
        });
        return false;
    });

    $("#btnResendConfirmMail").click(function () { // sandeep added new jquery function on 26-09-2013
        var userid = $('#hdnUserID').val();

        $.ajax({
            url: '/iHealthUser/AccountSettings/ResendMail',
            type: "POST",
            data: { UserID: userid },
            success: function (result) {
                $("#ajaxtab").html("<h3 style='padding: 30px;width:200px;word-wrap:break-word'>" + result + "</h3>").modal({
                    escclose: true,
                    onShow: function (e) {
                        closeHTML: "",
                    e.container.css({ 'height': 'auto', 'width': 'auto' });
                    }
                });

                setTimeout(function () {
                    location.reload();
                }, 3000);
            }
        });

    });


    // sandeep added new code end here on 18-09-2013

    //Start of Create Doula User sandeep added on 28-2-2014

    $('#addDoula').click(function () {
        var id = $('#hdnUserID').val();
        //alert("userid is:" + id);

        $.ajax({
            url: '/iHealthUser/AccountSettings/CreateDoulaUser',
            data: { id: id },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        $("#AddDoulaUser").validate({
                            rules: {
                                FirstName: {
                                    required: true,
                                    maxlength: 16
                                },
                                LastName: {
                                    required: true,
                                    maxlength: 16
                                },
                                Email: {
                                    required: true,
                                    email: true,
                                    //username: true,
                                    remote: '/iHealthUser/AccountSettings/EmailVerification'
                                }
                            },
                            messages: {
                                FirstName: {
                                    required: "Enter first name",
                                    maxlength: "Please enter maximum 16 letters"
                                },
                                LastName: {
                                    required: "Enter last name",
                                    maxlength: "Please enter maximum 16 letters"
                                },
                                Email: {
                                    required: "Email required",
                                    //username: "Enter valid email",
                                    email: "Enter valid email",
                                    remote: "Email already exist !"

                                }
                            },

                            submitHandler: function (form) {
                                //alert("hi");
                                form.submit();
                            }

                        });

                        e.container.css({ 'height': 'auto' });


                    }
                });
            }
        });

        return false;
    });

    //End of Create Doula User sandeep added on 28-2-2014

    // View Doula USer Start here

    $('input[name="viewDoulaUser"]').click(function () {
        //alert("1");
        var UserId = $(this).parent('td').find('input[type="hidden"]').val();
        var iHealthUser = $('#hdnUserID').val();
        $.ajax({
            url: '/iHealthUser/AccountSettings/ViewDoulaUser',
            data: { UserId: UserId, iHealthUser: iHealthUser },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });

                        $('#btnSubmit').hide();

                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });
                    }
                });
            }
        });
        return false;
    });

    // View Doula user end here

    // Delete Doula User start here

    $('#deleteDoula').click(function () {
        //debugger;
        var tr = $('#thead tbody tr').filter(':has(:checkbox:checked)');
        var id = tr.find('input[type="hidden"]').val();
        var iHealthUser = $('#hdnUserID').val();
        var chkids = "";

        tr.each(function (i, e) {
            chkids = chkids + $(this).find(':hidden').val() + ",";
        });

        if (tr.size() < 1) {
            alert("Please select atleast one Doula");
        }
        else {
            if (chkids != null) {
                var response = confirm("Do you want to delete the Doula");
                if (response) {

                    $.ajax({
                        url: '/iHealthUser/AccountSettings/DeleteDoulaUser',
                        async: false,
                        cache: false,
                        type: "POST",
                        data: { UserId: chkids, iHealthUserId: iHealthUser },
                        success: function (success) {
                            if (success == "1030") {
                                alert("Doula deleted successfully");
                            }
                            else if (success == "1032") {
                                alert("Some Doula deletion failed");
                            }

                            else {
                                alert("Doula deletion failed");
                            }

                            location.reload();
                        }
                    });
                }
                else {
                    return false;
                }
            }
        }
    });

    // Delete Doula user end here

    $("#btnAddEmail").click(function () {

        var UserId = $('#hdnUserID').val();
        // alert("Add email click");


        $.ajax({
            url: '/iHealthUser/AccountSettings/AddEmail',
            data: { UserId: UserId },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });

                        $("#btnaddemail").click(function () {
                            var emailid = $('#txtEmail').val();
                            // alert("Email is "+emailid);

                            if (emailid == "") {
                                alert("please enter email address");
                                $('input[tabindex=4]').focus();
                                return false;
                            }
                            else if (emailid) {
                                // alert("emailid");
                                if (emailid.search(/(\s|^)[a-zA-Z0-9_\.\-]+\@([a-zA-Z0-9\-]+\.)+[a-zA-Z0-9]{2,4}(\s|$)/) == -1) {
                                    error = 'Please Enter Valid Email address.';
                                    var success = false;
                                    alert(error);
                                    return false;
                                }
                            }

                            var a = $("#frmAddEmail").serialize();
                            $.ajax({
                                url: '/iHealthUser/AccountSettings/AddEmail',
                                type: "POST",
                                async: false,
                                cache: false,
                                data: a,
                                success: function (data) {

                                    $.modal.close();
                                    $('#ajaxtab').html("<h3 style='padding: 30px;width:200px;word-wrap:break-word'>" + data + "</h3>").modal({
                                        escclose: true,
                                        onShow: function (e) {
                                            closeHTML: "",
                                            e.container.css({ 'height': 'auto', 'width': 'auto' });
                                        }
                                    });

                                    setTimeout(function () {
                                        location.reload();
                                    }, 3000);

                                }
                            });
                        });
                    }
                });
            }
        });


    });





    $("#btnAddMobile").click(function () {

        var UserId = $('#hdnUserID').val();
        // alert("Add mobile  click");


        $.ajax({
            url: '/iHealthUser/AccountSettings/AddMobileNumber',
            data: { UserId: UserId },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });

                        $("#btnaddemail").click(function () {
                            var phnno = $('#txtPhoneNo').val();
                            // alert("mobileno is " + phnno);

                            if (phnno == "") {
                                alert("please enter phone number");
                                $('input[tabindex=1]').focus();
                                return false;
                            }
                            else if (phnno) {
                                // alert("emailid");
                                // if (phnno.search(/(\s|^)(?:\([2-9]\d{2}\)\ ?|[2-9]\d{2}(?:\-?|\ ?))[2-9]\d{2}[- ]?\d{4}(\s|$)/) == -1) {
                                if (phnno.search(/^[(]{0,1}[0-9]{3}[)\.\- ]{0,1}[0-9]{3}[\.\- ]{0,1}[0-9]{4}$/) == -1) {
                                    error = 'Please Enter Valid phone number.';
                                    alert(error);
                                    return false;
                                }
                            }

                            var a = $("#frmAddMobilenumber").serialize();
                            $.ajax({
                                url: '/iHealthUser/AccountSettings/AddMobileNumber',
                                type: "POST",
                                async: false,
                                cache: false,
                                data: a,
                                success: function (data) {

                                    $.modal.close();
                                    $('#ajaxtab').html("<h3 style='padding: 30px;width:200px;word-wrap:break-word'>" + data + "</h3>").modal({
                                        escclose: true,
                                        onShow: function (e) {
                                            closeHTML: "",
                                            e.container.css({ 'height': 'auto', 'width': 'auto' });
                                        }
                                    });

                                    setTimeout(function () {
                                        location.reload();
                                    }, 3000);

                                }
                            });
                        });
                    }
                });
            }
        });


    });
    $("#btnDeleteMobile").click(function () {
        var UserId = $('#hdnUserID').val();

        $.ajax({
            url: '/iHealthUser/AccountSettings/DeleteMobileNumber',
            data: { UserId: UserId },
            success: function (data) {
            }
        });
        setTimeout(function () {
            location.reload();
        }, 3000);

    });
    $("#btnEditMobile").click(function () {
        var UserId = $('#hdnUserID').val();
        $.ajax({
            url: '/iHealthUser/AccountSettings/AddMobileNumber',
            data: { UserId: UserId },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });
                        var oldNo = $('#txtPhoneNo').val();

                        $("#btnaddemail").click(function () {
                            var phnno = $('#txtPhoneNo').val();
                            
                            if (phnno == oldNo) {
                                alert("please enter another phone number  ");
                                $('input[tabindex=1]').focus();
                                return false;
                            }
                            if (phnno == "") {
                                alert("please enter phone number");
                                $('input[tabindex=1]').focus();
                                return false;
                            }

                            else if (phnno) {
                                // alert("emailid");
                                // if (phnno.search(/(\s|^)(?:\([2-9]\d{2}\)\ ?|[2-9]\d{2}(?:\-?|\ ?))[2-9]\d{2}[- ]?\d{4}(\s|$)/) == -1) {
                                if (phnno.search(/^[(]{0,1}[0-9]{3}[)\.\- ]{0,1}[0-9]{3}[\.\- ]{0,1}[0-9]{4}$/) == -1) {
                                    error = 'Please Enter Valid phone number.';
                                    alert(error);
                                    return false;
                                }


                            }

                            var a = $("#frmAddMobilenumber").serialize();
                            $.ajax({
                                url: '/iHealthUser/AccountSettings/AddMobileNumber',
                                type: "POST",
                                async: false,
                                cache: false,
                                data: a,
                                success: function (data) {

                                    $.modal.close();
                                    $('#ajaxtab').html("<h3 style='padding: 30px;width:200px;word-wrap:break-word'>" + data + "</h3>").modal({
                                        escclose: true,
                                        onShow: function (e) {
                                            closeHTML: "",
                                            e.container.css({ 'height': 'auto', 'width': 'auto' });
                                        }
                                    });

                                    setTimeout(function () {
                                        location.reload();
                                    }, 3000);

                                }
                            });
                        });
                    }
                });
            }
        });


        $("#btnAddMobile").click(function () {

            var UserId = $('#hdnUserID').val();
            // alert("Add mobile  click");


            $.ajax({
                url: '/iHealthUser/AccountSettings/AddMobileNumber',
                data: { UserId: UserId },
                success: function (data) {
                    $('#some').html(data).modal({
                        closeHTML: "",
                        onShow: function (e) {
                            e.container.css({ 'height': 'auto' });

                            $("#btnaddemail").click(function () {
                                var phnno = $('#txtPhoneNo').val();
                                // alert("mobileno is " + phnno);

                                if (phnno == "") {
                                    alert("please enter phone number");
                                    $('input[tabindex=1]').focus();
                                    return false;
                                }
                                else if (phnno) {
                                    // alert("emailid");
                                    // if (phnno.search(/(\s|^)(?:\([2-9]\d{2}\)\ ?|[2-9]\d{2}(?:\-?|\ ?))[2-9]\d{2}[- ]?\d{4}(\s|$)/) == -1) {
                                    if (phnno.search(/^[(]{0,1}[0-9]{3}[)\.\- ]{0,1}[0-9]{3}[\.\- ]{0,1}[0-9]{4}$/) == -1) {
                                        error = 'Please Enter Valid phone number.';
                                        alert(error);
                                        return false;
                                    }
                                }

                                var a = $("#frmAddMobilenumber").serialize();
                                $.ajax({
                                    url: '/iHealthUser/AccountSettings/AddMobileNumber',
                                    type: "POST",
                                    async: false,
                                    cache: false,
                                    data: a,
                                    success: function (data) {

                                        $.modal.close();
                                        $('#ajaxtab').html("<h3 style='padding: 30px;width:200px;word-wrap:break-word'>" + data + "</h3>").modal({
                                            escclose: true,
                                            onShow: function (e) {
                                                closeHTML: "",
                                            e.container.css({ 'height': 'auto', 'width': 'auto' });
                                            }
                                        });

                                        setTimeout(function () {
                                            location.reload();
                                        }, 3000);

                                    }
                                });
                            });
                        }
                    });
                }
            });


        });


    });
});
$(function () {
    $("#btndownloadmed").click(function () {
        //  alert("hi ");
        //            $.ajax({
        //                url: '/iHealthUser/Medwall/DownloadMedwall',
        //                type: 'get',
        //                success: function () {
        //                }
        //            });
        $.fileDownload('/iHealthUser/Medwall/DownloadMedwall');
    });
});