/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-10-16 16:24
 * 描  述：消息策略
 */
var selectedRow;
var refreshGirdData;
var dateBegin = '';
var dateEnd = '';
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
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
                selectfn: function (begin, end) {
                    dateBegin = begin;
                    dateEnd = end;
                    page.search();
                }
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                selectedRow = null;
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_Message/LR_StrategyInfo/Form',
                    width: 500,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            $('#lr_send').on('click', function () {
                selectedRow = null;
                var keyValue = $('#gridtable').jfGridGet('rowdata').F_StrategyCode;
                learun.layerForm({
                    id: 'sendform',
                    title: '发送消息',
                    url: top.$.rootUrl + '/LR_Message/LR_StrategyInfo/SendForm?keyValue=' + keyValue,
                    width: 500,
                    height: 600,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_Message/LR_StrategyInfo/Form?keyValue=' + keyValue,
                        width: 500,
                        height: 400,
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
                            learun.deleteForm(top.$.rootUrl + '/LR_Message/LR_StrategyInfo/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_Message/LR_StrategyInfo/GetPageList',
                headData: [
                        { label: '策略名称', name: 'F_StrategyName', width: 140, align: "left" },
                        { label: '策略编码', name: 'F_StrategyCode', width: 110, align: "left" },
                        {
                            label: '消息类型', name: 'F_MessageType', width: 120, align: "left",
                            formatter: function (cellvalue, row) {
                                cellvalue = (cellvalue || '') + '';
                                var vlist = cellvalue.split(',');
                                var rlist = [];
                                $.each(vlist, function (_index, _item) {
                                    switch (_item) {
                                        case '1':
                                            rlist.push('邮件');
                                            break;
                                        case '2':
                                            rlist.push('微信');
                                            break;
                                        case '3':
                                            rlist.push('短信');
                                            break;
                                        case '4':
                                            rlist.push('系统IM');
                                            break;
                                    }
                                });

                                return String(rlist);
                            }
                        },
                        { label: '备注', name: 'F_Description', width: 300, align: "left" },
                ],
                mainId: 'F_Id',
                sidx: 'F_CreateDate desc',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.dateBegin = dateBegin;
            param.dateEnd = dateEnd;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
