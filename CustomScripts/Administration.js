$(function () {

    $('#btncreateuser').click(function () {
      //  alert("create user btn click:");
        //$.modal.close();
        $('#ajaxtab').empty();
        $.ajax({
            url: '/iHealthUser/Administration/AdminSingleRegister', //Hospital Related
            type: 'GET',
            success: function (result) {
                $.modal.close();
                $('#ajaxtab').html(result).modal({
                    escClose: true,
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto', 'width': 'auto' });
                    }
                });
            }
        });
    });

    $('#btncreateuser2').click(function () {
          alert("create user");
        $.ajax({
            url: '/iHealthUser/Administration/CreateUser',
            //data: { id: id },
            success: function (data) {
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });
                        $('#idTryIt').click(function (e) {

                            if ($(this).attr("Checked")) {

                                // $("#tbPromoCode").val("");

                                $("#chkpromocode").hide();
                                $("#tbPromoCode").hide();
                                $("#chkfreeuser").hide();

                            }
                            else {
                                $("#chkfreeuser").show();
                                $("#chkpromocode").show();
                                $("#promocode").removeAttr("checked"); //, false);
                                $("#tbPromoCode").val("");
                            }
                        });
                        $('#idFreeUser').click(function (e) {
                            if ($(this).attr("Checked")) {

                                $("#chktryIt").hide();
                                $("#chkpromocode").hide();
                                $("#tbPromoCode").hide();

                            }
                            else {
                                $("#chktryIt").show();

                                $("#chkpromocode").show();
                                $("#promocode").removeAttr("checked"); //, false);
                                $("#tbPromoCode").val("");

                            }

                        });

                        $('#promocode').click(function (e) {
                            if ($(this).attr("Checked")) {
                                $("#tbPromoCode").show();
                            }
                            else
                                $("#tbPromoCode").hide();

                        });


                        $("#btnSubmit").click(function () {
                            // debugger;
                            alert("btn submit click");
                            if (!("#rbtusngModerator").checked) {
                                if ($("#txtPassword").val() == $("#txtConfirmPassword").val()) {
                                    return true;
                                }
                                else {
                                    alert("Please Enter Correct Password");
                                    return false;
                                }
                            }
                        });
                        $('#register-form').submit(function () {

                            $.ajax({
                                url: $(this).attr('action'),
                                type: 'POST',
                                data: $(this).serialize(),
                                success: function (edited) {
                                    $.modal.close();
                                    $("#viewAudits").html(edited);
                                }
                            });
                            return false;
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

    $("#getUsersByEmail").submit(function () {

        $.ajax({
            url: $(this).attr("action"),
            data: $(this).serialize(),
            async: false,
            cache: false,
            success: function (data) {
                $.modal.close();
                $("#viewAudits").html(data);
            }

        });

        return false;
    });


    // PRASHANTH
    $('#archivePCode').click(function () {
        var id = $(this).parent('td').find('input[type="hidden"]').val();

        // alert("archivePCode btn click");
        $.ajax({

            url: "/iHealthUser/Administration/ArchivePromoCode",
            type: 'POST',
            async: false,
            cache: false,
            data: { id: id },
            success: function (data) {
                $.modal.close();
                $("#viewAudits").html(data);
            }

        });

        return false;
    });

    //loading popup for all ajax requests.
    //$(document).ajaxStart(function () {
    //    $('#ajaxtab').html('<div class="img" style="width:300px; height:50px; padding-left:150px; padding-top:30px;"><h3>Please wait...</h3></div>').modal({ closeHTML: "" });
    //});

    //Start of Edit User

    $("input[name='editUseradmin']").click(function () {
       // alert("edit btn");
        var UserId = $(this).parent('td').find('input[type="hidden"]').val();
        $.ajax({
            url: '/iHealthUser/Administration/EditUser',
            cache: false,
            data: { UserId: UserId },
            success: function (data) {
                $.modal.close();
               // alert("success");
                $('#ajaxtab').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                       // alert("success1");
                        $('input:radio[name="RegUser.IsGroupUser"][value="RegUser.IsGroupUser"]').prop('checked', true);
                      //  alert("success2");
                        e.container.css({ 'height': 'auto' });
                      //  alert("success3");
                        if ($('input[name = "IHealthUser.Relationship"]').val() == "Moderator") {
                            //alert("1");
                            $('input[name = "IHealthUser.Relationship"]').attr("readonly", true);
                        }
                      //  alert("success4");
                        //For Checkbox
                        if ($('#rbtusngModerator').attr('checked')) {

                            $("#pwd").show();
                            $("#txtPassword").attr("required", true);
                            $("#txtConfirmPassword").attr("required", true);
                        }
                        else {
                            $("#pwd").hide();
                        }
                       // alert("success5");
                        $("#rbtusngModerator").click(function () {
                            $("#EmailId").val = "";
                            $("#Password").val = "";
                            $("#pwd").toggle(this.unchecked);

                        });
                        //End of Checkbox
                        $("#btnSubmit").click(function () {
                          //  alert("btn submit2 click");

                            // debugger;
                            if (!("#rbtusngModerator").checked) {
                                if ($("#txtPassword").val() == $("#txtConfirmPassword").val()) {
                                    return true;
                                }
                                else {
                                    alert("Please Enter Correct Password");
                                    return false;
                                }
                            }
                        });

                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });

                        $('#EditForm').submit(function () {
                            $.ajax({
                                url: $(this).attr('action'),
                                type: 'POST',
                                data: $(this).serialize(),
                                success: function (data) {
                                    $.modal.close();
                                    $("#viewAudits").html(data);
                                    //                                    $('#some').html('<h2 style="padding:5px;">' + edited + '</h2>').modal();
                                    //                                    setTimeout(function () {
                                    //                                        $.modal.close();
                                    //                                    }, 3000);
                                    //                                    location.reload();
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
    //Delete User Starts here
    $("input[name='deleteUser'],input[name='suspendUser'],input[name='RevokeUser']").click(function () {//$(".delUser, .clSuspendUser, .clRevokeUser").click(function () {
        //        debugger;
        // alert("1");
        var id = $(this).parent('td').find('input[type="hidden"]').val(); //$('input[name="UsrInfo.UserId"]').val(); //$(this).find('input[type="hidden"]').val(); //$('name=["UsrInfo.UserId"]').val(); //$(this).find('input[type="hidden"]').val(); //$(".CaseIdis").val();

        var Res = $(this).val();

        if (Res == "Suspend") {
            var response = confirm("Please confirm if you really want to suspend");
        }
        else if (Res == "Revoke") {
            var response = confirm("Please confirm if you really want to revoke");
        }
        else
            var response = confirm("Please confirm if you really want to delete");
        //            debugger
        if (response) {
            $.ajax({

                url: "/iHealthUser/Administration/DelSusRevkUser",
                type: 'POST',
                async: false,
                cache: false,
                dataType: 'json',
                data: { id: id, Res: Res },
                success: function (ResultData) {
                    //alert("Success");

                    $('#ajaxtab').html(ResultData).modal({
                        escClose: true,
                        closeHTML: ""
                    });
                    var hdnSelID = $("select#SelectId").val();
                    if (hdnSelID == 2) {
                        var SelName = "Manage Users";
                    }
                    else if (hdnSelID == 3) {
                        {
                            var SelName = "Manage PromoCodes";
                        }
                    }
                    else {
                        var SelName = "Audit Reports";
                    }

                    $('#hdnSelectedItem').val(SelName);

                    $.ajax({
                        url: $("#AllList").attr("action"),
                        data: $("#AllList").serialize(),
                        async: false,
                        cache: false,
                        success: function (data) {
                            $("#viewAudits").html(data);
                        }


                    });
                    //setTimeout(function () { $.modal.close(); }, 3000);
                    //  $("#viewAudits").html(data);
                    //location.reload();

                }

            }).done(function () {
                $.modal.close();
            });
        }

    });

    //Del User Ends here


    //JQuery Script for PromoCode Starts here

    $('#createPCode').click(function () {

        $.ajax({
            url: '/iHealthUser/Administration/CreatePromoCode',
            //data: { id: id },
            success: function (data) {
                $.modal.close();
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({'height':'auto'});
                        $('#register-form').submit(function () {
                            $.ajax({
                                url: $(this).attr('action'),
                                type: 'POST',
                                data: $(this).serialize(),
                                success: function (data) {
                                    $.modal.close();
                                    $("#viewAudits").html(data).modal({
                                    //$('#ajaxtab').html(result).modal({
                                        escClose: true,
                                        closeHTML: "",
                                        onShow: function (e) {
                                            e.container.css({ 'height': 'auto', 'width': '400px' });
                                        }
                                    });
                                }
                            });
                            return false;
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



    //Start of Edit User

    $("input[name='EditPCode']").click(function () {
        // alert("1");
        var pCodeId = $(this).parent('td').find('input[type="hidden"]').val();
        //        alert(pCodeId);
        $.ajax({
            url: '/iHealthUser/Administration/EditPromoCode',
            data: { pCodeId: pCodeId },
            success: function (data) {
                $.modal.close();
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });

                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });

                        $('#Edit-form').submit(function () {
                            $.ajax({
                                url: $(this).attr('action'),
                                type: 'POST',
                                data: $(this).serialize(),
                                success: function (data) {
                                    $.modal.close();
                                    $("#viewAudits").html(data);

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

    //Delete User Starts here
    $("input[name='delPCode']").click(function () {
        var id = $(this).parent('td').find('input[type="hidden"]').val();
        //       alert(id);
        var response = confirm("Please confirm if you really want to delete");


        if (response) {
            $.ajax({

                url: "/iHealthUser/Administration/DeletePCode",
                type: 'POST',
                async: false,
                cache: false,
                data: { id: id },
                success: function (data) {

                    $("#viewAudits").html(data);
                }

            });
        }

    });

    //Del Promo Code Ends here
    //Ends here JQuery Script for PromoCode

    //For Contact Us and Feedback

    $('#btncancelcontact,#btnfcancel').click(function () {
        $.modal.close();

    });
    $('#tab1').click(function () {
        $('#tab1').removeClass("tab_static").addClass("Current");
        $('#tab2').removeClass("Current").addClass("tab_static");
        $('#Contact').show();
        $('#Feedback').hide();


    });
    $('#tab2').click(function () {
        //alert("tab2");
        $('#Feedback').show(); $('#tab2').removeClass("tab_static").addClass("Current");
        $('#tab1').removeClass("Current").addClass("tab_static");
        $('#Contact').hide();

    });

    $('#btnSubmitInfo').click(function () {
        //        alert("Created Successfully");
        //        debugger;
        $.ajax({
            url: $(this).attr('action'),
            //cache: false,
            type: 'POST',
            data: $(this).serialize(),
            async: false,
            cache: false,
            //            contentType: 'application/json;',
            // dataType: 'json',
            //async: false,
            // processData: true,
            success: function (edited) {
                //                                alert("edited");
                //                                alert(edited);
                $.modal.close();
                if (edited == "1010") {
                    alert(edited);
                }
                //                    $('#resMessage').html('<h2 style="padding:5px;">' + edited + '</h2>').modal({
                //                        onShow: function (e) {
                //                            e.container.css({ 'height': 'auto' });
                //                        }
                //                    });
                //                    setTimeout(function () {
                //                        $.modal.close();
                //                    }, 3000);
            }
            // {
            //                    alert("Created Successfully");
            //                    $.modal.close();
            //                    $('#resMessage').html(edited).modal();
            //                    //                $.modal.close();
            //                    //                //                if (edited == "1010") {
            //                    //                alert("Created Successfully");
            //                    //                }
            //                    //                else {
            //                    //                    alert("Created Failed");
            //                    //                }
            //                    //                $("#viewAudits").html(edited);
            //                }
        });
        //return false;
    });


    // User Payment Starts here
    $("input[name='usrPayment']").click(function () {//$(".delUser, .clSuspendUser, .clRevokeUser").click(function () {
        //        debugger;

        var id = $(this).parent('td').find('input[type="hidden"]').val(); //$('input[name="UsrInfo.UserId"]').val(); //$(this).find('input[type="hidden"]').val(); //$('name=["UsrInfo.UserId"]').val(); //$(this).find('input[type="hidden"]').val(); //$(".CaseIdis").val();



        $.ajax({

            url: "/iHealthUser/Administration/PaymentForm",
            type: 'POST',
            async: false,
            cache: false,
            // dataType: 'json',
            data: { id: id },
            success: function (data) {
                //                alert("From Success");
                //                 $('#some').html(data).modal();
                $('#some').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });

                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });

                        //                        $('#btnPSubmit').submit(function () {
                        //                        alert("a");
                        //                            $.ajax({
                        //                                url: $(this).attr('action'),
                        //                                type: 'POST',
                        //                                data: $(this).serialize(),
                        //                                success: function (data) {
                        //                                    $.modal.close();
                        //                                    alert("returned Data");
                        //                                    //$("#viewAudits").html(data);

                        //                                }
                        //                            });
                        //                           // return false;
                        //                        });

                    }
                });
            }

        });


    });

    //User Payment Ends here

});