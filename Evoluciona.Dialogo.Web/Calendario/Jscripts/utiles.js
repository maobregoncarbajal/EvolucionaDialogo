
function getDay(fecha) {
    var day = fecha.getDate();
    day = day < 10 ? ('0' + day) : day;
    return day;
}

function getMonth(fecha) {
    var month = fecha.getMonth() + 1;
    month = month < 10 ? ('0' + month) : month;
    return month;
}

function getHours(fecha) {
    var hour = fecha.getHours();
    hour = hour < 10 ? ('0' + hour) : hour;
    return hour;
}

function getMinutes(fecha) {
    var minute = fecha.getMinutes();
    minute = minute < 10 ? ('0' + minute) : minute;
    return minute;
}