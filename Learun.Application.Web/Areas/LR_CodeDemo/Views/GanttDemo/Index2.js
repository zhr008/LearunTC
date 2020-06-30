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
                    isexpand: false,
                    complete: true,
                    timeList: [{
                        beginTime: learun.formatDate(day, 'yyyy-MM-dd'),
                        endTime: learun.formatDate(day.DateAdd('d', 8), 'yyyy-MM-dd'),
                        color: '#3286ed',
                        overtime: true,
                        text: '执行时间9天'
                    }],
                    hasChildren: true,
                    children: [{
                        id: learun.newGuid(),
                        text: '计划任务' + (i + 1) + '.1',
                        isexpand: false,
                        complete: true,
                        timeList: [{
                            beginTime: learun.formatDate(day, 'yyyy-MM-dd'),
                            endTime: learun.formatDate(day.DateAdd('d', 3), 'yyyy-MM-dd'),
                            color: '#1bb99a',
                            overtime: true,
                            text: '执行时间4天'
                        }]
                    },
                    {
                        id: learun.newGuid(),
                        text: '计划任务' + (i + 1) + '.2',
                        isexpand: false,
                        complete: true,
                        timeList: [{
                            beginTime: learun.formatDate(day.DateAdd('d', 4), 'yyyy-MM-dd'),
                            endTime: learun.formatDate(day.DateAdd('d', 8), 'yyyy-MM-dd'),
                            color: '#E4474D',
                            overtime: true,
                            text: '执行时间5天'
                        }]
                    }]
                }
                data.push(ponit);
            }

            page.initGantt();
            page.bind();
        },
        bind: function () {
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
        },
        initGantt: function () {
            $('#gridtable').lrGantt({
                data: data,
                timebtns: ['month', 'week', 'day'],//'month', 'week', 'day', 'hour'
            });
        },
        search: function (param) {
        }
    };
    page.init();
}


