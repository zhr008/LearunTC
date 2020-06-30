/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-10-16 16:24
 * 描  述：消息策略
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_SendRole').lrselect({
                // 字段
                value: "F_RoleId",
                text: "F_FullName",
                type: 'multiple',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/LR_OrganizationModule/Role/GetRoleList',
                // 访问数据接口参数
                param: { parentId: '' },
            });
            ///消息类型
            $('#F_MessageType').lrselect({
                type: 'multiple',
                value: "id",
                text: "text",
                data: [{ id: '1', text: '邮件' }, { id: '2', text: '微信' }, { id: '3', text: '短信' }, { id: '4', text: '系统IM' }]//1邮箱2微信3短信4系统IM
            });

            $('#F_StrategyCode').on('blur', function () {
                $.lrExistField(keyValue, 'F_StrategyCode', top.$.rootUrl + '/LR_Message/LR_StrategyInfo/ExistStrategyCode');
            });
        },
        initData: function () {
            if (!!selectedRow) {
                $('#form').lrSetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData();
        $.lrSaveForm(top.$.rootUrl + '/LR_Message/LR_StrategyInfo/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
