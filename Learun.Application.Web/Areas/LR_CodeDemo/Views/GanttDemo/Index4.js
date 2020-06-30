/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：208.11.22
 * 描 述：甘特图
 */
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGantt();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
        },
        initGantt: function () {
            $('#gridtable').lrGantt({
                url: top.$.rootUrl + '/LR_CodeDemo/GanttDemo/GetPageList',
                childUrl: top.$.rootUrl + '/LR_CodeDemo/GanttDemo/GetTimeList',
                isPage: true,
                rows: 30,
                timebtns: ['month', 'week', 'day'],//'month', 'week', 'day', 'hour'
            }).lrGanttSet('reload');
        },
        search: function (param) {
            $('#gridtable').lrGanttSet('reload', param || {});
        }
    };
    page.init();
}


