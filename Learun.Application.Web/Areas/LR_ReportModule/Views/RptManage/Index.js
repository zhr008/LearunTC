/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-03-14 15:17
 * 描  述：报表文件管理
 */
var refreshGirdData;
var fileId;
var keyValue;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            //page.initGird();
            page.bind();
        },
        bind: function () {
            // 初始化左侧树形数据
            $('#dataTree').lrtree({
                url: top.$.rootUrl + '/LR_ReportModule/RptManage/GetDetailTree',
                nodeClick: function (item) {
                    //type = item.value;
                    $('#titleinfo').text(item.text);
                    if (item.id.length > 20) {
                        fileId = item.value;
                        keyValue = item.id;
                        page.show(item.value);
                    }
                }
            });
            //$('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
            //    page.search(queryJson);
            //}, 180, 400);
            $('#F_Type').lrDataItemSelect({ code: 'ReportSort' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_ReportModule/RptManage/Form',
                    width: 600,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(page.init());
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                //var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_ReportModule/RptManage/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(page.init());
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                //var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_ReportModule/RptManage/DeleteForm', { keyValue: keyValue }, function () {
                                page.init();
                            });
                        }
                    });
                }
            });
            // 打印
            $('#lr_print').on('click', function () {
                //var reportId = $('#gridtable').jfGridValue('F_File');
                learun.frameTab.open({ F_ModuleId: 'report', F_Icon: 'fa magic', F_FullName: fileId, F_UrlAddress: '/LR_ReportModule/RptManage/Report?reportId=' + encodeURI(encodeURI(fileId)) });
                
                //learun.layerForm({
                //    id: 'form',
                //    title: '预览',
                //    url: top.$.rootUrl + '/LR_ReportModule/RptManage/Report?reportId=' + encodeURI(encodeURI(reportId)),
                //    width: 1024,
                //    height: 768,
                //    callBack: function (id) {
                //        // return top[id].acceptClick(refreshGirdData);
                //    }
                //});
            });
        },
        show: function (reportId) {
            var viewer = GrapeCity.ActiveReports.Viewer(
                {
                    element: '#viewerContainer',
                    report: {
                        id: "Reports/" + reportId
                    },
                    reportService: {
                        url: top.$.rootUrl + '/WebService1.asmx'
                    },
                    uiType: 'desktop'
                });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_ReportModule/RptManage/GetPageList',
                headData: [
                    { label: "报表编码", name: "F_Code", width: 100, align: "left" },
                    { label: "报表名称", name: "F_Name", width: 100, align: "left" },
                    {
                        label: "报表类型", name: "F_Type", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'ReportSort',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "排序", name: "F_SortCode", width: 100, align: "left" },
                    { label: "报表文件", name: "F_File", width: 100, align: "left" },
                    { label: "备注", name: "F_Description", width: 100, align: "left" },
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
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
