/* File Created: November 20, 2013 */

!function ($) {
    $(document).ready(function () {
        $("span.close").click(function () {
            $(this).parents("li").hide();
        });


        var ua = navigator.userAgent;
        //        alert(ua);
        //Javascript Browser Detection - Android
        if (ua.indexOf("Android") >= 0) {
            var androidversion = parseFloat(ua.slice(ua.indexOf("Android") + 8));

            $("#device_alert>ul>li:nth-child(1)").show();
            //            window.open("http://market.android.com/details?id=com.ebiz.eis.bumpdocs", "_top");
            if (androidversion < 2.3) {
                // do whatever
                //alert("Your using older version of Android please update to new version");
            }
        }

        //Javascript Browser Detection - IOS
        //if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(ua))
        if (/iPhone|iPad|iPod/i.test(ua)) {
            // alert("IOS");
            $("#device_alert>ul>li:nth-child(2)").show();
            // Check if the orientation has changed 90 degrees or -90 degrees... or 0
            window.addEventListener("orientationchange", function () {
                //alert(window.orientation);
            });
        }
        //Javascript Browser detection -Windows
        if (/WPDesktop/i.test(ua)) {
            $("#device_alert>ul>li:nth-child(3)").show();
            //alert("windows");
        }
    });

    $(function () {
        $('#myCarousel').carousel();

        $('ul.nav>li>a, a.brand, p.pull-right>a').on('click', function (e) {
           // e.preventDefault();
            var target = $(this).attr('href');

            $('html,body').animate({
                scrollTop: $(target).offset().top
            }, 700);
        });

        /*parallax*/
        var $window = $(window);
        var velocity = 0.35;

        function update() {
            var pos = $window.scrollTop();
            $('#about').each(function () {
                var $element = $(this);
                var height = $element.height();
                var sum = Math.round((-pos) * velocity);

                $(this).css('backgroundPosition', '50% ' + sum + 'px');
            });
            $('#nextabout').each(function () {
                var $element = $(this);
                var height = $element.height();
                var sum = Math.round((height - pos) * 0.30);

                $(this).css('backgroundPosition', '25% ' + sum + 'px');
            });
        };
        update();
        $window.bind('scroll', update);

        $.validator.addMethod("username", function (value, element) {
            return this.optional(element) || /^(\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)|(\d{10})$/i.test(value);
        }, "Valid email or phone number");

        //modal + login form validation.
        $('#signupbtn,#signupbtn1').click(function (e) {
            e.preventDefault();

            var url = $(this).attr('data-rel');

            if (url.indexOf('#') == 0) {
                $('#res_modal').modal('open');
            } else {
                $.get(url, function (data) {
                    $('#res_modal').html(data);
                    $('#res_modal').modal().on('shown', function () {

                        $('#signup').validate({

                            rules: {
                                RUserName: {
                                    required: true,
                                    maxlength: 16
                                },
                                EmailId: {
                                    required: true,
                                    username: true,
                                    remote: '/Home/EmailVerification'

                                },
                                Password: {
                                    required: true,
                                    minlength: 10
                                },
                                ConfirmPassword: {
                                    equalTo: "#txtpassword"
                                },
                                agree: "required"
                            },
                            messages: {
                                RUserName: {
                                    required: "User name required.",
                                    maxlength: "16 characters allowed."
                                },
                                EmailId: {
                                    required: "Email or Phone required.",
                                    username: "Valid username or valid phone number.",
                                    remote: "Account already exist !"


                                },
                                Password: {
                                    required: "Password required.",
                                    minlength: "Minimum 10 characters."
                                },
                                ConfirmPassword: {
                                    equalTo: "Should match the password."
                                },
                                agree: "Please accept our policy"
                            },
                            submitHandler: function (form) {
                                var password = $(form).find('#Password');
                                var cpassword = $(form).find('#ConfirmPassword');
                                password.val($.md5(password.val())); cpassword.val($.md5(cpassword.val()));

                                form.submit();
                                $('#res_modal').modal('hide');
                            }
                        });
                    });

                });
            }
        });


    });
    var nua = navigator.userAgent;

    $("#fbsign").click(function () {

        if (nua.indexOf("Android") >= 0 || /iPhone|iPad|iPod/i.test(nua)) {
            if (nua.indexOf("Android") >= 0) {
                window.open("http://market.android.com/details?id=com.ebiz.eis.bumpdocs", "_top");
                return false;
            }
        }
    });


    if (nua.indexOf("Android") >= 0 || /iPhone|iPad|iPod/i.test(nua)) {
        $("#log-signin").click(function () {
            if (nua.indexOf("Android") >= 0) {
                window.open("http://market.android.com/details?id=com.ebiz.eis.bumpdocs", "_top");
            }

        });
    }
    else {
        $('#login').validate({

            rules: {
                EmailId: {
                    required: true,
                    username: true
                },
                Password: {
                    required: true,
                    minlength: 10
                }
            },
            messages: {
                EmailId: {
                    required: "Email or Phone required.",
                    username: "Valid email or phone no."
                },
                Password: {
                    required: "Password required",
                    minlength: "Minimum 10 characters"
                }
            },
            submitHandler: function (form) {
                //alert("thank u for using bumpdocs");

                form.submit();


            }
        });
    }

    $('#pwd').validate({

        rules: {
            password: {
                required: true,
                minlength: 10
            },
            rpassword: {
                equalTo: "#password"
            }
        },
        messages: {
            password: {
                required: "Password required.",
                minlength: "Minimum 10 characters."
            },
            rpassword: {
                equalTo: "Should match the password."
            }
        }
    });



    $('#signup').validate({

        rules: {
            RUserName: {
                required: true,
                maxlength: 16
            },
            EmailId: {
                required: true,
                username: true,
                remote: '/Home/EmailVerification'
            },
            Password: {
                required: true,
                minlength: 10
            },
            ConfirmPassword: {
                equalTo: "#txtpassword"
            }
        },
        messages: {
            RUserName: {
                required: "User name required.",
                maxlength: "16 characters allowed."
            },
            EmailId: {
                required: "Email or Phone required.",
                username: "Valid username or phone number.",
                remote: "Account already exist !"

            },
            Password: {
                required: "Password required.",
                minlength: "Minimum 10 characters."
            },
            ConfirmPassword: {
                equalTo: "Should match the password."
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });

    $('#ContactMessage').validate({

        rules: {
            ContactName: {
                required: true

            },
            ContactEmail: {
                required: true

            },
            Contactsubject: {
                required: true

            },
            ContactMessage: {
                required: true

            }
        },
        messages: {
            ContactName: {
                required: "User name required."
                // maxlength: "16 characters allowed."
            },
            ContactEmail: {
                required: "Valid Email required."
                //username: "Valid username or valid phone number."
            },
            Contactsubject: {
                required: "Subject is required."
                //minlength: "Minimum 10 characters."
            },
            ContactMessage: {
                required: "Message required."
            }
        },
        submitHandler: function (form) {
            $.ajax({
                url: '/Home/SendingMail',
                type: "POST",
                aync: false,
                cache: false,
                data: $('#ContactMessage').serialize(),
                success: function (data) {
                    console.log(data);
                    if (data != "0") {
                        alert("Mail Send Successfully");
                    }
                    else {
                        alert("Mail Send Failed !!");
                    }
                    $('#ContactMessage').get(0).reset();
                }
            });
            return false;
        }
    });

    $('div.abs > img').click(function () {
        $(this).parent('div.abs').fadeOut(500);
    });

} (window.jQuery);


var stockholm = new google.maps.LatLng(38.93387, -77.17726);
var parliament = new google.maps.LatLng(38.93387, -77.17726);

var marker;
var map;

function initialize() {
    var mapOptions = {
        zoom: 14,
        center: stockholm
    };

    map = new google.maps.Map(document.getElementById('map-canvas'),
          mapOptions);

    marker = new google.maps.Marker({
        map: map,
        draggable: true,
        animation: google.maps.Animation.DROP,
        position: parliament,
        title: "InnDocs Corporation."

    });
    google.maps.event.addListener(marker, 'click', toggleBounce);
}

function toggleBounce() {

    if (marker.getAnimation() != null) {
        marker.setAnimation(null);
    } else {
        marker.setAnimation(google.maps.Animation.BOUNCE);
    }
}

google.maps.event.addDomListener(window, 'load', initialize);

