(function ($) {

    //得到url中的路径
    $.getUrlParam = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }

    //字符串格式化 类似c#的string.Format();
    $.format = function (source, params) {
        if (arguments.length == 1)
            return function () {
                var args = $.makeArray(arguments);
                args.unshift(source);
                return $.format.apply(this, args);
            };
        if (arguments.length > 2 && params.constructor != Array) {
            params = $.makeArray(arguments).slice(1);
        }
        if (params.constructor != Array) {
            params = [params];
        }
        $.each(params, function (i, n) {
            source = source.replace(new RegExp("\\{" + i + "\\}", "g"), n);
        });
        return source;
    };

    ///时间格式化  传入格式 和 日期
    $.formatDate = function (pattern, date) {
        //如果不设置，默认为当前时间  
        if (!date) date = new Date();
        if (typeof (date) === "string") {
            if (date == "") date = new Date();
            else date = new Date(date.replace(/-/g, "/"));
        }
        /*补00*/
        var toFixedWidth = function (value) {
            var result = 100 + value;
            return result.toString().substring(1);
        };

        /*配置*/
        var options = {
            regeExp: /(yyyy|M+|d+|h+|m+|s+|ee+|ws?|p)/g,
            months: ['January', 'February', 'March', 'April', 'May',
                     'June', 'July', 'August', 'September',
                      'October', 'November', 'December'],
            weeks: ['Sunday', 'Monday', 'Tuesday',
                    'Wednesday', 'Thursday', 'Friday',
                        'Saturday']
        };

        /*时间切换*/
        var swithHours = function (hours) {
            return hours < 12 ? "AM" : "PM";
        };

        /*配置值*/
        var pattrnValue = {
            "yyyy": date.getFullYear(),                      //年份  
            "MM": toFixedWidth(date.getMonth() + 1),           //月份  
            "dd": toFixedWidth(date.getDate()),              //日期  
            "hh": toFixedWidth(date.getHours()),             //小时  
            "mm": toFixedWidth(date.getMinutes()),           //分钟  
            "ss": toFixedWidth(date.getSeconds()),           //秒  
            "ee": options.months[date.getMonth()],           //月份名称  
            "ws": options.weeks[date.getDay()],              //星期名称  
            "M": date.getMonth() + 1,
            "d": date.getDate(),
            "h": date.getHours(),
            "m": date.getMinutes(),
            "s": date.getSeconds(),
            "p": swithHours(date.getHours())
        };

        return pattern.replace(options.regeExp, function () {
            return pattrnValue[arguments[0]];
        });
    };

})(jQuery);