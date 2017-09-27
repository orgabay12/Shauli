/**
This is shauli project maing js file
*/

/*
Consts
*/
WHEATHER_API_KEY = "g8a4zcmnyTcOwb0OdkTafAvwTnSjF2vu";
WHEATHER_API_URL = "http://dataservice.accuweather.com";
WHEATHER_API_SEARCH_PATH = "/locations/v1/cities/search";
WHEATHER_API_WEATHER_PATH = "/currentconditions/v1/";


/* Color navbar items according to url */
$(document).ready(function () {
    switch ($(location).attr('pathname')) {
        case '/Fans':
            $('#navfans').addClass('selected');
            break;
        case '/Posts':
            $('#navposts').addClass('selected');
            break;
        case '/Statistics':
            $('#navstats').addClass('selected');
            break;
        default:
            $('#navblog').addClass('selected');
    }
});

function getWeather(cityKey) {
    var args = "?apikey=" + WHEATHER_API_KEY;
    var weather_url = WHEATHER_API_URL + WHEATHER_API_WEATHER_PATH + cityKey + args;
    $.get(weather_url, function (data, status, xhr) {
        weatherText = data[0].WeatherText + ", " + data[0].Temperature.Metric.Value + " " + data[0].Temperature.Metric.Unit;
        $('#weatherRes').text(weatherText);
    }, "json");
}

$('#weatherWrap button').click(function(){
    var city = $('#cities').val();
    var args = "?apikey=" + WHEATHER_API_KEY + "&q=" + city;
    var searchUrl = WHEATHER_API_URL + WHEATHER_API_SEARCH_PATH + args;
    $.get(searchUrl, function (data, status, xhr) {
        var cityKey = data[0].Key;
        getWeather(cityKey);
    }, "json");
});
