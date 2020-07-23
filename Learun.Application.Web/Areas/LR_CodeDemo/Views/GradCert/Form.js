/* * 创建人：超级管理员
 * 日  期：2020-07-02 23:28
 * 描  述：毕业证书
 */
var acceptClick;
var keyValue = request('keyValue');

var F_PersonId = request('F_PersonId');
var F_UserName = request('F_UserName');
var F_IDCardNo = request('F_IDCardNo');
var ParentDisable = request('ParentDisable');
if (ParentDisable == "true") {
    F_UserName = decodeURIComponent(escape(F_UserName));
}

var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_EducationType').lrDataItemSelect({ code: 'EducationType' });
            $('#F_Term').lrDataItemSelect({ code: 'Term' });
            $('#F_OriginalType').lrDataItemSelect({ code: 'OriginalType' });
            $('#F_CertStatus').lrDataItemSelect({ code: 'CertStatus' });
            $('#F_MajorType').lrDataItemSelect({ code: 'MajorType' });

        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/GradCert/GetFormData?keyValue=' + keyValue, function (data) {
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
            $("#F_PersonId").val(F_PersonId);
            $("#F_UserName").val(F_UserName);
            $("#F_IDCardNo").val(F_IDCardNo);
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData();
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/GradCert/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
