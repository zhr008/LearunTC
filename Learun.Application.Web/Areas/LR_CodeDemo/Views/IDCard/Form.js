/* * 创建人：超级管理员
 * 日  期：2020-06-29 21:15
 * 描  述：身份证管理
 */
var acceptClick;
var keyValue = request('keyValue');

var F_PersonId = request('F_PersonId');
debugger
var F_UserName = request('F_UserName');
var F_IDCardNo = request('F_IDCardNo');

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
            $("#F_PersonId").val(F_PersonId);
            $("#F_UserName").val(decodeURIComponent(escape(F_UserName)));
            $("#F_IDCardNo").val(F_IDCardNo);
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData();
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/IDCard/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
