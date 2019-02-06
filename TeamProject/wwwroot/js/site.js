// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#GetLocations').on('click', function () {
        $.getJSON("/api/Locations", function (data) {
            data.forEach(function (d) {
                $('#locations').append('<li><span class="bg-primary">' + d.description + ' => ' + d.latitude + ',' + d.longitude + '</span></li>');
                console.log(d.description);
                console.log(d.latitude);
                console.log(d.longitude);
            });
        });

    });
});
