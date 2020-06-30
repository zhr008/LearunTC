/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.06.05
 * 描 述：写邮件	
 */
var keyValue = request('keyValue');

var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            var userlist = [];
            var userlist1 = [];
            var userlist2 = [];
            learun.clientdata.getAllAsync('department', {
                callback: function (departmentData) {
                    learun.clientdata.getAllAsync('user', {
                        callback: function (data) {
                            $.each(data, function (_id, _item) {
                                var _text = _item.name;
                                departmentData[_item.departmentId] &&(_text = departmentData[_item.departmentId].name + ":" + _text);
                                if (_id != 'System') {
                                    var point = {
                                        id: _id,
                                        text: _text
                                    };
                                    userlist.push(point);
                                    userlist1.push(point);
                                    userlist2.push(point);
                                }
                            });

                            $('#addresssIds').lrselect({
                                type: 'multiple',
                                data: userlist,
                                allowSearch: true
                            });
                            $('#copysendIds').lrselect({
                                type: 'multiple',
                                data: userlist1,
                                allowSearch: true
                            });
                            $('#bccsendIds').lrselect({
                                type: 'multiple',
                                data: userlist2,
                                allowSearch: true
                            });
                        }
                    });
                }
            });
            //内容编辑器
            var ue = UE.getEditor('F_EmailContent');
            $('#F_EmailContent')[0].ue = ue;

            $('#F_Files').lrUploader();

            // 保存草稿
            $('#btn_draft').on('click', function () {
                if (!$('#form').lrValidform()) {
                    return false;
                }
                var postData = {};
                var formData = $('#form').lrGetFormData(keyValue);
                var content = {
                    F_Theme: formData.F_Theme,
                    F_EmailContent: formData.F_EmailContent,
                    F_Files: formData.F_Files,
                    F_EmailType: 1,
                    F_SendState:0
                };
                postData = {
                    keyValue: keyValue,
                    content: JSON.stringify(content),
                    addresssIds: formData.addresssIds,
                    copysendIds: formData.copysendIds,
                    bccsendIds: formData.bccsendIds
                };
                $.lrSaveForm(top.$.rootUrl + '/LR_OAModule/Email/SaveForm', postData, function (res) {
                    // 保存成功后才回调
                    learun.frameTab.currentIframe().refreshGirdData();
                });
            });
            // 发送邮件
            $('#btn_send').on('click', function () {
                if (!$('#form').lrValidform()) {
                    return false;
                }
                var postData = {};
                var formData = $('#form').lrGetFormData(keyValue);
                var content = {
                    F_Theme: formData.F_Theme,
                    F_EmailContent: formData.F_EmailContent,
                    F_Files: formData.F_Files,
                    F_EmailType: 1,
                    F_SendState: 1
                };
                postData = {
                    keyValue: keyValue,
                    content: JSON.stringify(content),
                    addresssIds: formData.addresssIds,
                    copysendIds: formData.copysendIds,
                    bccsendIds: formData.bccsendIds
                };
                $.lrSaveForm(top.$.rootUrl + '/LR_OAModule/Email/SaveForm', postData, function (res) {
                    // 保存成功后才回调
                    learun.frameTab.currentIframe().refreshGirdData();
                });
            });
        },
        initData: function () {
            if (!!keyValue) {
                var selecedRow = learun.frameTab.currentIframe().selectedRow;
                var formData = {
                    F_Theme: selecedRow.F_Theme,
                    F_Files: selecedRow.F_Files,
                    F_EmailContent: selecedRow.F_EmailContent,
                    addresssIds: selecedRow.F_AddresssHtml,
                    copysendIds: selecedRow.F_CopysendHtml,
                    bccsendIds: selecedRow.F_BccsendHtml
                }
                console.log(formData);
                $('#form').lrSetFormData(formData);
            }
        }
    };
    page.init();
}