/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-01-02 09:35
 * 描  述：页面风格选择
 */
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('.box-content').on('click', function () {//1.列表2图形列表3详细信息 
                var value = $(this).attr('data-value');
                learun.frameTab.open({ F_ModuleId: learun.newGuid(), F_Icon: 'fa fa-file-text-o', F_FullName: '新增门户页面', F_UrlAddress: '/LR_PortalSite/Page/Form?type=' + value });
                learun.layerClose(window.name);
            });
        }
    };
    page.init();
}
