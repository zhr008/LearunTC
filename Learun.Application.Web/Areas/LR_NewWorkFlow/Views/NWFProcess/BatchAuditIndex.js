/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.08.04
 * 描 述：流程（我的任务）	
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var logbegin = '';
    var logend = '';

    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    }

    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            $('#datesearch').lrdate({
                dfdata: [
                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
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
                    logbegin = begin;
                    logend = end;

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
            // 同意
            $('#lr_agree').on('click', function () {
                var taskIds = $('#gridtable').jfGridValue('F_TaskId');
                if (learun.checkrow(taskIds)) {
                    learun.layerConfirm('是否审核同意？', function (res, _index) {
                        if (res) {
                            learun.loading(true, '批量审核...');
                            var postData = {
                                operationCode: 'agree',
                                taskIds: taskIds,
                            };
                            learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/AuditFlows', postData, function (data) {
                                learun.loading(false);
                                refreshGirdData();
                                learun.frameTab.currentIframe().refreshGirdData && learun.frameTab.currentIframe().refreshGirdData();
                            });
                            top.layer.close(_index);
                        }
                    });
                }
               
            });
            // 不同意
            $('#lr_disagree').on('click', function () {
                var taskIds = $('#gridtable').jfGridValue('F_TaskId');
                if (learun.checkrow(taskIds)) {
                    learun.layerConfirm('是否审核不同意？', function (res, _index) {
                        if (res) {
                            learun.loading(true, '批量审核...');
                            var postData = {
                                operationCode: 'disagree',
                                taskIds: taskIds,
                            };
                            learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/AuditFlows', postData, function (data) {
                                learun.loading(false);
                                refreshGirdData();
                                learun.frameTab.currentIframe().refreshGirdData && learun.frameTab.currentIframe().refreshGirdData();
                            });
                            top.layer.close(_index);
                        }
                    });
                }
            });
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/GetBatchTaskPageList',
                headData: [
                    {
                        label: "任务", name: "F_TaskName", width: 160, align: "left"
                    },
                    {
                        label: "标题", name: "F_Title", width: 300, align: "left", formatter: function (cellvalue, row) {
                            if (row.F_SchemeName != row.F_Title && row.F_Title) {
                                return row.F_SchemeName + "(" + row.F_Title + ")";
                            }
                            else {
                                return row.F_SchemeName;
                            }
                        }
                    },
                    {
                        label: "等级", name: "F_Level", width: 60, align: "center",
                        formatter: function (cellvalue) {
                            switch (cellvalue) {
                                case 0:
                                    return '普通';
                                    break;
                                case 1:
                                    return '重要';
                                    break;
                                case 2:
                                    return '紧急';
                                    break;
                                default:
                                    return '普通';
                                    break;
                            }
                        }
                    },
                    {
                        label: "状态", name: "F_EnabledMark", width: 70, align: "center",
                        formatter: function (cellvalue, row) {
                            if (row.F_IsFinished == 0) {
                                if (cellvalue == 1) {
                                    if (row.F_IsUrge == "1") {
                                        return "<span class=\"label label-danger\">催办加急</span>";
                                    }
                                    return "<span class=\"label label-success\">运行中</span>";
                                } else if (cellvalue == 2) {
                                    return "<span class=\"label label-primary\">草稿</span>";
                                } else {
                                    return "<span class=\"label label-danger\">作废</span>";
                                }
                            }
                            else {
                                return "<span class=\"label label-warning\">结束</span>";
                            }

                        }
                    },
                    { label: "发起者", name: "F_CreateUserName", width: 70, align: "center" },
                    {
                        label: "时间", name: "F_CreateDate", width: 150, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    }
                ],
                mainId: 'F_Id',
                isPage: true,
                sidx: 'F_CreateDate DESC',
                isMultiselect:true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = logbegin;
            param.EndTime = logend;

            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param)});
        }

    };

    page.init();
}


