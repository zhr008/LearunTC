/* * 创建人：超级管理员
 * 日  期：2020-07-10 18:11
 * 描  述：项目详情
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
            $('#CertType').lrDataItemSelect({ code: 'CertType' });
            $('#SocialSecurityRequire').lrDataItemSelect({ code: 'SocialSecurityRequire' });
            $('#CertRequire').lrDataItemSelect({ code: 'CertRequire' });
            $('#IDCardRequire').lrDataItemSelect({ code: 'CertRequire' });
            $('#GradCertRequire').lrDataItemSelect({ code: 'CertRequire' });
            $('#SceneRequire').lrDataItemSelect({ code: 'SceneRequire' });
            $('#Status').lrDataItemSelect({ code: 'CurrentCertStatus' });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/ProjectDetail/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#' + id ).jfGridSet('refreshdata', data[id]);
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
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/ProjectDetail/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
