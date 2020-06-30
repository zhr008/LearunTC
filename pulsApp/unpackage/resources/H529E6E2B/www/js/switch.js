/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 开关插件
 */
(function ($, learun, fui, window) {
    "use strict";
    // 开关插件(初始化)
    $.fn.lrswitch = function () {
        var $this = $(this);
        $this.attr('type', 'lrswitch');
        $this.on('tap', function () {
            learun.formblur();
        });
        return $this.fswitch();
    };
    // 开关插件(设置值)1选中 0 没有选中
    $.fn.lrswitchSet = function (value) {
        $(this).fswitchSet(value);
    };
    // 开关插件(获取值)
    $.fn.lrswitchGet = function () {
        return $(this).fswitchGet();
    };
})(window.jQuery, window.lrmui, window.fui, window);