$(document).ready(function () {
    var autocomplete;
    var postParameterLat = $('#latitude');
    var postParameterLon = $('#longitude');
    var placeLocation;

    autocomplete = new google.maps.places.Autocomplete((document.getElementById('autocomplete')), { types: ['geocode'] });

    // Στο autocomplete γεμιζει το placeLocation με το location που εχει βρει και κανει submit
    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        var place = autocomplete.getPlace();

        if (place.geometry) {
            placeLocation = place.geometry.location;
            $('#form-submit').submit();
        }
    });

    // Ελεγχος αν εχει στοιχεια απο το autocomplete ωστε να γεμισει 
    // τα κρυφα πεδια postParameterLat, postParameterLon και να συνεχισει το submit
    $('#form-submit').submit(function (e) {
        if (placeLocation == undefined) {
            e.preventDefault();
        }
        postParameterLat.val(placeLocation.lat());
        postParameterLon.val(placeLocation.lng());
    });

});
