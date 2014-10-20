(function ($) {

    //All users
    $.fn.getAllUsers = function () {
        var userList = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetAllUsers',
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
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
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
//                if (data.PageNumber >= data.TotalPages) {
//                    var pageNo = data.PageNumber;

//                    $('.Prev').css({ 'display': 'block' });
//                    $('.Prev').prop({ "data-rel": pageNo });
//                    $('.Prev').css("cursor", "pointer");


//                }
                if (data.PageNumber < data.TotalPages) {
                    var pageNo = data.PageNumber;

                    $('.Next').css({ 'display': 'block' });
                    $('.Next').prop({ "data-rel": pageNo });
                    $('.Next').css("cursor", "pointer");


                }


            }

        });
        return this;
    };
    $.fn.getAllUsersByPinCode = function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        var userlist = $(this);
        $.ajax({
            url: '/iHealthUser/Chat/GetAllUsersByPinCode',
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {
                data = $.map(data, function (key, index) {
                    return "<li style='display:none' data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                userlist.empty().html(data.join(''));
                userlist.find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });
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