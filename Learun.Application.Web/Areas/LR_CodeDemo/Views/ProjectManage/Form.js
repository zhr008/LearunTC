/* * 创建人：超级管理员
 * 日  期：2020-07-06 17:37
 * 描  述：projectmanage
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
            $('#lr_form_tabs').lrFormTab();
            $('#lr_form_tabs ul li').eq(0).trigger('click');
            $('#ApplicantId').lrDataSourceSelect({ code: 'recruitdata',value: 'f_applicantid',text: 'f_companyname' });
            $('#ProjectType').lrDataItemSelect({ code: 'projecttype' });
            $('#SocialSecurities').lrDataItemSelect({ code: 'SocialSecurities' });
            $('#Rank').lrDataItemSelect({ code: 'rank' });
            $('#Status').lrDataItemSelect({ code: 'projectstatus' });
            $('#tc_ProjectDetail').jfGrid({
                headData: [
                    {
                        label: '证书类型', name: 'CertType', width:100, align: 'left'
                        ,edit:{
                            type:'select',
                            datatype: 'dataItem',
                            code:'CertType'
                        }
                    },
                    {
                        label: '证书专业', name: 'CertMajor', width:100, align: 'left'
                        ,edit:{
                            type:'input'
                        }
                    },
                    {
                        label: '标准数量', name: 'StandardNum', width:100, align: 'left'
                        ,edit:{
                            type:'input'
                        }
                    },
                    {
                        label: '社保要求', name: 'SocialSecurityRequire', width:100, align: 'left'
                        ,edit:{
                            type:'select',
                            datatype: 'dataItem',
                            code:'SocialSecurityRequire'
                        }
                    },
                    {
                        label: '资格证要求', name: 'CertRequire', width:100, align: 'left'
                        ,edit:{
                            type:'select',
                            datatype: 'dataItem',
                            code:'CertRequire'
                        }
                    },
                    {
                        label: '身份证要求', name: 'IDCardRequire', width:100, align: 'left'
                        ,edit:{
                            type:'select',
                            datatype: 'dataItem',
                            code:'CertRequire'
                        }
                    },
                    {
                        label: '毕业证要求', name: 'GradCertRequire', width:100, align: 'left'
                        ,edit:{
                            type:'select',
                            datatype: 'dataItem',
                            code:'CertRequire'
                        }
                    },
                    {
                        label: '到场要求', name: 'SceneRequire', width:100, align: 'left'
                        ,edit:{
                            type:'select',
                            datatype: 'dataItem',
                            code:'SceneRequire'
                        }
                    },
                    {
                        label: '其它要求', name: 'OtherRequire', width:100, align: 'left'
                        ,edit:{
                            type:'input'
                        }
                    },
                    {
                        label: '甲方提供数量', name: 'AlreadyNum', width:100, align: 'left'
                        ,edit:{
                            type:'input'
                        }
                    },
                    {
                        label: '我方配置数量', name: 'NeedNum', width:100, align: 'left'
                        ,edit:{
                            type:'input'
                        }
                    },
                    {
                        label: '配置状态', name: 'Status', width:100, align: 'left'
                        ,edit:{
                            type:'select',
                            datatype: 'dataItem',
                            code:'CurrentCertStatus'
                        }
                    },
                    {
                        label: '配置说明', name: 'F_Description', width:100, align: 'left'
                        ,edit:{
                            type:'input'
                        }
                    },
                ],
                isEdit: true,
                height: 400
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/projectmanage/GetFormData?keyValue=' + keyValue, function (data) {
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
        postData.strEntity = JSON.stringify($('[data-table="tc_Project"]').lrGetFormData());
        postData.strtc_ProjectDetailList = JSON.stringify($('#tc_ProjectDetail').jfGridGet('rowdatas'));
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/projectmanage/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
