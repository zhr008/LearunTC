/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-03-26 18:29
 * 描  述：报表菜单关联设置
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ F_FullName: keyword });
            });
            $('#F_RptFileId').lrDataSourceSelect({ code: 'crmCustomer',value: 'f_customerid',text: 'f_fullname' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_ReportModule/RptRelation/Form',
                    width: 600,
                    height: 400,
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
                        url: top.$.rootUrl + '/LR_ReportModule/RptRelation/Form?keyValue=' + keyValue,
                        width: 600,
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
                            learun.deleteForm(top.$.rootUrl + '/LR_ReportModule/RptRelation/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            //预览
            $('#lr_print').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_TempId');
                learun.frameTab.open({ F_ModuleId: 'preview_' + keyValue, F_Icon: 'fa fa fa-eye', F_FullName: '预览报表', F_UrlAddress: '/LR_ReportModule/ReportManage/Preview?reportId=' + keyValue + '&type=preview' });
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_ReportModule/RptRelation/GetPageList',
                headData: [
                    { label: "菜单编号", name: "F_EnCode", width: 100, align: "left"},
                    { label: "菜单名称", name: "F_FullName", width: 200, align: "left"},
                    { label: "父级菜单", name: "F_ParentId", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: '',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "绑定报表", name: "F_ModifyUserName", width: 200, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('custmerData', {
                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'crmCustomer',
                                 key: value,
                                 keyId: 'f_customerid',
                                 callback: function (_data) {
                                     callback(_data['f_fullname']);
                                 }
                             });
                        }},
                    { label: "备注", name: "F_Description", width: 300, align: "left"},
                ],
                mainId:'F_Id',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
