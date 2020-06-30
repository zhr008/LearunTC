/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-09-25 18:18
 * 描  述：销售出库
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            page.search();
        },
        bind: function () {
            // 时间搜索框
            $('#datesearch').lrdate({
                dfdata: [
                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                    //startTime = begin;
                    //endTime = end;
                    //page.search();
                }
            });
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            $('#F_PurchaseType').lrDataItemSelect({ code: 'menuTrage' });
            $('#F_Appler').lrDataSourceSelect({ code: 'userdata',value: 'f_userid',text: 'f_realname' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/ERPDemo/SalesReceipt/Form',
                    width: 1400,
                    height: 800,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/ERPDemo/SalesReceipt/Form?keyValue=' + keyValue,
                        width: 1200,
                        height: 700,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/ERPDemo/SalesReceipt/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 打印
            $('#lr_print').on('click', function () {
                $('#gridtable').jqprintTable();
            });
            //  生成付款单
            $('#lr_produced').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '生成付款单',
                        url: top.$.rootUrl + '/ERPDemo/SalesReceipt/ReceiptForm?keyValue=' + keyValue,
                        width: 1200,
                        height: 700,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //查看历史记录
            $('#lr_history').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '采购申请历史记录',
                        url: top.$.rootUrl + '/ERPDemo/SalesReceipt/HistoryForm?&keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        btn: null
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/ERPDemo/SalesReceipt/GetPageList',
                headData: [
                    { label: "单据编号", name: "F_PurchaseNo", width: 100, align: "left"},
                    { label: "报价日期", name: "F_ApplyDate", width: 150, align: "left"},
                    { label: "主题", name: "F_Theme", width: 200, align: "left"},
                    { label: "报价类别", name: "F_PurchaseType", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'menuTrage',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "报价人", name: "F_Appler", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('custmerData', {
                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'userdata',
                                 key: value,
                                 keyId: 'f_userid',
                                 callback: function (_data) {
                                     callback(_data['f_realname']);
                                 }
                             });
                        }},
                    { label: "报价单位", name: "F_Department", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('custmerData', {
                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'company',
                                 key: value,
                                 keyId: 'f_companyid',
                                 callback: function (_data) {
                                     callback(_data['f_shortname']);
                                 }
                             });
                        }},
                    { label: "支付方式", name: "F_PaymentType", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'Client_PaymentMode',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "总价", name: "F_Total", width: 100, align: "left"},
                    { label: "出库日期", name: "F_DeliveryDate", width: 150, align: "left"},
                    { label: "点收人", name: "F_PurchaseWarehousinger", width: 100, align: "left"},
                    { label: "点收日期", name: "F_PurchaseWarehousingDate", width: 150, align: "left"},
                    //{ label: "对方代表", name: "F_Your", width: 100, align: "left"},
                    //{ label: "订单号", name: "F_Order", width: 100, align: "left"},
                    //{ label: "收货地址", name: "F_ToAddress", width: 100, align: "left"},
                    //{ label: "发货地址", name: "F_FromAddress", width: 100, align: "left"},
                    { label: "备注", name: "F_Description", width: 200, align: "left"},
                    //{ label: "附件", name: "F_File", width: 100, align: "left"},
                ],
                mainId:'F_Id',
                isPage: true
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
