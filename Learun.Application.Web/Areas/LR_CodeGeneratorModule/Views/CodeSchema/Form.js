/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-03-01 11:09
 * 描  述：代码模板
 */
var acceptClick;
var keyValue = request('keyValue');
var f_type = request('F_Type');
var schemadata = top.layer_CustmerCodeIndex.postData;


var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            $('.lr-form-wrap').lrscroll();
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_Catalog').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailTree',
                param: { itemCode: 'CodeSchemaType' },
                type: 'tree',
                maxHeight: 180,
                allowSearch: true,
                value:'value'
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeGeneratorModule/CodeSchema/GetFormData?keyValue=' + keyValue, function (data) {
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
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').lrValidform()) {
            return false;
        }
        var codeData = $('body').lrGetFormData();
        codeData['F_Type'] = f_type;
        codeData['F_CodeSchema'] = JSON.stringify(schemadata);
        var postData = {
            strEntity: JSON.stringify(codeData)
        };
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeGeneratorModule/CodeSchema/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
