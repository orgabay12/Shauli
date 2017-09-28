/**
This is shauli project maing js file
*/

/*
Consts
*/
WHEATHER_API_KEY = "Sy3NGZwCpFGxObRlFcn6i8mqvTnEeGwj";
WHEATHER_API_URL = "http://dataservice.accuweather.com";
WHEATHER_API_SEARCH_PATH = "/locations/v1/cities/search";
WHEATHER_API_WEATHER_PATH = "/currentconditions/v1/";


/* Color navbar items according to url */
$(document).ready(function () {
    switch ($(location).attr('pathname')) {
        case '/Blog/About':
            $('#navabout').addClass('selected');
            break;
        case '/Fans':
            $('#navfans').addClass('selected');
            break;
        case '/Posts':
            $('#navposts').addClass('selected');
            break;
        case '/Blog/Statistics':
            $('#navstats').addClass('selected');
            break;
        default:
            $('#navblog').addClass('selected');
    }
});


/**
* Weather web service handlers
* First request for the city key
*/
$('#weatherWrap button').click(function(){
    var city = $('#cities').val();
    var args = "?apikey=" + WHEATHER_API_KEY + "&q=" + city;
    var searchUrl = WHEATHER_API_URL + WHEATHER_API_SEARCH_PATH + args;
    $.get(searchUrl, function (data, status, xhr) {
        var cityKey = data[0].Key;
        getWeather(cityKey);
    }, "json");
});
/**
 * Second request for the weather
 * @param {any} cityKey - from the first request
 */
function getWeather(cityKey) {
    var args = "?apikey=" + WHEATHER_API_KEY;
    var weather_url = WHEATHER_API_URL + WHEATHER_API_WEATHER_PATH + cityKey + args;
    $.get(weather_url, function (data, status, xhr) {
        weatherText = data[0].WeatherText + ", " + data[0].Temperature.Metric.Value + " " + data[0].Temperature.Metric.Unit;
        $('#weatherRes').text(weatherText);
    }, "json");
}


/**
* Facebook api initiate
*/
document.getElementById('shareBtn').onclick = function () {
    console.log("asfd");
    FB.init({
        appId: '1648053898603021',
        autoLogAppEvents: true,
        xfbml: true,
        version: 'v2.10'
    });
    FB.ui({
        method: 'share',
        href: 'http://www.mako.co.il/Tagit/%D7%A9%D7%90%D7%95%D7%9C%D7%99',
        title: "Shauli",
    }, function (response) { }
    )
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));



/* About page functionallity */
if ($(location).attr('pathname') === '/Blog/About') {
    /* Canvas draw image */
    var context = document.getElementById('myCanvas').getContext("2d");
    var img = new Image();
    img.onload = function () {
        context.drawImage(img, 0, 0);
    }
    img.src = "/Content/images/shuliabout.jpg";

    /* Google maps*/
    function initMap() {
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 3,
            center: new google.maps.LatLng(40.717245, 15.907102)
        });
        $.get("/Blog/About", function (centers, status, xhr) {
            for (var i = 0; i < centers.length; i++) {
                var label = "<h5>" + centers[i].city + "</h5>" + "<h6>" + centers[i].description + "</h6>"
                addMarker(centers[i].lat, centers[i].lng, label, map)
            };
        }, "json");
    }
    function addMarker(lat, lng, label, map) {
        var location = new google.maps.LatLng(lat, lng);
        var marker = new google.maps.Marker({
            position: location,
            map: map
        });
        google.maps.event.addListener(marker, 'click', function () {
            infowindow = new google.maps.InfoWindow({
                content: label,
            });
            infowindow.open(map, marker);
        });
    }

}