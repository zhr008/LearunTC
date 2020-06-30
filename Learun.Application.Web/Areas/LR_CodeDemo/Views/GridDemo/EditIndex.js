/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：单据编码	
 */
var refreshGirdData; // 更新数据
var selectedRow;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 保存
            $('#lr_save').on('click', function () {
                var data = $('#gridtable').jfGridGet('rowdatas');
                $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/GridDemo/SaveList', { jsondata: JSON.stringify(data) }, function (res) {
                    $('#gridtable').jfGridSet('reload');
                }
                );
            })
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_CodeDemo/GridDemo/GetList',
                headData: [
                    {
                        label: '请假类型', name: 'F_Type', width: 120, align: 'left',
                        edit: {
                            type: 'select',
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (row, num, item) {// 行数据和行号,下拉框选中数据

                            },
                            op: {// 下拉框设置参数 和 lrselect一致
                                data: [
                                    { 'id': '1', 'text': '婚假' },
                                    { 'id': '2', 'text': '产假' },
                                    { 'id': '3', 'text': '事假' },
                                    { 'id': '4', 'text': '病假' }
                                ],
                                allowSearch: true
                            }
                        }
                    },
                    {
                        label: '请假事由', name: 'F_Reason', width: 300, align: 'left',
                        edit: {
                            type: 'input',
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象
                            },
                            change: function (data, num) {// 行数据和行号

                            }
                        }
                    },
                    {
                        label: '项目组', name: 'F_Field', width: 260, align: 'left',
                        edit: {
                            type: 'checkbox',
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (data, num) {// 行数据和行号

                            },
                            data: [
                                { 'id': '1', 'text': '快速组' },
                                { 'id': '2', 'text': '敏捷组' },
                                { 'id': '3', 'text': 'ERP组' },
                                { 'id': '4', 'text': 'CRM组' }
                            ],
                            dfvalue: '1,2'// 默认选中项
                        }
                    },
                    {
                        label: '时间', name: 'F_Begin', width: 120, align: 'left',
                        edit: {
                            type: 'datatime',
                            dateformat: '0',       // 0:yyyy-MM-dd;1:yyyy-MM-dd HH:mm,格式
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (data, num) {// 行数据和行号

                            }
                        }
                    },
                    {
                        label: '负责项目', name: 'F_DataItem', width: 120, align: 'left',
                        edit: {
                            type: 'layer',
                            init: function (data, $edit, rownum) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (data, rownum, selectData) {// 行数据和行号,弹层选择行的数据，如果是自定义实现弹窗方式则该方法无效
                                data.F_DataItem = selectData.F_ItemValue;
                                data.F_CreateUserId = selectData.F_ItemName;
                                $('#gridtable').jfGridSet('updateRow', rownum);
                            },
                            op: { // 如果未设置op属性可以在init中自定义实现弹窗方式
                                width: 600,
                                height: 400,
                                colData: [
                                    { label: "商品编号", name: "F_ItemValue", width: 100, align: "left" },
                                    { label: "商品名称", name: "F_ItemName", width: 450, align: "left" }
                                ],
                                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailList',
                                param: { itemCode: 'Client_ProductInfo' }
                            }
                        }
                    },
                    {
                        label: '项目名称', name: 'F_CreateUserId', width: 120, align: 'left'
                    }

                ],
                mainId: 'F_Id',
                isEdit: true,
                isMultiselect: true,
                onAddRow: function (row, rows) {//行数据和所有行数据

                },
                onMinusRow: function (row, rows) {//行数据和所有行数据

                },
                beforeMinusRow: function (row) {// 行数据 返回false 则不许被删除
                    return true;
                }
            });
        }
    };
    page.init();
    $('#gridtable').jfGridSet('reload');
}


