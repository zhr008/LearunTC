/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 多选框插件
 */
(function ($, learun, fui, window) {
    "use strict";
    // 多选框(初始化)
    $.fn.lrcheckbox = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        var dfop = {
            placeholder: '请选择',
            data: [],
            ivalue: 'value',
            itext: 'text',
            change: false
        };
        $.extend(dfop, op || {});
        dfop.callback = function () {
            learun.formblur();
        };
        $this.attr('type', 'lrcheckbox');
        $this.addClass('lr-checkbox');
        $this.html('<div class="text">' + dfop.placeholder + '</div>');

        setTimeout(function () {
            $this.fcheckbox(dfop).on('change', function () {
                var $self = $(this);
                var text = $self[0].fop.text || '';
                var $text = $self.find('.text');
                if (text === ''){
                    $self.find('.text').text($self[0].fop.placeholder);
                }
                else {
                    $text.html('');
                    var textlist = text.split(',');
                    $.each(textlist, function (_index, _item) {
                        var _html = '<div class="lr-checkbox-item" >' + _item + '</div>';
                        $text.append(_html);
                    });
                }
                $text = null;
            });
        }, 100);

        return $this;
    };
    // 多选框(获取数据值)
    $.fn.lrcheckboxGet = function (type) {
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
    // 多选框(设置数据值)
    $.fn.lrcheckboxSet = function (value) {
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
                $this.fcheckboxSet(value);
                if (value === '') {
                    fop.value = '';
                    fop.text = '';
                    $this.find('.text').text(fop.placeholder);
                }
            }
        }
        set(value, $this);
    };
    // 多选框(更新数据)
    $.fn.lrcheckboxSetData = function (data) {
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
                $this.fcheckboxSetData(data);
                $this.lrcheckboxSet($this[0].fop._lrTmpValue);
            }
        }
        updateData(data, $this);
    };

})(window.jQuery, window.lrmui, window.fui, window);