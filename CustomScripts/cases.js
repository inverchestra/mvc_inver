$(function () {
    $('#btnCreate').click(function () {
        $.ajax({
            url: '/iHealthUser/HealthRecord/CreateCase',
            cache: false,
            async: false,
            type: 'GET',
            success: function (result) {
               
                $('#tab').html(result).modal({
                    onShow: function (e) {
                        e.container.css({ 'height': 'auto' });
                        $('#cancel').click(function () {
                            $.modal.close();
                        });

                        $('ul.accordion').accordion({
                            collapsible: true
                        });

                        $('input[name="cancel"]').click(function () {
                            //$(this).parents('form').get(0).reset();
                            $(this).parents('div.slide').hide('slide', { direction: 'down' }, 600);
                        });

                        $('#procdiv').click(function () {
                            $('#procedures').show('slide', { direction: 'down' }, 600);
                        });

                        $('#medtdiv').click(function () {
                            $('#medicaltests').show('slide', { direction: 'down' }, 600);
                        });

                        $('#meddiv').click(function () {
                            $('#medications').show('slide', { direction: 'down' }, 600);
                        });

                        $('#ad').click(function () {
                            $('#handdinfod').show('slide', { direction: 'down' }, 600);
                        });

                        $('a').click(function () {
                            $('#handdinfo, #procedures, #medicaltests, #medications,#docview').hide('slide', { direction: 'down' }, 600);
                        });

                        $("#s1,#s2").dropdownchecklist();

                        $('#btnCaseLogView').click(function () {
                            var logIds = $('#s2').val();
                            var Caseids = $('#s1').val();
                            if (logIds.length > 1) {
                                alert("ur selcected more than one value for view, Please select only one item");
                            }
                            else {
                                var logId = logIds[0].toString();

                                $.ajax({
                                    url: '/iHealthUser/HealthLog/ViewLog',
                                    type: 'GET',
                                    async: false,
                                    cache: false,
                                    data: { logId: logId },
                                    success: function (result) {
                                        $('#modal-div1').html(result).show('slide', { direction: 'down' }, 600);
                                        $('.accordion').accordion({
                                            collapsible: true
                                        });
                                        $('#modal-div1').find('#btnCancel').click(function () {
                                            $("#modal-div1").hide("slide", { direction: 'down' }, 600);
                                        });


                                    }
                                });
                            }

                        });

                        //forCaseToCaseView
                        $('#btnCaseView').click(function () {

                            var Caseids = $('#s1').val();
                            if (Caseids.length > 1) {
                                alert("ur selcected more than one value for view, Please select only one item");
                            }
                            else {
                                var caseId = Caseids[0].toString();

                                $.ajax({
                                    url: '/iHealthUser/HealthRecord/ViewCase',
                                    type: 'GET',
                                    async: false,
                                    cache: false,
                                    data: { caseId: caseId },
                                    success: function (result) {
                                        $('#tab1').html(result).show('slide', { direction: 'down' }, 600);
                                        $('.accordion').accordion({
                                            collapsible: true
                                        });
                                        $('#btnCancel').click(function () {
                                            $("#tab1").hide("slide", { direction: 'down' }, 600);
                                        });


                                    }
                                });
                            }
                        });
                        //end
                    }
                });
            }
        });
    });


});