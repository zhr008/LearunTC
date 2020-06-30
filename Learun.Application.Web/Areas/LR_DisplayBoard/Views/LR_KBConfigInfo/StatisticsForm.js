/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-09-20 09:41
 * 描  述：统计指标界面
 */
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var selectedModel = top.layer_form.selectedModel;

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            ///显示比例
            $('#F_WidthValue').lrselect();
            $('#F_LeftValue').lrselect();
        },
        initData: function () {
            if (selectedModel) {
                $('#form').lrSetFormData(selectedModel);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();
        formData.F_Configuration = '';
        formData.F_Type = 'statistics';
        formData.F_HightValue = '257';
        if (selectedModel) {
            selectedModel.F_ModeName = formData.F_ModeName;
            selectedModel.F_TopValue = formData.F_TopValue;
            selectedModel.F_LeftValue = formData.F_LeftValue;
            selectedModel.F_HightValue = formData.F_HightValue;
            selectedModel.F_WidthValue = formData.F_WidthValue;
            selectedModel.F_RefreshTime = formData.F_RefreshTime;
        }
        
        callBack(formData);
        return true;
    };
    page.init();
}
