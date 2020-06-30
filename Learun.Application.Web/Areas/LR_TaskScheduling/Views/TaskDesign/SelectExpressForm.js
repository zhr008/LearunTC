var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initData();
            page.bind();
        },
        bind: function () {
            ///设置配置信息的滚动条
            $('.expression').lrscroll();
            $('.express-list li').click(function () {
                $(this).addClass("active").siblings().removeClass("active");
            })
        },
        initData: function () {
            var cron = top.layer_form.cron;
            if (!!cron) {
                $('#F_CronExpression').find('li').each(function () {
                    if ($(this).attr("data-value") == cron) {
                        $(this).addClass("active").siblings().removeClass("active");
                    }
                })
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        var cron = $('.express-list li.active').attr("data-value");
        if (!!callBack) {
            callBack(cron);
        }
        return true;
    };
    page.init();

}