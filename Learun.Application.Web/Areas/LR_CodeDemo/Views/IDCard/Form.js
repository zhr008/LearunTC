/* * 创建人：超级管理员
 * 日  期：2020-06-29 21:15
 * 描  述：身份证管理
 */
var acceptClick;
debugger;
var keyValue = request('keyValue');


console.log(keyValue);
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_SafeguardType').lrDataItemSelect({ code: 'SafeguardType' });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/IDCard/GetFormData?keyValue=' + keyValue, function (data) {
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
        var postData = {};
        postData.strtc_IDCardEntity = JSON.stringify($('[data-table="tc_IDCard"]').lrGetFormData());
        postData.strEntity = JSON.stringify($('[data-table="tc_Personnels"]').lrGetFormData());
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/IDCard/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
