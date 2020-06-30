/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-06-10 17:21
 * 描  述：工单管理
 */
var acceptClick;
var keyValue = request('keyValue');
// 设置权限
var setAuthorize;
// 设置表单数据
var setFormData;
// 验证数据是否填写完整
var validForm;
// 保存数据
var save;
var bootstrap = function ($, learun) {
    "use strict";
    // 设置权限
    setAuthorize = function (data) {
                            if(!!data)
                            {
                                for (var field in data) {
                                    if (data[field].isLook != 1) {// 如果没有查看权限就直接移除
                                        $('#' + data[field].fieldId).parent().remove();
                                    }
                                    else {
                                        if (data[field].isEdit != 1) {
                                            $('#' + data[field].fieldId).attr('disabled', 'disabled');
                                            if ($('#' + data[field].fieldId).hasClass('lrUploader-wrap')) {
                                                $('#' + data[field].fieldId).css({ 'padding-right': '58px' });
                                                $('#' + data[field].fieldId).find('.btn-success').remove();
                                            }
                                        }
                                    }
                                }
                                }
                            };    
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_DepartmentId').lrselect({
                type: 'tree',
                allowSearch: true,
                url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',
                param: {} 
            });
            $('#F_Process').lrDataItemSelect({ code: 'Process' });
            $('#F_Status').lrDataItemSelect({ code: 'HaveOrNot' });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/WorkOrder/GetFormData?keyValue=' + keyValue, function (data) {
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
    // 设置表单数据
    setFormData = function (processId,param,callback) {
        if (!!processId) {
            $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/WorkOrder/GetFormDataByProcessId?processId=' + processId, function (data) {
                for (var id in data) {
                    if (!!data[id] && data[id].length > 0) {
                            $('#' + id ).jfGridSet('refreshdata', data[id]);
                        }
                        else {
                        if(id == 'LR_Demo_WorkOrder' && data[id] ){
                            keyValue = data[id].F_Id;
                        }
                            $('[data-table="' + id + '"]').lrSetFormData(data[id]);
                        }
                    }
                });
            }
                callback && callback();        }
    // 验证数据是否填写完整
    validForm = function () {
        if (!$('body').lrValidform()) {
            return false;
        }
        return true;
    };
    // 保存数据
    save = function (processId, callBack, i) {

        learun.layerConfirm("注：您确定要保存吗？", function (r) {
            if (r) {
                var formData = $('body').lrGetFormData();
                if (!!processId) {
                    formData.F_Id = processId;
                }
                var postData = {
                    strEntity: JSON.stringify(formData)
                };
                $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/WorkOrder/SaveForm?keyValue=' + keyValue, postData, function (res) {
                    // 保存成功后才回调
                    if (!!callBack) {
                        callBack(res, i);
                    }
                });
                alert('同意退出');
            }
            else {

                alert('不同意');
            }
            
        });

    };
    page.init();
}
