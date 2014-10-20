$.validator.unobtrusive.parse(document);

function beginAjax() {
    $.modal.close();
    // $('#ajaxtab').html('<div class="img" style="width:300px; height:50px; padding-left:200px; padding-top:30px;"><h3>Please wait...</h3></div>').modal();
}

function errorAjax(xhr, status, error) {
    $('#ajaxtab').html('<h2>' + textStatus + ': ' + error + '</h2>').modal({
        escClose: true
    });
    setTimeout(function () { $.modal.close(); }, 2500);
    $('#ajaxtab').empty();
}

function successAjax(result) {
    // alert(result);
    $.modal.close();

    //    $('#ajaxtab').html("<h3>Case information saved successfully</h3>");
    if ($(result).find('form').length > 0) {
        $('#ajaxtab').html(result).modal({
            closeHTML: "",
            onShow: function (e) {
                e.container.css({ 'height': 'auto' });
            }
        });
    }

    else if ($(result).find('[id*="chart"]').length > 0) {
        alert("Charts information saved successfully");
        $('#chartlst').html(result);
    }
    else if ($(result).find('[id*="cprocList"]').length > 0) {
        alert("Your modifications are updates successfully");
        $('#proclst').html(result);

    }
    else if ($(result).find('[id*="cvisitsList"]').length > 0) {
        alert("Your modifications are updates successfully");
        $('#vstlst').html(result);

    }
    else if ($(result).find('[id*="cmListt"]').length > 0) {
        alert("Your modifications are updates successfully");
        $('#medtstlst').html(result);
    }
    else if ($(result).find('[id*="proc"]').length > 0) {
        alert("Procedure information saved successfully");
        $('#proclst').html(result);
    }
    else if ($(result).find('[id*="msc"]').length > 0) {
        $('#mslst').html(result);
    }
    else if ($(result).find('[id*="visitList"]').length > 0) {
        alert("Your doctor visit information saved successfully");
        $('#vstlst').html(result);
    }
    else if ($(result).find('[id*="medicalTest"]').length > 0) {
        alert("Medical test information saved successfully");
        $('#medtstlst').html(result);
    }


    else if ($(result).find('[id*="cListt"]').length > 0) {
        alert("update chart Success");
        $('#chartlst').html(result);
    }

    else {
        $('#ajaxtab').html(result).modal();
        setTimeout(function () { $.modal.close(); }, 2500);
    }
}

