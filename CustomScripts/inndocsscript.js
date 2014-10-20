$(document).ready(function () {

    $.ajaxSetup({
        cache: false,
        async: false
    });

    //$(document).ajaxStart(function () {
    //    $('#ajaxtab').html('<div class="img" style="width:300px; height:50px; padding-left:200px; padding-top:30px;"><h3>Please wait...</h3></div>').modal();
    //});

    var changeYearButtons = function () {
        setTimeout(function () {

            var widgetHeader = inputDate.datepicker("widget").find(".ui-datepicker-header");
            //you can opt to style up these simple buttons tho
            var prevYrBtn = $('<button title="PrevYr">&lt;&lt; Prev Year</button>');
            prevYrBtn.bind("click", function () {
                $.datepicker._adjustDate(inputDate, -1, 'Y');
            });
            var nextYrBtn = $('<button title="NextYr">Next year &gt;&gt;</button>');
            nextYrBtn.bind("click", function () {
                $.datepicker._adjustDate(inputDate, +1, 'Y');

            });
            prevYrBtn.appendTo(widgetHeader);
            nextYrBtn.appendTo(widgetHeader);

        }, 1);
    };

    var dates = $("#DOB").datepicker({
        beforeShow: changeYearButtons,
        onChangeMonthYear: changeYearButtons
    });

    $('#feedbackIMG').click(function () {


        //        $('#Feedback').hide();
        $.ajax({
            url: '/Home/feedback',
            type: "GET",
            async: false,
            success: function (data) {
                $('#ajaxtab').html(data).modal({
                    escClose: true,
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });
                        $('#Contact').show();
                        $('#Feedback').hide();
                        $('#tab2').removeClass("Current").addClass("tab_static");

                    }

                });
            }
        });
    });

    $('input#btnSignup2').click(function () {

        $.modal.close();
        $('#ajaxtab').empty();
        $.ajax({
            url: '/Home/SingleRegister',  //Hospital Related
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

    $('#fp').click(function () {
       // alert("hi");
        //  $.modal.close();
        $('#ajaxtab').empty();
        $.ajax({
            url: '/Home/ForgotPassword',
            type: 'GET',
            success: function (result) {
                alert(result);
                $.modal.close();
                $('#ajaxtab').html(result).modal({
                    escClose: true,
                    closeHTML: "",
                    onShow: function (e) {
                        $.modal.close();
                        e.container.css({ 'height': 'auto', 'clear': 'both' });
                    }
                });
                //                $('#reset').click(function () {
                //                    alert("hi");
                //                    //var userid = $('#UserId').val();
                //                    var userid = $('input[name="UserId"').val();
                //                    alert(userid);
                //                    var a = $('#subscription_order_form').serialize();
                //                    $.ajax({
                //                        type: 'post',
                //                        url: '/ChangeNewPassword/ChangeNewPassword',
                //                        data: a,
                //                        success: function (result) {
                //                            $('#ajaxtab').html("<h6 style='padding: 30px;width:200px;word-wrap:break-word'>" + result + "</h6>").modal({
                //                                onShow: function (e) {
                //                                    e.container.css({ 'height': 'auto', 'width': 'auto' });
                //                                }
                //                            });

                //                            setTimeout(function () {
                //                                // window.location = "/";
                //                                location.reload();
                //                            }, 3000);
                //                        }
                //                    });
                //                });
            }
        });
    });
    $('#ca').click(function () {

        // $.modal.close();
        $('#ajaxtab').empty();
        $.ajax({
            url: '/ConfirmAccount/ConfirmAccountByMobile',
            type: 'GET',
            success: function (result) {
                $.modal.close();
                $('#ajaxtab').html(result).modal({
                    escClose: true,
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto', 'clear': 'both' });
                    }
                });
            }
        });
    });

    $('#ca1').click(function () {

        // $.modal.close();
        $('#ajaxtab').empty();
        $.ajax({
            url: '/Home/ResendOtaCode',
            type: 'GET',
            success: function (result) {
                $.modal.close();
                $('#ajaxtab').html(result).modal({
                    escClose: true,
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto', 'clear': 'both' });
                    }
                });
            }
        });
    });

});

$.validator.addMethod("username", function (value, element) {
    return this.optional(element) || /^(\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)|(\d{10})$/i.test(value);
}, "Please enter either email or phone number");

$('#signup1').validate({

    rules: {
        RUserName: {
            required: true,
            maxlength: 16
        },
        EmailId: {
            required: true,
            username: true
        },
        Password: {
            required: true,
            minlength: 10
        },
        ConfirmPassword: {
            equalTo: "#Password"
        }
    },
    messages: {
        RUserName: {
            required: "Please enter your name.",
            maxlength: "Only 16 characters are allowed."
        },
        EmailId: {
            required: "Please enter username.",
            username: "Valid username or valid phone number."
        },
        Password: {
            required: "Please enter your password.",
            minlength: "Minimum 10 characters."
        },
        ConfirmPassword: {
            equalTo: "Should match the above password."
        }
    },
    submitHandler: function (form) {
        var password = $(form).find('#Password');
        var cpassword = $(form).find('#ConfirmPassword');
        password.val($.md5(password.val())); cpassword.val($.md5(cpassword.val()));

        form.submit();

    }
});