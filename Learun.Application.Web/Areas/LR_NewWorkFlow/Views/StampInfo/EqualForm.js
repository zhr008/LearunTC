/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-11-15
 * 描  述：密码验证
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {

        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData();
        var F_Password = $.md5(postData.F_Password);
        learun.postForm(top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/EqualForm', { keyValue: keyValue, Password: F_Password }, function (res) {
            // 正确之后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
