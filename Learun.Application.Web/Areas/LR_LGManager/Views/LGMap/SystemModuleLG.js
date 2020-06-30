/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：功能模块	
 */
var refreshGirdData; // 更新数据
var selectedRow;
var formHeight;
var keyValue;
var moduleType = true;
var bootstrap = function ($, learun) {
    "use strict";
    var moduleId = '0';
    var page = {
        init: function () {
            page.inittree();
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ parentId: moduleId, keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(selectedRow)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_LGManager/LGMap/AddForm?keyValue=' + keyValue,
                        width: 400,
                        height: formHeight,
                        callBack: function (id) {
                            return top[id].acceptClick(function(){
                                page.search({ parentId: moduleId });
                            });
                        }
                    });
                }
            });
        },
        inittree: function () {
            $('#module_tree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/Module/GetModuleTree',
                nodeClick: page.treeNodeClick
            });
        },
        treeNodeClick: function (item) {
            moduleId = item.id;
            moduleType = item.hasChildren
            $('#titleinfo').text(item.text);
            page.search({ parentId: moduleId });
        },
        initGrid: function () {
            var data = [];
            //获取语言类型
            learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGType/GetList', function (res) {
                if (res.data) {
                    data.push({ label: "名称", name: res.data[0].F_Code, width: 200, align: "left" });
                    keyValue = res.data[0].F_Code;//主语言
                    for (var i = 1; i < res.data.length; i++) {
                        var obj = { label: res.data[i].F_Name, name: res.data[i].F_Code, width: 200, align: "left" };
                        data.push(obj);
                    }
                    $('#gridtable').jfGrid({
                        headData: data,
                        dblclick: function (row) {
                            if (learun.checkrow(row)) {
                                selectedRow = row;
                                learun.layerForm({
                                    id: 'form',
                                    title: '编辑',
                                    url: top.$.rootUrl + '/LR_LGManager/LGMap/AddForm?keyValue=' + keyValue,
                                    width: 400,
                                    height: formHeight,
                                    callBack: function (id) {
                                        return top[id].acceptClick(function () {
                                            page.search({ parentId: moduleId });
                                        });
                                    }
                                });
                            }
                        }
                    });
                    page.search({ parentId: moduleId });
                    if (res.data.length <= 3) {
                        formHeight = 230;
                    }
                    else {
                        formHeight = 230 + (res.data.length - 3) * 40;
                    }
                }
            });
        },
        search: function (param) {
            //获取表数据并赋值
            var rowData = [];
            var obj = {};
            if (moduleType) {//判断是否有下级
                learun.httpAsyncGet(top.$.rootUrl + '/LR_SystemModule/Module/GetModuleListByParentId?parentId=' + param.parentId, function (res) {
                    learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGMap/GetList', function (mapRes) {
                        if (res.data && mapRes.data) {
                            for (var i = 0; i < res.data.length; i++) {
                                var val = mapRes.data.find(function (element) {
                                    return element.F_Name == res.data[i].F_FullName;
                                });
                                if (typeof val != 'undefined') {
                                    var list = mapRes.data.filter(function (element) {
                                        return element.F_Code == val.F_Code;
                                    });
                                    for (var j = 0; j < list.length; j++) {
                                        obj[list[j].F_TypeCode] = list[j].F_Name;
                                        obj.F_Code = list[j].F_Code;//每一行数据的F_Code
                                    }
                                }
                                else {
                                    obj[keyValue] = res.data[i].F_FullName;
                                    obj.F_Code = "";
                                }
                                rowData.push(obj);
                                obj = {};
                            }
                            $('#gridtable').jfGridSet('refreshdata', rowData);
                            rowData = [];
                        }
                    });
                });
            }
            else {//展示按钮与列
                learun.httpAsyncGet(top.$.rootUrl + '/LR_SystemModule/Module/GetFormData?keyValue=' + param.parentId, function (res) {
                    learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGMap/GetList', function (mapRes) {
                        if (res.data && mapRes.data) {
                            // 按钮信息
                            for (var i = 0; i < res.data.moduleButtons.length; i++) {
                                var val = mapRes.data.find(function (element) {
                                    return element.F_Name == res.data.moduleButtons[i].F_FullName;
                                });
                                if (typeof val != 'undefined') {
                                    var list = mapRes.data.filter(function (element) {
                                        return element.F_Code == val.F_Code;
                                    });
                                    for (var j = 0; j < list.length; j++) {
                                        obj[list[j].F_TypeCode] = list[j].F_Name;
                                        obj.F_Code = list[j].F_Code;//每一行数据的F_Code
                                    }
                                }
                                else {
                                    obj[keyValue] = res.data.moduleButtons[i].F_FullName;
                                    obj.F_Code = "";
                                }
                                rowData.push(obj);
                                obj = {};
                            }
                            // 列表信息
                            for (var i = 0; i < res.data.moduleColumns.length; i++) {
                                var val = mapRes.data.find(function (element) {
                                    return element.F_Name == res.data.moduleColumns[i].F_FullName;
                                });
                                if (typeof val != 'undefined') {
                                    var list = mapRes.data.filter(function (element) {
                                        return element.F_Code == val.F_Code;
                                    });
                                    for (var j = 0; j < list.length; j++) {
                                        obj[list[j].F_TypeCode] = list[j].F_Name;
                                        obj.F_Code = list[j].F_Code;//每一行数据的F_Code
                                    }
                                }
                                else {
                                    obj[keyValue] = res.data.moduleColumns[i].F_FullName;
                                    obj.F_Code = "";
                                }
                                rowData.push(obj);
                                obj = {};
                            }
                            // 表单字段
                            for (var i = 0; i < res.data.moduleFields.length; i++) {
                                var val = mapRes.data.find(function (element) {
                                    return element.F_Name == res.data.moduleFields[i].F_FullName;
                                });
                                if (typeof val != 'undefined') {
                                    var list = mapRes.data.filter(function (element) {
                                        return element.F_Code == val.F_Code;
                                    });
                                    for (var j = 0; j < list.length; j++) {
                                        obj[list[j].F_TypeCode] = list[j].F_Name;
                                        obj.F_Code = list[j].F_Code;//每一行数据的F_Code
                                    }
                                }
                                else {
                                    obj[keyValue] = res.data.moduleFields[i].F_FullName;
                                    obj.F_Code = "";
                                }
                                rowData.push(obj);
                                obj = {};
                            }

                            $('#gridtable').jfGridSet('refreshdata', rowData);
                            rowData = [];
                        }
                    });
                });
            }
        }
    };
    // 保存数据后回调刷新
    refreshGirdData = function () {
        page.search({ parentId: moduleId });
    }

    page.init();
}