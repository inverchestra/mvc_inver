$(function () {
    $('input[name="btncreateDoctors"]').click(function () {
       // alert("Hi");
        $.ajax({
            url: '/HospitalUser/HospAdmin/CreateDoctor',
            type: 'GET',
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
                       // $("#s3").dropdownchecklist({ maxDropHeight: 150 }, { emptyText: "Click to see", width: 150 });
                        
                    }

                });
            }
        });
        return false;
    });
});