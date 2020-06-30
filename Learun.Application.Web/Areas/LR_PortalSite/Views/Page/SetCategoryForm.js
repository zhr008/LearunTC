/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2019.01.02
 * 描 述：设置分类项	
 */
var type = request('type');
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var categoryData = learun.frameTab.currentIframe().categoryData;

    var page = {
        init: function () {
            $('#F_Category').lrDataItemSelect({ code: 'PortalSiteType' });
            if (type == '3') {
                $('#F_Article').lrselect({
                    text: 'F_Title',
                    value: 'F_Id',
                    allowSearch: true
                });
                $('#F_Category').on('change', function () {
                    var v = $(this).lrselectGet();
                    if (v != '') {
                        $('#F_Article').lrselectRefresh({
                            data: [],
                            url: top.$.rootUrl + '/LR_PortalSite/Article/GetList',
                            param: { queryJson: JSON.stringify({ category: v }) }
                        });
                    }
                    else {
                        $('#F_Article').lrselectRefresh({
                            data: [],
                            url: false
                        });
                    }
                });
            }
            else {
                $('#F_Article').parent().remove();
            }
            page.initData();
        },
        initData: function () {
            if (categoryData) {
                $('#F_Name').val(categoryData.name);
                // 分类信息设置
                $('#F_Category').lrselectSet(categoryData.category);
                if (type == '3') {
                    $('#F_Article').lrselectSet(categoryData.articleId);
                }
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData();
        callBack(postData);
        learun.layerClose(window.name);
    };
    page.init();
}