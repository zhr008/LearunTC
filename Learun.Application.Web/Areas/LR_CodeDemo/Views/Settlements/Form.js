/* * 创建人：超级管理员
 * 日  期：2020-07-05 20:42
 * 描  述：合同结算
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
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
            page.buildTree();
        },
        bind: function () {
            $('#F_ContractStatus').lrDataItemSelect({ code: 'ContractStatus' });
            $('#F_ApplicantId').lrDataSourceSelect({ code: 'applicant',value: 'f_applicantid',text: 'f_companyname' });
            $('#F_PayStatus').lrDataItemSelect({ code: 'PayStatus' });




            $('#F_PersonAmount').on('input', function () {
                var PersonAmount=  $("#F_PersonAmount").val();
                var ApplicantAmount = $("#F_ApplicantAmount").val();
                $('#F_ContractAmount').val(learun.accAdd(PersonAmount, ApplicantAmount))
            });
            $('#F_ApplicantAmount').on('input', function () {
                var PersonAmount = $("#F_PersonAmount").val();
                var ApplicantAmount = $("#F_ApplicantAmount").val();
                $('#F_ContractAmount').val(learun.accAdd(PersonAmount, ApplicantAmount))
            });

        },

        buildTree: function () {
            $('#F_ApplicantId').lrDataSourceSelect({ code: 'applicantdata', value: 'f_applicantid', text: 'f_companyname' });
            //$('#F_ApplicantId').lrselect({
            //    url: top.$.rootUrl + '/LR_CodeDemo/Settlements/GetApplicantRepresentative?PersonId=' + F_PersonId,
            //    select: function (item) {
            //        symbolName = item.text;
            //    }
            //});

            //$('#F_ApplicantId').lrselect({
            //    url: top.$.rootUrl + '/LR_CodeDemo/Settlements/GetApplicantRepresentative?PersonId=' + F_PersonId,
            //    maxHeight: 230,
            //    value: "F_ApplicantId",
            //    text: "F_CompanyName",
            //    select: function (item) {
                   
            //    }
            //});


            //$('#F_ModuleId').lrselect({
            //    url: top.$.rootUrl + '/LR_SystemModule/Module/GetModuleTree',
            //    type: 'tree',
            //    maxHeight: 250,
            //    allowSearch: true
            //}).on('change', function () {
            //    moduleId = $(this).lrselectGet();
            //    var module = learun.clientdata.get(['modulesMap', moduleId]);
            //    $('#F_ModuleUrl').val(module.F_UrlAddress);
            //});

        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/Settlements/GetFormData?keyValue=' + keyValue, function (data) {
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
            $("#F_UserName").val(F_UserName);
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
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/Settlements/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
