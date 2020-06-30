/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：
 * 日 期：2018.11.08
 * 描 述：印章管理	
 */
var selectedRow;
var refreshGirdData;
var F_StampType;
var F_StampName;
var F_EnabledMark;
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 左侧数据加载
            $('#lr_left_tree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailTree',
                param: { itemCode: 'StampType' },
                nodeClick: function (item) {
                    F_StampType = item.value;
                    F_StampName = null;//将文本框数据设置为null，如果不设置，那么不能按照左侧分类查询
                    F_EnabledMark = null;//将文本框数据设置为null，如果不设置，那么不能按照左侧分类查询
                    $('#titleinfo').text(item.text);
                    page.search();
                }
            });
            //查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                F_StampType = null;
                page.search({ F_StampName: keyword });
            });

            $('[name="isEnabled"]').on('click', function () {
                var value = $(this).val(); //状态查询
                if (value == 1) {
                    F_EnabledMark = value;
                    page.search();
                }
                else {
                    F_EnabledMark = value;
                    page.search();
                }
            });
            //刷新
            $("#lr_refresh").on('click', function () {
                location.reload();
            });
            //新增
            $("#lr_add").on('click', function () {
                selectedRow = null;
                learun.layerForm({
                    id: 'form',
                    title: '添加印章',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/Form',
                    width: 700,
                    height: 500,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            //编辑
            $("#lr_edit").on("click", function () {
                selectedRow = $("#gridtable").jfGridGet("rowdata");//获取选中的当前行数据
                var keyValue = $("#gridtable").jfGridValue("F_StampId");//获取当前选中的主键值
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'Form',
                        title: '编辑印章',
                        url: top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/Form?keyValue=' + keyValue,
                        width: 750,
                        height: 500,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            //删除
            $("#lr_delete").on("click", function () {
                var keyValue = $('#gridtable').jfGridValue('F_StampId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 启用
            $('#lr_enable').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_StampId');
                var enabledMark = $('#gridtable').jfGridValue('F_EnabledMark');
                if (learun.checkrow(keyValue)) {
                    if (enabledMark != 1) {
                        learun.layerConfirm('是否确认启用该项！', function (res) {
                            if (res) {
                                learun.postForm(top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/UpDateSate', { keyValue: keyValue, state: 1 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        learun.alert.warning('该项已启用!');
                    }
                }
            });
            // 禁用
            $('#lr_disabled').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_StampId');
                var enabledMark = $('#gridtable').jfGridValue('F_EnabledMark');

                if (learun.checkrow(keyValue)) {
                    if (enabledMark == 1) {
                        learun.layerConfirm('是否确认禁用该项！', function (res) {
                            if (res) {
                                learun.postForm(top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/UpDateSate', { keyValue: keyValue, state: 0 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        learun.alert.warning('该项已禁用!');
                    }
                }
            });
            /*分类管理*/
            $('#lr_category').on('click', function () {
                learun.layerForm({
                    id: 'ClassifyIndex',
                    title: '分类管理',
                    url: top.$.rootUrl + '/LR_SystemModule/DataItem/DetailIndex?itemCode=StampType',
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: null,
                    end: function () {
                        learun.clientdata.update('dataItem');
                        location.reload();
                    }
                });
            });
        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/GetPageList',
                headData: [
                    { label: '印章名称', name: 'F_StampName', width: 150, align: "center" },
                    { label: '印章分类', name: 'F_StampType', width: 100, align: "center" },
                    {
                        label: '图片', name: 'F_ImgFile', width: 110, align: "center",
                        formatter: function (value, row, op, $cell) {
                            return '<img src="' + top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/GetImg?keyValue=' + row.F_StampId + '"  style="position: absolute;height:100px;width:100px;top:5px;left:5px;" >';
                        }
                    }, 
                    {
                        label: '状态', name: 'F_EnabledMark', width: 60, align: "center", formatter: function (value, row, op, $cell) {
                            if (value == 1) {
                                return '<span class=\"label label-success\" style=\"cursor: pointer;\">启用</span>';
                            } else if (value == 0) {
                                return '<span class=\"label label-default\" style=\"cursor: pointer;\">禁用</span>';
                            }
                        }
                    },
                    { label: '备注', name: 'F_Description', width: 200, align: "left" }
                ],
                mainId: 'F_StampId',
                isPage: true,
                rowHeight: 110,
                sidx: 'F_EnabledMark Desc,F_Sort ASC'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.F_StampType = F_StampType;
            param.F_EnabledMark = F_EnabledMark;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    }
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}