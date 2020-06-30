/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-05-08 18:30
 * 描  述：甘特图应用
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#LR_OA_ProjectDetail').jfGrid({
                headData: [
                    {
                        label: '项目名称', name: 'F_ItemName', width: 100, align: 'left', edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: '开始时间', name: 'F_StartTime', width: 100, align: 'left'
                        , edit: {
                            type: 'datatime',
                            dateformat: '1'
                        }
                    },
                    {
                        label: '结束时间', name: 'F_EndTime', width: 100, align: 'left'
                        , edit: {
                            type: 'datatime',
                            dateformat: '1'
                        }
                    },
                    {
                        label: '备注', name: 'F_Remark', width: 100, align: 'left'
                        , edit: {
                            type: 'input'
                        }
                    },
                ],
                isEdit: true,
                height: 400
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/GantProject/GetFormData?keyValue=' + keyValue, function (data) {
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
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="LR_OA_Project"]').lrGetFormData());
        postData.strlR_OA_ProjectDetailList = JSON.stringify($('#LR_OA_ProjectDetail').jfGridGet('rowdatas'));
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/GantProject/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
