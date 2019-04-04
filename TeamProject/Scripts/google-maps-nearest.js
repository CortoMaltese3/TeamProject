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

        // add seach location marker 
        addMarker(locations[0], {
            url: '/favicon.ico', size: new google.maps.Size(71, 71),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(25, 25)
        });

        for (var i = 1; i < locations.length; i++) {

            // add marker
            let marker = addMarker(locations[i]);

            // add marker event
            addMarkerEvent(infowindow, locations[i], marker);

            markers.push(marker);
        }

    }

    function addMarkerEvent(infowindow, location, marker) {
        let card = $('.hidden-marker-card .card').clone();

        // set card info
        card.find('img')
            .attr('src', location[3])
            .attr('alt', location[0]);
        card.find('.card-body a')
            .attr('href', `${location[5]}`);
        card.find('.card-title')
            .text(location[0]);
        card.find('.card-text')
            .text(location[4]);

        // set event handler
        google.maps.event.addListener(marker, 'click', function () {

            infowindow.setContent(card[0]);

            infowindow.open(map, marker);

        });
    }

    function addMarker(location, url) {

        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(location[1], location[2]),
            map: map,
            title: location[0],
            icon: url
        });

        return marker;
    }
    initialize();
});
