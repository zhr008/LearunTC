/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-09-20 10:10
 * 描  述：看板信息
 */
var selectedRow;
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
                    title: '新增看板',
                    url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/Form',
                    width: 1180,
                    height: 792,
                    btn: null
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/Form?keyValue=' + keyValue,
                        width: 1180,
                        height: 800,
                        btn: null
                    });
                }
            });
            $('#lr_see').on('click', function () {
                var formId = $('#gridtable').jfGridValue('F_Id');
                if (!!formId) {
                    learun.layerForm({
                        id: 'custmerForm_PreviewForm',
                        title: '预览当前看板',
                        url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/PreviewForm?keyValue=' + formId,
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        btn: null
                    });
                }
                else {
                    learun.alert.warning('请选择看板！');
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/GetPageList',
                headData: [
                        { label: '看板名称', name: 'F_KanBanName', width: 200, align: "left" },
                        { label: '看板编号', name: 'F_KanBanCode', width: 130, align: "center" },
                        { label: '刷新时间', name: 'F_RefreshTime', width: 100, align: "center" },
                        {
                            label: '创建日期', name: 'F_CreateDate', width: 100, align: "center",
                            formatter: function (cellvalue) {
                                return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                            }
                        },
                        { label: '创建用户', name: 'F_CreateUserName', width: 100, align: "left" },
                        { label: '看板说明', name: 'F_Description', width: 300, align: "left" }
                ],
                mainId: 'F_Id',
                sidx: 'F_CreateDate desc',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
