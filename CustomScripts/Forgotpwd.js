function closeModal(response) {
    // $('#res_modal').modal('hide')
    if ($(response).find('#form0').length > 0) {
        $('#md').html(response).modal();
        $('#md').modal('hide')
    }
    else {
        $('#md').html(response).modal();
        
        //$('#ajaxtab').empty();
    }
    
}

//function displayError(xhr, textStatus, error) {
// $('#ajaxtab').html('<h2>' + textStatus + ': ' + error + '</h2>').modal({
// escClose: true
// });
// setTimeout(function () { $.modal.close(); }, 2500);
// $('#ajaxtab').empty();
//}
function displayError(xhr, textStatus, error) {
    $('#md').html('<h2>' + textStatus + ': ' + error + '</h2>').modal({
        // escClose: true
    });
    setTimeout(function () { $('#res_modal').modal('hide'); }, 2500);
    $('#md').empty();
}
function loading() {
    //$.modal.close();
    $('#md').html('<div class="img" style="width:300px; height:50px; padding-left:200px; padding-top:30px;"><h3>Please wait...</h3></div>').modal();
}

$.validator.unobtrusive.parse(document);

