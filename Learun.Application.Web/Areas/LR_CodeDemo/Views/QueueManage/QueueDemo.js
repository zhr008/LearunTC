/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2019.04.11
 * 描 述：消息队列
 */
var loaddfimg;
var baseinfo;
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.initData();
            page.bind();
        },
        bind: function () {
            //提交购票信息
            $('#lr_save_btn').on('click', function () {
                if (!$('#form').lrValidform()) {
                    return false;
                }
                var postData = {};
                postData.list = JSON.stringify($('#form').lrGetFormData());
                $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/QueueManage/SaveList', postData, function (res) {
                    // 保存成功后才回调
                    page.search();
                });
            });
        },
        //排队列表
        queueGrid: function () {
            $('#queuetable').jfGrid({
                headData: [
                    { label: '购票人', name: 'name', width: 100, align: 'left' },
                    { label: '身份证号', name: 'id', width: 200, align: 'left' },
                ]
            });
            page.search();
        },
        //购票结果列表
        ticketGrid: function () {
            $('#tickettable').jfGrid({
                headData: [
                    { label: '购票人', name: 'name', width: 100, align: 'left' },
                    { label: '身份证号', name: 'id', width: 200, align: 'left' },
                    { label: '座位号', name: 'code', width: 100, align: 'left' },
                    { label: '乘车时间', name: 'ticketdate', width: 150, align: 'left' }

                ]
            });
            page.search();
        },
        initData: function () {
            page.queueGrid();
            page.ticketGrid();
        },
        search: function () {
            learun.httpAsync('get', top.$.rootUrl + '/LR_CodeDemo/QueueManage/GetBuyerList', {}, function (data) {
                $('#queuetable').jfGridSet('refreshdata', data);
            })

            learun.httpAsync('get', top.$.rootUrl + '/LR_CodeDemo/QueueManage/GetTicketList', {}, function (data) {
                $('#tickettable').jfGridSet('refreshdata', data);
            })

            //$('#queuetable').jfGridSet('reload');
            //$('#tickettable').jfGridSet('reload');
        }
    };
    page.init();
    setInterval(function () { page.search(); }, 3000);
}