var fileInfoId = request('fileInfoId');
var isFolder = request('isFolder');
var keyValue = request('keyValue');

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var auditorName;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_ObjId').lrselect({
                url: top.$.rootUrl + '/LR_OrganizationModule/Role/GetList',
                text: 'F_FullName',
                value: 'F_RoleId',
                select: function (item) {
                    auditorName = item.F_FullName;
                },
                allowSearch: true
            });
            // 1 查看 2上传 3 下载  4 删除  5 复原
            $('#F_AuthType').lrselect({
                text: 'name',
                value: 'id',
                data: [{ id: '1', name: '查看' }, { id: '2', name: '上传' }, { id: '3', name: '下载' }, { id: '4', name: '删除' }, { id: '5', name: '复原' }, { id: '6', name: '彻底删除' }],
                type: 'multiple'
            }).lrselectSet('1');
        },
        initData: function () {
            if (keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_SystemModule/Files/GetAuthEntity?keyValue=' + keyValue, function (data) {//
                    if (data.F_Time == "9999-12-31 00:00:00") {
                        data.F_Time = '';
                    }
                    $('#form').lrSetFormData(data);
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData(keyValue);
        postData.F_ObjType = 2;
        postData.F_ObjName = auditorName;
        postData.F_FileInfoId = fileInfoId;
        postData.F_IsFolder = isFolder;
        $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/Files/SaveAuth?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };

    page.init();
}