/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.11.22
 * 描 述：甘特图
 */
(function ($, learun) {
    "use strict";

    //+---------------------------------------------------  
    //| 日期计算  
    //+---------------------------------------------------  
    Date.prototype.DateAdd = function (strInterval, Number) {
        var dtTmp = this;
        switch (strInterval) {
            case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));// 秒
            case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));// 分
            case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));// 小时
            case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));// 天
            case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));// 星期
            case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());// 季度
            case 'm': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());// 月
            case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());// 年
        }
    }
    //+---------------------------------------------------  
    //| 比较日期差 dtEnd 格式为日期型或者 有效日期格式字符串  
    //+---------------------------------------------------  
    Date.prototype.DateDiff = function (strInterval, dtEnd) {
        var dtStart = this;
        if (typeof dtEnd == 'string')//如果是字符串转换为日期型  
        {
            dtEnd = learun.parseDate(dtEnd);
        }
        switch (strInterval) {
            case 's': return parseInt((dtEnd - dtStart) / 1000);
            case 'n': return parseInt((dtEnd - dtStart) / 60000);
            case 'h': return parseInt((dtEnd - dtStart) / 3600000);
            case 'd': return parseInt((dtEnd - dtStart) / 86400000);
            case 'w': return parseInt((dtEnd - dtStart) / (86400000 * 7));
            case 'm': return (dtEnd.getMonth() + 1) + ((dtEnd.getFullYear() - dtStart.getFullYear()) * 12) - (dtStart.getMonth() + 1);
            case 'y': return dtEnd.getFullYear() - dtStart.getFullYear();
        }
    }
    //+---------------------------------------------------  
    //| 取得当前日期所在月的最大天数  
    //+---------------------------------------------------  
    Date.prototype.MaxDayOfDate = function () {
        var myDate = this;
        var date1 = learun.parseDate(learun.formatDate(myDate, 'yyyy-MM-01 00:00:00'));
        var date2 = date1.DateAdd('m', 1);
        var result = date1.DateDiff('d', date2);
        return result;
    }
    //---------------------------------------------------  
    // 判断闰年  
    //---------------------------------------------------  
    Date.prototype.isLeapYear = function () {
        return (0 == this.getYear() % 4 && ((this.getYear() % 100 != 0) || (this.getYear() % 400 == 0)));
    }

    var _gantt = {
        init: function ($self, op) {
           
            $self.addClass('lr-gantt');
            var $title = $('<div class="lr-gantt-title">\
                                <div class="lr-gantt-title-text" >时间维度:</div>\
                                <div class="btn-group btn-group-sm" >\
                                </div>\
                            </div>');
            $.each(op.timebtns, function (_index, _item) {
                switch (_item) {
                    case 'month':
                        $title.find('.btn-group-sm').append('<a class="btn btn-default month" data-value="month" >月</a>');
                        break;
                    case 'week':
                        $title.find('.btn-group-sm').append('<a class="btn btn-default week" data-value="week" >周</a>');
                        break;
                    case 'day':
                        $title.find('.btn-group-sm').append('<a class="btn btn-default day" data-value="day" >天</a>');
                        break;
                    case 'hour':
                        $title.find('.btn-group-sm').append('<a class="btn btn-default hour" data-value="hour" >时</a>');
                        break;
                }
            });


            var $footer = $('<div class="lr-gantt-footer" ></div>');
            var $body = $('<div class="lr-gantt-body lr-gantt-body-pr" ></div>').css({ 'padding-left': op.leftWidh });

            var $left = $('<div class="lr-gantt-left" ><div class="lr-gantt-left-content" ></div></div>').css({ width: op.leftWidh });   // 左侧显示区域
            var $right = $('<div class="lr-gantt-right" >\
                                <div class="lr-gantt-rightheader" >\
                                </div>\
                                <div class="lr-gantt-rightbody" ></div>\
                            </div>'); // 右侧显示区域

            $left.append('<div class="lr-gantt-move" ></div>');      // 左右移动滑块
            $right.append('<div class="lr-gantt-nodata-img"  ><img src="' + op.imgUrl + '"></div>');

            $body.append('<div class="lr-gantt-showtext" >\
                            <div class="lr-gantt-showtext-title-remove"><i title="关闭" class="fa fa-close"></i></div>\
                            <div class="lr-gantt-showtext-content" ></div>\
                          </div>');

            op._row = 0;

            _gantt.initTitle($title, op);
            _gantt.initFooter($footer, $self, op);

            _gantt.initLeft($left, $self, op);
            _gantt.initRight($right, $self, op);

            _gantt.initMove($self);
            _gantt.initShow($body);

            $body.append($left);
            $body.append($right);

            $self.append($title);
            $self.append($body);
            $self.append($footer);

            // 绘制数据
            _gantt.renderData($self, op);

            // 加载后台数据
            // _gantt.loadData($self, op);

            $title = null;
            $body = null;
            $footer = null;
            $left = null;
            $right = null;
            $self = null;
        },
        initShow: function ($body) {
            $body.find('.lr-gantt-showtext-content').lrscroll();
            $body.find('.lr-gantt-showtext-title-remove').on('click', function () {
                var $this = $(this);
                $this.parent().removeClass('active');
                $this.parents('.lr-gantt').removeClass('lr-gantt-showtext-active');
                $this.parent().find('.lr-scroll-box').html('');
                $this = null;
            });
        },
        initMove: function ($self) {
            $self.on('mousedown', function (e) {
                e = e || window.event;
                var et = e.target || e.srcElement;
                var $et = $(et);
                var $this = $(this);
                var dfop = $this[0].dfop;
                if ($et.hasClass('lr-gantt-move')) {
                    dfop._ismove = true;
                    dfop._pageX = e.pageX;
                }
            });

            $self.mousemove(function (e) {
                var $this = $(this);
                var dfop = $this[0].dfop;
                if (dfop._ismove) {
                    var $block = $this.find('.lr-gantt-left');
                    $block.css('width', dfop.leftWidh + (e.pageX - dfop._pageX));
                    $this.find('.lr-gantt-body').css('padding-left', dfop.leftWidh + (e.pageX - dfop._pageX));
                }
            });

            $self.on('click', function (e) {
                var $this = $(this);
                var dfop = $this[0].dfop;
                if (dfop._ismove) {
                    dfop.leftWidh += (e.pageX - dfop._pageX);
                    dfop._ismove = false;
                }
            });
        },

        initTitle: function ($title, op) {
            $title.find('.' + op.type).addClass('active');
            $title.find('a').on('click', function () {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    $this.parent().find('.active').removeClass('active');


                    var $self = $this.parents('.lr-gantt');
                    var _op = $self[0].dfop;
                    _op.type = $this.attr('data-value');

                    setTimeout(function () {
                        _gantt.renderRightHeader($self.find('.lr-gantt-rightheader'), op);
                        _gantt.renderRightData($self, op);
                        $self = null;
                    });
                    $this.addClass('active');
                }
                $this = null;
            });

        },
        initFooter: function ($footer, $self, op) {
            if (op.isPage) {
                var $pagebar = $('<div class="lr-gantt-page-bar" id="lr_gantt_page_bar_' + op.id + '"><div class="lr-gantt-page-bar-info" >无显示数据</div>\
                <div class="paginations" id="lr_gantt_page_bar_nums_' + op.id + '" style="display:none;" >\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">首页</a></li></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">上一页</a></li></ul>\
                <ul class="pagination pagination-sm" id="lr_gantt_page_bar_num_' + op.id + '" ></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">下一页</a></li></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">尾页</a></li></ul>\
                <ul class="pagination"><li><span></span></li></ul>\
                <ul class="pagination"><li><input type="text" class="form-control"></li></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">跳转</a></li></ul>\
                </div></div>');
                $footer.append($pagebar);
                $pagebar.find('#lr_gantt_page_bar_num_' + op.id).on('click', _gantt.turnPage);
                $pagebar.find('#lr_gantt_page_bar_nums_' + op.id + ' .pagebtn').on('click', { op: op }, _gantt.turnPage2);
                $pagebar = null;
            }
            else {
                $self.addClass('lr-gantt-nopage');
            }
            $footer = null;
        },

        initLeft: function ($left, $self, op) {
            $left.find('.lr-gantt-left-content').lrscroll(function (x, y) {
                if (!$self.is(":hidden")) {
                    $self.find('.lr-gantt-rightbody').lrscrollSet('moveY', y);
                   
                }
            });
        },
        initRight: function ($right, $self, op) {
            $right.find('.lr-gantt-rightbody').lrscroll(function (x, y) {
                if (!$self.is(":hidden")) {
                    $self.find('.lr-gantt-rightheader').css('left', -x);

                    $self.find('.lr-gantt-left-content').lrscrollSet('moveY', y);
                }
            });
        },
        renderRightHeader: function ($header, op) {
            $header.hide();
            $header.html('');
            op._time = DateUtils.getBoundaryDatesFromData(op.data, op.cellNum, op.type);
            // 绘制头部
            var $month = $('<div class="lr-gantt-rightheader-months" ></div>');
            var $day = $('<div class="lr-gantt-rightheader-days" ></div>');

            var len = 0;
            var last = op._time.min;
            var y = '';
            var w = 0;
            var $y = null;
            op._num = 0;
            switch (op.type) {
                case 'month':
                    len = op._time.min.DateDiff('m', op._time.max) + 1;
                    w = 0;
                    for (var i = 0; i < len; i++) {
                        var _y = last.getFullYear();
                        if (y != _y) {
                            y = _y;
                            if ($y != null) {
                                $y.css({ 'width': w * 28 });
                            }
                            $y = $('<div class="lr-gantt-rightheader-month" >' + y + '</div>');
                            $month.append($y);
                            w = 0;
                        }
                        $day.append('<div class="lr-gantt-rightheader-day" >' + (last.getMonth()+1) + '</div>');
                        last = last.DateAdd('m', 1);
                        w++;
                        op._num++;
                    }
                    $y.css({ 'width': w * 28 });
                    break;
                case 'week':
                    len = op._time.min.DateDiff('w', op._time.max) +1;
                    w = 0;
                    var start = null;
                    for (var i = 0; i < len; i++) {
                        var _y = op.monthNames[last.getMonth()] + '/' + last.getFullYear();
                        if (y != _y) {
                            y = _y;
                            if ($y != null) {
                                if (op._time.min.DateDiff('m', start) > 0) {
                                    $y.css({ 'width': start.MaxDayOfDate() * 4 });
                                }
                                else {
                                    $y.css({ 'width': (start.MaxDayOfDate() - start.getDate() + 1) * 4 });
                                }
                            }
                            start = last;
                            $y = $('<div class="lr-gantt-rightheader-month" >' + y + '</div>');
                            $month.append($y);
                            w = 0;
                        }
                        $day.append('<div class="lr-gantt-rightheader-day" >' + DateUtils.getWeekNumber(last) + '</div>');
                        last = last.DateAdd('w', 1);
                        w++;
                        op._num++;
                    }
                    $y.css({ 'width': (last.DateAdd('w', -1).getDate() + 6) * 4 });

                    break;
                case 'day':
                    len = op._time.min.DateDiff('d', op._time.max) + 1;
                    w = 0;
                    for (var i = 0; i < len; i++) {
                        var _y = op.monthNames[last.getMonth()] + '/' + last.getFullYear();

                        if (y != _y) {
                            y = _y;
                            if ($y != null) {
                                $y.css({ 'width': w * 28 });
                            }
                            $y = $('<div class="lr-gantt-rightheader-month" >' + y + '</div>');
                            $month.append($y);
                            w = 0;
                        }
                        $day.append('<div class="lr-gantt-rightheader-day ' + (DateUtils.isWeekend(last)?'lr-gantt-weekend':'') + ' " >' + last.getDate() + '</div>');
                        last = last.DateAdd('d', 1);
                        w++;
                        op._num++;
                    }
                    $y.css({ 'width': w * 28 });
                    break;
                case 'hour':
                    len = op._time.min.DateDiff('h', op._time.max) + 1;
                    w = 0;
                    for (var i = 0; i < len; i++) {
                        var _y = last.getDate() + '/' + op.monthNames[last.getMonth()] + '/' + last.getFullYear();
                        if (y != _y) {
                            y = _y;
                            if ($y != null) {
                                $y.css({ 'width': w * 28 });
                            }
                            $y = $('<div class="lr-gantt-rightheader-month" >' + y + '</div>');
                            $month.append($y);
                            w = 0;
                        }
                        $day.append('<div class="lr-gantt-rightheader-day " >' + last.getHours() + '</div>');
                        last = last.DateAdd('h', 1);
                        w++;
                        op._num++;
                    }
                    $y.css({ 'width': w * 28 });
                    break;
            }
            op._width = op._num * 28;

            $header.css("width", op._width + "px");
            $header.append($month);
            $header.append($day);
            $header.show();
            $header = null;
            $month = null;
            $day = null;
        },
        renderRightGird: function ($content, op) {
            $content.hide();
            $content.css({ 'width': op._width });
            $content.html('');
            var $row = $('<div class="lr-gantt-grid-row"></div>');
            for (var j = 0; j < op._num; j++) {
                var $cell = $('<div class="lr-gantt-grid-row-cell" ></div>', { "class": "ganttview-grid-row-cell" });
                $row.append($cell);
                $cell = null;
            }
            for (var j = 0; j < op._row; j++) {
                $content.append($row.clone());
            }
            $content.show();

            if (op._row == 0) {
                $content.parents('.lr-gantt-right').find('.lr-gantt-nodata-img').show();
            }
            else {
                $content.parents('.lr-gantt-right').find('.lr-gantt-nodata-img').hide();
            }
            $row = null;
            $content = null;
        },

        // 加载数据
        loadData: function ($self, op) {
            var _param = op.param || {};
            if (op.isPage) {
                learun.loading(true, '正在获取数据');
                op.pageparam = op.pageparam || {
                    rows: op.rows,                // 每页行数      
                    page: 1,                  // 当前页
                    sidx: '',                 // 排序列
                    sord: '',                 // 排序类型
                    records: 0,               // 总记录数
                    total: 0                  // 总页数
                };

                op.pageparam.rows = op.rows;
                op.pageparam.sidx = op.sidx;
                op.pageparam.sord = op.sord;
                op.pageparam.page = op.pageparam.page || 1;
                op.pageparam.records = 0;
                op.pageparam.total = 0;


                op.param = op.param || {};
                delete op.param['pagination'];
                var _paramString = JSON.stringify(op.param);
                if (op.paramString != _paramString) {
                    op.paramString = _paramString;
                    op.pageparam.page = 1;
                }


                op.param['pagination'] = JSON.stringify(op.pageparam);

                learun.httpAsync('GET', op.url, op.param, function (data) {
                    learun.loading(false);
                    if (data) {
                        op.data = data.rows;
                        op.pageparam.page = data.page;
                        op.pageparam.records = data.records;
                        op.pageparam.total = data.total;

                    }
                    else {
                        op.data = [];
                        op.pageparam.page = 1;
                        op.pageparam.records = 0;
                        op.pageparam.total = 0;
                    }
                    _gantt.renderData($self, op);


                    var $pagebar = $self.find('#lr_gantt_page_bar_' + op.id);
                    var $pagebarBtn = $pagebar.find('#lr_gantt_page_bar_num_' + op.id);
                    var $pagebarBtns = $pagebar.find('#lr_gantt_page_bar_nums_' + op.id);

                    var pagebarLabel = '';
                    var btnlist = "";
                    if (op.data.length == 0) {
                        pagebarLabel = '无显示数据';
                    }
                    else {
                        var pageparam = op.pageparam;
                        var beginnum = (pageparam.page - 1) * pageparam.rows + 1;
                        var endnum = beginnum + op.data.length - 1;
                        pagebarLabel = '显示第 ' + beginnum + ' - ' + endnum + ' 条记录  <span>|</span> 检索到 ' + pageparam.records + ' 条记录';

                        if (pageparam.total > 1) {
                            var bpage = pageparam.page - 6;
                            bpage = bpage < 0 ? 0 : bpage;
                            var epage = bpage + 10;
                            if (epage > pageparam.total) {
                                epage = pageparam.total;
                            }
                            if ((epage - bpage) < 10) {
                                bpage = epage - 10;
                            }
                            bpage = bpage < 0 ? 0 : bpage;

                            for (var i = bpage; i < epage; i++) {
                                btnlist += '<li><a href="javascript:void(0);" class=" pagebtn ' + ((i + 1) == pageparam.page ? 'active' : '') + '" >' + (i + 1) + '</a></li>';
                            }

                            $pagebarBtns.find('span').text('共' + pageparam.total + '页,到');

                            $pagebarBtns.show();
                        }
                        else {
                            $pagebarBtns.hide();
                        }
                    }
                    $pagebarBtn.html(btnlist);
                    $pagebar.find('.lr-gantt-page-bar-info').html(pagebarLabel);

                    op.onRenderComplete && op.onRenderComplete(op.data);
                });
            }
            else {
                if (op.url) {
                    learun.loading(true, '正在获取数据');
                    
                    learun.httpAsync('GET', op.url, _param, function (data) {
                        learun.loading(false);
                        op.data = data || [];
                        _gantt.renderData($self, op);
                        op.onRenderComplete && op.onRenderComplete(op.data);
                    });
                }
            }
        },
        turnPage: function (e) {
            e = e || window.event;

            var $this = $(this);
            var $self = $('#' + $this.attr('id').replace('lr_gantt_page_bar_num_', ''));
            var op = $self[0].dfop;

            var et = e.target || e.srcElement;
            var $et = $(et);
            if ($et.hasClass('pagebtn')) {
                var page = parseInt($et.text());
                if (page != op.pageparam.page) {
                    $this.find('.active').removeClass('active');
                    $et.addClass('active');
                    op.pageparam.page = page;
                    _gantt.loadData($self, op);
                }
            }
        },
        turnPage2: function (e) {
            var $this = $(this);
            var op = e.data.op;
            var name = $this.text();
            var $pagenum = $('#lr_gantt_page_bar_num_' + op.id);
            var page = parseInt($pagenum.find('.active').text());
            var falg = false;
            switch (name) {
                case '首页':
                    if (page != 1) {
                        op.pageparam.page = 1;
                        falg = true;
                    }
                    break;
                case '上一页':
                    if (page > 1) {
                        op.pageparam.page = page - 1;
                        falg = true;
                    }
                    break;
                case '下一页':
                    if (page < op.pageparam.total) {
                        op.pageparam.page = page + 1;
                        falg = true;
                    }
                    break;
                case '尾页':
                    if (page != op.pageparam.total) {
                        op.pageparam.page = op.pageparam.total;
                        falg = true;
                    }
                    break;
                case '跳转':
                    var text = $this.parents('#lr_gantt_page_bar_nums_' + op.id).find('input').val();
                    if (!!text) {
                        var p = parseInt(text);
                        if (String(p) != 'NaN') {
                            if (p < 1) {
                                p = 1;
                            }
                            if (p > op.pageparam.total) {
                                p = op.pageparam.total;
                            }
                            op.pageparam.page = p;
                            falg = true;
                        }
                    }
                    break;
            }
            if (falg) {
                _gantt.loadData($('#' + op.id), op);
            }

        },
        // 渲染数据
        renderData: function ($self, op) {
            _gantt.hideinfo($self);

            _gantt.renderRightHeader($self.find('.lr-gantt-rightheader'), op);
            // 绘制左侧列表数据
            _gantt.renderLeftData($self, op);
            // 绘制右侧数据
            _gantt.renderRightData($self, op);

        },
        // 左侧
        renderLeftData: function ($self, op) {
            var $treeRoot = $('<ul class="lr-gantt-tree-root" ></ul>');
            var _len = op.data.length;
            op._timeDatas = {};
            op._row = 0;
            for (var i = 0; i < _len; i++) {
                var $node = _gantt.renderNode(op.data[i], 0, i, op, true);
                $treeRoot.append($node);
            }
            $self.find('.lr-gantt-left .lr-scroll-box').html($treeRoot);
        },
        renderNode: function (node, deep, path, dfop, isShow) {
            if (isShow) {
                dfop._timeDatas[path + ''] = node;
                dfop._row++;
            }
            node._deep = deep;
            node._path = path;
            // 渲染成单个节点
            var nid = node.id.replace(/[^\w]/gi, "_");
            var title = node.title || node.text;

            var $node = $('<li class="lr-gantt-tree-node"></li>');
            var $nodeDiv = $('<div id="' + dfop.id + '_' + nid + '" tpath="' + path + '" title="' + title + '"  dataId="' + dfop.id + '"  class="lr-gantt-tree-node-el" ></div>');
            if (node.hasChildren) {
                var c = (node.isexpand || dfop.isAllExpand) ? 'lr-gantt-tree-node-expanded' : 'lr-gantt-tree-node-collapsed';
                $nodeDiv.addClass(c);
            }
            else {
                $nodeDiv.addClass('lr-gantt-tree-node-leaf');
            }
            // span indent
            var $span = $('<span class="lr-gantt-tree-node-indent"></span>');
            if (deep == 1) {
                $span.append('<img class="lr-gantt-tree-icon" src="' + dfop.cbiconpath + 's.gif"/>');
            }
            else if (deep > 1) {
                $span.append('<img class="lr-gantt-tree-icon" src="' + dfop.cbiconpath + 's.gif"/>');
                for (var j = 1; j < deep; j++) {
                    $span.append('<img class="lr-gantt-tree-icon" src="' + dfop.cbiconpath + 's.gif"/>');
                }
            }
            $nodeDiv.append($span);
            // img
            var $img = $('<img class="lr-gantt-tree-ec-icon" src="' + dfop.cbiconpath + 's.gif"/>');
            $nodeDiv.append($img);
            // a
            var ahtml = '<a class="lr-gantt-tree-node-anchor" href="javascript:void(0);">';
            ahtml += '<span data-value="' + node.id + '" class="lr-gantt-tree-node-text" >' + node.text + '</span>';
            ahtml += '</a>';
            $nodeDiv.append(ahtml);
            // 节点事件绑定
            $nodeDiv.on('click', _gantt.nodeClick);

            if (!node.complete) {
                $nodeDiv.append('<div class="lr-gantt-tree-loading"><img class="lr-gantt-tree-ec-icon" src="' + dfop.cbiconpath + 'loading.gif"/></div>');
            }

            $node.append($nodeDiv);
            if (node.hasChildren) {
                var $treeChildren = $('<ul  class="lr-gantt-tree-node-ct" >');
                if (!node.isexpand && !dfop.isAllExpand) {
                    $treeChildren.css('display', 'none');
                }
                if (node.children) {
                    var l = node.children.length;
                    for (var k = 0; k < l; k++) {
                        node.children[k].parent = node;
                        var $childNode = _gantt.renderNode(node.children[k], deep + 1, path + "." + k, dfop);
                        $treeChildren.append($childNode);
                    }
                    $node.append($treeChildren);
                }
            }
            node.render = true;
            return $node;
        },
        renderNodeAsync: function ($this, node, dfop) {
            var $treeChildren = $('<ul  class="lr-gantt-tree-node-ct" >');
            if (!node.isexpand && !dfop.isAllExpand) {
                $treeChildren.css('display', 'none');
            }
            if (node.children) {
                var l = node.children.length;
                for (var k = 0; k < l; k++) {
                    node.children[k].parent = node;
                    var $childNode = _gantt.renderNode(node.children[k], node._deep + 1, node._path + "." + k, dfop);
                    $treeChildren.append($childNode);
                }
                $this.parent().append($treeChildren);
            }
            return $treeChildren;
        },
        getItem: function (path, dfop) {
            var ap = path.split(".");
            var t = dfop.data;
            for (var i = 0; i < ap.length; i++) {
                if (i == 0) {
                    t = t[ap[i]];
                }
                else {
                    t = t.children[ap[i]];
                }
            }
            return t;
        },
        nodeClick: function (e) {
            e = e || window.event;
            var et = e.target || e.srcElement;
            var $this = $(this);
            var $parent = $('#' + $this.attr('dataId'));
            var dfop = $parent[0].dfop;
            
            var path = $this.attr('tpath');
            var node = _gantt.getItem(path, dfop);

            if (et.tagName == 'IMG') {
                var $et = $(et);
                var $ul = $this.next('.lr-gantt-tree-node-ct');
                if ($et.hasClass("lr-gantt-tree-ec-icon")) {
                    if ($this.hasClass('lr-gantt-tree-node-expanded')) {
                        $ul.slideUp(200, function () {
                            $this.removeClass('lr-gantt-tree-node-expanded');
                            $this.addClass('lr-gantt-tree-node-collapsed');

                            _gantt.removeTimeDatas(node.children, dfop);

                            // 重新刷新下右侧的数据
                            _gantt.renderRightData($parent, dfop);
                        });
                    }
                    else if ($this.hasClass('lr-gantt-tree-node-collapsed')) {
                        // 展开
                      
                        if (!node.complete) {
                            if (!node._loading) {
                                node._loading = true;// 表示正在加载数据
                                $this.find('.lr-gantt-tree-loading').show();
                                var param = dfop.childParam || {};
                                param.parentId = node.id;
                                var url = dfop.childUrl || dfop.url;
                                learun.httpAsync('GET', url, param, function (data) {
                                    if (data) {
                                        node.children = data;
                                        $ul = _gantt.renderNodeAsync($this, node, dfop);
                                        $ul.slideDown(200, function () {
                                            $this.removeClass('lr-gantt-tree-node-collapsed');
                                            $this.addClass('lr-gantt-tree-node-expanded');

                                            // 检测下当前节点下哪些节点显示了
                                            _gantt.addTimeDatas(node.children, dfop);
                                            // 重新刷新下右侧的数据
                                            _gantt.renderRightData($parent, dfop);
                                        });
                                        node.complete = true;
                                        $this.find('.lr-gantt-tree-loading').hide();
                                    }
                                    node._loading = false;
                                });
                            }
                        }
                        else {
                            $ul.slideDown(200, function () {
                                $this.removeClass('lr-gantt-tree-node-collapsed');
                                $this.addClass('lr-gantt-tree-node-expanded');

                                // 检测下当前节点下哪些节点显示了
                                _gantt.addTimeDatas(node.children, dfop);

                                // 重新刷新下右侧的数据
                                _gantt.renderRightData($parent, dfop);
                            });
                        }
                    }

                }
            }
            else {
                dfop.currentItem = node;
                $parent.find('.lr-gantt-tree-selected').removeClass('lr-gantt-tree-selected');
                $this.addClass('lr-gantt-tree-selected');
                dfop.click && dfop.click(node, $this);
            }
           

            return false;
        },
        addTimeDatas: function (data, op) {
            $.each(data, function (_index, _item) {
                var nid = _item.id.replace(/[^\w]/gi, "_");
                var id = op.id + '_' + nid;
                var $node = $('#' + id);
                if (!$node.is(":hidden")) {
                    var path = $node.attr('tpath');
                    op._timeDatas[path] = _item;
                    op._row++;
                    if (_item.hasChildren && _item.children && _item.children.length) {
                        _gantt.addTimeDatas(_item.children, op);
                    }
                }
            });
        },
        removeTimeDatas: function (data, op) {
            $.each(data, function (_index, _item) {
                var nid = _item.id.replace(/[^\w]/gi, "_");
                var id = op.id + '_' + nid;
                var $node = $('#' + id);
                if ($node.is(":hidden")) {
                    var path = $node.attr('tpath');
                    if (op._timeDatas) {
                        delete op._timeDatas[path];
                        op._row--;
                    }
                   
                    if (_item.hasChildren && _item.children && _item.children.length) {
                        _gantt.addTimeDatas(_item.children, op);
                    }
                }
            });
        },

        // 右侧
        renderRightData: function ($self, op) {
            _gantt.renderRightGird($self.find('.lr-gantt-rightbody .lr-scroll-box'), op);
            var $blocks = $('<div class="lr-gantt-blocks" ></div>');

            // 对 op._timeDatas 进行排序
            var _dataTemp = [];
            $.each(op._timeDatas, function (_index, _item) {
                if (_index.indexOf('.') == -1) {
                    _dataTemp.push(_item);
                    // 获取他的子节点
                    _gantt.addRightChildTimeDatas(op._timeDatas, _dataTemp, _index);
                }
            });

            $.each(_dataTemp, function (_index, _item) {
                var $blockContainer = $('<div class="lr-gantt-block-container" ></div>');
                $.each(_item.timeList || [], function (_i, _t) {
                    var res = DateUtils.getDateBlock(_t.beginTime, _t.endTime, op);
                    if (res.width > 0) {
                        var $block = $('<div class="lr-gantt-block" ><div class="lr-gantt-block-text" ></div></div>').css({ width: res.width, left: res.left, 'background-color': _t.color || '#3286ed' });
                        if (_t.text) {
                            $block.find('.lr-gantt-block-text').text(_t.text);
                        }
                        if (_t.overtime) {
                            $block.append('<div class="lr-gantt-block-icon" title="超时" ><i class="fa fa-arrow-circle-down" ></i></div>');
                        }
                        $block[0].ganttData = {
                            item: _item,
                            mytime:_t
                        };

                        // 点击
                        $block.on('click', { op: op }, function (e) {
                            e = e || window.event;
                            var _op = e.data.op;
                            var ganttData = $(this)[0].ganttData;
                            _op.timeClick && _op.timeClick(ganttData, $('#' + _op.id));
                        });
                        // 双击
                        $block.on('dblclick', { op: op }, function (e) {
                            e = e || window.event;
                            var _op = e.data.op;
                            var ganttData = $(this)[0].ganttData;
                            _op.timeDoubleClick && _op.timeDoubleClick(ganttData, $('#' + _op.id));
                        });
                        
                        // 移入、移出
                        $block.hover(function () {
                            var ganttData = $(this)[0].ganttData;
                            op.timeHover && op.timeHover(ganttData, true, $('#' + op.id));
                        }, function () {
                            var ganttData = $(this)[0].ganttData;
                            op.timeHover && op.timeHover(ganttData, false, $('#' + op.id));
                        });
                        $blockContainer.append($block);
                    }
                });
                $blocks.append($blockContainer);
            });
            $self.find('.lr-gantt-rightbody .lr-scroll-box').append($blocks);
        },
        addRightChildTimeDatas: function (data, _dataTemp, path) {
            var num =0;
            while(true){
                var _path = path + '.' + num;
                if (data[_path]) {
                    _dataTemp.push(data[_path]);
                    _gantt.addRightChildTimeDatas(data, _dataTemp, _path);
                    num++;
                }
                else {
                    break;
                }
            }
        },

        // 方法
        showInfo: function ($self, info) {// 显示右侧信息板信息
            var $content = $self.find('.lr-gantt-showtext-content .lr-scroll-box');
            $content.html(info);
            var $showText = $self.find('.lr-gantt-showtext');
            if (!$showText.hasClass('active')) {
                $showText.addClass('active');
                $self.addClass('lr-gantt-showtext-active');
            }
        },
        hideinfo: function ($self) {// 隐藏右侧信息板信息
            var $content = $self.find('.lr-gantt-showtext-content .lr-scroll-box');
            var $showText = $self.find('.lr-gantt-showtext');
            if ($showText.hasClass('active')) {
                $showText.removeClass('active');
                $self.removeClass('lr-gantt-showtext-active');
                $content.html('');
            }
        }
    };

    var DateUtils = {
        getDateBlock: function (start, end, op) {// 根据开始结束时间获取宽度和起始位置
            start = DateUtils.parseDate(start, 'h');
            end = DateUtils.parseDate(end, 'h');

            var wnum = 0;
            var dnum = 0;
            var res = {
                left: 0,
                width: 0
            };
            switch (op.type) {
                case 'day':
                    wnum = start.DateDiff('d', end) + 1;
                    dnum = op._time.min.DateDiff('d', start);
                    break;
                case 'week':

                    dnum = op._time.min.DateDiff('w', start);
                    var eweek = op._time.min.DateDiff('w', end);
                    wnum = eweek - dnum + 1;

                    break;
                case 'month':
                    dnum = op._time.min.DateDiff('m', start);
                    var emonth = op._time.min.DateDiff('m', end);
                    wnum = emonth - dnum + 1;
                    break;
                case 'hour':
                    dnum = op._time.min.DateDiff('h', start);
                    var ehour = op._time.min.DateDiff('h', end);
                    wnum = ehour - dnum + 1;
                    break;
            }

            res.left = dnum * 28 + 2;
            res.width = wnum * 28 - 4;
            return res;
        },
        isLeapYear:function(year) {
            return (year % 400 == 0) || (year % 4 == 0 && year % 100 != 0);
        },
        getMonthDays:function(year, month) {
            return [31, (DateUtils.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
        },
        getWeekNumber: function (targetDay) {
            var year = targetDay.getFullYear();
            var month = targetDay.getMonth();
            var days = targetDay.getDate();
            //那一天是那一年中的第多少天
            for (var i = 0; i < month; i++) {
                days += DateUtils.getMonthDays(year, i);
            }
            //那一年第一天是星期几
            var yearFirstDay = (new Date(year, 0, 1)).getDay();
            //计算是第几周
            days += yearFirstDay;
            var week = Math.ceil(days / 7);

            var _num = 7 - targetDay.getDay();
            if (targetDay.DateAdd('d', _num).getFullYear() != year) {
                return 1;
            }

            return week;
        },
        isWeekend: function (date) {
            return date.getDay() % 6 == 0;
        },

        getBoundaryDatesFromData: function (data, num, type) {
            var time = {
                min: DateUtils.parseDate(new Date(), 'h'),
                max: DateUtils.parseDate(new Date(), 'h')
            };
            DateUtils.getMinMax(data, time, true);

            switch (type) {
                case 'month':
                    time.min = DateUtils.parseDate(time.min, 'm');
                    time.max = DateUtils.parseDate(time.max, 'm').DateAdd('m',1);
                    if (time.min.DateDiff('m', time.max) < num) {
                        time.max = time.min.DateAdd('m', num);
                    }
                    if (time.min.getMonth() == 11) {
                        time.min = time.min.DateAdd('m', -1);
                    }
                    if (time.max.getMonth() == 0) {
                        time.max = time.max.DateAdd('m', 1);
                    }
                    break;
                case 'week':
                    time.min = DateUtils.parseDate(time.min, 'w');
                    time.max = DateUtils.parseDate(time.max, 'w').DateAdd('w', 1);
                    if (time.min.DateDiff('w', time.max) < num) {
                        time.max = time.min.DateAdd('w', num);
                    }

                    if (time.min.MaxDayOfDate() - time.min.getDate() + 1 < 21) {
                        var _wnum = time.min.getDate() - time.min.getDate() % 7;
                        time.min = time.min.DateAdd('d',-_wnum);
                    }

                    if (time.max.getDate() < 21) {
                        var _wnum = parseInt((time.max.MaxDayOfDate() - time.max.getDate()) / 7);
                        time.max = time.max.DateAdd('w', _wnum);
                    }

                    break;
                case 'day':
                    if (time.min.DateDiff('d', time.max) < num) {
                        time.max = time.min.DateAdd('d', num);
                    }
                    // 获取当前月最大天数
                    var minMonths = time.min.MaxDayOfDate();
                    var minCurrentDay = time.min.getDate();
                    var maxCurrentDay = time.max.getDate();

                    if (minMonths - minCurrentDay < 2) {
                        time.min = time.min.DateAdd('d', -(2 + minCurrentDay - minMonths));
                    }
                    if (maxCurrentDay < 3) {
                        time.max = time.max.DateAdd('d', (3 - maxCurrentDay));
                    }
                    break;
                case 'hour':
                    if (time.min.DateDiff('h', time.max) < num) {
                        time.max = time.min.DateAdd('h', num);
                    }
                    break;
            }
            return time;
        },
        getMinMax:function(data, time, isFirst) {
            $.each(data || [], function (_index, _item) {
                $.each(_item.timeList, function (_jindex, _jitem) {
                    var start = DateUtils.parseDate(_jitem.beginTime, 'h');
                    var end = DateUtils.parseDate(_jitem.endTime, 'h');
                    if (isFirst) {
                        time.min = start;
                        time.max = end;
                        isFirst = false;
                    }
                    if (time.min.DateDiff('h', start) < 0) { time.min = start; }
                    if (time.max.DateDiff('h', end) > 0) { time.max = end; }
                });
                if (data.children && data.children.length > 0) {
                    DateUtils.getMinMax(data.children, time, false);
                }
            });
        },
        parseDate: function (day, strInterval) {
            switch (strInterval) {
                case 'd':
                    return learun.parseDate(learun.formatDate(day, 'yyyy-MM-dd 00:00:00'));
                    break;
                case 'w':// 获取当前周的第一天
                    var d = learun.parseDate(learun.formatDate(day, 'yyyy-MM-dd 00:00:00'));
                    var w = d.getDay();
                    return d.DateAdd('d', (1 -w));
                    break;
                case 'm':
                    return learun.parseDate(learun.formatDate(day, 'yyyy-MM-01 00:00:00'));
                    break;
                case 'h':
                    return learun.parseDate(learun.formatDate(day, 'yyyy-MM-dd hh:00:00'));
                    break;
                default:
                    return learun.parseDate(learun.formatDate(day, 'yyyy-MM-dd 00:00:00'));
                    break;
            }
           
        }
    };

    $.fn.lrGantt = function (op) {
        //id,                            // id 对应字段
        //text,                          // 显示文本对应字段
        //isexpand:false,                // 是否展开
        //complete:true,                 // 是否加载完数据
        //timeList                       // 显示时间字段数组
        // ----
            //-beginTime,                // 开始时间对应字段
            //-endTime,                  // 结束时间对应字段
            //-color,                    // 颜色对应字段
            //-overtime,                 // 超时对饮字段
        //children

        var dfop = {
            url: false,                 // 接口地址
            childUrl: false,            // 加载子节点参数
            data: [],                   // 加载数据
            param: false,               // 访问接口参数
            childParam: false,          // 访问子节点接口参数

            leftWidh: 200,
            type: 'day',                // month,week,day,hour
            timebtns: ['month', 'week', 'day', 'hour'],
            monthNames: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一", "十二"],

            isAllExpand:false,
            isPage: false,
            rows: 50,
            imgUrl:top.$.rootUrl + '/Content/images/jfgrid/nodata.jpg',
            cbiconpath: top.$.rootUrl + '/Content/images/learuntree/',

            cellNum: 100,

            click: false,               // 单击事件 item

            timeClick: false,           // 时间段点击事件 item 和 时间段信息
            timeDoubleClick: false,     // 时间段双击事件 item 和 时间段信息

            timeHover: false,           // 时间段hover事件 移入事件/移出事件  item 和 时间段信息 flag 标志 true 移入 false 移出

            onRenderComplete:false      // 动态加载后台数据完成后执行
        };
        $.extend(dfop, op || {});
        var $self = $(this);
        $self[0].dfop = dfop;
        dfop.id = $self.attr('id');
        _gantt.init($self, dfop);

        return $self;
    }

    $.fn.lrGanttSet = function (name, data) {
        var $this = $(this);
        var op = $this[0].dfop;

        switch (name) {
            case 'showinfo': // 显示信息框
                _gantt.showInfo($this, data);
                break;
            case 'hideinfo': // 关闭信息框
                _gantt.hideinfo($this);
                break;
            case 'refreshdata': // 刷新数据
                op.data = data || [];
                _gantt.renderData($this, op);
                break;
            case 'reload':
                if (data) {
                    op.param = data;
                }
                _gantt.loadData($this, op);
                break;

        }
    };

})(window.jQuery, top.learun);