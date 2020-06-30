/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-01-03 09:35
 * 描  述：设置模块1
 */
var sort = request('sort');
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var keyValue = '';
    var categoryId = '';
    var currentList = [];
    var currentMap = {};

    var currentModule = learun.frameTab.currentIframe().currentModule;

    var page = {
        init: function () {
            page.bind();
            page.initData();
            page.search();
        },
        bind: function () {
            // 页面选择
            $('#F_Url').lrselect({
                text: 'F_Title',
                value: 'F_Id',
                url: top.$.rootUrl + '/LR_PortalSite/Page/GetList',
                allowSearch: true
            });

            $('#F_Category').lrDataItemSelect({ code: 'PortalSiteType' });
            $('#F_Category').on('change', function () {
                var v = $(this).lrselectGet();
                categoryId = v;
                page.search();
            });
            $('#select_grid').jfGrid({
                url: top.$.rootUrl + '/LR_PortalSite/Article/GetPageList',
                headData: [
                    { label: '标题', name: 'F_Title', width: 330, align: "left" },
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
                    }
                ],
                mainId: 'F_Id',
                isPage: true,
                isMultiselect: true,
                multiselectfield: 'isCheck',
                sidx: 'F_PushDate desc',
                onRenderBefore: function (datas) {
                    $.each(datas, function (_index, _item) {
                        if (currentMap[_item.F_Id]) {
                            _item.isCheck = 1;
                        }
                    });
                },
                onSelectRow: function (row, isCheck) {
                    if (isCheck) {
                        var _row = { F_Id: row.F_Id, F_Title: row.F_Title, F_Category: row.F_Category, F_PushDate: row.F_PushDate };
                        $('#selected_grid').jfGridSet('addRow', _row);
                        currentMap[row.F_Id] = true;

                    } else {
                        $('#selected_grid').jfGridSet('removeRow', row.F_Id);
                        currentMap[row.F_Id] = false;
                    }
                }
            });

            $('#selected_grid').jfGrid({
                headData: [
                    {
                        label: "", name: "btn", width: 60, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                $('#selected_grid').jfGridSet('removeRow', row.F_Id);
                                $('#select_grid').jfGridSet('nocheck', row.F_Id);
                                currentMap[row.F_Id] = false;
                                return false;
                            });
                            return '<span class=\"label label-danger \" style=\"cursor: pointer;\">移除</span>';
                        }
                    },
                    { label: '标题', name: 'F_Title', width: 300, align: "left" },
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
                    }
                ],
                mainId: 'F_Id'
            });

            // 查询
            $('#btn_Search').on('click', function () {
                var queryJson = {};
                var keyword = $('#txt_Keyword').val();
                queryJson.F_Category = categoryId;
                queryJson.F_Title = keyword;
                $('#select_grid').jfGridSet('reload', { queryJson: JSON.stringify(queryJson) });
            });
        },
        search: function (param) {
            param = param || {};
            param.F_Category = categoryId;
            $('#select_grid').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        },
        initData: function () {
            if (currentModule) {
                $('#form1').lrSetFormData(currentModule);

                currentList = JSON.parse(currentModule.F_Scheme).list;

                $.each(currentList, function (_index, _item) {
                    currentMap[_item.F_Id] = true;
                });
                $('#selected_grid').jfGridSet('refreshdata', currentList);

                keyValue = currentModule.F_Id;
            }
            else {
                $('#selected_grid').jfGridSet('refreshdata', currentList);
            }
        }
    };

    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form1').lrValidform()) {
            return false;
        }
        var formData = $('#form1').lrGetFormData(keyValue);

        var postData = {
            F_Name: formData.F_Name,
            F_Type: 9,
            F_UrlType: 1,
            F_Url: formData.F_Url,
            F_Scheme: JSON.stringify({ list: currentList || [], type: "1" }),
            F_Sort: sort
        }

        $.lrSaveForm(top.$.rootUrl + '/LR_PortalSite/HomeConfig/SaveForm?keyValue=' + keyValue, postData, function (res) {
            postData.F_Id = res.data;
            callBack && callBack(postData);
        });
    };
    page.init();
}
