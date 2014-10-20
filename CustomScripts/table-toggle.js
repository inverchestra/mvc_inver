$(function () {

    $('table>tbody>tr:nth-child(2n+1)>td.add').click(function (e) {
        e.stopPropagation();
        $(this).parent('tr').trigger('click');
        var id = $(this).find('input[type="hidden"]').val();
        //debugger;
        $.ajax({
            url: '/iHealthUser/HealthLog/AddSymptom',
            type: 'GET',
            async: false,
            cache: false,
            data: { id: id },
            success: function (result) {
                if (checkTimeOut(result)) {
                    $('#modal-div').html(result).modal({
                        closeHTML: '',
                        escClose: true,
                        onShow: function (e) {
                            e.container.css({ 'height': 'auto' });


                            $('#btnCancel').click(function () {
                                $.modal.close();
                            });

                            //From here it is for intellisense
                            function split(val) {
                                return val.split(/,\s*/);
                            }

                            function extractLast(term) {
                                return split(term).pop();
                            }

                            $('#reasons').bind('keydown', function (event) {
                                if (event.keyCode === $.ui.keyCode.TAB &&
                                    $(this).data('autocomplete').menu.active) {
                                    event.preventDefault();
                                }
                            }).autocomplete({
                                source: function (request, response) {
                                    $.getJSON('/iHealthUser/HealthLog/ReturnList', {
                                        term: extractLast(request.term)
                                    }, response);
                                },
                                search: function () {
                                    var term = extractLast(this.value);
                                    if (term.length < 2) {
                                        return false;
                                    }
                                },
                                focus: function () { return false; },
                                select: function (event, ui) {
                                    var terms = split(this.value);
                                    terms.pop();
                                    terms.push(ui.item.value);
                                    terms.push('');
                                    this.value = terms.join(', ');
                                    return false;
                                }
                            });

                            //End of Intellisense

                        }
                    });
                }
            }
        });
    });
    //keerthi changes
    $('input[name="View"]').click(function () {
        var logId = $(this).parents('tr').find('td>input[type="hidden"]').val();
        //debugger;
        $.ajax({
            url: '/iHealthUser/HealthLog/ViewLog',
            type: 'GET',
            async: false,
            cache: false,
            data: { logId: logId },
            success: function (result) {
                if (checkTimeOut(result)) {
                    $('#modal-div').html(result).modal({
                        closeHTML: '',
                        escClose: true,
                        onShow: function (e) {

                            e.container.css({ 'height': 'auto', 'top': '150px' });
                            $("#s3").dropdownchecklist({ maxDropHeight: 150 }, { emptyText: "Click to see", width: 150 });

                        }
                    });
                }
            }
        });
    });
    //keerhi chnages
    $('tbody tr td img.btnEdit').click(function () {
        var logId = $(this).parents('tr').find('td>input[type="hidden"]').val();
        //debugger;
        $.ajax({
            url: '/iHealthUser/HealthLog/EditLog',
            type: 'GET',
            async: false,
            data: { logId: logId },
            success: function (result) {
                if (checkTimeOut(result)) {
                    $('#modal-div').html(result).modal({
                        closeHTML: '',
                        escClose: true,
                        onShow: function (e) {
                            e.container.css({ 'height': 'auto' });
                            $("#s3").dropdownchecklist({ maxDropHeight: 150 }, { emptyText: "Please select", width: 150 });
                            $('#btnCancel').click(function () {
                                $.modal.close();
                            });
                        }
                    });
                }
            }
        });
    });

    //For DetaildViewCase
    $('input[name="CaseView"]').click(function () {
        //    alert("clicking detailed view of case");
        var CaseId = $(this).parents('tr').find('td>input[type="hidden"]').val();
        $.ajax({
            url: '/iHealthUser/HealthRecord/ViewCase',
            type: 'GET',
            async: false,
            cache: false,
            data: { CaseId: CaseId },
            success: function (result) {
                $('#tab').html(result).modal({
                    closeHTML: '',
                    escClose: true,
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto', 'top': '90px' });

                        $('.accordion').accordion({
                            collapsible: true
                        });

                        $('#btnCancel').click(function () {
                            $.modal.close();
                        });


                    }
                });
            }
        });
    });
    // Prashanth For Edit Click on MyCase table

    // for EditViewCase
    $('img[name="editViewCaseImgBtn"]').click(function () {
        var CaseId = $(this).parents('tr').find('td>input[type="hidden"]').val();
        $.ajax({
            url: '/iHealthUser/HealthRecord/EditViewCase',
            type: 'GET',
            async: false,
            cache: false,
            data: { CaseId: CaseId },
            success: function (result) {
                $('#tab').html(result).modal({
                    closeHTML: '',
                    escClose: true,
                    onShow: function (e) {

                        e.container.css({ 'height': 'auto', 'top': '90px' });

                        $('ul.accordion').accordion({ collapsible: true });

                        $('#btnCancel').click(function () {
                            $.modal.close();
                        });
                        $('input[name="cancel"]').click(function () {
                            $(this).parents('div.slide').hide('slide', { direction: 'down' }, 600);
                        });
                    }
                });
            }
        });
    });
    //End Prashath

    //AD Code to Delete Case
    $('table>tbody>tr:nth-child(2n+1)>td.delete').click(function (e) {
        e.stopPropagation();
        var id = $(this).find('input[type="hidden"]').val();
        var response = confirm("Please confirm if you really want to delete");
        if (response) {
            $.ajax({
                url: "/iHealthUser/Cases/DeleteCase",
                type: 'POST',
                async: false,
                cache: false,
                data: { id: id },
                success: function (success) {
                    if (success == "1020") {
                        alert("Deleted Successfully");
                        location.reload();

                    }
                    else {
                        alert("Delete Failed");
                    }
                }

            });
        }

    });

    //    $('#btnCaseDownload').click(function () {
    //        //alert("Clicked btnDownload");
    //        // debugger;

    //        var tr = $('table tbody tr:nth-child(2n+1)').filter(':has(:checkbox:checked)');

    //        var id = tr.find('td>input[type="hidden"]').val();


    //        if (tr.size() > 1) {
    //            alert("Please select only one Case");
    //        }
    //        else {
    //            if (isNaN(id)) {
    //                alert("Please select one Case");
    //                return false;

    //            }

    //            else {
    //                var result = confirm("Do you want related Cases?");
    //                if (result) {
    //                    $.fileDownload('/iHealthUser/Cases/DownloadFile', { httpMethod: "POST", data: { CaseId: id, strRelatedCases: "YesRelatedCases"} });
    //                }
    //                else {
    //                    $.fileDownload('/iHealthUser/Cases/DownloadFile', { httpMethod: "POST", data: { CaseId: id, strRelatedCases: "NoRelatedCases"} });
    //                }
    //            }
    //        }

    //    });
    //    $('.caseDownload').click(function () {

    //        var Id = $(this).parents('td').find('input[type="hidden"]').val();
    //         var id = $(this).find('input[type="hidden"]').val();

    //        if (isNaN(Id)) {
    //           alert("Please select one Case");
    //            return false;

    //        }

    //        else {
    //            var result = confirm("Do you want related Cases?");
    //            if (result) {
    //                $.fileDownload('/iHealthUser/Cases/DownloadFile', { httpMethod: "POST", data: { CaseId: Id, strRelatedCases: "YesRelatedCases"} });
    //            }
    //            else {
    //                $.fileDownload('/iHealthUser/Cases/DownloadFile', { httpMethod: "POST", data: { CaseId: Id, strRelatedCases: "NoRelatedCases"} });
    //            }
    //        }

    //    });
    $('.caseDownload').click(function () {
       // alert("onclick");
        var Id = $(this).parents('td').find('input[type="hidden"]').val();
//        alert(Id);
//        if (isNaN(Id)) {
//            alert("Please select one Case");
//            return false;

//        }
//        else {
            var result = confirm("Do you want to download related Cases?");
            if (result) {
                $.fileDownload('/iHealthUser/Cases/DownloadFile', { httpMethod: "POST", data: { CaseId: Id, strRelatedCases: "YesRelatedCases"} });
            }
            else {
                $.fileDownload('/iHealthUser/Cases/DownloadFile', { httpMethod: "POST", data: { CaseId: Id, strRelatedCases: "NoRelatedCases"} });
            }
//        }
    });
    //added by jagadeesh

    $('.caseDownload1').click(function () {
        var Id = $('#caseid').val();
      
        var result = confirm("Do you want to download related Cases?");
        if (result) {
            $.fileDownload('/iHealthUser/Cases/DownloadFile', { httpMethod: "POST", data: { CaseId: Id, strRelatedCases: "YesRelatedCases"} });
        }
        else {
            $.fileDownload('/iHealthUser/Cases/DownloadFile', { httpMethod: "POST", data: { CaseId: Id, strRelatedCases: "NoRelatedCases"} });
        }


    });
    //end.


});