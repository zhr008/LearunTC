/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2019.01.02
 * 描 述：设置模块5	
 */
var sort = request('sort');
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var keyValue = '';
    var currentModule = learun.frameTab.currentIframe().currentModule;

    var page = {
        init: function () {
            // 比例选择
            $('#prop').lrselect({
                data: [{ id: '0.5', text: '1/2' }, { id: '0.333333', text: '1/3' }, { id: '0.66666', text: '2/3' }, { id: '1', text: '1' }],
                placeholder: false
            }).lrselectSet('1');


            $('#F_Category').lrDataItemSelect({ code: 'PortalSiteType' });
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
            page.initData();
        },
        initData: function () {
            if (currentModule) {
                var schemeObj = JSON.parse(currentModule.F_Scheme);
                currentModule.F_Category = schemeObj.category;
                currentModule.F_Article = schemeObj.article;
                currentModule.prop = schemeObj.prop;
                $('#form').lrSetFormData(currentModule);

                keyValue = currentModule.F_Id;
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();

        var postData = {
            F_Name: formData.F_Name,
            F_Type: 9,
            F_Scheme: JSON.stringify({ category: formData.F_Category, article: formData.F_Article, prop: formData.prop, type: "5" }),
            F_Sort: sort
        }

        $.lrSaveForm(top.$.rootUrl + '/LR_PortalSite/HomeConfig/SaveForm?keyValue=' + keyValue, postData, function (res) {
            postData.F_Id = res.data;
            callBack && callBack(postData);
        });
    };
    page.init();
}