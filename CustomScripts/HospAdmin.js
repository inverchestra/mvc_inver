$(function () {
    $('input[name="btncreatebranch"]').click(function (s) {
       // alert("createbranch");
        $.ajax({
            url: '/HospitalUser/HospAdmin/CreateBranch',
            success: function (result) {
                $('#ajaxtab').html(result).modal({
                    closeHTML: '',
                    escClose: true,
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });
                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });
                        $('#savebranch').click(function () {
                           // alert("hi");
                            var bname = $('#textbranchname').val();
                            var baddress = $('#textbranchaddress').val();
                            if (bname == "") {
                                alert("Please enter branch name");
                                $('input[tabindex=1]').focus();

                                return false;
                            }
                            else if (baddress == "") {
                                alert("please enter branch address");
                                return false;
                                $('input[tabindex=2]').focus();
                            }

                            else {
                                var a = $('#subscription_order_form').serialize();
                                $.ajax({
                                    type: 'post',
                                    url: '/HospitalUser/HospAdmin/CreateBranchInfo',
                                    data: a,
                                    success: function (result) {
                                        $('#ajaxtab').html("<h6 style='padding: 30px;width:200px;word-wrap:break-word'>" + result + "</h6>").modal({
                                            onShow: function (e) {
                                                e.container.css({ 'height': 'auto', 'width': 'auto' });
                                            }
                                        });

                                        setTimeout(function () {
                                            // window.location = "/";
                                            location.reload();
                                        }, 3000);
                                    }
                                });

                            }
                        });

                    }

                });
            }
        });
        return false;
        location.reload(true);
    });



       $('tbody tr td img.btnEditBranch').click(function () {
        var branchid = $(this).parents('tr').find('td>input[type="hidden"]').val();
        //alert("deptid is " + branchid);

        $.ajax({
            url: '/HospitalUser/HospAdmin/UpdateBranch',
            type: 'GET',
            asyn: false,
            data: { branchid: branchid },
            cache: false,
            success: function (result) {
                $('#ajaxtab').html(result).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });
                        $("#branchupdate").click(function () {
                          //  alert("update");
                            var txtname = $("#textbranchname").val();
                            var txtaddress = $('#textbranchaddress').val();

                            if (txtname == "") {
                                alert("please enter branch name");
                                $('input[tabindex="1"]').focus();
                                return false;
                            }
                            if (txtaddress == "") {
                                alert("please enter branch address");
                                $('input[tabindex="2"]').focus();
                                return false;
                            }
                            dataString = $("#subscription_order_form1").serialize();

                            $.ajax({

                                //var a = $('#subscription_order_form1').serialize();
                                url: '/HospitalUser/HospAdmin/SaveUpdateBranch',
                                type: "POST",
                                async: false,
                                cache: false,
                                //data: $('#subscription_order_form1').serialize(),
                                data: dataString,
                                success: function (data) {
                                    $('#ajaxtab').html("<h6 style='padding: 10px;width:200px;word-wrap:break-word'>" + data + "</h6>").modal({
                                        onShow: function (e) {
                                            e.container.css({ 'height': 'auto', 'width': 'auto' });
                                        }
                                    });
                                }
                            });

                            setTimeout(function () {
                                location.reload();
                            }, 3000);


                        });
                    }
                });
            }
        });
    });



    $('tbody tr td img.btnDeleteBranch').click(function () {
        var branchid = $(this).parents('tr').find('td>input[type="hidden"]').val();
        //alert("deptid is " + branchid);
        $.ajax({
            url: '/HospitalUser/HospAdmin/DeleteBranch',
            type: 'POST',
            asyn: false,
            cache: false,
            data: { branchid: branchid },

            success: function (result) {

                $('#ajaxtab').html("<h6 style='padding: 10px;width:200px;word-wrap:break-word'>" + result + "</h6>").modal({
                    closeHTML: "",
                    onShow: function (e) {
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




    $('table tbody tr:nth-child(2n+1)').filter(':has(:checkbox:checked)').addClass('selected').end().click(function (event) {
        $(this).next('tr').toggle();
        $(this).toggleClass('selected');
        if (event.target.type !== 'checkbox') {
            $(':checkbox', this).attr('checked', function () {
                return !this.checked;
            });
        }
    });




    $('input[name="btnAssignDoctor"]').click(function (s) {

       /// alert("Hi");
        //s.preventDefault();
        $.ajax({
            url: '/HospitalUser/HospAdmin/AssignDoctor',
            type: 'GET',
            success: function (result) {
                $('#ajaxtab').html(result).modal({
                    closeHTML: '',
                    escClose: true,
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });

                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });
                    }

                });
            }
        });

        return false;
        location.reload(true);
    });


});
