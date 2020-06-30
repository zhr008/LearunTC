/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-03-01 11:09
 * 描  述：代码模板
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";

    var ftype;

    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 初始化左侧树形数据
            $('#dataTree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailTree',
                param: { itemCode: 'CodeSchemaType' },
                nodeClick: function (item) {
                    console.log(item);

                    ftype = item.value;
                    $('#titleinfo').text(item.text);
                    page.search();
                }
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#F_Name').val();
                page.search({ F_Name: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var ftype = $('#gridtable').jfGridValue('F_Type');
                if (learun.checkrow(keyValue)) {
                    switch (ftype) {
                        case '1'://
                            learun.layerForm({
                                id: 'CustmerCodeIndex',
                                title: '在线代码生成器 并自动创建代码(移动开发模板)',
                                url: top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/AppCustmerCodeIndex?schemaId=' + keyValue,
                                width: 1100,
                                height: 700,
                                maxmin: true,
                                btn: null
                            });
                            break;
                        case '2'://
                            learun.layerForm({
                                id: 'CustmerCodeIndex',
                                title: '在线代码生成器 并自动创建代码(自定义开发模板)',
                                url: top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/CustmerCodeIndex?schemaId=' + keyValue,
                                width: 1100,
                                height: 700,
                                maxmin: true,
                                btn: null
                            });
                            break;
                        case '3'://
                            learun.layerForm({
                                id: 'CustmerCodeIndex',
                                title: '在线代码生成器 并自动创建代码(流程系统表单开发模板)',
                                url: top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/WorkflowCodeIndex?schemaId=' + keyValue,
                                width: 1100,
                                height: 700,
                                maxmin: true,
                                btn: null
                            });
                            break;
                        case '4'://
                            learun.layerForm({
                                id: 'CustmerCodeIndex',
                                title: '在线代码生成器 并自动创建代码(编辑列表页模板)',
                                url: top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/GridEditCodeIndex?schemaId=' + keyValue,
                                width: 1100,
                                height: 700,
                                maxmin: true,
                                btn: null
                            });
                            break;
                        case '5'://
                            learun.layerForm({
                                id: 'CustmerCodeIndex',
                                title: '在线代码生成器 并自动创建代码(报表显示页模板)',
                                url: top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/ReportCodeIndex?schemaId=' + keyValue,
                                width: 1100,
                                height: 700,
                                maxmin: true,
                                btn: null
                            });
                            break;
                    }


                    
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeGeneratorModule/CodeSchema/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_CodeGeneratorModule/CodeSchema/GetPageList',
                headData: [
                    { label: "模板名称", name: "F_Name", width: 100, align: "left" },
                    {
                        label: "模板分类", name: "F_Catalog", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'CodeSchemaType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: "开发模板", name: "F_Type", width: 100, align: "center",
                        formatter: function (cellvalue) {
                            if (cellvalue == 0) {
                                return '<span class=\"label label-success\" style=\"cursor: pointer;\">Web功能模板</span>';
                            } else if (cellvalue == 1) {
                                return '<span class=\"label label-default\" style=\"cursor: pointer;\">含APP功能模板</span>';
                            }
                            else if (cellvalue == 2) {
                                return '<span class=\"label label-danger\" style=\"cursor: pointer;\">含工作流模板</span>';
                            }
                        }
                    },
                    { label: "模板描述", name: "F_Description", width: 100, align: "left" },
                ],
                mainId: 'F_Id',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.F_Catalog = ftype;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
