$(function () {

    //登录验证
    $('#loginForm').bootstrapValidator({
        message: '这个值没有被验证',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            Username: {
                message: '用户名还没有验证',
                validators: {
                    notEmpty: {
                        message: '用户名不能为空'
                    },
                    stringLength: {
                        min: 1,
                        max: 16,
                        message: '用户名长度在1到16位之间'
                    }
                }
            },
            Password: {
                message: '密码还没有验证',
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    stringLength: {
                        min: 6,
                        max: 16,
                        message: '密码长度在6到16之间'
                    },
                    //regexp: {
                    //    regexp: /^(?![0-9a-z]+$)(?![0-9A-Z]+$)(?![0-9\W]+$)(?![a-z\W]+$)(?![a-zA-Z]+$)(?![A-Z\W]+$)[a-zA-Z0-9\W_]+$/,
                    //    message: '密码必须包括至少一个数字，大写字母，小写字母，特殊字符'
                    //},
                    different: {
                        field: 'username',
                        message: '密码不能和用户名相同'
                    }
                }
            }
        }
    }).on('success.form.bv', function (e) {
        // Prevent form submission
        e.preventDefault();
        // Get the form instance
        var $form = $(e.target);
        // Get the BootstrapValidator instance
        var bv = $form.data('bootstrapValidator');
        // Use Ajax to submit form data
        $.post("/Account/Login", $form.serialize(), function (data) {
            console.log(data)
            console.log(typeof (data.Status));
            if (data.Status == "ok") {
                window.location.href = "/Home/Index";
            }
            else if (data.Status == "error") {
                //  $("#warning").html(data.Message);
                alert(data.Message);
            }
            else {
                //   $("#warning").html("未知错误！");
                alert("未知错误!");
            }
        });
    });

    //注册验证
    $('#registerForm').bootstrapValidator({
        message: '这个值没有被验证',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            email: {
                validators: {
                    notEmpty: {
                        message: '邮箱是必填项'
                    },
                    emailAddress: {
                        message: '邮箱格式正确'
                    }
                }
            },
            Username: {
                message: '用户名还没有验证',
                validators: {
                    notEmpty: {
                        message: '用户名不能为空'
                    },
                    stringLength: {
                        min: 1,
                        max: 16,
                        message: '用户名长度在1到16位之间'
                    }
                }
            },
            Password: {
                message: '密码还没有验证',
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    },
                    stringLength: {
                        min: 6,
                        max: 16,
                        message: '密码长度在6到16之间'
                    },
                    different: {
                        field: 'Username',
                        message: '密码不能和用户名相同'
                    }
                }
            },
            Confirm: {
                message: '密码重复还没有验证',
                validators: {
                    notEmpty: {
                        message: '密码重复不能为空'
                    },
                    stringLength: {
                        min: 6,
                        max: 16,
                        message: '密码长度在6到16之间'
                    },
                    identical: {
                        field: 'Password',
                        message: '两次密码不同请重新输入'
                    }
                }
            }
        }
    }).on('success.form.bv', function (e) {
        // Prevent form submission
        e.preventDefault();
        // Get the form instance
        var $form = $(e.target);
        // Get the BootstrapValidator instance
        var bv = $form.data('bootstrapValidator');
        // Use Ajax to submit form data
        $.post("/Account/Register", $form.serialize(), function (data) {
            console.log(data)
            if (data.Status == "ok") {
                window.location.href = "/Home/Index";
            }
            else if (data.Status == "error") {
                //$("#warning").html(data.Message);
                alert(data.Message);
            }
            else {
                //  $("#warning").html("未知错误！");
                alert("未知错误!");
            }
        });
    });

    //$('#loginModal').on('show.bs.modal', function (e) {
    //    $("#txtPassword").parent().removeClass("has-error");
    //    $("#txtUsername").parent().removeClass("has-error");
    //    $(".warning").html("");
    //});

    //$('#loginModal').on('shown.bs.modal', function (e) {
    //    $("#txtUsername").focus();
    //});

    $("#loginModal").on('hide.bs.modal', function (e) {
        $("#txtPassword").val("");
        $("#txtUsername").val("");
        $(".warning").html("");
    });

});