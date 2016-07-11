var resultMesssage = function (data) {
    if (data.ResultCode === 0) {
        messageData.style.background = 'rgba(0, 131, 17, 0.50)';
    } else {
        messageData.style.background = 'rgba(182, 0, 0, 0.50)';
    }
    messageData.style.visibility = 'visible';
    $('#messageIn').text(data.ErrorMessage + ': ' + data.CustomMessage);
}

var closeButton = function () {
    messageData.style.visibility = 'hidden';
    $('#messageIn').text();
}