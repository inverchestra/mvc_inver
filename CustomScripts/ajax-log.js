$.validator.unobtrusive.parse(document);

function beginAjax() {
    $.modal.close();
    $('#ajaxtab').html('<div class="img" style="width:300px; height:50px; padding-left:200px; padding-top:30px;"><h3>Please wait...</h3></div>').modal();
}

function errorAjax(xhr, status, error) {
    $('#ajaxtab').html('<h2>' + textStatus + ': ' + error + '</h2>').modal({
        escClose: true
    });
    setTimeout(function () { $.modal.close(); }, 2500);
    $('#ajaxtab').empty();
}

function successAjax(result) {
    $.modal.close();
    alert("create log");


}
function successFeedback(result) {
    $.modal.close();
    alert("We will getback to u soon");


}
function successContact(result) {
    $.modal.close();
    alert("We will getback to u soon");


}