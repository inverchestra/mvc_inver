



function closeModal(response) { // sandeep changed this method on 25-09-2013
    $.modal.close();
    
    if (response == "1041") {
        var id = $('#hdnUserID').val();
        $.ajax({
            url: "/iHealthUser/AccountSettings/CreateSubUser",
            data: { id: id },
            type: "POST",
            success: function (res) {
                $("#some").html(res).modal({
                    escClose: true,
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });
                        $("#resultspan").text("Email already exist");
                    }
                });
            }
        });
    }
    else if (response == "1051") {
        var id = $('#hdnUserID').val();
        $.ajax({
            url: "/iHealthUser/AccountSettings/CreateSubUser",
            data: { id: id },
            type: "POST",
            success: function (res) {
                $("#some").html(res).modal({
                    escClose: true,
                    closeHTML: "",
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });
                        $("#resultspan").text("Phone Number already exist");
                    }
                });
            }
        });
    }
    else {

        if ($(response).find('#form0').length > 0) {
            $('#ajaxtab').html(response).modal({
                escClose: true
            });
        }
        else {
            $('#ajaxtab').html(response).modal({
                escClose: true
            });
            setTimeout(function () { $.modal.close(); }, 3000);
            //$('#ajaxtab').empty();
        }
        location.reload();
    }
}

function displayError(xhr, textStatus, error) {
   // alert("Display Error");
    $('#ajaxtab').html('<h2>' + textStatus + ': ' + error + '</h2>').modal({
        escClose: true
    });
    setTimeout(function () { $.modal.close(); }, 3000);
    $('#ajaxtab').empty();
}

$("#rbtusngModerator").click(function () {
    $("#EmailId").val = "";
    $("#Password").val = "";
    $("#pwd").toggle(this.unchecked);

});

function loading() {
   // alert("loading");
    $.modal.close();
    $('#ajaxtab').html('<div class="img" style="width:300px; height:50px; padding-left:200px; padding-top:30px;"><h3>Please wait...</h3></div>').modal();
}

$.validator.unobtrusive.parse(document);
