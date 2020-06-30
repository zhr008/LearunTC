var parentId = request('parentId');
var keyValue = request('keyValue');

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 上级
            $('#F_PId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/Files/GetFolderTree',
                type: 'tree',
                allowSearch: true,
                maxHeight: 225
            }).lrselectSet(parentId);
        },
        initData: function () {
            if (keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_SystemModule/Files/GetFolderEntity?keyValue=' + keyValue, function (data) {//
                    $('#form').lrSetFormData(data);
                });
                //$('#form').lrSetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData(keyValue);
        if (postData["F_PId"] == '' || postData["F_PId"] == '&nbsp;' || postData["F_PId"] == '-1') {
            postData["F_PId"] = '0';
        }
        else if (postData["F_PId"] == keyValue) {
            learun.alert.error('上级不能是自己本身!');
            return false;
        }
        $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/Files/SaveFolder?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };

    page.init();
}