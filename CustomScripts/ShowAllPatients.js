$(function () {
    $('input[name="txtOtacode"]').toggle();

    $('input[name="btncreatePatient"]').click(function () {
        $.ajax({
            url: '/HospitalUser/HospAdmin/CreateUser',
            success: function (data) {
                $('#ajaxtab').html(data).modal({
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });
                        $("#rbtusngModerator").click(function () {
                            alert("checkbox click");
                            $("#EmailId").val("");
                            $("#Password").val("");
                            $("#cgroup").toggle();
                        });
                        $('#btnCancel').click(function () {
                            $.modal.close();
                            return false;
                        });
                      //  $("#s3").dropdownchecklist({ maxDropHeight: 150 }, { emptyText: "Click to see", width: 150 });


                        if (e != "0") {
                            $('#ajaxtab').html("<h6 style='padding: 10px;width:200px;word-wrap:break-word'>" + e + "</h6>").modal({
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
                            $('#ajaxtab').html("<h6 style='padding: 10px;width:200px;word-wrap:break-word'>" + e + "</h6>").modal({
                                onShow: function (e) {
                                    e.container.css({ 'height': 'auto', 'width': 'auto' });
                                }
                            });
                            setTimeout(function () {
                                // window.location = "/";
                                location.reload();
                            }, 3000);
                        }
                    }

                });
            }
        });
        return false;
    });


    $('input[name="btnassigntodoctor1"]').click(function () {
        //            alert("hi");
        //            alert("Hi assign doctor click");
        var userid = $(this).parent('td').find("input[name='UserId']").val();
        //var GrouptType = $(this).parent('td').find("input[name='GroupType']").val();
        var GrouptType = "Hospital";
        //            alert("groupType" + GrouptType);
        //            alert("userid is" + userid);
        $.ajax({
            url: '/HospitalUser/HospAdmin/AddPatientToDoctor',
            type: 'GET',
            asyn: false,
            cache: false,
            data: { userid: userid, GroupType: GrouptType },
            success: function (result) {
                //                    alert("in success ajax");
                $('#ajaxtab').html(result).modal({
                    closeHTML: '',
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });

                    }
                });
            }
        });
    });





    $("input[name='btnEditHospUser']").click(function () {


        var userid = $(this).parent('td').find("input[name='UserId']").val();
        var GrouptType = "Hospital";
        //alert(userid);
        $.ajax({
            url: '/HospitalUser/HospAdmin/EditHospUser',
            type: 'POST',
            asyn: false,
            cache: false,
            data: { userid: userid, GroupType: GrouptType },
            success: function (result) {
                alert("in success ajax");
                $('#ajaxtab').html(result).modal({
                    closeHTML: '',
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });

                    }
                });
            }
        });


    });



    $("input[name='btnHospitalDeleteUser']").click(function () {

       // alert("Hi Delete btnHospitalDeleteUser");

        var hospitaluserid = $(this).parent('td').find("input[name='UserId']").val();
       // alert("hospitaluserid ID is " + hospitaluserid);
        $.ajax({
            url: '/HospitalUser/HospAdmin/Deletehospitaluser',
            type: 'POST',
            asyn: false,
            cache: false,
            data: { id: hospitaluserid },
            success: function (data) {
                location.reload(true);
            }
        });
    });




    $('input[name="btnotacode"]').click(function () {
       // alert("hi");

        var userid = $(this).parent('td').find("input[name='UserId']").val();
        // var did = $(this).parent('td').find("input[name='DomainId']").val();

        var GroupType = "Hospital";
        // var t = ("#ota").prop("checked", true);
        //  alert(t);
        var val = $('input:radio[name=ota]:checked').val();
        //alert(val);
        //alert("domain id:" + did);


        $.ajax({
            url: '/HospitalUser/HospAdmin/SendOTACode',
            type: 'POST',
            asyn: false,
            cache: false,
            data: { userid: userid, GroupType: GroupType, val: val },
            success: function (result) {
                //                    alert("in success ajax");
                $('#ajaxtab').html(result).modal({
                    closeHTML: '',
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });

                    }
                });
            }
        });
    });




    $('input[name="btnotaverify"]').click(function () {
        alert("hi");

        //var userid = $("input[name='UserId']").val();
     
        //var userid = $(this).parent('td').find("input[id='uid']").val();
        var userid = $(this).parent('td').find('input[name="UserId"]').val();

        var otacode = $(this).parent('td').find("input[name='TxtOTACode']").val();
      //  alert("Userid is " + userid);
       // alert("ota code");

        var GroupType = "Hospital";
        $.ajax({
            url: '/HospitalUser/HospAdmin/VerifyOTACODE',
            type: 'POST',
            asyn: false,
            cache: false,
            data: { userid: userid, GroupType: GroupType, otacode: otacode },
            success: function (result) {
                //                    alert("in success ajax");
                $('#ajaxtab').html(result).modal({
                    closeHTML: '',
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto' });

                    }
                });
            }
        });
    });

});