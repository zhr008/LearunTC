/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-10-19 16:07
 * 描  述：任务计划模板
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
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_TaskScheduling/TaskDesign/Form',
                    width: 700,
                    height: 610,
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
                        url: top.$.rootUrl + '/LR_TaskScheduling/TaskDesign/Form?keyValue=' + keyValue,
                        width: 700,
                        height: 610,
                        btn: null
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_TaskScheduling/TaskDesign/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            ///暂停任务
            $('#lr_disabled').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    if (selectedRow.F_TaskStage == "2") {
                        learun.layerConfirm('是否确认暂停该任务！', function (res) {
                            if (res) {
                                learun.operateForm(top.$.rootUrl + '/LR_TaskScheduling/TaskDesign/PauseJob', { keyValue: keyValue }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    } else {
                        learun.alert.error('当前任务状态下，无法暂停任务');
                    }
                }
            });
            ///重新启动任务
            $('#lr_enabled').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(keyValue)) {
                    if (selectedRow.F_TaskStage == "5") {
                        learun.layerConfirm('是否确认暂停该任务！', function (res) {
                            if (res) {
                                learun.operateForm(top.$.rootUrl + '/LR_TaskScheduling/TS_TaskDesign/ResumeJob', { keyValue: keyValue }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    } else {
                        learun.alert.error('当前任务状态下，无法重新启动任务');
                    }
                }
            });
        },
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_TaskScheduling/TaskDesign/GetPageList',
                headData: [
                    { label: '任务名称', name: 'F_TaskName', width: 200, align: "left" },
                    {
                        label: '任务状态', name: 'F_TaskStage', width: 80, align: "center",
                        formatter: function (cellvalue) {
                            if (cellvalue == 1) {

                                return '<span class=\"label label-primary\" style=\"cursor: pointer;\">待执行</span>';
                            } else if (cellvalue == 2) {
                                return '<span class=\"label label-success\" style=\"cursor: pointer;\">运行中</span>';
                            }
                            else if (cellvalue == 3) {
                                return '<span class=\"label label-default\" style=\"cursor: pointer;\">异常</span>';
                            }
                            else if (cellvalue == 4) {
                                return '<span class=\"label label-info\" style=\"cursor: pointer;\">已结束</span>';
                            }
                            else if (cellvalue == 7) {
                                return '<span class=\"label label-info\" style=\"cursor: pointer;\">已开始</span>';
                            }
                            else if (cellvalue == 5) {
                                return '<span class=\"label label-info\" style=\"cursor: pointer;\">暂停中</span>';
                            }
                        }
                    },
                    {
                        label: '创建日期', name: 'F_CreateDate', width: 140, align: "center",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    },
                    { label: '任务说明', name: 'F_Description', width: 200, align: "left" },

                ],
                mainId: 'F_Id',
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
