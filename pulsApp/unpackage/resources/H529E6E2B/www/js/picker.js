/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 选择器插件
 */
(function ($, learun, fui, window) {
    "use strict";

    // 选择器(初始化)
    $.fn.lrpicker = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        var dfop = {
            placeholder: '请选择',
            data: [],
            level: 1,
            ivalue: 'value',
            itext: 'text'
        };
        $.extend(dfop, op || {});
        dfop.callback = function () {
            learun.formblur();
        };
        $this.attr('type', 'lrpicker');
        $this.addClass('lr-picker');
        $this.html('<div class="text">' + dfop.placeholder + '</div>');

        setTimeout(function () {
            $this.fpoppicker(dfop).on('change', function () {
                var $self = $(this);
                var text = $self[0].fop.text;
                $self.find('.text').text(text.replace(/,/g, '/'));
            });
        }, 100);

        return $this;
    };
    // 选择器(获取数据值)
    $.fn.lrpickerGet = function (type) {
        var $this = $(this);
        if ($this.length === 0) {
            return "";
        }
        var fop = $this[0].fop;
        if (type === 'text') {
            return fop.text;
        }
        else {
            return fop.value;
        }
    };
    // 选择器(设置数据值)
    $.fn.lrpickerSet = function (value) {
        var $this = $(this);
        if ($this.length === 0) {
            return false;
        }
        function set(value, $this) {
            if (!$this[0].fop) {
                setTimeout(function () {
                    set(value, $this);
                }, 100);
            }
            else {
                var fop = $this[0].fop;
                if (value) {
                    fop._lrTmpValue = value;
                }
                $this.fpoppickerSet(value);
                if (value === '') {
                    fop.value = '';
                    fop.text = '';
                    $this.find('.text').text(fop.placeholder);
                }
                else if (fop.text !== "" && fop.text !== undefined && fop.text !== null) {
                    $this.find('.text').text(fop.text.replace(/,/g, '/'));
                }
            }
        }
        set(value, $this);
    };      
    // 选择器(更新数据)
    $.fn.lrpickerSetData = function (data) {
        var $this = $(this);
        if ($this.length === 0) {
            return false;
        }
        function updateData(data, $this) {
            if (!$this[0].fop) {
                setTimeout(function () {
                    updateData(data, $this);
                }, 100);
            }
            else {
                $this.fpoppickerSetData(data);
                $this.lrpickerSet($this[0].fop._lrTmpValue);
            }
        }
        updateData(data, $this);
    };

})(window.jQuery, window.lrmui, window.fui, window);

