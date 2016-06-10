$(function () {

    $(".dataDel").deleteData();

    Dropzone.autoDiscover = false;
    $("#dvDescription").dropzone({
        url: "/Admin/Home/ImageUpload",
        previewsContainer: false,
        success: function (file, response, e) {
            var img = ' ![image](' + response + ')';
            console.log(img);
            var description = $("#txtDescription").val() + "<br />" + img;
            console.log(description);
            $("#txtDescription").html(description);
        }
    });
});