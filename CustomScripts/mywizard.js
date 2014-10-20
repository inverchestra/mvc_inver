(function ($) {

    $.fn.mywizard = function () {

        var alldivs = $('div#wizard');
        var childdivs = alldivs.children('div');

        $(childdivs).not(':eq(0)').hide();

        $(".addbutton").click(function () {

            if ($('#txtmessage').val() == "") {
                return;
            }
            else {
                var index = $(this).parent('div').index();
                $(childdivs).eq(index).hide();
                $(childdivs).eq(index + 1).show();
            }

        });
        $(".prev").click(function () {

            //var index = $(this).parent('div').index();
            var index = $(this).parent('div[id^="step"]').index();
            $(childdivs).eq(index).hide();
            $(childdivs).eq(index - 1).show();

        });
        $('#btnContactSubmit').click(function () {

            $.ajax({
                url: '/iHealthUser/AccountSettings/InsertContact',
                data: $('#frmWizard').serialize(),
                type: "POST",
                async: false,
                success: function (data) {
                    if (data != null) {
                        $('#wizard').css("display", "none");
                        $('#resultDiv').css("display", "block");

                        if (data[1] == "Praise") {
                            $('#resultDiv h1').text("Message submitted successfully!");
                            $('#resultDiv p').eq(0).text("Thank you for your message.");
                        }
                        else if (data[1] == "Support Request") {
                           
                            $('#resultDiv h1').text("Message submitted successfully!");
                            //$('#resultDiv p').text("Request number :" + data[0] + ", We will get back to you soon.");
                            $('#resultDiv p').eq(0).text("Request number : " + data[0] + "");
                            $('#resultDiv p').eq(1).text("We will get back to you soon.");
                        }
                        else if (data[1] == "Sales Request") {
                            $('#resultDiv h1').text("Message submitted successfully!");
                            $('#resultDiv p').eq(0).text("Thank you for your feedback, we will touch base with you");
                        }
                        else if (data[1] == "Feedback") {
                            $('#resultDiv h1').text("Message submitted successfully!");
                            $('#resultDiv p').eq(0).text("Thank you for your sales request, we will get back to you soon");
                        }
                    }
                }
            });
            return false;
        });
        $('input[name="btnclose"]').click(function () {
            $('#wizard').css("display", "none");
        });
        $('input[name="btnresclose"]').click(function () {
            $('#resultDiv').css("display", "none");
        });

        $('#frmWizard').validate({
            rules: {
                txtmessage: { required: true }

            },
            messages: {

                txtmessage: {
                    required: "Please fill text box"
                }
            },
            submitHandler: function (form) {
                if (form.valid()) {
                    return true;
                }
                else {
                    return false;
                }

            }
        });

    }


} (jQuery));
