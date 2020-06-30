/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 客户端存储（框架核心部分）
 */
(function ($, learun, fui, window) {
    "use strict";
    learun.storage = {
        get: function (name) {// 获取
            return JSON.parse(localStorage.getItem(name));
        },
        set: function (name, value) {// 设置
            localStorage.setItem(name, JSON.stringify(value));
        }
    };
})(window.jQuery, window.lrmui, window.fui, window);
