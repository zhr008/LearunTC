/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.07.06
 * 描 述：日期查询选择框
 */
(function ($, learun, fui, window) {
    "use strict";

    var _searchdate = {
        init: function ($self, op) {
            var _html = '\
            <div class="lr-search-date" >\
                <a href="javascript:;" class="lr-search-date-btn" data-value="0">今天</a>\
                <a href="javascript:;" class="lr-search-date-btn" data-value="1">近7天</a>\
                <a href="javascript:;" class="lr-search-date-btn" data-value="2">近1个月</a>\
                <a href="javascript:;" class="lr-search-date-btn" data-value="3">近3个月</a>\
                <a href="javascript:;" class="lr-search-date-btn active" data-value="4">自定义</a>\
                <div class="lr-search-date-custmer">\
                    <div class="lr-form-row"><label>开始时间</label><div id="lr_search_date_custmer1"></div></div>\
                    <div class="lr-form-row"><label>结束时间</label><div id="lr_search_date_custmer2"></div></div>\
                </div >\
            </div>';

            var _$html = $(_html);
            $self.parents('.f-page').append(_$html);


         

            _$html.fpopright({
                restBtn: '',
                callBack: function (type, $content) {
                    if (type === 'ok') {
                        var btn = $content.find('.lr-search-date-btn.active').attr('data-value');
                        if (btn === '4') {
                            setTimeout(function () {
                                var begin = ($content.find('#lr_search_date_custmer1').lrdateGet() || '1000-01-01') + " 00:00:00";
                                var end = ($content.find('#lr_search_date_custmer2').lrdateGet() || fui.date.get('yyyy-MM-dd')) + " 23:59:59";
                                op.callback && op.callback(begin, end);
                            }, 300);
                        }
                    }
                }
            });

            _$html.find('#lr_search_date_custmer1').lrdate({
                type: 'date'
            });
            _$html.find('#lr_search_date_custmer2').lrdate({
                type: 'date'
            });
            _$html.find('.lr-search-date-btn').on('tap', { dfop: op }, function (e) {
                var $this = $(this);
                var v = $this.attr('data-value');
                var _op = e.data.dfop;
                var $p = $this.parents('.lr-search-date');
                var begin = '';
                var end = '';
                $p.find('.lr-search-date-custmer').hide();
                $p.find('.lr-search-date-btn').removeClass('active');
                $this.addClass('active');

                switch (v) {
                    case '0':// 今天
                        $p.fpoprightHide();
                        setTimeout(function () {
                            begin = fui.date.get('yyyy-MM-dd 00:00:00');
                            end = fui.date.get('yyyy-MM-dd 23:59:59');
                            _op.callback && _op.callback(begin, end);
                        }, 300);
                        break;
                    case '1':// 近7天
                        $p.fpoprightHide();
                        setTimeout(function () {
                            begin = fui.date.get('yyyy-MM-dd 00:00:00', 'd', -6);
                            end = fui.date.get('yyyy-MM-dd 23:59:59');
                            _op.callback && _op.callback(begin, end);
                        }, 300);
                        break;
                    case '2':// 近1个月
                        $p.fpoprightHide();
                        setTimeout(function () {
                            begin = fui.date.get('yyyy-MM-dd 00:00:00', 'm', -1);
                            end = fui.date.get('yyyy-MM-dd 23:59:59');
                            _op.callback && _op.callback(begin, end);
                        }, 300);
                        break;
                    case '3':// 近3个月
                        $p.fpoprightHide();
                        setTimeout(function () {
                            begin = fui.date.get('yyyy-MM-dd 00:00:00', 'm', -3);
                            end = fui.date.get('yyyy-MM-dd 23:59:59');
                            _op.callback && _op.callback(begin, end);
                        }, 300);
                        break;
                    case '4':// 自定义
                        $p.find('.lr-search-date-custmer').show();
                        break;
                }
            });

            $self.on('tap', { $content: _$html }, function (e) {
                e.data.$content.fpoprightShow();
            });
        }
    };

    $.fn.searchdate = function (op) {
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
        _searchdate.init($this, dfop);
        return $this;
    }; 

})(window.jQuery, window.lrmui, window.fui, window);