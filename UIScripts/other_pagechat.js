$(function () {
    alert("cc");
    var interval = setInterval(function () {
        $.each($('#chat_container>div[data-rel*="notification"]'), function (index, obj) {
            $(obj).delay(index * 1000).fadeOut(500, function () { $(obj).remove() });
        });
    }, 10000);

    var chat = $.connection.chat;
    var i = 0;

    $.connection.hub.logging = true;

    $.connection.hub.start({ xdomain: true, transport: ['webSockets', 'serverSentEvents'] })
        .done(function () {
            console.log("Connected, transport = " + $.connection.hub.transport.name);
            alert("ts name" + $.connection.hub.transport.name)
        });

    chat.client.addMessage = function (message) {
        var j = i++;
        var msgdiv = $('#msgcnt').clone(true);
        msgdiv.attr('data-rel', 'notification' + j);
        msgdiv.find('p:nth-child(1)').text(message.from);
        msgdiv.find('p:nth-child(2)').text('"' + message.text + '"');
        msgdiv.find('p:nth-child(3)').text(message.time.split('T')[1]);
        $('#chat_container').append(msgdiv);
        $('#chat_container').find('div[data-rel="' + 'notification' + j + '"]').fadeIn(500);
    };
    chat.client.invitation = function (invitation) {
        var j = i++;
        var invitdiv = $('#invitcnt').clone(true);
        invitdiv.attr('data-rel', 'notification' + j);
        invitdiv.find('p:nth-child(1)').text(invitation.Name);
        invitdiv.find('p:nth-child(2)').text(invitation.message);
        $('#chat_container').append(invitdiv);
        $('#chat_container').find('div[data-rel="' + 'notification' + j + '"]').fadeIn(500);
    };
    chat.client.addActiveUser = function (user) {
        var j = i++;
        var usrdiv = $('#actusr').clone(true);
        usrdiv.attr('data-rel', 'notification' + j);
        usrdiv.find('p:nth-child(1)').text(user.Name);
        usrdiv.find('p:nth-child(2)').text(user.Name + " has accepted your invitation.");
        $('#chat_container').append(usrdiv);
        $('#chat_container').find('div[data-rel="' + 'notification' + j + '"]').fadeIn(500);
    };
});