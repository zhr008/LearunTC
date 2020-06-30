/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-08-20 15:04
 * 描  述：其他页面配置
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
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
                learun.layerForm({
                    id: 'formadd',
                    title: '新增页面-选择风格',
                    url: top.$.rootUrl + '/LR_PortalSite/Page/SelectForm',
                    width: 800,
                    height: 350,
                    btn:null
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var type = $('#gridtable').jfGridValue('F_Type');
                if (learun.checkrow(keyValue)) {
                    learun.frameTab.open({ F_ModuleId: keyValue, F_Icon: 'fa fa-file-text-o', F_FullName: '编辑门户页面', F_UrlAddress: '/LR_PortalSite/Page/Form?type=' + type + "&keyValue=" + keyValue });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_PortalSite/Page/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            })
        },
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_PortalSite/Page/GetPageList',
                headData: [
                        { label: '标题', name: 'F_Title', width: 200, align: "left" },
                        {
                            label: '风格', name: 'F_Type', width: 80, align: "center",
                            formatter(value, row, op, $cell) {
                                if (value == 1) {
                                    return '<img src="' + top.$.rootUrl + '/Content/images/plhome/newpage2.png"  style="position: absolute;height:60px;width:70px;top:5px;left:5px;" >';
                                }
                                else if (value == 2) {
                                    return '<img src="' + top.$.rootUrl + '/Content/images/plhome/product2.png"  style="position: absolute;height:60px;width:70px;top:5px;left:5px;" >';
                                } else {
                                    return '<img src="' + top.$.rootUrl + '/Content/images/plhome/aboutpage2.png"  style="position: absolute;height:60px;width:70px;top:5px;left:5px;" >';
                                }
                            }
                        },
                        {
                            label: '发布时间', name: 'F_CreateDate', width: 100, align: "center",
                            formatter: function (cellvalue) {
                                return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                            }
                        },
                        { label: "创建人", name: "F_CreateUserName", width: 100, align: "left" },
                ],
                mainId: 'F_Id',
                isPage: true,
                rowHeight: 70,
                sidx: 'F_CreateDate DESC'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
}
