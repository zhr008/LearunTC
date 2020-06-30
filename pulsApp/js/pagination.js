/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 分页列表插件
 */
(function ($, learun, fui, window) {
    "use strict";

    // 列表分页加载
    var _lrpagination = {
        renderData: function (op, data, page, records) {
            op.lrpage = page;
            op.lrrecords = records;
            op.lrtotal = parseInt(parseInt(records) / parseInt(op.lrrows)) + (parseInt(records) % parseInt(op.lrrows) > 0 ? 1 : 0);
            if (data) {
                var startnum = (page - 1) * op.lrrows + 1;
                for (var i = 0, l = data.length; i < l; i++) {
                    op.lrdata.push(data[i]);
                    var _html = '';
                    var $item = null;
                  
                    if (op.lrbtns.length > 0) {
                        $item = $('\
                        <div data-index="' + (startnum + i - 1) + '" class="lr-page-item f-swipebtn" >\
                            <div class="f-swipebtn-handler"></div>\
                            <div class="f-swipebtn-right"></div>\
                        </div>');
                        _html = op.lrrenderData(startnum + i, data[i], $item.find('.f-swipebtn-handler'));
                    }
                    else {
                        $item = $('<div data-index="' + (startnum + i - 1) + '" class="lr-page-item" ></div>');
                        _html = op.lrrenderData(startnum + i, data[i], $item);
                    }

                    if (_html !== '') {
                        var _$html = $(_html);
                        _$html.attr('data-index',startnum + i - 1);
                        _$html.addClass('lr-page-item');
                        op.$list.append(_$html);

                        if (op.lrbtns.length > 0) {
                            _$html.addClass('f-swipebtn');
                            var $handler = $('<div class="f-swipebtn-handler">' + _$html.html() + '</div>');
                            var $btn = $('<div class="f-swipebtn-right"></div>');
                            _$html.html($handler);
                            _$html.append($btn);
                            $.each(op.lrbtns, function (_index, _item) {
                                var _$item = $(_item).addClass('f-swipebtn-btn');
                                $btn.append(_$item);
                            });
                            _$html.fswipebtn();

                            $handler = null;
                            $btn = null;
                        }
                        _$html = null;
                    } else {
                        op.$list.append($item);
                        if (op.lrbtns.length > 0) {
                            var $btns = $item.find('.f-swipebtn-right');
                            $.each(op.lrbtns, function (_index, _item) {
                                var _$item = $(_item).addClass('f-swipebtn-btn');
                                $btns.append(_$item);
                            });
                            $btns = null;
                            $item.fswipebtn();
                        }
                    }
                    $item = null;
                }
            }
        },
        reload: function () {
            fui.loading(true, '加载数据中');
            var self = this;
            var op = self.options;
            var pageparam = {
                page: 1,
                rows: op.lrrows
            };
            op.lrgetData && op.lrgetData(pageparam, function (data, records) {
                op.$list.html("");
                op.lrdata = [];
                _lrpagination.renderData(op, data, 1, records);
                fui.loading(false);
                op = null;
                self = null;
            });
        }
    };
    $.fn.lrpagination = function (op) {
        var dfop = {
            lclass: 'lr-list',
            rows: 10,                  // 每页行数
            getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                callback([], 0);
            },
            renderData: function (_index, _item) {// 渲染数据模板
                return '';
            },
            click: false, // item, $self 点击事件,
            down: {
                contentinit: '下拉可以刷新',
                contentdown: '下拉可以刷新',
                contentover: '释放立即刷新',
                contentrefresh: '正在刷新...'
            },
            up: {
                contentinit: '上拉显示更多',
                contentdown: '上拉显示更多',
                contentrefresh: '正在加载...',
                contentnomore: '没有更多数据了'
            }
        };
        $.extend(dfop, op || {});
        var $this = $(this);
        $this[0].lrop = dfop;

        var fop = {
            lrdata: [],
            lrpage: 1,                   // 当前页
            lrrecords: 0,                // 总记录数
            lrtotal: 0,                  // 总页数

            lrrows: dfop.rows,
            lrgetData: dfop.getData,
            lrrenderData: dfop.renderData,
            lrbtns: dfop.btns || [],
            lrclick: dfop.click,

            down: {
                contentinit: dfop.down.contentinit,
                contentdown: dfop.down.contentdown,
                contentover: dfop.down.contentover,
                contentrefresh: dfop.down.contentrefresh,
                callback: function () {
                    var self = this;
                    var op = self.options;
                    var pageparam = {
                        page: 1,
                        rows: op.lrrows
                    };

                    op.lrgetData && op.lrgetData(pageparam, function (data, records) {
                        op.$list.html("");
                        op.lrdata = [];
                        _lrpagination.renderData(op, data, 1, records);
                        self.endPulldownToRefresh();
                        self.refresh(true);

                    });
                }
            },
            up: {
                contentinit: dfop.up.contentinit,
                contentdown: dfop.up.contentdown,
                contentrefresh: dfop.up.contentrefresh,
                contentnomore: dfop.up.contentnomore,
                callback: function () {
                    var self = this;
                    var op = self.options;
                    op.lrpage = op.lrpage + 1;
                    var pageparam = {
                        page: op.lrpage,
                        rows: op.lrrows
                    };
                    if (op.lrpage > op.lrtotal) {
                        self.endPullupToRefresh(true);
                    }
                    else {
                        op.lrgetData && op.lrgetData(pageparam, function (data, records) {
                            _lrpagination.renderData(op, data, op.lrpage, records);
                            if (op.lrpage >= op.lrtotal) {
                                self.endPullupToRefresh(true);
                            }
                            else {
                                self.endPullupToRefresh();
                            }
                        });
                    }
                }
            }
        };

        var $res = $this.pullRefresh(fop);
        var $list = $('<div class="' + dfop.lclass + '" ></div>');
        $($res.wrapper.children[1]).prepend($list);
        $list.html("");
        fui.loading(true, '加载数据中');

        var _fop = $res.options;
        _fop.$list = $list;

        $list.delegate('.lr-page-item', 'tap', { op: _fop }, function (e) {
            e = e || window.event;
            var et = e.target || e.srcElement;
            var $et = $(et);

            var op = e.data.op;
            var data = op.lrdata;
            var $this = $(this);
            var _index = $this.attr('data-index');
            op.lrclick && op.lrclick(data[_index], $this, $et);
        });

        var pageparam = {
            page: 1,
            rows: _fop.lrrows
        };
        _fop.lrgetData && _fop.lrgetData(pageparam, function (data, records) {
            $list.html("");
            _fop.lrdata = [];
            _lrpagination.renderData(_fop, data, 1, records);
            fui.loading(false);
            _fop = null;
        });
        fop = null;
        dfop = null;
        op = null;
        $res.reload = _lrpagination.reload;
        return $res;
    };

})(window.jQuery, window.lrmui, window.fui, window);


