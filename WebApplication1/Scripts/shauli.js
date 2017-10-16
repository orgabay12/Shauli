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
* First request - get the city key
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
 * Second request - get the weather
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

/* Posts Create and Update files validation */
// Enable/Disable fileds according to checkboxes
$('#IsImage').change(function () {
    if ($(this).is(":checked")) {
        $("#Image").prop('disabled', false);
    }
    else {
        $("#Image").prop('disabled', true);
    }
});
$('#IsVideo').change(function () {
    if ($(this).is(":checked")) {
        $("#Video").prop('disabled', false);
    }
    else {
        $("#Video").prop('disabled', true);
    }
});

/**
* Facebook api initialization
*/
document.getElementById('shareBtn').onclick = function () {
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
}

/* Google maps callback*/
function initMap() {
    // Verify the current page is the about page
    if ($(location).attr('pathname') !== '/Blog/About') {
        return;
    }
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

/* Google mpas add marker on map function */
function addMarker(lat, lng, label, map) {
    var location = new google.maps.LatLng(lat, lng);
    var marker = new google.maps.Marker({
        position: location,
        map: map
    });
    // Add info window to marker
    google.maps.event.addListener(marker, 'click', function () {
        infowindow = new google.maps.InfoWindow({
            content: label,
        });
        infowindow.open(map, marker);
    });
}

/* Statistics page functionallity */
if ($(location).attr('pathname') === '/Blog/Statistics') {
    $(document).ready(function () {
        // Patch for aside css disorder 
        $('aside').css('display', 'inline-block');

        /* Posts per user pie initialization */
        var canvas = document.querySelector("canvas"),
            context = canvas.getContext("2d");

        var width = canvas.width,
            height = canvas.height,
            radius = Math.min(width, height) / 2;

        var colors = ["#98abc5", "#8a89a6", "#7b6888", "#6b486b", "#a05d56", "#d0743c", "#ff8c00"];

        var arc = d3.arc()
            .outerRadius(radius - 10)
            .innerRadius(0)
            .context(context);

        var labelArc = d3.arc()
            .outerRadius(radius - 40)
            .innerRadius(radius - 40)
            .context(context);

        var pie = d3.pie()
            .sort(null)
            .value(function (d) { return d.Posts; });

        context.translate(width / 2, height / 2);

        d3.json("/Blog/Statistics?publisher=True", function (error, data) {
            if (error) throw error;

            var arcs = pie(data);

            arcs.forEach(function (d, i) {
                context.beginPath();
                arc(d);
                context.fillStyle = colors[i];
                context.fill();
            });

            context.beginPath();
            arcs.forEach(arc);
            context.strokeStyle = "#fff";
            context.stroke();

            context.textAlign = "center";
            context.textBaseline = "middle";
            context.fillStyle = "#000";
            arcs.forEach(function (d) {
                var c = labelArc.centroid(d);
                context.fillText(d.data.Name + " - " + d.data.Posts, c[0], c[1]);
            });
        });


        /* Posts per date bar-chart initialization */
        var svg = d3.select("svg"),
            margin = { top: 20, right: 20, bottom: 30, left: 40 },
            width = +svg.attr("width") - margin.left - margin.right,
            height = +svg.attr("height") - margin.top - margin.bottom;

        var x = d3.scaleBand().rangeRound([0, width]).padding(0.1),
            y = d3.scaleLinear().rangeRound([height, 0]);

        var g = svg.append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

        d3.json("/Blog/Statistics?dates=True", function (error, data) {
            if (error) throw error;

            x.domain(data.map(function (d) { return d.Date; }));
            y.domain([0, d3.max(data, function (d) { return d.Count; })]);

            g.append("g")
                .attr("class", "axis axis--x")
                .attr("transform", "translate(0," + height + ")")
                .call(d3.axisBottom(x));

            g.append("g")
                .attr("class", "axis axis--y")
                .call(d3.axisLeft(y).ticks(3))
                .append("text")
                .attr("transform", "rotate(-90)")
                .attr("y", 6)
                .attr("dy", "0.71em")
                .attr("text-anchor", "end")

            g.selectAll(".bar")
                .data(data)
                .enter().append("rect")
                .attr("class", "bar")
                .attr("x", function (d) { return x(d.Date); })
                .attr("y", function (d) { return y(d.Count); })
                .attr("width", x.bandwidth())
                .attr("height", function (d) { return height - y(d.Count); });
        });
        
    })
}