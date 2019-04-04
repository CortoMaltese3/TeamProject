$(document).ready(function () {
    var map, markers;

    function initialize() {
        if (locations.length == 0) {
            //alert("No Maps");
            return 0;
        }
        var mapProp = {
            center: new google.maps.LatLng(locations[0][1], locations[0][2]),
            zoom: 12,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById('map'), mapProp);
        var infowindow = new google.maps.InfoWindow();

        markers = new Array();

        for (var i = 0; i < locations.length; i++) {
            addLocation(infowindow, locations[i], i == 0 ? {
                url: '/favicon.ico' , size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            }: undefined);
        }

    }

    function addLocation(infowindow, location,url) {

        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(location[1], location[2]),
            map: map,
            title: location[0],
            icon: url
            
        });

        markers.push(marker);

        if (url == undefined)
        {
            google.maps.event.addListener(marker, 'click', function ()
            {
                infowindow.setContent('<div class="card mb-3 rounded rounded-lg d-flex p-2 bd-highlight d-flex">' +
                    '    <div class="row no-gutters">' +
                    '        <div class="col-md-4">' +
                    '            <img src="' + location[3] + '" class="card-img" alt="' + location[0] + '">' +
                    '        </div>' +
                    '           <div class="col-md-8">' +
                    '                <div class="card-body align-content-end flex-column">' +
                    '                    <h5 class="card-title">' + location[0] + '</h5>' +
                    '                    <h6 class="card-text">' + location[4] + '</h6>' +
                    '                    <a href="/courts/index/' + location[5] + '" class="btn btn-success btn-lg active mt-auto" role="button" aria-pressed="true">View details!</a>' +
                    '                </div>' +
                    '            </div>' +
                    '    </div>' +
                    '</div>');
                infowindow.open(map, marker);

            });
        }
    }
    initialize();
});
