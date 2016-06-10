$(function () {
    $.fn.extend({
        deleteData: function () {
            $(this).click(function () {
                var url = $(this).attr("del-url");
                var id = $(this).attr("data-id");
                swal({
                    title: "操作提示",   //弹出框的title
                    text: "确定删除吗？",  //弹出框里面的提示文本
                    type: "warning",    //弹出框类型
                    showCancelButton: true, //是否显示取消按钮
                    confirmButtonColor: "#DD6B55",//确定按钮颜色
                    cancelButtonText: "取消",//取消按钮文本
                    confirmButtonText: "是的，确定删除！",//确定按钮上面的文档
                    closeOnConfirm: true
                }, function () {
                    $.post(url, function (data) {
                        swal(data.ReturnMsg, data.ReturnMsg, data.Status);
                        $('#' + id).remove();
                    });
                });
            });
        }
    });
}(jQuery));