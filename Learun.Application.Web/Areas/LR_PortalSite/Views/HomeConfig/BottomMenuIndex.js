/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：数据字典管理	
 */
var refreshGirdData; // 更新数据
var selectedRow;
var alldata = [];
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
                    id: 'BottomMenuForm',
                    title: '添加菜单',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/BottomMenuForm',
                    width: 500,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'BottomMenuForm',
                        title: '编辑菜单',
                        url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/BottomMenuForm',
                        width: 500,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
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
                            learun.deleteForm(top.$.rootUrl + '/LR_PortalSite/HomeConfig/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/GetList?type=7',
                headData: [
                    { label: '名称', name: 'F_Name', width: 200, align: 'left' },
                    { label: '排序码', name: 'F_Sort', width: 50, align: 'center' },
                    {
                        label: '链接类型', name: 'F_UrlType', width: 80, align: 'center',
                        formatter: function (cellvalue, row) {
                            if (cellvalue == 1) {
                                return '<span class=\"label label-warning\" style=\"cursor: pointer;\">内部链接</span>';
                            } else {
                                return '<span class=\"label label-primary\" style=\"cursor: pointer;\">外部链接</span>';
                            }
                        }
                    },
                    {
                        label: '链接地址', name: 'F_Url', width: 200, align: 'left',
                        formatterAsync: function (callback, value, row) {
                            if (row.F_UrlType == 1) {
                                learun.clientdata.getAsync('custmerData', {
                                    url: '/LR_PortalSite/Page/GetList',
                                    key: value,
                                    keyId: 'F_Id',
                                    callback: function (item) {
                                        callback(item.F_Title);
                                    }
                                });
                            } else {
                                return callback(value);
                            }
                        }
                    }
                ],
                isTree: true,
                mainId: 'F_Id',
                reloadSelected: true,
                onRenderComplete: function (data) {
                    alldata = data;
                    learun.frameTab.currentIframe().renderBottomMenu(alldata);
                }
            });
            page.search();
        },
        search: function (param) {
            $('#gridtable').jfGridSet('reload', param);
        }
    };

    // 保存数据后回调刷新
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');

    }

    page.init();
}


