/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2020 上海力软信息技术有限公司 
 * 创建人：超级管理员 
 * 日  期：2019-05-09 11:38 
 * 描  述：F 
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_Status').lrRadioCheckbox({
                type: 'radio',
                code: 'ProStatus',
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/GantProject/GetGantData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#' + id).jfGridSet('refreshdata', data[id]);
                        }
                        else {
                            $('[data-table="' + id + '"]').lrSetFormData(data[id]);
                        }
                    }
                });
            }
        }
    };
    // 保存数据 
    acceptClick = function (callBack) {
        if (!$('body').lrValidform()) {
            return false;
        }
        var postData = {
            strEntity: JSON.stringify($('body').lrGetFormData())
        };
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/GantProject/SaveGant?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调 
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
} 