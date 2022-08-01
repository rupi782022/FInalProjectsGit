function ajaxCall2(method, api, data, successCB, errorCB) {
    $.ajax({
        type: method,
        url: api,
        data: data,
        cache: false,
        headers: {
            'x-rapidapi-key': '3d6c24db64msh3beae04c060ecdap1a453djsn478b3e38fe7a',   // replace it with your own key
        },
        contentType: "application/json",
        dataType: "json",
        success: successCB,
        error: errorCB
    });
}