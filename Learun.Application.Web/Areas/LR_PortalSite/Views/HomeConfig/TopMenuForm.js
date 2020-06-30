/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2019.01.02
 * 描 述：设置分类项	
 */
var parentId = request('parentId');
var acceptClick;
var keyValue = "";
     
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = top.layer_TopMenuIndex.selectedRow;
    var flag = "1";
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 上级菜单
            $('#F_ParentId').lrselect({
                url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/GetTree',
                type: 'tree',
                maxHeight: 180,
                allowSearch: true,
                select: function (item) {
                    if (item) {
                        if (item.parentId == "0") {
                            flag = "1";
                        }
                        else {
                            flag = "2";
                        }
                    }
                }
            }).lrselectSet(parentId);
            // 页面选择
            $('#F_Page').lrselect({
                text: 'F_Title',
                value: 'F_Id',
                url: top.$.rootUrl + '/LR_PortalSite/Page/GetList',
                allowSearch: true
            });

            $('[name="F_UrlType"]').on('click', function () {
                var v = $(this).val();
                if (v == '1') {
                    $('#F_Url').parent().hide();
                    $('#F_Page').parent().show();
                }
                else {
                    $('#F_Page').parent().hide();
                    $('#F_Url').parent().show();
                }
            });

        },
        initData: function () {
            if (selectedRow) {
                keyValue = selectedRow.F_Id || '';
                if (selectedRow.F_UrlType == '1') {
                    selectedRow.F_Page = selectedRow.F_Url;
                }

                $('#form').lrSetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData(keyValue);
        if (postData.F_ParentId && postData.F_ParentId != '&nbsp;') {
            postData.F_Img = flag;
        }
        else {
            postData.F_Img = "0";
            postData.F_ParentId = "0";

            if (!keyValue && top.layer_TopMenuIndex.topMenuList["0"] && top.layer_TopMenuIndex.topMenuList["0"].length >= 5) {
                learun.alert.warning("一级菜单最多维护5个！");
                return false;
            }

        }
        if (postData.F_UrlType == '1') {
            postData.F_Url = postData.F_Page;
        }
        postData.F_Type = 6;
        $.lrSaveForm(top.$.rootUrl + '/LR_PortalSite/HomeConfig/SaveForm?keyValue=' + keyValue, postData, function (res) {
            callBack && callBack();
        });


    };
    page.init();
}