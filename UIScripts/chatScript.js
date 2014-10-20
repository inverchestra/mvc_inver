(function ($) {

    //All users
    $.fn.getAllUsers = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetAllUsers',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                $('#gcount').text(data.gestCount);
                $('#ccount').text(data.CountryCount);
                $('#icount').text(data.interestscount);
                $('#pcount').text(data.pincodeCount);

                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                        $('.Prev').prop({ "data-rel": pageNo });
                        $('.Prev').css("cursor", "pointer");

                    }

                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });

                        $('.Next').css("cursor", "pointer");
                    }



                }


            }

        });
        return this;
    };
    $.fn.getAllOnlineUsers = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetAllOnlineUsers',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {
                if (data.Users.length > 0) {
                    data1 = $.map(data.Users, function (key, index) {
                        return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                    });
                    userlist.empty().html(data1.join(''));
                    userlist.find('li').each(function (index) {
                        $(this).delay(100 * index).fadeIn(500);
                    });
                    $('#gcount').text(data.gestCount);
                    $('#ccount').text(data.CountryCount);
                    $('#icount').text(data.interestscount);
                    $('#pcount').text(data.pincodeCount);

                    if (data.PageNumber >= data.TotalPages) {

                        var pageNo = data.PageNumber;
                        if (data.TotalPages > 1) {
                            $('.Prev').css({ 'display': 'block' });
                            $('.Prev').prop({ "data-rel": pageNo });
                            $('.Prev').css("cursor", "pointer");
                        }


                    }
                    if (data.PageNumber < data.TotalPages) {
                        var pageNo = data.PageNumber;
                        if (data.TotalPages > 1) {
                            $('.Next').css({ 'display': 'block' });
                            $('.Next').prop({ "data-rel": pageNo });
                            $('.Next').css("cursor", "pointer");

                        }


                    }
                }

            }

        });
        return this;
    };
    //GetUsersByCountry
    //    $.fn.getAllUsersByCountry = function () {
    //        var userlist = $(this);
    //        $.ajax({
    //            url: '/iHealthUser/Chat/GetAllUsersByCountry',
    //            datType: 'json',
    //            Type: 'GET',
    //            cache: false,
    //            success: function (data) {
    //                data = $.map(data, function (key, index) {
    //                    return "<li style='display:none' data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
    //                });
    //                userlist.empty().html(data.join(''));
    //                userlist.find('li').each(function (index) {
    //                    $(this).delay(100 * index).fadeIn(500);
    //                });
    //            }

    //        });
    //        return this;
    //    };
    $.fn.getAllUsersByCountry = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetAllUsersByCountry',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };
    $.fn.getOnlineUsersByCountry = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetOnlineUsersByCountry',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };


    $.fn.getAllUsersByPinCode = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetAllUsersByPinCode',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                        $('.Prev').prop({ "data-rel": pageNo });
                        $('.Prev').css("cursor", "pointer");
                    }



                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };
    $.fn.getOnlineUsersbyPincode = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetOnlineUsersbyPincode',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };


    //gestation users
    $.fn.getAllGestationUsers = function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        var userList = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetAllGestationUsers',
            dataType: 'json',
            type: 'GET',
            cache: false,
            success: function (data) {
                data = $.map(data, function (key, index) {
                    return "<li style='display:none' data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userList.empty().html(data.join(''));
                userList.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
            }
        });
        return this;
    };

    //get intrest users
    $.fn.getAllUsersbyInterests = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetAllUsersbyInterests',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };

    $.fn.getonlineUsersbyInterests = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetonlineUsersbyInterests',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };
    $.fn.getAllUsersbyGestation = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetAllUsersbyGestation',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");


                    }

                }


            }

        });
        return this;
    };
    $.fn.getonlineUsersbyGestation = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetOnlineUsersbyGestation',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");
                    }



                }


            }

        });
        return this;
    };
    $.fn.getAllUsersByFilters = function () {

        var userlist = $(this);
        var filters = $('.CheckBox:checked').map(function () { return this.value; }).get().join(',');
        if (filters.length < 1) {
            alert("Please select atleast one filter");
            return this;
        } else {
            $.ajax({
                url: '/iHealthUser/Chat/GetAllUsersByFilterText',
                data: { filters: filters },
                dataType: 'json',
                type: 'GET',
                cache: false,
                success: function (data) {
                    data1 = $.map(data.Users, function (key, index) {
                        return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                    });
                    userlist.empty().html(data1.join(''));
                    userlist.find('li').each(function (index) {
                        $(this).delay(100 * index).fadeIn(500);
                    });
                    if (data.PageNumber >= data.TotalPages) {
                        var pageNo = data.PageNumber;
                        if (data.TotalPages > 1) {
                            $('.Prev').css({ 'display': 'block' });
                        }
                        $('.Prev').prop({ "data-rel": pageNo });
                        $('.Prev').css("cursor", "pointer");


                    }
                    if (data.PageNumber < data.TotalPages) {
                        var pageNo = data.PageNumber;
                        if (data.TotalPages > 1) {
                            $('.Next').css({ 'display': 'block' });
                            $('.Next').prop({ "data-rel": pageNo });
                            $('.Next').css("cursor", "pointer");
                        }



                    }
                }
            });
            $('.CheckBox').each(function () {
                this.checked = false;
            });
            return this;
        }

    }
    $.fn.getonlineUsersbyFilters = function () {

        var userlist = $(this);
        var filters = $('.CheckBox:checked').map(function () { return this.value; }).get().join(',');
        if (filters.length < 1) {
            alert("Please select atleast one filter");
            return this;
        } else {
            $.ajax({
                url: '/iHealthUser/Chat/GetonlineUsersyFilters',
                data: { filters: filters },
                dataType: 'json',
                type: 'GET',
                cache: false,
                success: function (data) {
                    data1 = $.map(data.Users, function (key, index) {
                        return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                    });
                    userlist.empty().html(data1.join(''));
                    userlist.find('li').each(function (index) {
                        $(this).delay(100 * index).fadeIn(500);
                    });
                    if (data.PageNumber >= data.TotalPages) {
                        var pageNo = data.PageNumber;
                        if (data.TotalPages > 1) {
                            $('.Prev').css({ 'display': 'block' });
                        }
                        $('.Prev').prop({ "data-rel": pageNo });
                        $('.Prev').css("cursor", "pointer");


                    }
                    if (data.PageNumber < data.TotalPages) {
                        var pageNo = data.PageNumber;
                        if (data.TotalPages > 1) {
                            $('.Next').css({ 'display': 'block' });
                            $('.Next').prop({ "data-rel": pageNo });
                            $('.Next').css("cursor", "pointer");

                        }


                    }
                }
            });
            $('.CheckBox').each(function () {
                this.checked = false;
            });
            return this;
        }

    }
    $.fn.getRecentActiveUsers = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetRecentActiveUsers',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                $('#gcount').text(data.gestCount);
                $('#ccount').text(data.CountryCount);
                $('#icount').text(data.interestscount);
                $('#pcount').text(data.pincodeCount);
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };
    $.fn.getRecentUsersByCountry = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetRecentUsersByCountry',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };
    $.fn.getRecentUsersByPincode = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetRecentUsersByPincode',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };
    $.fn.getRecentUsersByGestation = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetRecentUsersByGestation',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 15) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };
    $.fn.getRecentActiveUsersByFilters = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetRecentActiveUsersByFilters',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' });
                        $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");

                    }


                }


            }

        });
        return this;
    };
    $.fn.getRecentActiveUsersByInterests = function () {
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetRecentActiveUsersByInterests',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data1.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
                if (data.PageNumber >= data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Prev').css({ 'display': 'block' });
                    }
                    $('.Prev').prop({ "data-rel": pageNo });
                    $('.Prev').css("cursor", "pointer");


                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;
                    if (data.TotalPages > 1) {
                        $('.Next').css({ 'display': 'block' }); $('.Next').prop({ "data-rel": pageNo });
                        $('.Next').css("cursor", "pointer");
                    }



                }


            }

        });
        return this;
    };
    $.fn.GetUsersbyInterests = function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        var userList = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetUsersbyInterests',
            dataType: 'json',
            type: 'GET',
            cache: false,
            success: function (data) {
                data = $.map(data, function (key, index) {
                    return "<li style='display:none' data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userList.empty().html(data.join(''));
                userList.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
            }
        });

        return this;

    };

    //get online users
    $.fn.onlineUsers = function () {
        var userList = $(this);
        $.ajax({
            url: '/getOnlineUsers',
            dataType: 'json',
            type: 'GET',
            cache: false,
            success: function (data) {
                $(data).each(function (index, obj) {
                    userList.find('li[data-rel="' + obj.userId + '"]').addClass('online', 200, 'linear');
                });
            }
        });
        return this;
    };

    //get users by filters
    $.fn.getUsersByFilter = function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        var userList = $(this);
        var filters = $('.CheckBox:checked').map(function () { return this.value; }).get().join(',');
        if (filters.length < 1) {
            alert("Please select atleast one filter");
            return this;
        } else {
            $.ajax({
                url: '/iHealthUser/Chat/GetAllUsersByFilterText',
                data: { filters: filters },
                dataType: 'json',
                type: 'GET',
                cache: false,
                success: function (data) {
                    data = $.map(data, function (key, index) {
                        return "<li style='display:none' data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                    });
                    userList.empty().html(data.join(''));
                    userList.find('li').each(function (index) {
                        $(this).delay(100 * index).fadeIn(500);
                    });
                }
            });
            $('.CheckBox').each(function () {
                this.checked = false;
            });
            return this;
        }

    }

    $.fn.getActiveUsers = function () {
        return this;
    };

} (jQuery));