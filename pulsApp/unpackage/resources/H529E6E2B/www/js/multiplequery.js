/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.07.06
 * 描 述：日期查询选择框
 */
(function ($, learun, fui, window) {
    "use strict";

    var _multiplequery = {
        init: function ($self, op) {
            var $content = $self.find('.lr-tool-right-btn-content').show();
            $content.parents('.f-page').append($content);
           
            $content.show();
            $content.fpopright({
                callBack: function (type, $content) {
                    if (type === 'ok') {// 确定
                        setTimeout(function () {
                            var data = $content.lrformGet();
                            op.callback && op.callback(data);
                        }, 300);
                    }
                    else if (type === 'rest') {// 重置
                        setTimeout(function () {
                            var data = $content.lrformGet();
                            $.each(data, function (_id, _item) {
                                data[_id] = "";
                            });
                            $content.lrformSet(data);
                        }, 300);
                    }
                }
            });
            $content.parents('.f-popright-body').addClass('lr-tool-right-btn-body ');
            $self.on('tap', { $content: $content }, function (e) {
                e.data.$content.fpoprightShow();
            });

            return $content;
        }
    };

    $.fn.multiplequery = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        if ($this[0].dfop) {
            return $this;
        }
        var dfop = {
            callback: false
        };
        $.extend(dfop, op || {});
        $this[0].dfop = dfop;
        return _multiplequery.init($this, dfop);
    };

})(window.jQuery, window.lrmui, window.fui, window);