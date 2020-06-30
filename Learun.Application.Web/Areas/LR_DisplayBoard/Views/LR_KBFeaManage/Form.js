/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-09-20 10:08
 * 描  述：看板发布
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
            // 上级
            $('#F_ParentId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/Module/GetExpendModuleTree',
                type: 'tree',
                maxHeight: 180,
                allowSearch: true
            });
            // 选择图标
            $('#selectIcon').on('click', function () {
                learun.layerForm({
                    id: 'iconForm',
                    title: '选择图标',
                    url: top.$.rootUrl + '/Utility/Icon',
                    height: 700,
                    width: 1000,
                    btn: null,
                    maxmin: true,
                    end: function () {
                        if (top._learunSelectIcon != '') {
                            $('#F_Icon').val(top._learunSelectIcon);
                        }
                    }
                });
            });
            // 选在表单
            $('#F_KanBanId').lrselect({
                text: 'F_KanBanName',
                value: 'F_Id',
                url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/GetList?queryJson=null',
                allowSearch: true
            });
            $('#lr_preview').on('click', function () {
                var formId = $('#F_KanBanId').lrselectGet();
                if (!!formId) {
                    learun.layerForm({
                        id: 'custmerForm_PreviewForm',
                        title: '预览当前模板',
                        url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/PreviewForm?keyValue=' + formId,
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        btn: null
                    });
                }
                else {
                    learun.alert.warning('请选择看板！');
                }
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_DisplayBoard/LR_KBFeaManage/GetFormData?keyValue=' + keyValue, function (data) {
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
        var postData = $('#form').lrGetFormData();
        $.lrSaveForm(top.$.rootUrl + '/LR_DisplayBoard/LR_KBFeaManage/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
