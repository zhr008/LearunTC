/* * 创建人：超级管理员
 * 日  期：2020-07-05 19:31
 * 描  述：从业经历
 */
var acceptClick;
var keyValue = request('keyValue');

var F_PersonId = request('F_PersonId');
var F_UserName = request('F_UserName');
var F_IDCardNo = request('F_IDCardNo');

var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_VocationType').lrDataItemSelect({ code: 'VocationType' });
            $('#F_CertType').lrDataItemSelect({ code: 'CertType' });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/WorkExperience/GetFormData?keyValue=' + keyValue, function (data) {
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
            $("#F_PersonId").val(F_PersonId);
            $("#F_UserName").val(decodeURIComponent(escape(F_UserName)));
            $("#F_IDCardNo").val(F_IDCardNo);
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
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/WorkExperience/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
