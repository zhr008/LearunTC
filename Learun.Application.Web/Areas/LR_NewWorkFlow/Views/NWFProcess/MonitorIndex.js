/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.12.19
 * 描 述：流程监控
 */
var bootstrap = function ($, learun) {
    "use strict";
    var categoryId = '1';
    var logbegin = '';
    var logend = '';

    var page = {
        init: function () {
            $('#lr_verify').hide();
            page.initleft();
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
            // 查看
            $('#lr_eye').on('click', function () {
                page.eye();
            });
            // 指定审核人
            $('#lr_appoint').on('click', function () {
                var processId = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(processId)) {
                    learun.layerForm({
                        id: 'AppointForm',
                        title: '指派审核人',
                        url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/AppointForm?processId=' + processId,
                        height: 500,
                        width: 600,
                        callBack: function (id) {
                            return top[id].acceptClick();
                        }
                    });
                }
            });
            // 作废
            $('#lr_cancel').on('click', function () {
                var processId = $('#gridtable').jfGridValue('F_Id') || '';
                if (learun.checkrow(processId)) {
                    learun.layerConfirm('是否确认作废此流程进程？', function (res, _index) {
                        if (res) {
                            learun.loading(true, '作废流程中...');
                            var postData = {
                                processId: processId,
                            };
                            learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/DeleteFlow', postData, function (data) {
                                learun.loading(false);
                                page.search();
                            });
                            top.layer.close(_index);
                        }
                    });
                }
            });
            // 添加审核节点
            $('#lr_add').on('click', function () {
                var processId = $('#gridtable').jfGridValue('F_Id') || '';
                if (learun.checkrow(processId)) {
                    learun.layerForm({
                        id: 'AppointForm',
                        title: '添加审核节点',
                        url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/AddTaskForm?processId=' + processId,
                        height: 500,
                        width: 800,
                        callBack: function (id) {
                            return top[id].acceptClick(function () {
                                $('#gridtable').jfGridSet('reload');
                            });
                        }
                    }); 
                }
            });
        },
        initleft: function () {
            $('#lr_left_list li').on('click', function () {
                var $this = $(this);
                var $parent = $this.parent();
                $parent.find('.active').removeClass('active');
                $this.addClass('active');
                categoryId = $this.attr('data-value');
                page.search();
            });
        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/GetPorcessList',
                headData: [
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
                        label: "状态", name: "F_EnabledMark", width: 80, align: "center",
                        formatter: function (cellvalue, row) {
                            if (row.F_IsFinished == 0) {
                                if (cellvalue == 1) {
                                    if (row.F_IsUrge == "1" && categoryId == '2') {
                                        return "<span class=\"label label-danger\">催办加急</span>";
                                    }
                                    return "<span class=\"label label-success\">运行中</span>";
                                } else if (cellvalue == 2) {
                                    return "<span class=\"label label-primary\">草稿</span>";
                                } else if (cellvalue == 500) {
                                    return "<span class=\"label label-danger\">异常,审核中断</span>";
                                }else {
                                    return "<span class=\"label label-danger\">作废</span>";
                                }
                            }
                            else {
                                return "<span class=\"label label-warning\">结束</span>";
                            }

                        }
                    },
                    { label: "发起者", name: "F_CreateUserName", width: 80, align: "center" },
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
                dblclick: function () {
                    page.eye();
                }
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = logbegin;
            param.EndTime = logend;
            param.categoryId = categoryId;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        },
        eye: function () {
            var processId = $('#gridtable').jfGridValue('F_Id') || '';
            var title = $('#gridtable').jfGridValue('F_Title');
            var schemeName = $('#gridtable').jfGridValue('F_SchemeName');

            if (schemeName != title && title) {
                title = schemeName + "(" + title + ")";
            }
            else {
                title = schemeName;
            }

            if (learun.checkrow(processId)) {
                learun.frameTab.open({ F_ModuleId: 'monitor' + processId, F_FullName: '查看-' + title, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/MonitorDetailsIndex?processId=' + processId });
            }
        }
    };

    page.init();
}


