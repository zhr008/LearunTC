/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-06-10 17:21
 * 描  述：工单管理
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var processId = '';
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            //setTimeout("  $('#dataTree [tpath=0]').trigger('click')", 1000)
        },
        bind: function () {
            // 初始化左侧树形数据
            $('#dataTree').lrtree({
                url: top.$.rootUrl + '/LR_CodeDemo/WorkOrder/GetTree',
                nodeClick: function (item) {
                    page.search({ F_DepartmentId: item.value });
                }
            });
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            $('#F_DepartmentId').lrDepartmentSelect();
            $('#F_Process').lrDataItemSelect({ code: 'Process' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_CodeDemo/WorkOrder/Form',
                    width: 600,
                    height: 400,
                    callBack: function (id) {
                        var res = false;
                        // 验证数据
                        res = top[id].validForm();
                        // 保存数据
                        if (res) {
                            processId = learun.newGuid();
                            res = top[id].save(processId, refreshGirdData);
                        }
                        return res;
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
                        url: top.$.rootUrl + '/LR_CodeDemo/WorkOrder/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        callBack: function (id) {
                            var res = false;
                            // 验证数据
                            res = top[id].validForm();
                            // 保存数据
                            if (res) {
                                res = top[id].save('', function () {
                                    page.search();
                                });
                            }
                            return res;
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
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/WorkOrder/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 打印
            $('#lr_print').on('click', function () {
                $('#gridtable').jqprintTable();
            });
            // 套打
            $('#lr_printItem').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.frameTab.open({
                        F_ModuleId: 'report',
                        F_Icon: 'fa magic',
                        F_FullName: '工单套打',
                        F_UrlAddress: '/LR_ReportModule/RptManage/Report?reportId=' + encodeURI(encodeURI("制程工单.rdlx|" + keyValue))
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_CodeDemo/WorkOrder/GetPageList',
                headData: [
                    { label: "工单编码", name: "F_Code", width: 200, align: "left" },
                    {
                        label: "生产部门", name: "F_DepartmentId", width: 150, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('department', {
                                key: value,
                                callback: function (_data) {
                                    callback(_data.name);
                                }
                            });
                        }
                    },
                    { label: "负责人", name: "F_ManagerId", width: 100, align: "left" },
                    { label: "生产数量", name: "F_Qty", width: 100, align: "left" },
                    {
                        label: "工单制程", name: "F_Process", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'Process',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "规格型号", name: "F_Spec", width: 150, align: "left" },
                    {
                        label: "完工状态", name: "F_Status", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'HaveOrNot',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
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
    refreshGirdData = function (res, postData) {
        if (!!res) {
              if (res.code == 200)
              {
                 // 发起流程
                 var postData = {
                      schemeCode:'wf001',// 填写流程对应模板编号
                      processId:processId,
                      level:'1',
                 };
                 learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/CreateFlow', postData, function(data) {
                     learun.loading(false);
                 });
            }
            page.search();
        }
    };
    page.init();
}
