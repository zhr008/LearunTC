/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.12.17
 * 描 述：流程详情
 */
var processId = request('processId');      // 流程实例主键
var nodeId = request('nodeId');         // 发起子流程的节点Id
var schemeObj;

var bootstrap = function ($, learun) {
    "use strict";

    var history;
    var currentIds;

    var page = {
        nodeMap: {},
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#lr_form_tabs').lrFormTab();
            $('#wf_shcemeinfo').lrworkflow({
                isPreview: true,
                openNode: function (node) {
                    top.wflookNode = node;
                    if (node.history) {
                        learun.layerForm({
                            id: 'LookNodeForm',
                            title: '审批记录查看【' + node.name + '】',
                            url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/LookNodeForm',
                            width: 600,
                            height: 400,
                            btn: null
                        });
                    }
                }
            });
        },
        initData: function () {
            if (processId && nodeId) {
                learun.httpAsync('GET', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/GetChildProcessDetails', { processId: processId, nodeId: nodeId }, function (data) {
                    if (data) {
                        var info = data.info;
                        page.taskInfo = data.task;

                        schemeObj = JSON.parse(info.Scheme);
                        history = info.TaskLogList;
                        currentIds = info.CurrentNodeIds;

                        page.loadFlowInfo();
                        page.loadTimeLine();
                    }
                });
            }
            else {
                learun.httpAsync('GET', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/GetProcessDetails', { processId: processId }, function (data) {
                    if (data) {
                        var info = data.info;
                        page.taskInfo = data.task;

                        schemeObj = JSON.parse(info.Scheme);

                        history = info.TaskLogList;
                        currentIds = info.CurrentNodeIds;

                        page.loadFlowInfo();
                        page.loadTimeLine();
                    }
                });
            }
        },

        loadFlowInfo: function () {
            learun.clientdata.getAllAsync('department', {
                callback: function (departmentMap) {
                    learun.clientdata.getAllAsync('user', {
                        callback: function (userMap) {
                            var nodeInfoes = {};
                            // 当前节点处理人信息
                            $.each(page.taskInfo, function (_index, _item) {
                                var nameList = [];
                                $.each(_item.nWFUserInfoList, function (_jindex, _jitem) {
                                    if (userMap[_jitem.Id]) {
                                        var name = userMap[_jitem.Id].name;
                                        var _department = departmentMap[userMap[_jitem.Id].departmentId];
                                        if (_department) {
                                            name = '【' + _department.name + '】' + name;
                                        }

                                        nameList.push(name);
                                    }
                                });
                                var point = {
                                    namelist: String(nameList)
                                }
                                nodeInfoes[_item.F_NodeId] = nodeInfoes[_item.F_NodeId] || [];
                                nodeInfoes[_item.F_NodeId].push(point);
                            });
                            // 初始化工作流节点历史处理信息
                            $.each(history, function (id, item) {
                                nodeInfoes[item.F_NodeId] = nodeInfoes[item.F_NodeId] || [];
                                nodeInfoes[item.F_NodeId].push(item);
                            });

                            var strcurrentIds = String(currentIds);
                            $.each(schemeObj.nodes, function (_index, _item) {//0正在处理 1 已处理同意 2 已处理不同意 3 未处理 
                                _item.state = '3';
                                if (nodeInfoes[_item.id]) {
                                    _item.history = nodeInfoes[_item.id];
                                    _item.state = '1';
                                }
                                if (strcurrentIds.indexOf(_item.id) > -1) {
                                    _item.state = '0';
                                }
                                if (_item.isAllAuditor == "2") {
                                    _item.name += '<br/>【多人审核:';

                                    if (_item.auditorType == "1") {
                                        _item.name += '并行】';
                                    }
                                    else {
                                        _item.name += '串行】';
                                    }
                                }
                                page.nodeMap[_item.id] = _item;
                            });

                            $('#wf_shcemeinfo').lrworkflowSet('set', { data: schemeObj });
                        }
                    });
                }
            });
        },
        loadTimeLine: function () {
            var nodelist = [];
            learun.clientdata.getAllAsync('department', {
                callback: function (departmentMap) {
                    learun.clientdata.getAllAsync('user', {
                        callback: function (userMap) {
                            for (var i = 0, l = history.length; i < l; i++) {
                                var item = history[i];


                                var name = (item.F_CreateUserName || '系统处理') + '：';
                                if (item.F_CreateUserId && userMap[item.F_CreateUserId]) {
                                    var _department = departmentMap[userMap[item.F_CreateUserId].departmentId];
                                    if (_department) {
                                        name = '【' + _department.name + '】' + name;
                                    }
                                }


                                //F_TaskType //0创建1审批2传阅3加签审核4子流程5重新创建6.超时流转7会签审核8加签9催办10超时提醒
                                var content = item.F_OperationName;
                                if (item.F_Des) {
                                    content += '【审批意见】' + item.F_Des;
                                }
                                var nodeName = '';
                                if (item.F_NodeId && page.nodeMap[item.F_NodeId]) {
                                    nodeName = page.nodeMap[item.F_NodeId].name;
                                }
                                var point = {
                                    title: item.F_NodeName || nodeName,
                                    people: name,
                                    content: content,
                                    time: item.F_CreateDate
                                };

                                if (item.F_TaskType == 0 || item.F_TaskType == 1 || item.F_TaskType == 3 || item.F_TaskType == 5 || item.F_TaskType == 6 || item.F_TaskType == 8) {
                                    // 查看表单
                                    point.content = content + '&nbsp;&nbsp;<span class="lr-event" >查看表单</span>';
                                    point.nodeId = item.F_NodeId;
                                    point.processId = item.F_ProcessId;
                                    point.callback = function (data) {
                                        top['lrwfschemeObj'] = schemeObj;


                                        learun.layerForm({
                                            id: 'MonitorForm',
                                            title: '表单查看-' + data.title,
                                            url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/MonitorForm?nodeId=' + data.nodeId + '&processId=' + data.processId,
                                            width: 800,
                                            height: 700,
                                            maxmin: true,
                                            btn: null
                                        });
                                    }
                                }
                                else if (item.F_OperationCode == 'createChild' || item.F_OperationCode == 'againCreateChild') {
                                    point.content = content + '&nbsp;&nbsp;<span class="lr-event" >查看子流程</span>';
                                    point.nodeId = item.F_NodeId;
                                    point.processId = item.F_ProcessId;
                                    point.callback = function (data) {
                                        learun.layerForm({
                                            id: 'MonitorDetailsIndex',
                                            title: '子流程查看',
                                            url: top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/MonitorDetailsIndex?processId=' + data.processId + "&nodeId=" + data.nodeId,
                                            width: 1000,
                                            height: 900,
                                            maxmin: true,
                                            btn: null
                                        });
                                    }
                                }



                                nodelist.push(point);
                            }
                            $('#wf_timeline').lrtimeline(nodelist);
                        }
                    });
                }
            });




        }
    };
    page.init();
}