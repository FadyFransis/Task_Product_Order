$(document).ready(function () {
    $('#SliderImageUrl').attr('src', document.getElementById('ImageUrl').value);
});
function readURL(input) {
    
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#SliderImageUrl').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$("#file-input").change(function () {
    readURL(this);
});
$('#form-submit-btn').click(function () {
    $('#form-submit-btn').addClass('disabled');
    uploadImage();
});

function uploadImage() {
    var formData = new FormData();
    if ($('#file-input')[0].files.length > 0) {
        formData.append('file', $('#file-input')[0].files[0]);
        $.ajax({
            url: UploadUrl + 'API/Common/UploadFile/Slider',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                if (data.isError === false) {
                    document.getElementById('ImageUrl').value = data.result[0].url;
                    $('#form-slider').submit();
                }
            },
            error: function (error) {
                alert(error.message)
            }

        });
    }
    else {
        if (document.getElementById('Id').value > 0 && document.getElementById('ImageUrl').value != undefined) {
            document.getElementById('ImageUrl').value = '/Uploads' + document.getElementById('ImageUrl').value.split('Uploads')[1]
        }
        $('#form-slider').submit();
    }
}