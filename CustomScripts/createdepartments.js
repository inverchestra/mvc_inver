
$(function () {
    $('input[name="btnAddDept"]').click(function (s) {

        $.ajax({
            url: '/HospitalUser/HospAdmin/CreateDepartments',
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

                        $('#btnsubmit').click(function () {

                            var txtdeptname = $('#deptname').val();
                            var txtdeptspl = $('#deptspl').val();
                            if (txtdeptname == "") {
                                alert("please Enter DeptName");
                                $('input[tabindex=1]').focus();
                                return false;
                            }
                            else if (txtdeptspl == "") {
                                alert("please Enter Dept Specialization");
                                $('input[tabindex=2]').focus();
                                return false;
                            }
                            else {
                                var a = $('#subscription_order_form').serialize();
                                $.ajax({
                                    type: 'post',
                                    url: '/HospitalUser/HospAdmin/CreateDepartments',
                                    data: a,
                                    success: function (result) {
                                        if (result != "0") {
                                            $('#ajaxtab').html("<h6 style='padding: 10px;width:200px;word-wrap:break-word'>" + result + "</h6>").modal({
                                                onShow: function (e) {
                                                    e.container.css({ 'height': 'auto', 'width': 'auto' });
                                                }
                                            });
                                            setTimeout(function () {
                                                // window.location = "/";
                                                location.reload();
                                            }, 3000);
                                        }
                                        else {
                                            alert("Creation Failed")
                                        }
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





    $('tbody tr td img.btnEdit').click(function () {
        var deptid = $(this).parents('tr').find('td>input[type="hidden"]').val();
       // alert("deptid is " + deptid);
        $.ajax({
            url: '/HospitalUser/HospAdmin/UpdateDepartment',
            type: 'GET',
            asyn: false,
            cache: false,
            data: { deptid: deptid },
            success: function (data) {
                $('#ajaxtab').html(data).modal({
                    closeHTML: '',
                    escClose: true,
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });

                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });



                        $('#btnsubmit').click(function () {

                            var txtdeptname = $('#deptname').val();
                            var txtdeptspl = $('#deptspl').val();
                            if (txtdeptname == "") {
                                alert("please Enter DeptName");
                                $('input[tabindex=1]').focus();
                                return false;
                            }
                            else if (txtdeptspl == "") {
                                alert("please Enter Dept Specialization");
                                $('input[tabindex=2]').focus();
                                return false;
                            }
                            else {
                                var a = $('#subscription_order_form1').serialize();
                                $.ajax({
                                    type: 'post',
                                    url: '/HospitalUser/HospAdmin/UpdateDepartment',
                                    data: a,
                                    success: function (result) {
                                        if (result != "0") {
                                            $('#ajaxtab').html("<h6 style='padding: 10px;width:200px;word-wrap:break-word'>" + result + "</h6>").modal({
                                                onShow: function (e) {
                                                    e.container.css({ 'height': 'auto', 'width': 'auto' });
                                                }
                                            });
                                            setTimeout(function () {
                                                // window.location = "/";
                                                location.reload();
                                            }, 3000);
                                        }
                                        else {
                                            alert("Creation Failed")
                                        }
                                    }
                                });

                            }
                        });



                    }

                });
                //location.reload(true);
            }

        });
    });



    $('tbody tr td img.btnDelete').click(function () {
       // alert("Hi");
        var deptid = $(this).parents('tr').find('td>input[type="hidden"]').val();
        //alert("deptid is " + deptid);
        $.ajax({
            url: '/HospitalUser/HospAdmin/DeleteDepartment',
            type: 'POST',
            asyn: false,
            cache: false,
            data: { deptid: deptid },
            success: function (data) {
                // alert(data);
                // alert("sucess");
                $('#ajaxtab').html("<h6 style='padding: 10px;width:200px;word-wrap:break-word'>" + data + "</h6>").modal({
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto', 'width': 'auto' });
                    }
                });
                //  alert("dat1");
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



});



    




  