/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.05.09
 * 描 述：个人中心-语言设置	
 */
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('#Setting').lrselect({
                url: top.$.rootUrl + '/LR_LGManager/LGType/GetList',
                value: 'F_Code',
                text: 'F_Name',
                title: 'F_Name'
            });
            var code = top.$.cookie('Learn_ADMS_V7_Language') || learun.language.getMainCode();
            $('#Setting').lrselectSet(code);

            $('#lr_save_btn').on('click', function () {
                var formData = $('#form').lrGetFormData();
                top.$.cookie('Learn_ADMS_V7_Language', formData.Setting, { path: "/" });

                learun.alert.success('设置成功');

            });
        }
    };
    page.init();
}