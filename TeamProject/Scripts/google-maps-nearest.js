$(document).ready(function () {
    var map, markers;

    function initialize() {
        if (locations.length == 0) {
            //alert("No Maps");
            return 0;
        }
        var mapProp = {
            center: new google.maps.LatLng(locations[0][1], locations[0][2]),
            zoom: 14,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById('map'), mapProp);
        var infowindow = new google.maps.InfoWindow();

        markers = new Array();

        for (var i = 1; i < locations.length; i++) {
            addLocation(infowindow, locations[i]);
        }

    }
    function addLocation(infowindow, location) {

        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(location[1], location[2]),
            map: map,
            title: location[0]
        });

        markers.push(marker);

        google.maps.event.addListener(marker, 'click', function () {
            infowindow.setContent('<div class="card mb-3 rounded rounded-lg border border-success d-flex p-2 bd-highlight d-flex">'+
            '    <div class="row no-gutters">'+
            '        <div class="col-md-4">'+
            '            <img src="' + location[3] + '" class="card-img" alt="' + location[0] + '">'+
            '        </div>'+
            '           <div class="col-md-8">'+
            '                <div class="card-body align-content-end flex-column">'+
            '                    <h5 class="card-title">' + location[0] + '</h5>'+
            '                    <p class="card-text">' + location[4] + '</p>'+
            '                    <a href="/Courts/index" class="btn btn-success btn-lg active mt-auto" role="button" aria-pressed="true">View details!</a>'+
            '                </div>'+
            '            </div>'+
            '    </div>'+
            '</div>');
            infowindow.open(map, marker);
        });
    }
    //function zoomOut() {
    //    map.setZoom(mapzoom);
    //    AutoCenter();
    //}
    //function AutoCenter() {
    //    //  Create a new viewpoint bound
    //    var bounds = new google.maps.LatLngBounds();

    //    for (var i = 0; i < markers.length; i++) {
    //        bounds.extend(markers[i].position);
    //    }
    //    //  Fit these bounds to the map
    //    map.fitBounds(bounds);
    //}
//    google.maps.event.addDomListener(window, 'load', initialize);
    initialize();
});
