/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.06.12
 * 描 述：力软移动端框架 列表左移按钮
 */
(function ($, learun, fui, window) {
    "use strict";

    var _lrlistswipe = {
        init: function ($this, op) {
            $this.addClass('f-swipebtn');
            var $handler = $('<div class="f-swipebtn-handler">' + $this.html() + '</div>');
            var $btn = $('<div class="f-swipebtn-right"></div>');
            $this.html($handler);
            $this.append($btn);
            $.each(op.btns, function (_index, _item) {
                var _$item = $(_item).addClass('f-swipebtn-btn');
                $btn.append(_$item);
            });

            $this.fswipebtn();

            $this = null;
            $handler = null;
            $btn = null;
        }
    };

    $.fn.lrlistswipe = function (op) {
        var dfop = {
            btns: []
        };
        $.extend(dfop, op || {});
        $(this).each(function () {
            _lrlistswipe.init($(this), dfop);
        });

    };
})(window.jQuery, window.lrmui, window.fui, window);