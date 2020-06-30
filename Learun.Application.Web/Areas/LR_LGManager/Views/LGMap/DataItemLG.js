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
var bootstrap = function ($, learun) {
    "use strict";
    var classify_itemCode = '';
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
                page.search({ keyword: keyword });
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
                            return top[id].acceptClick(page.search);
                        }
                    });
                }
            });
        },
        inittree: function () {
            $('#lr_left_tree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetClassifyTree',
                nodeClick: function (item) {
                    classify_itemCode = item.value;
                    $('#titleinfo').text(item.text + '(' + classify_itemCode + ')');
                    page.search();
                }
            });
        },
        initGrid: function () {
            var data = [];
            //获取语言类型
            learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGType/GetList', function (res) {
                if (res.data) {
                    data.push({ label: "项目值", name: res.data[0].F_Code, width: 200, align: "left" });
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
                                        return top[id].acceptClick(page.search);
                                    }
                                });
                            }
                        }
                    });
                    page.search();
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
            learun.httpAsyncGet(top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailList?itemCode=' + classify_itemCode, function (res) {
                learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGMap/GetList', function (mapRes) {
                    if (res.data && mapRes.data) {
                        for (var i = 0; i < res.data.length; i++) {
                            var val = mapRes.data.find(function (element) {
                                return element.F_Name == res.data[i].F_ItemName;
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
                                obj[keyValue] = res.data[i].F_ItemName;
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
    };
    // 保存数据后回调刷新
    refreshGirdData = function () {
        page.search();
    }

    page.init();
}