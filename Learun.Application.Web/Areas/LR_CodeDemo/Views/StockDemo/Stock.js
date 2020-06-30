/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-06-12 18:49
 * 描  述：库存
 */
var stockArea = request('stockArea');
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_CodeDemo/StockDemo/GetStock?stockArea=' + stockArea,
                headData: [
                    { label: "物料名称", name: "F_ItemName", width: 100, align: "left"},
                    { label: "库存数量", name: "F_Qty", width: 100, align: "left"},
                    { label: "库存单位", name: "F_Unit", width: 100, align: "left"},
                    { label: "存放库位", name: "F_Area", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'StockArea',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                ],
                mainId:'F_Id',
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload');
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
