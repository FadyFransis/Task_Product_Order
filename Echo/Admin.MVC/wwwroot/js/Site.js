//var UploadUrl = "https://echoapi.sbtechnology.host/"
var UploadUrl = "https://localhost:44369/"
//var UploadUrl = "https://localhost:44369/"

function createAdminAlert(title, summary, details, severity) {

    var iconMap = {
        info: "fa fa-info-circle",
        success: "fa fa-thumbs-up",
        warning: "fa fa-exclamation-triangle",
        danger: "fa ffa fa-exclamation-circle"
    };

    var iconAdded = false;

    var alertClasses = ["alert", "animated", "flipInX"];
    alertClasses.push("alert-" + severity.toLowerCase());


    alertClasses.push("alert-dismissible");


    var msgIcon = $("<i />", {
        "class": iconMap[severity] // you need to quote "class" since it's a reserved keyword
    });

    var msg = $("<div />", {
        "class": alertClasses.join(" ") // you need to quote "class" since it's a reserved keyword
    });

    if (title) {
        var msgTitle = $("<h4 />", {
            html: title
        }).appendTo(msg);

        if (!iconAdded) {
            msgTitle.prepend(msgIcon);
            iconAdded = true;
        }
    }

    if (summary) {
        var msgSummary = $("<strong />", {
            html: summary
        }).appendTo(msg);

        if (!iconAdded) {
            msgSummary.prepend(msgIcon);
            iconAdded = true;
        }
    }

    if (details) {
        var msgDetails = $("<p />", {
            html: details
        }).appendTo(msg);

        if (!iconAdded) {
            msgDetails.prepend(msgIcon);
            iconAdded = true;
        }
    }



    var msgClose = $("<span />", {
        "class": "close", // you need to quote "class" since it's a reserved keyword
        "data-dismiss": "alert",
        html: "<i class='fa fa-times-circle'></i>"
    }).appendTo(msg);


    $('#alertMessage').prepend(msg);


    setTimeout(function () {
        msg.addClass("flipOutX");
        setTimeout(function () {
            msg.remove();
        }, 1000);
    }, 4000);

};

function fireSuccessAlert(_textAr, _textEn) {
    document.getElementById('language').value === 'ar' ?
        createAdminAlert(null, _textAr, null, 'success')
        : createAdminAlert(null, _textEn, null, 'success')
};
function fireErrorAlert(_textAr, _textEn) {
    document.getElementById('language').value === 'ar' ?
        createAdminAlert(null, _textAr, null, 'success')
        : createAdminAlert(null, _textEn, null, 'success')

};


function loaderHandler(state) {
    switch (state) {
        case 'show':
            $("#preloader-active").fadeIn("slow");
            break;

        case 'hide':
            $("#preloader-active").fadeOut("slow");
            break;
    }
}

$(window).on("load", function () {
    if (!window.location.href.toLocaleLowerCase().includes('product/edit'))
        $("#preloader-active").fadeOut("slow");
});

$('.changable').on('change', function (e) {
    document.getElementById("form-submit-btn").disabled = false;
});