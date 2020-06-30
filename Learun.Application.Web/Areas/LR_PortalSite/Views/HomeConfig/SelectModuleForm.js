/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-01-02 09:35
 * 描  述：模块风格选择
 */
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('.box-content').on('click', function () {//1.公告；2消息列表；3消息列表2； 4图形列表；5详细列表
                var value = $(this).attr('data-value');
                learun.frameTab.currentIframe().addModule(value);

                learun.layerClose(window.name);
            });
        }
    };
    page.init();
}
