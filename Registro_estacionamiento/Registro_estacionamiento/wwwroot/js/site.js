function requestJsonService(url, type, data, callback) {
    if (type === "POST") {
        $.ajax({
            type: type,
            url: url,
            data: data,
            dataType: 'json',
            contentType: 'application/json',
            success: function (response) {
                if (typeof (callback) === 'function') {
                    callback(response);
                }
            },
            error: function (jqXHR, exception, thrownError) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status === 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status === 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                swal("Upss! ocurrió un problema", msg, "error");
            }
        });
    } else {
        $.ajax({
            type: type,
            url: url,
            data: data,
            dataType: 'json',
            success: function (response) {
                if (typeof (callback) === 'function') {
                    callback(response);
                }
            },
            error: function (jqXHR, exception, thrownError) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status === 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status === 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                swal("Upss! ocurrió un problema", msg, "error");
            }
        });
    }
}

function uploadFiles(url, type, data, callback) {
    if (type === "POST") {
        $.ajax({
            type: type,
            url: url,
            data: data,
            dataType: 'json',
            contentType: false,
            processData: false,
            cache: false,
            async: false,
            success: function (response) {
                if (typeof (callback) === 'function') {
                    callback(response);
                }
            },
            error: function (jqXHR, exception, thrownError) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status === 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status === 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                swal("Upss! ocurrió un problema", msg, "error");
            }
        });
    }
}
