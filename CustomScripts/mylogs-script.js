$(function () {

    $.ajaxSetup({ cache: false });

    $('.btnDownload').click(function () {
        var Id = $(this).parents('tr').find('td>input[type="hidden"]').val();
     
        var result = confirm("Do you want related Logs?");
        if (result) {
            $.fileDownload('/iHealthUser/HealthLog/DownloadFile', { httpMethod: "POST", data: { LogID: Id, strRelatedLogs: "YesRelatedLogs"} });
        }
        else {
            $.fileDownload('/iHealthUser/HealthLog/DownloadFile', { httpMethod: "POST", data: { LogID: Id, strRelatedLogs: "NoRelatedLogs"} });
        }

    });

    $('#btnLogtoLogView').click(function () {

        var logIds = $('#s3').val();

        if (logIds.length > 1) {
            alert("you have selected more than one log,please select only one log");
        }
        else {
            var logId = logIds[0].toString();
            //                    alert(logId);
            $.ajax({
                url: '/iHealthUser/HealthLog/ViewLogToLog',
                type: 'GET',
                async: false,
                cache: false,
                data: { logId: logId },
                success: function (result) {
                    $('#modal-div1').html(result).show('slide', { direction: 'down' }, 600);

                    $('#btnlogCancel').click(function () {

                        $("#modal-div1").hide("slide", { direction: 'down' }, 600);
                    });



                }
            });
        }
    });

    $('#btnCreate').click(function () {
        $.ajax({
            url: '/iHealthUser/HealthLog/CreateNewLog',
            type: 'GET',
            async: false,
            success: function (result) {
                if (checkTimeOut(result)) {
                    $('#modal-div').html(result).modal({

                        closeHTML: "",
                        onShow: function (e) {
                            e.container.css({ 'height': 'auto' });
                            $("#s3").dropdownchecklist({ maxDropHeight: 150 }, { emptyText: "Please select ...", width: 150 });
                            $('#modal-div1').hide();
                            $('#btnCaseLogView').click(function () {

                                var logIds = $('#s3').val();

                                if (logIds.length > 1) {
                                    alert("you have selected more than one log,please select only one log");
                                }
                                else {
                                    var logId = logIds[0].toString();
                                    //                    alert(logId);
                                    $.ajax({
                                        url: '/iHealthUser/HealthLog/ViewLogToLog',
                                        type: 'GET',
                                        async: false,
                                        cache: false,
                                        data: { logId: logId },
                                        success: function (result) {
                                            $('#modal-div1').html(result).show('slide', { direction: 'down' }, 600);
                                            $('.accordion').accordion({
                                                collapsible: true
                                            });
                                            $('#btnlogCancel').click(function () {

                                                $("#modal-div1").hide("slide", { direction: 'down' }, 600);
                                            });



                                        }
                                    });
                                }
                            });

                            $('#date').datepicker({

                                changeYear: true,
                                changeMonth: true,
                                maxDate: new Date()
                            });

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
                                    if (term.length < 1) {
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
});