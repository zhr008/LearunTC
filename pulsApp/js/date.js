/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 日期选择插件
 */
(function ($, learun, fui, window) {
    "use strict";

    // 日期选择插件
    $.fn.lrdate = function (op) {
        var $this = $(this);
        var dfop = {
            placeholder: '请选择',
            type: 'datetime',//datetime:2017-10-12 00:00;date:2017-10-12; time:00:00; month:2017-10
            label: ['年', '月', '日', '时', '分'],
            change: false    // 选择时间改变的时候触发

        };
        $.extend(dfop, op || {});
        dfop.callback = function () {
            learun.formblur();
        };

        $this.attr('type', 'lrdate');
        $this.addClass('lr-date');
        $this.html('<div class="text">' + dfop.placeholder + '</div>');
        setTimeout(function () {
            $this.fdtpicker(dfop).on('change', function () {
                var $self = $(this);
                $self.find('.text').text($self[0].fop.value);
            });
        }, 100);
        return $this;
    };
    // 日期选择插件（设置数据）
    $.fn.lrdateSet = function (value) {
        var $this = $(this);

        function set($this, value) {
            if ($this[0].fop) {
                if (value) {
                    switch ($this[0].fop.type) {
                        case 'datetime':
                            value = fui.date.format(value, 'yyyy-MM-dd hh:mm');
                            break;
                        case 'date':
                            value = fui.date.format(value, 'yyyy-MM-dd');
                            break;
                        case 'time':
                            var _value = fui.date.format(value, 'hh:mm');
                            if (!_value) {
                                _value = fui.date.format('2017-12-18 ' + value, 'hh:mm');
                            }
                            value = _value;
                            break;
                        case 'month':
                            value = fui.date.format(value, 'yyyy-MM');
                            break;
                    }
                    $this[0].fop.value = value;
                    $this.find('.text').text(value);
                }
                else {
                    $this[0].fop.value = '';
                    $this.find('.text').text($this[0].fop.placeholder);
                }
            }
            else {
                setTimeout(function () {
                    set($this, value);
                }, 100);
            }
        }
        set($this, value);
    };
    // 日期选择插件（获取数据）
    $.fn.lrdateGet = function () {
        var $this = $(this);
        return $this[0].fop.value;
    };
})(window.jQuery, window.lrmui, window.fui, window);


