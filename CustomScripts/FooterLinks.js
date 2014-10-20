$(document).ready(function () {
    // --------------------------------------------------------
    // Businesspage footer links
    // --------------------------------------------------------
    $('#files #services').click(function () {
      //  alert('services click');
        $.ajax({
            url: '/Home/Services',
            data: { detector: $(this).attr('id') },
            cache: false,
            type: 'GET',
            success: function (response) {
                $('#ajaxtab').html(response).modal({
                    escClose: true
                    //closeHTML: ''
                });
            }
        });
        return false;
    });
    $('#files #pricing').click(function () {
       //  alert('pricing click');
        $.ajax({
            url: '/Home/Pricing',
            data: { detector: $(this).attr('id') },
            cache: false,
            type: 'GET',
            success: function (response) {
                $('#ajaxtab').html(response).modal({
                    escClose: true
                    //closeHTML: ''
                });
            }
        });
        return false;
    });
    $('#files #blog').click(function () {
       //  alert('blog click');
        $.ajax({
            url: '/Home/Blog',
            data: { detector: $(this).attr('id') },
            cache: false,
            type: 'GET',
            success: function (response) {
                $('#ajaxtab').html(response).modal({
                    escClose: true
                    //closeHTML: ''
                });
            }
        });
        return false;
    });
    $('#files #careers').click(function () {
       //  alert('careers click');
        $.ajax({
            url: '/Home/Careers',
            data: { detector: $(this).attr('id') },
            cache: false,
            type: 'GET',
            success: function (response) {
                $('#ajaxtab').html(response).modal({
                    escClose: true
                    //closeHTML: ''
                });
            }
        });
        return false;
    });
    $('#files #contactus').click(function () {
      //  alert('contactus click');
        $.ajax({
            url: '/Home/FContactus',
            data: { detector: $(this).attr('id') },
            cache: false,
            type: 'GET',
            success: function (response) {
                $('#ajaxtab').html(response).modal({
                    escClose: true
                    //closeHTML: ''
                });
            }
        });
        return false;
    });

    //-------------------------------------------------------------------------
    //                      iHealth User layout footer links
    //-------------------------------------------------------------------------
    
    $('#mfservices').click(function () {
       // alert('mfservices click');
      //  alert($(this).attr('id'));
        $.ajax({
            url: '/iHealthUser/AccountSettings/MFServices',
            data: { detector: $(this).attr('id') },
            cache: false,
            type: 'GET',
            success: function (response) {
                $('#tab').html(response).modal({
                    escClose: true
                    //closeHTML: ''
                });
            }
        });
        return false;
    });
    $('#mfsupport').click(function () {
      //  alert('mfsupport click');
      //  alert($(this).attr('id'));
        $.ajax({
            url: '/iHealthUser/AccountSettings/MFSupport',
            data: { detector: $(this).attr('id') },
            cache: false,
            type: 'GET',
            success: function (response) {
                $('#tab').html(response).modal({
                    escClose: true
                    //closeHTML: ''
                });
            }
        });
        return false;
    });
    $('#mfcontact').click(function () {
       // alert('mfcontact click');
      //  alert($(this).attr('id'));
        $.ajax({
            url: '/iHealthUser/AccountSettings/MFContact',
            data: { detector: $(this).attr('id') },
            cache: false,
            type: 'GET',
            success: function (response) {
                $('#tab').html(response).modal({
                    escClose: true
                    //closeHTML: ''
                });
            }
        });
        return false;
    });

});