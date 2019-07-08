
var cookName = "testcookie";
var cookVal = "testvalue";
var date = new Date();
date.setTime(date.getTime() + 604800000);
var expireDate = date.toLocaleDateString();
var path = ";path=/";
var myCookie = cookName + "=" + cookVal + ";expires=" + expireDate + path;
document.cookie = myCookie

function startTime() {
    var today = new Date();

    var d = today.toLocaleDateString();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();

    //d = checkTime(d);
    h = checkTime(h);
    m = checkTime(m);
    s = checkTime(s);

    //document.getElementById("displayTime").innerHTML = h + ":" + m + ":" + s;
    document.getElementById("displayTime").firstChild.nodeValue = d + " - " + h + ":" + m + ":" + s;
    t = setTimeout('startTime()', 500);
}

function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}
