﻿function closeModal(response) {
    $.modal.close();
    if ($(response).find('#form0').length > 0) {
        $('#ajaxtab').html(response).modal({
            escClose: true
        });
    }
    else {
        $('#ajaxtab').html(response).modal();
        setTimeout(function () { window.location = "/"; $.modal.close(); }, 2500);
        //$('#ajaxtab').empty();
    }
}

function displayError(xhr, textStatus, error) {
    $('#ajaxtab').html('<h2>' + textStatus + ': ' + error + '</h2>').modal({
        escClose: true
    });
    setTimeout(function () { $.modal.close(); }, 3500);
    $('#ajaxtab').empty();
}



function loading() {
    $.modal.close();
    $('#ajaxtab').html('<div class="img" style="width:300px; height:50px; padding-left:200px; padding-top:30px;"><h3>Please wait...</h3></div>').modal();
}

function change() {
    $('#id1').toggle();
}
function change1() {
    $('#tryit').toggle();
    $('#RegUser_PromoCodeValue').val('');
    $('#RegUser_PromoCodeValue').toggle();
    // $('#RegUser_PromoCodeValue').show();
}

$.validator.unobtrusive.parse('#form0');
