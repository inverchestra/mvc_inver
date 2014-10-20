$(function () {
    $.ajaxSetup({
        cache: false,
        async: false
    });

    //For Change password

    $('#chngpwd').click(function () {
        var UserId = $(this).parent('li').find('input[type="hidden"]').val();
        $.ajax({
            url: '/iHealthUser/AccountSettings/ChangePassword',
            data: { UserId: UserId },
            success: function (data1) {
                $('#some').html(data1).modal({
                    closeHTML: "",
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });
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
                                type: 'POST',
                                dataType: 'json',
                                data: { UserId: UserId, oldPassword: oldPassword, newPassword: newPassword },
                                success: function (changed) {
                                    $('#ajaxtab').html('<h2 style="padding:5px;">' + changed + '</h2>').modal({
                                        onShow: function (e) {
                                            e.container.css({ 'height': 'auto','width':'auto' });
                                        }
                                    });
                                    setTimeout(function () {
                                        $.modal.close();
                                    }, 3000);
                                }
                            });
                            //return false;
                        });

                    }
                });
            }
        });
        return false;
    });

    //doula changepassword
    $('#doulachngpwd').click(function () {
        var UserId = $(this).parent('li').find('input[type="hidden"]').val();
        $.ajax({
            url: '/DoulaBumpUser/DoulaBump/ChangePassword',
            data: { UserId: UserId },
            success: function (data1) {
                $('#some').html(data1).modal({
                    closeHTML: "",
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });
                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });

                        $('#btnChange').click(function () {
                            var UserId = $('#UserId').val();
                            var oldPassword = $('#txtOldPassword').val();
                            var newPassword = $('#txtNewPassword').val();
                            $.ajax({
                                url: '/DoulaBumpUser/DoulaBump/ChangePassword',
                                type: 'POST',
                                dataType: 'json',
                                data: { UserId: UserId, oldPassword: oldPassword, newPassword: newPassword },
                                success: function (changed) {
                                    $('#ajaxtab').html('<h2 style="padding:5px;">' + changed + '</h2>').modal({
                                        onShow: function (e) {
                                            e.container.css({ 'height': 'auto', 'width': 'auto' });
                                        }
                                    });
                                    setTimeout(function () {
                                        $.modal.close();
                                    }, 3000);
                                }
                            });
                            //return false;
                        });

                    }
                });
            }
        });
        return false;
    });
    //doula change password


    //AD Code for Download Scan Setup
    $('#dlScanSetup').click(function () {



//        var result = confirm("Do you want to Download Scan Setup?");
//        if (result) {
            $.fileDownload('/iHealthUser/AccountSettings/DownloadScan');
//        }


    });

   
});