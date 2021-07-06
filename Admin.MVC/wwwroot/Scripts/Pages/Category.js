$(document).ready(function () {
    $('#CategoryImageUrl').attr('src', document.getElementById('ImageUrl').value);
});
function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#CategoryImageUrl').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$("#file-input").change(function () {
    readURL(this);
});
$('#form-submit-btn').click(function () {
    uploadImage();
});

function uploadImage() {
    var formData = new FormData();
    if ($('#file-input')[0].files.length > 0) {
        formData.append('file', $('#file-input')[0].files[0]);
        if ($('#file-input')[0].files[0].size > 1024 * 1024 * 1) {
            alert('Max size is 1 MB')
            return;
        }
        loaderHandler('show')
        $.ajax({
            url: UploadUrl + 'API/Common/UploadFile/Category',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                document.getElementById('ImageUrl').value = data.result[0].url;
                $('#form-category').submit();
            },
            error: function (error) {
                console.log(error)
            }

        });
    }
    else {
        if (document.getElementById('Id').value > 0 && document.getElementById('ImageUrl').value != undefined) {
            document.getElementById('ImageUrl').value ='/Uploads'+  document.getElementById('ImageUrl').value.split('Uploads')[1]
        }
        $('#form-category').submit();
    }

}


