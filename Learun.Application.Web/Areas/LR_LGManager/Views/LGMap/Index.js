/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-04-10 15:00
 * 描  述：语言映照
 */
var selectedRow;
var refreshGirdData;
var formHeight;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
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
            // 新增
            $('#lr_add').on('click', function () {
                selectedRow = null;
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_LGManager/LGMap/Form',
                    width: 400,
                    height: formHeight,
                    callBack: function (id) {
                        return top[id].acceptClick(page.search);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(selectedRow)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_LGManager/LGMap/Form?keyValue=' + selectedRow.f_code,
                        width: 400,
                        height: formHeight,
                        callBack: function (id) {
                            return top[id].acceptClick(page.search);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(selectedRow)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_LGManager/LGMap/DeleteForm', { keyValue: selectedRow["f_code"] }, function () {
                                page.search()
                            });
                        }
                    });
                }
            });
        },
        initGrid: function () {
            var data = [];
            //获取语言类型
            learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGType/GetList', function (res) {
                if (res.data) {
                    var typeList = [];
                    for (var i = 0; i < res.data.length; i++) {
                        var obj = { label: res.data[i].F_Name, name: res.data[i].F_Code.toLowerCase(), width: 200, align: "left" };
                        data.push(obj);
                        typeList.push(res.data[i].F_Code);
                    }
                    $('#gridtable').jfGrid({
                        url: top.$.rootUrl + '/LR_LGManager/LGMap/GetPageList?typeList=' + typeList,
                        headData: data,
                        isPage: true,
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
            param = param || {};
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) } );
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
