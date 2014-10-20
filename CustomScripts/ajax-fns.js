function closeModal(response) {
    $.modal.close();
    if ($(response).find('#form0').length > 0) {
        $('#ajaxtab').html(response).modal({
            escClose: true
        });
    }
    else {
        $('#ajaxtab').html(response).modal();
        setTimeout(function () { $.modal.close(); }, 45000);
        //$('#ajaxtab').empty();
    }
}

function displayError(xhr, textStatus, error) {
    $('#ajaxtab').html('<h2>' + textStatus + ': ' + error + '</h2>').modal({
        escClose: true
    });
    setTimeout(function () { $.modal.close(); }, 45000);
    $('#ajaxtab').empty();
}

function changefreeuser() {
    $('#id1').toggle();
    $('#tryuser').toggle();
}

function changetryuser() {
    $('#freeuser').toggle();
    $('#id1').toggle();
}
function changepromocode() {
    $('#RegUser_PromoCodeValue').toggle();
    $('#freeuser').toggle();
    $('#tryuser').toggle();

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

function loading() {
    $.modal.close();
    $('#ajaxtab').html('<div class="img" style="width:300px; height:50px; padding-left:100px; padding-top:30px;"><h3>Please wait we are creating your account...</h3></div>').modal();
}

$.validator.unobtrusive.parse('#form0');