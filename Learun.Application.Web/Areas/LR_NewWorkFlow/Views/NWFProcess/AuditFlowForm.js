/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：审核流程	
 */
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var processId = request('processId');      // 流程进程主键
    var taskId = request('taskId');            // 流程任务主键
    var next = request('next');                // 下一节点是否允许指定审核人 1不允许 2允许
    var operationCode = request('operationCode');
         
    var currentNode = learun.frameTab.currentIframe().nwflow.currentNode;
    var nodeList = learun.frameTab.currentIframe().nwflow.schemeObj.nodes;
    var nodeMap = {};

    var page = {
        init: function () {
            if (next == '2') {// 获取下一节点数据
                // 节点信息
                $.each(nodeList, function (_index, _item) {
                    nodeMap[_item.id] = _item;
                });
                var param = {
                    processId: processId,
                    taskId: taskId,
                    nodeId: currentNode.id,
                    operationCode: operationCode
                };
                learun.httpAsync('GET', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/GetNextAuditors', param, function (data) {
                    var $form = $('#form .lr-scroll-box');
                    if (data) {
                        $.each(data, function (_id, _list) {
                            if (_list.length > 1) {
                                $form.append('<div class="col-xs-12 lr-form-item"><div class="lr-form-item-title" >' + nodeMap[_id].name.replace('<br/>【多人审核:并行】', '').replace('<br/>【多人审核:串行】', '') + '</div><div id="' + _id + '" class="nodeId" ></div></div >');
                                $('#' + _id).lrselect({
                                    type: 'multiple',
                                    data: _list,
                                    value: 'Id',
                                    text: 'Name'
                                });
                            }
                        });
                    }
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        var formData = $('#form').lrGetFormData();
        // 获取审核人员
        var auditers = {};
        $('#form').find('.nodeId').each(function () {
            var $this = $(this);
            var id = $this.attr('id');
            if (formData[id]) {
                auditers[id] = formData[id];
            }
        });
        callBack(formData.des, auditers);
        return true;
    };
    page.init();
}