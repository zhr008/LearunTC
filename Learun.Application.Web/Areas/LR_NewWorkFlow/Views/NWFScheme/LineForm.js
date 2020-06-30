/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：流程线条设置	
 */
var layerId = request('layerId');
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var currentLine = top[layerId].currentLine;
    var fromNode = top[layerId].fromNode;

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#lr_form_tabs').lrFormTab();
            $('#color').lrselect({// 1.黑 2.红 
                placeholder: false,
                data: [{ id: '1', text: '黑' }, { id: '2', text: '红' }]
            }).lrselectSet('1');
            $('#strategy').lrselect({
                placeholder: false,
                data: [{ id: '1', text: '所有情况都通过' }, { id: '2', text: '自定义设置' }],
                select: function (item) {
                    if (item && item.id == '2') {
                        $('#agreeList').parent().show();
                    }
                    else {
                        $('#agreeList').parent().hide();
                    }
                }
            }).lrselectSet('1');
            var agreeList = [{ name: '同意', code: 'agree' }, { name: '不同意', code: 'disagree' }];
            // 处理上一节点的数据
            switch (fromNode.type) {
                case 'startround':   // 开始节点
                case 'childwfnode':  // 子流程
                    $('#strategy').parent().remove();
                    $('#color').parent().remove();
                    $('#agreeList').parent().remove();
                    break;
                case 'stepnode':     // 一般审核节点
                    if (fromNode.btnList && fromNode.btnList.length > 0) {
                        agreeList = [];
                        $.each(fromNode.btnList, function (_index, _item) {
                            var point = { name: _item.name, code: _item.code };
                            agreeList.push(point);
                        });
                        if (fromNode.timeoutAction && fromNode.timeoutAction != '0') {
                            agreeList.push({ name: '超时', code: 'lrtimeout' });
                        }
                    }
                    $('#agreeList').lrselect({
                        text: 'name',
                        value: 'code',
                        data: agreeList,
                        type: 'multiple'
                    }).lrselectSet('agree');
                    break;
                case 'confluencenode':  // 会签
                    $('#agreeList').lrselect({
                        data: agreeList,
                        text: 'name',
                        value: 'code',
                        type: 'multiple'
                    }).lrselectSet('agree');
                    break;
                case 'conditionnode':  // 条件
                    $('#agreeList').lrselect({
                        data: [{ name: '是', code: 'agree' }, { name: '否', code: 'disagree' }],
                        text: 'name',
                        value: 'code',
                        type: 'multiple'
                    }).lrselectSet('agree');
                    break;
            }

            $('input[name="operationType"]').on('click', function () {
                var value = $(this).val();
                $('.operationDiv').hide();
                $('#' + value).show();
            });

            $('#dbId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true
            });
        },
        initData: function () {
            $('#baseInfo').lrSetFormData(currentLine);
            $('#operationTypeDiv').lrSetFormData(currentLine);
            switch (currentLine.operationType) {
                case 'sql':
                    $('#dbId').lrselectSet(currentLine.dbId);
                    $('#strSql').val(currentLine.strSql);
                    $('#strSqlR').val(currentLine.strSqlR);
                    break;
                case 'interface':
                    $('#strInterface').val(currentLine.strInterface);
                    $('#strInterfaceR').val(currentLine.strInterfaceR);
                    break;
                case 'ioc':
                    $('#iocName').val(currentLine.iocName);
                    $('#iocNameR').val(currentLine.iocNameR);
                    break;
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var baseInfo = $('#baseInfo').lrGetFormData();
        var operation = $('#operationTypeDiv').lrGetFormData();

        currentLine.name = baseInfo.name;
        switch (fromNode.type) {
            case 'stepnode':     // 一般审核节点
            case 'confluencenode':  // 会签
            case 'conditionnode':  // 条件
                currentLine.name = baseInfo.name;
                currentLine.color = baseInfo.color;
                currentLine.strategy = baseInfo.strategy;
                currentLine.agreeList = baseInfo.agreeList;
                break;
        }

        currentLine.operationType = operation.operationType;
        switch (currentLine.operationType) {
            case 'sql':
                currentLine.dbId = $('#dbId').lrselectGet();
                currentLine.strSql = $('#strSql').val();
                currentLine.strSqlR = $('#strSqlR').val();
                break;
            case 'interface':
                currentLine.strInterface = $('#strInterface').val();
                currentLine.strInterfaceR = $('#strInterfaceR').val();
                break;
            case 'ioc':
                currentLine.iocName = $('#iocName').val();
                currentLine.iocNameR = $('#iocNameR').val();
                break;
        }
        

        callBack();
        return true;
    };
    page.init();
}
