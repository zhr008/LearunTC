/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-09-05 09:35
 * 描  述：详细信息维护
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";

    var dateBegin = '';
    var dateEnd = '';
    var categoryId = '';

    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            $('#datesearch').lrdate({
                dfdata: [
                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                selectfn: function (begin, end) {
                    dateBegin = begin;
                    dateEnd = end;
                    page.search();
                }
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var queryJson = {};
                var keyword = $('#txt_Keyword').val();
                queryJson.dateBegin = dateBegin;
                queryJson.dateEnd = dateEnd;
                queryJson.F_Category = categoryId;
                queryJson.F_Title = keyword;
                $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(queryJson) });
            });
            $('#F_Category').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailTree',
                param: { itemCode: 'PortalSiteType' },
                nodeClick: page.treeNodeClick
            });
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.frameTab.open({ F_ModuleId: 'PL_ArticleInfo_List_add', F_Icon: 'fa fa-file-text-o', F_FullName: '新增文章信息', F_UrlAddress: '/LR_PortalSite/Article/Form?categoryId=' + categoryId });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    if (keyValue.split(',').length > 1) {
                        learun.alert.warning("编辑只能选择一项");
                    }
                    else {
                        learun.frameTab.open({ F_ModuleId: 'PL_ArticleInfo_List_add', F_Icon: 'fa fa-file-text-o', F_FullName: '编辑文章信息', F_UrlAddress: '/LR_PortalSite/Article/Form?keyValue=' + keyValue });
                    }
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除所有选择项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_PortalSite/Article/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            /*分类管理*/
            $('#lr_category').on('click', function () {
                learun.layerForm({
                    id: 'ClassifyIndex',
                    title: '分类管理',
                    url: top.$.rootUrl + '/LR_SystemModule/DataItem/DetailIndex?itemCode=PortalSiteType',
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
        treeNodeClick: function (item) {
            categoryId = item.value;
            page.search();
        },
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_PortalSite/Article/GetPageList',
                headData: [
                    { label: '标题', name: 'F_Title', width: 200, align: "left" },
                    {
                        label: "分类", name: "F_Category", width: 150, align: "center",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'PortalSiteType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: '发布时间', name: 'F_PushDate', width: 100, align: "center",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "创建人", name: "F_CreateUserName", width: 100, align: "center" },

                ],
                mainId: 'F_Id',
                isPage: true,
                isMultiselect: true,
                sidx: 'F_PushDate desc'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.dateBegin = dateBegin;
            param.dateEnd = dateEnd;
            param.F_Category = categoryId;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
