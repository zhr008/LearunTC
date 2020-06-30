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
    var categoryId = '2';
    var logbegin = '';
    var logend = '';

    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    }

    var page = {
        init: function () {
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
            // 查看流程进度
            $('#lr_eye').on('click', function () {
                page.eye();
            });

            // 发起流程
            $('#lr_release').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '选择流程模板',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/ReleaseForm',
                    height: 600,
                    width: 825,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick();
                    }
                });
            });

            // 审核流程
            $('#lr_verify').on('click', function () {
                page.verify();
            });
            // 批量审核
            $('#lr_batchAudit').on('click', function () {
                learun.layerForm({
                    id: 'BatchAuditIndex',
                    title: '批量审核',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/BatchAuditIndex',
                    height: 700,
                    width: 900,
                    maxmin: true,
                    btn:null
                });
            });
        },
        initleft: function () {
            $('#lr_left_list li').on('click', function () {
                var $this = $(this);
                var $parent = $this.parent();
                $parent.find('.active').removeClass('active');
                $this.addClass('active');

                categoryId = $this.attr('data-value');

                if (categoryId == '2') {
                    $('#lr_verify').show();
                    $('#lr_verify span').text("审核");
                }
                else {
                    $('#lr_verify').hide();
                }

                page.search();
            });
        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/GetTaskPageList',
                headData: [
                    {
                        label: "任务", name: "F_TaskName", width: 160, align: "left",
                        formatter: function (cellvalue, row, dfop, $cell) {
                            if (row.F_EnabledMark == 3) {
                                if (categoryId == '1') {
                                    return '本人发起';
                                }
                                else {
                                    cellvalue;
                                }
                            }

                            // 草稿
                            if (row.F_EnabledMark == 2) {
                                $cell.on('click', '.create', function () {// 创建
                                    // 关闭草稿页
                                    learun.frameTab.closeByParam('tabProcessId', row.F_Id);

                                    learun.frameTab.open({ F_ModuleId: row.F_Id, F_Icon: 'fa magic', F_FullName: '创建流程-' + row.F_SchemeName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?processId=' + row.F_Id + '&tabIframeId=' + row.F_Id + '&type=draftCreate' });
                                    return false;
                                });
                                $cell.on('click', '.delete', function () {// 删除
                                    learun.layerConfirm('是否确认删除该草稿？', function (res) {
                                        if (res) {
                                            // 关闭草稿页

                                            learun.frameTab.closeByParam('tabProcessId', row.F_Id);
                                            learun.deleteForm(top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/DeleteDraft', { processId: row.F_Id }, function () {
                                                refreshGirdData();
                                            });
                                        }
                                    });
                                    return false;
                                });
                                return '<span class="label label-success create" title="编辑创建">编辑创建</span><span class="label label-danger delete" style="margin-left:5px;" title="删除草稿" >删除草稿</span>';
                            }



                            var isaAain = false;
                            if (categoryId == '1') {
                                if (row.F_IsAgain == 1) {
                                    isaAain = true;
                                }
                                else if (row.F_IsFinished == 0) {
                                    // 加入催办和撤销按钮
                                    $cell.on('click', '.urge', function () {// 催办审核
                                        learun.layerConfirm('是否确认催办审核？', function (res, _index) {
                                            if (res) {
                                                learun.loading(true, '催办审核...');
                                                var postData = {
                                                    processId: row.F_Id,
                                                };
                                                learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/UrgeFlow', postData, function (data) {
                                                    learun.loading(false);
                                                });
                                                top.layer.close(_index);
                                            }
                                        });
                                        return false;
                                    });
                                    $cell.on('click', '.revoke', function () {// 删除
                                        learun.layerConfirm('是否确认撤销流程？', function (res, _index) {
                                            if (res) {
                                                
                                                learun.loading(true, '撤销流程...');
                                                var postData = {
                                                    processId: row.F_Id,
                                                };
                                                learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/RevokeFlow', postData, function (data) {
                                                    learun.loading(false);
                                                    refreshGirdData();
                                                });
                                                top.layer.close(_index);
                                            }
                                        });
                                        return false;
                                    });


                                    var _btnHtml = '<span class="label label-primary urge" title="催办审核" >催办审核</span>';
                                    if (row.F_IsStart == 0) {
                                        _btnHtml += '<span class="label label-warning revoke" style="margin-left:5px;" title="撤销流程" >撤销流程</span>';
                                    }
                                    return _btnHtml;
                                }
                                else {
                                    return '本人发起';
                                }
                            }
                            if (row.F_TaskType == 3) {
                                return "【加签】" + cellvalue;
                            }
                            else if (row.F_TaskType == 5 && categoryId == '2') {
                                isaAain = true;
                            }
                            else if (row.F_TaskType == 5) {
                                return '重新发起';
                            }

                            if (isaAain) {
                                $cell.on('click', '.AgainCreate', function () {
                                    var title = "";
                                    if (row.F_SchemeName != row.F_Title && row.F_Title) {
                                        title = row.F_SchemeName + "(" + row.F_Title + ")";
                                    }
                                    else {
                                        title = row.F_SchemeName;
                                    }
                                    learun.frameTab.open({ F_ModuleId: row.F_Id, F_Icon: 'fa magic', F_FullName: '重新发起-' + title, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?processId=' + row.F_Id + '&tabIframeId=' + row.F_Id + '&type=againCreate' });
                                    return false;
                                });
                                return '<span class="label label-danger AgainCreate" title="重新发起">重新发起</span>';
                            }

                            
                            if (categoryId == '3' && row.F_TaskType == 1 && row.F_IsFinished == 0) {// 已完成任务，添加一个撤销按钮
                                cellvalue = '<span class="label label-warning revoke2" style="margin-left:5px;margin-right:5px;" title="撤销审核" >撤销审核</span>' + cellvalue;

                                $cell.on('click', '.revoke2', function () {// 删除
                                    learun.layerConfirm('是否确认撤销审核？', function (res, _index) {
                                        if (res) {

                                            learun.loading(true, '撤销审核...');
                                            var postData = {
                                                processId: row.F_Id,
                                                taskId: row.F_TaskId,
                                            };
                                            learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/RevokeAudit', postData, function (data) {
                                                learun.loading(false);
                                                refreshGirdData();
                                            });
                                            top.layer.close(_index);
                                        }
                                    });
                                    return false;
                                });
                            }

                            return cellvalue;
                        }
                    },
                    {
                        label: "标题", name: "F_Title", width: 300, align: "left", formatter: function (cellvalue,row) {
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
                           
                            if (row.F_TaskType == 4) {
                                if (row.F_IsFinished == 0) {
                                    if (row.F_IsUrge == "1" && categoryId == '2') {
                                        return "<span class=\"label label-danger\">催办加急</span>";
                                    }
                                    return "<span class=\"label label-success\">运行中</span>";
                                }
                                else {
                                    return "<span class=\"label label-warning\">结束</span>";
                                }
                                
                            }
                            if (row.F_IsFinished == 0) {
                                if (cellvalue == 1) {
                                    if (row.F_IsUrge == "1" && categoryId == '2') {
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
                onSelectRow: function (row) {
                    if (categoryId == '2') {
                        if (row.F_TaskType == 5) {
                            $('#lr_verify span').text("重新发起");
                        }
                        else if (row.F_TaskType == 3) {
                            $('#lr_verify span').text("【加签】" + row.F_TaskName);
                        }
                        else {
                            $('#lr_verify span').text(row.F_TaskName);
                        }
                    }
                },
                dblclick: function () {
                    if (categoryId == '2') {
                        page.verify();
                    }
                    else {
                        page.eye();
                    }
                }
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = logbegin;
            param.EndTime = logend;

            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param), categoryId: categoryId });
        },

        verify: function () {
            var processId = $('#gridtable').jfGridValue('F_Id');
            var taskId = $('#gridtable').jfGridValue('F_TaskId');
            var title = $('#gridtable').jfGridValue('F_Title');
            var schemeName = $('#gridtable').jfGridValue('F_SchemeName');
            var taskName = $('#gridtable').jfGridValue('F_TaskName');
            var taskType = $('#gridtable').jfGridValue('F_TaskType');

            if (schemeName != title && title) {
                title = schemeName + "(" + title + ")";
            }
            else {
                title = schemeName;
            }
            
            //1审批2传阅3加签4子流程5重新创建
            switch (taskType) {
                case 1:// 审批
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '审批-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=audit' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
                case 2:// 传阅
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '查阅-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=refer' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
                case 3:// 加签
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '加签审核-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=signAudit' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
                case 4:// 子流程
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '子流程-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=chlid' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
                case 5:// 重新创建
                    learun.frameTab.open({ F_ModuleId: processId, F_Icon: 'fa magic', F_FullName: '重新发起-' + title, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?processId=' + processId + '&tabIframeId=' + processId + '&type=againCreate' });
                    break;
                case 6:// 重新创建
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '子流程-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=againChild' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
            }
        },
        eye: function () {
            var processId = $('#gridtable').jfGridValue('F_Id') || '';
            var taskId = $('#gridtable').jfGridValue('F_TaskId') || '';
            var title = $('#gridtable').jfGridValue('F_Title');
            var schemeName = $('#gridtable').jfGridValue('F_SchemeName');
            var taskType = $('#gridtable').jfGridValue('F_TaskType');

            if (schemeName != title && title) {
                title = schemeName + "(" + title + ")";
            }
            else {
                title = schemeName;
            }

            var enabledMark = $('#gridtable').jfGridValue('F_EnabledMark');
            if (enabledMark == 2) {// 草稿不允许查看进度
                learun.alert.warning("草稿不能查看进度");
                return;
            }

            if (learun.checkrow(processId)) {
                if (taskType == '4' || taskType == '6') {
                    learun.frameTab.open({ F_ModuleId: processId + taskId, F_Icon: 'fa magic', F_FullName: '查看流程进度【' + title + '】', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + processId + taskId + '&type=childlook' + "&processId=" + processId + "&taskId=" + taskId });
                }
                else {
                    learun.frameTab.open({ F_ModuleId: processId + taskId, F_Icon: 'fa magic', F_FullName: '查看流程进度【' + title + '】', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + processId + taskId + '&type=look' + "&processId=" + processId + "&taskId=" + taskId });
                }
            }
        }

    };

    page.init();
}


