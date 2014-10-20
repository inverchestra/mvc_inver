$(function () {
    var pgurl = window.location.href.substr(window.location.href.lastIndexOf("/") + 1);
    $("#mid-head li a").each(function () {
        //debugger;
        if ($(this).attr('href').substr($(this).attr("href").lastIndexOf('/') + 1) == pgurl)
            $(this).find('div.selectedtab').addClass("selected-tab");
    });

});

function checkTimeOut(data) {
    if (data != "_LogOn_") {
        return true;
    } else {
        window.location.href = "/Home/Home";
    }
}