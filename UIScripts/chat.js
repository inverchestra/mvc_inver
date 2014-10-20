$(function () {

    function groupModel(groupName, groupId, from, to, type) {
        this.groupName = groupName;
        this.groupId = groupId;
        this.from = from;
        this.to = to;
        this.type = type;
    };

    function messageModel(text, from, gid, time, name, classa, offl) {
        this.gid = gid;
        this.text = text;
        this.from = from;
        this.time = time;
        this.groupname = name;
        this.classname = classa;
        this.offl = offl;
    };

    function invitation(to) {
        this.To = to;
    };

    function accept(user, accepted, GID, caller) {
        this.accepted = accepted;
        this.reciever = user;
        this.GID = GID;
        this.caller = caller;
    };

    function currentDate() {
        var date = new Date();
        return date.getMonth() + "/" + date.getDate() + "/" + date.getFullYear() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    }

    var chat = $.connection.chat;

    //Client methods
    chat.client.joined = function (user, connectionId) {
        $('#userList').find('li[data-rel=' + user + ']').addClass('online');
        chat.server.setInvitationOn(user);
    };

    chat.client.userOffline = function (user) {
        $('#userList').find('li[data-rel=' + user + ']').removeClass('online');
        chat.server.setInvitationOff(user);
    };

    chat.client.invitationOff = function (Id) {
        console.log(Id);
        if ($('#active').find('li[data-iid="' + Id + '"]').hasClass('offline')) {
            $('#active').find('li[data-iid="' + Id + '"]').removeClass('online');
        } else {
            $('#active').find('li[data-iid="' + Id + '"]').removeClass('online').addClass('offline');
        }
    };

    chat.client.invitationOn = function (Id) {
        console.log(Id);
        $('#active').find('li[data-iid="' + Id + '"]').addClass('online');
    };

    chat.client.onlineUsers = function (users) {
        if (users.length > 1) {
            $.each(users, function (index, obj) {
                $('#userList').find('li[data-rel=' + obj + ']').addClass('online');
            });
        }
    };

    chat.client.invitation = function (invitation) {

        var invit = $('#invitations_templt').find('li').clone(true);
        invit.find('p[data-rel="name"]').prepend(invitation.Name);
        invit.find('p[data-rel="message"]').text(invitation.message);
        invit.attr('data-from', invitation.From); invit.attr('data-to', invitation.To);
        invit.attr('data-invit', invitation.Id);
        $('#invitations').prepend(invit);

    };

    chat.client.setChatWindow = function (id, name) {
        if ($('div[data-gid="' + id + '"]').length == 0) {
            var chatWindow = $('div#chatWindow').clone(true);
            $(chatWindow).css('position', 'absolute');
            $(chatWindow).attr('data-gid', id);
            $(chatWindow).find('[data-rel="userName"]').text(name);
            $('div#chatContainer').append(chatWindow);
            $('div[data-gid="' + id + '"]').siblings('div').hide();
            $('div[data-gid="' + id + '"]').show();
        }
    };

    chat.client.alertMessage = function (message) {
        $('#alert h5').text(message);
        $('#alert').show(500).delay(5000).hide(500);
    };

    chat.client.addMessage = function (message) {
        var win = $('div[data-gid="' + message.gid + '"]');
        if (win.length === 0) {
            var chatWindow = $('div#chatWindow').eq(0).clone(true);
            $(chatWindow).css('position', 'absolute');
            $(chatWindow).attr('data-gid', message.gid);
            $(chatWindow).find('[data-rel="userName"]').text(message.from);
            $('div#chatContainer').append(chatWindow);
            win = chatWindow;
        }

        if ($('div[data-gid="' + message.gid + '"]').siblings('div').length <= 1) {
            $('div[data-gid="' + message.gid + '"]').show();
        }

        if (message.from == $(win).find('[data-rel="userName"]').text()) {
            var p = '<p class="chat_to"><strong class="chat_nameto">' + message.from + ': </strong>' + message.text + '</p>';
            win.find('div[data-rel="msgs"]').append(p);
            $('#active').find('li[data-iid="' + message.gid + '"]').find('p[data-rel="message"]').text(message.text);
            if (!$(win).is(':visible')) {
                var count = $('#active').find('li[data-iid="' + message.gid + '"]').find('span.msgtime').text();
                count = (count == "") ? 1 : (parseInt(count, 10) + 1);
                $('#active').find('li[data-iid="' + message.gid + '"]').find('span.msgtime').text(count);
            }
        } else {
            var p = '<p class="chat_from"><strong class="chat_namefrom">' + message.from + ': </strong>' + message.text + '</p>';
            win.find('div[data-rel="msgs"]').append(p);
            $('#active').find('li[data-iid="' + message.gid + '"]').find('p[data-rel="message"]').text(message.text);
        }

        win.find('div[data-rel="msgs"]').animate({
            scrollTop: win.find('div[data-rel="msgs"]')[0].scrollHeight
        }, 500);

    };

    chat.client.signoutSession = function () {
        $.connection.hub.stop();
        location.href = "/ihealthuser/dashboard/logout";
    };

    chat.client.showHistory = function (lines, gid) {
        console.log('Lines: ', lines);
        $('div[data-gid="' + gid + '"]').find('div[data-rel="msgs"]').empty();
        $.each(lines, function (e, txt) {
            if (txt.from == $('div[data-gid="' + gid + '"]').find('[data-rel="userName"]').text()) {
                var p = '<p class="chat_to"><strong class="chat_nameto">' + txt.from + ': </strong>' + txt.text + '</p>';
                $('div[data-gid="' + gid + '"]').find('div[data-rel="msgs"]').append(p);
                $('#active').find('li[data-iid="' + gid + '"]').find('p[data-rel="message"]').text(txt.text);
            } else {
                var p = '<p class="chat_from"><strong class="chat_namefrom">' + txt.from + ': </strong>' + txt.text + '</p>';
                $('div[data-gid="' + gid + '"]').find('div[data-rel="msgs"]').append(p);
                $('#active').find('li[data-iid="' + gid + '"]').find('p[data-rel="message"]').text(txt.text);
            }
        });
    }

    chat.client.displayPendingInvitations = function (invitations) {
        $.each(invitations, function (index, obj) {
            var invit = $('#invitations_templt').find('li').clone(true);
            invit.find('p[data-rel="name"]').prepend(obj.Name);
            invit.find('p[data-rel="message"]').text(obj.message);

            invit.attr('data-from', obj.From); invit.attr('data-to', obj.To);
            invit.attr('data-invit', obj.Id);
            $('#invitations').prepend(invit);
        });
    };

    chat.client.displayActiveUsers = function (users) {
        if (users.length > 0) {
            $.each(users, function (index, obj) {
                var userItem = $('#active_templt').find('li').clone(true);
                userItem.attr('data-IID', obj.ID);
                userItem.attr('data-name', obj.Name);
                userItem.find('p[data-rel="name"]').prepend(obj.Name);
                userItem.find('p[data-rel="message"]').text(obj.rm);
                userItem.find('span.msgtime').text(obj.offline);
                userItem.addClass(obj.status);
                $('#active').prepend(userItem);
            });
        }
    }

    chat.client.addActiveUser = function (user) {
        var userItem = $('#active_templt').find('li').clone(true);
        userItem.attr('data-IID', user.ID);
        userItem.attr('data-name', user.Name);
        userItem.attr('data-other', user.other);
        userItem.find('p[data-rel="name"]').prepend(user.Name);
        $('#active').prepend(userItem);
    }

    chat.client.deleteInvitation = function (id) {
        $('#active').find('li[data-iid="' + id + '"]').remove();
    }
    /* new */
    chat.client.displayOnlineUsers = function (usrModel) {
        alert($('#gcount').text());
        if (usrModel.Users.length > 0) {
            data1 = $.map(usrModel.Users, function (key, index) {
                return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
            });
            $("#userList").empty().html(data1.join(''));
            $("#userList").find('li').each(function (index) {
                $(this).delay(100 * index).fadeIn(500);
            });
            // $("#userList").append(data1.join(''));

        }

        $('#gcount').text(usrModel.gestCount);
        $('#ccount').text(usrModel.CountryCount);
        $('#icount').text(usrModel.interestscount);
        $('#pcount').text(usrModel.pincodeCount);
    }
    chat.client.displayOnlineCountryUsers = function (users) {

        if (users.length > 0) {
            data1 = $.map(users, function (key, index) {
                return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
            });

            //$("#userList").append(data1.join(''));
            $("#userList").empty().html(data1.join(''));
            $("#userList").find('li').each(function (index) {
                $(this).delay(100 * index).fadeIn(500);
            });
        }
    }
    chat.client.displayOnlinePincodeUsers = function (users) {

        if (users.length > 0) {
            data1 = $.map(users, function (key, index) {
                return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
            });

            // $("#userList").append(data1.join(''));
            $("#userList").empty().html(data1.join(''));
            $("#userList").find('li').each(function (index) {
                $(this).delay(100 * index).fadeIn(500);
            });
        }
    }
    chat.client.displayOnlineGestationUsers = function (users) {

        if (users.length > 0) {
            data1 = $.map(users, function (key, index) {
                return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
            });

            // $("#userList").append(data1.join(''));
            $("#userList").empty().html(data1.join(''));
            $("#userList").find('li').each(function (index) {
                $(this).delay(100 * index).fadeIn(500);
            });
        }
    }
    chat.client.displayOnlineFiltersUsers = function (users) {

        if (users.length > 0) {
            data1 = $.map(users, function (key, index) {
                return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
            });

            // $("#userList").append(data1.join(''));
            $("#userList").empty().html(data1.join(''));
            $("#userList").find('li').each(function (index) {
                $(this).delay(100 * index).fadeIn(500);
            });
        }
    }
    chat.client.displayOnlineInterestUsers = function (users) {
        alert(users);
        if (users.length > 0) {
            data1 = $.map(users, function (key, index) {
                return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
            });

            //$("#userList").append(data1.join(''));
            $("#userList").empty().html(data1.join(''));
            $("#userList").find('li').each(function (index) {
                $(this).delay(100 * index).fadeIn(500);
            });
        }
    }
    chat.client.displayRecentActiveInvitationsByUser = function (users) {
        if (users.length > 0) {
            data1 = $.map(users, function (key, index) {
                return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
            });

            //$("#userList").append(data1.join(''));
            $("#userList").empty().html(data1.join(''));
            $("#userList").find('li').each(function (index) {
                $(this).delay(100 * index).fadeIn(500);
            });
        }
    }
    /* new */
    $.connection.hub.logging = true;

    $.connection.hub.start({ xdomain: true,  waitForPageLoad: false }).done(function () {
        $('ul#tUsers a').css("cursor", "pointer");
        $('#tUsers li:nth-child(1)').addClass('current');
        $('.Next').prop({ "data-from": "GetAllUsers" });

        $('#userList').getAllUsers();
        chat.server.onlineUsersList();
       
        chat.server.getPendingInvitations();
        chat.server.getActiveUsers();



    });

    $('#tUsers li:nth-child(1)').live('click', function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        $('#tUsers li ').removeClass("current");

        $('#tUsers li:nth-child(1)').addClass('current');
        $('#userList').empty();
        $('#userList').getAllUsers();
        $('.Next').prop({ "data-from": "GetAllUsers" });
        chat.server.onlineUsersList();
    });

    $('#tUsers li:nth-child(2)').live('click', function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        $('#tUsers li ').removeClass("current");

        $('#tUsers li:nth-child(2)').addClass('current');
        $('#userList').empty();
        /* new */
        //        $('#userList').getAllOnlineUsers();
        //  $('.Next').prop({ "data-from": "GetAllOnlineUsers" });
        chat.server.allOnlineUsersList();
        /* new */
        chat.server.onlineUsersList();
    });
    $('#tUsers li:nth-child(3)').live('click', function () {

        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        $('#tUsers li').removeClass("current");

        $('#tUsers li:nth-child(3)').addClass('current');
        $('#userList').empty();
        $('#userList').getRecentActiveUsers();
        $('.Next').prop({ "data-from": "GetRecentActiveUsers" });

    });
    $('#peer li:nth-child(1)').live('click', function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        var itemType = $('ul#tUsers li.current').find('a').html();
        if (itemType == "All") {
            $('#userList').getAllUsersByCountry();
            $('.Next').prop({ "data-from": "GetAllUsersByCountry" });
        }
        else if (itemType == "Online") {
//            $('#userList').getOnlineUsersByCountry();
            //            $('.Next').prop({ "data-from": "GetOnlineUsersByCountry" });
            chat.server.allOnlineUsersList();
        }
        else if (itemType == "Recent") {
            $('#userList').getRecentUsersByCountry();
            $('.Next').prop({ "data-from": "GetRecentUsersByCountry" });
        }
        chat.server.onlineUsersList();
    });
    $('#peer li:nth-child(2)').live('click', function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        var itemType = $('ul#tUsers li.current').find('a').html();
        if (itemType == "All") {
            $('#userList').getAllUsersByPinCode();
            $('.Next').prop({ "data-from": "GetAllUsersByPinCode" });
        }
        else if (itemType == "Online") {
//            $('#userList').getOnlineUsersbyPincode();
            //            $('.Next').prop({ "data-from": "GetOnlineUsersbyPincode" });
            chat.server.getOnlinePincodeUsers();
        }
        else if (itemType == "Recent") {
            $('#userList').getRecentUsersByPincode();
            $('.Next').prop({ "data-from": "GetRecentUsersByPincode" });

        }
        chat.server.onlineUsersList();
    });
    $('#peer li:nth-child(3)').live('click', function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        var itemType = $('ul#tUsers li.current').find('a').html();
        if (itemType == "All") {
            $('#userList').getAllUsersbyGestation();
            $('.Next').prop({ "data-from": "GetAllUsersbyGestation" });
        }
        else if (itemType == "Online") {
//            $('#userList').getonlineUsersbyGestation();
            //            $('.Next').prop({ "data-from": "GetonlineUsersbyGestation" });
            chat.server.getOnlineGestationUsers();
        }
        else if (itemType == "Recent") {
            $('#userList').getRecentUsersByGestation();
            $('.Next').prop({ "data-from": "GetRecentUsersByGestation" });
        }
        chat.server.onlineUsersList();
    });
    $('#peer li:nth-child(4)').live('click', function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        var itemType = $('ul#tUsers li.current').find('a').html();
        if (itemType == "All") {
            $('#userList').getAllUsersbyInterests();
            $('.Next').prop({ "data-from": "GetAllUsersbyInterests" });
        }
        else if (itemType == "Online") {
//            $('#userList').getonlineUsersbyInterests();
            //            $('.Next').prop({ "data-from": "GetonlineUsersbyInterests" });
            chat.server.getOnlineInterestUsers();
        }
        else if (itemType == "Recent") {
            $('#userList').getRecentActiveUsersByInterests();
            $('.Next').prop({ "data-from": "GetRecentActiveUsersByInterests" });
        }
        chat.server.onlineUsersList();
    });
    $('#btnFilter').live('click', function () {
        $('.Next').css({ 'display': 'none' });
        $('.Prev').css({ 'display': 'none' });
        var itemType = $('ul#tUsers li.current').find('a').html();
        if (itemType == "All") {
            $('#userList').getAllUsersByFilters();
            $('.Next').prop({ "data-from": "GetAllUsersByFilters" });
        }
        else if (itemType == "Online") {
//            $('#userList').getonlineUsersbyFilters();
            //            $('.Next').prop({ "data-from": "GetonlineUsersbyFilters" });
            chat.server.getOnlineFiltersUsers();
        }
        else if (itemType == "Recent") {
            $('#userList').getRecentActiveUsersByFilters()
            $('.Next').prop({ "data-from": "GetRecentActiveUsersByFilters" });
        }
        chat.server.onlineUsersList();
    });

    $('#userList li').live('click', function (e) {

        var itemType = $('ul#tUsers li.current').find('a').html();
        if (itemType == "All") {
            chat.server.sendInvitation(new invitation($(this).attr('data-rel')));
        }
        else {
            return;
        }


    });

    $('#active li').live('click', function (e) {
        var iid = $(this).attr('data-iid');
        var name = $(this).attr('data-name');
        var classname = $(this).attr('class');
        // alert(classname);

        if ($('div[data-gid="' + iid + '"]').length == 0) {
            var chatWindow = $('div#chatWindow').eq(0).clone(true);
            $(chatWindow).css('position', 'absolute');
            $(chatWindow).attr('data-gid', iid);
            $(chatWindow).find('[data-rel="userName"]').text(name);
            $(chatWindow).attr('data-class', classname);

            $('div#chatContainer').append(chatWindow);
            chat.server.getHistory(iid, name);
        }
        $(this).find('span.msgtime').empty();
        $('div[data-gid="' + iid + '"]').siblings('div').hide();
        $('div[data-gid="' + iid + '"]').show();


        $('div[data-gid="' + iid + '"]').find('div[data-rel="msgs"]').animate({
            scrollTop: $('div[data-gid="' + iid + '"]').find('div[data-rel="msgs"]')[0].scrollHeight
        }, 500);
    });

    $('#active a').live('click', function (e) {

        var li = $(this).parents('li').attr('data-iid');
        chat.server.deleteActiveUser(li);
        $(this).parents('li').remove();
        return false;

    });

    $('#list li:nth-child(1)>a').click(function () {
        $.connection.hub.stop();
    });

    $('#others').click(function () {
        chat.server.signoutOthers();
    });

    $('#invitations a').live('click', function (e) {
        e.preventDefault();
        var li = $(this).parents('li');
        $('#invitation').fadeOut(500);
        if ($(this).attr('class') == 'tick') {
            chat.server.createActiveUser(new accept(li.attr('data-to'), true, li.attr('data-invit'), li.attr('data-from')));
        } else {
            chat.server.createActiveUser(new accept(li.attr('data-to'), false, li.attr('data-invit'), li.attr('data-from')));
        }
        $(this).parents('li').remove();
    });

    $('#chatWindow textarea').live('keypress', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            if ($(this).val().trim() != "") {
                $(this).parents('form').submit();
            }
        }
    });

    $('#chatWindow form').live('submit', function (e) {
        e.preventDefault();
        var message = $(this).find('textarea').val();
        var gid = $(this).parents('#chatWindow').attr('data-gid');
        var gname = $(this).parents('#chatWindow').find('label[data-rel="userName"]').html();
        var classa = $('#active').find('li[data-iid="' + gid + '"]').attr('class');
        alert(classa);
        var msg = new messageModel(message, "", gid, currentDate(), classa);
        chat.server.sendMessage(msg);
        $(this).get(0).reset();
        return false;
    });

    $('#Pasthistory').click(function (e) {
        e.preventDefault();
        var gid = $(this).parents('#chatWindow').attr('data-gid');
        chat.server.getHistory(gid);
    });


    $('.Prev').click(function () {

        var page = $('.Prev').prop("data-rel") - 1;

        $.ajax({
            url: '/iHealthUser/Chat/GetAllUsers',
            data: { page: page },
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                $('#userList').empty().html(data1.join(''));
                $('#userList').find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });



                if (data.PageNumber > 0 && data.PageNumber < data.TotalPages) {
                    $('.Prev').css({ 'display': 'block' });
                    $('.Next').css({ 'display': 'block' });
                    $('.Prev').prop({ "data-rel": page });
                    $('.Prev').css("cursor", "pointer");
                    $('.Next').prop({ "data-rel": page });
                    $('.Next').css("cursor", "pointer");

                }
                else if (data.PageNumber >= data.TotalPages) {
                    ;

                    $('.Prev').css({ 'display': 'block' });
                    $('.Prev').prop({ "data-rel": page });
                    $('.Prev').css("cursor", "pointer");
                    $('.Next').css({ 'display': 'none' });
                }
                else if (data.PageNumber < data.TotalPages) {

                    $('.Next').css({ 'display': 'block' });
                    $('.Next').prop({ "data-rel": page });
                    $('.Next').css("cursor", "pointer");
                    $('.Prev').css({ 'display': 'none' });
                }
            }

        });

    });


    $('.Next').click(function () {

        var page = $('.Next').prop("data-rel") + 1;

        $.ajax({
            url: '/iHealthUser/Chat/GetAllUsers',
            data: { page: page },
            datType: 'json',
            Type: 'GET',
            cache: false,
            success: function (data) {

                data1 = $.map(data.Users, function (key, index) {
                    return "<li data-rel='" + key.Id + "' class='offline'><p><label>" + key.Name + "</label></p></li>";
                });
                $("#userList").append(data1.join(''));
                //                $('#userList').append().html(data1.join(''));
                $('#userList').find('li').each(function (index) {
                    $(this).delay(100 * index).fadeIn(500);
                });




                if (data.PageNumber > 0 && data.PageNumber < data.TotalPages) {
                    //                    $('.Prev').css({ 'display': 'block' });
                    $('.Next').css({ 'display': 'block' });
                    //                    $('.Prev').prop({ "data-rel": page });
                    //                    $('.Prev').css("cursor", "pointer");
                    $('.Next').prop({ "data-rel": page });
                    $('.Next').css("cursor", "pointer");
                }
                else if (data.PageNumber >= data.TotalPages) {


                    //                    $('.Prev').css({ 'display': 'block' });
                    //                    $('.Prev').prop({ "data-rel": page });
                    //                    $('.Prev').css("cursor", "pointer");
                    $('.Next').css({ 'display': 'none' });
                }
                else if (data.PageNumber < data.TotalPages) {

                    $('.Next').css({ 'display': 'block' });
                    $('.Next').prop({ "data-rel": page });
                    $('.Next').css("cursor", "pointer");
                    //                    $('.Prev').css({ 'display': 'none' });
                }
            }

        });

    });
});