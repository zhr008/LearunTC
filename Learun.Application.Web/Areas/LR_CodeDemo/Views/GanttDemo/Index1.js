/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：208.11.22
 * 描 述：甘特图
 */
var bootstrap = function ($, learun) {
    "use strict";

    var data = [];

    var page = {
        init: function () {
            // 初始化数据
            for (var i = 0; i < 10; i++) {
                var day = new Date();
                day = day.DateAdd('d', i * 2);
                var ponit = {
                    id: learun.newGuid(),
                    text: '计划任务' + (i + 1),
                    isexpand:false,
                    complete:true,
                    timeList: [{
                        beginTime: learun.formatDate(day, 'yyyy-MM-dd'),
                        endTime: learun.formatDate(day.DateAdd('d', 3), 'yyyy-MM-dd'),
                        color: '#3286ed',
                        overtime: true,
                        text: '执行时间9天'
                    }, {
                        beginTime: learun.formatDate(day.DateAdd('d', 4), 'yyyy-MM-dd'),
                        endTime: learun.formatDate(day.DateAdd('d', 7), 'yyyy-MM-dd'),
                        color: '#1bb99a',
                        overtime: false,
                        text: '执行时间4天'
                    }]
                }
                data.push(ponit);
            }

            page.initGantt();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                if (keyword) {
                    var _data = []
                    $.each(data, function (_index, _item) {
                        if (_item.text.indexOf(keyword) != -1) {
                            _data.push(_item);
                        }
                    });
                    $('#gridtable').lrGanttSet('refreshdata', _data);
                }
                else {
                    $('#gridtable').lrGanttSet('refreshdata', data);
                }
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
        },
        initGantt: function () {
            $('#gridtable').lrGantt({
                data: data,
                timeHover: function (data, flag, $self) {
                    // data:当前行数据 和 所在时间段数据  flag：true 移入 false 移出 $self:整个gantt对象
                    if (flag) {
                        var _html = '<div class="title" >任务名称</div><div><input  type="text" class="text" value="' + data.item.text + '" ></div>';
                        _html += '<div class="title" >开始时间</div><div><input  type="text" class="text" value="' + learun.formatDate(data.mytime.beginTime,'yyyy-MM-dd') + '" ></div>';
                        _html += '<div class="title" >结束时间</div><div><input  type="text" class="text" value="' + learun.formatDate(data.mytime.endTime, 'yyyy-MM-dd') + '" ></div>';

                        $self.lrGanttSet('showinfo', _html);
                    }
                    else {
                        //$self.lrGanttSet('hideinfo');
                    }

                },
                timebtns: ['month', 'week', 'day'],//'month', 'week', 'day', 'hour'
                timeClick: function (data, $self) {
                },
                timeDoubleClick: function (data, $self) {
                },
                click: function (item,$item) {
                }


            });
        },
        search: function (param) {
        }
    };
    page.init();
}

// /LR_CodeDemo/GanttDemo/Index1 fa fa-reorder