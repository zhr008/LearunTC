/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2019.01.02
 * 描 述：添加模块3标签项	
 */
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var tabName = top.layer_ModuleForm3.tabName;

    var page = {
        init: function () {
            page.initData();
        },
        initData: function () {
            $('#text').val(tabName);
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData();
        callBack && callBack(postData.text);
        learun.layerClose(window.name);
    };
    page.init();
}