/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 ajax请求方法
 */
(function ($, learun, fui, window) {
    "use strict";

    learun.http = {
        post: function (url, param, callback, dataType) {
            return learun.http.async('POST', url, param, callback, dataType);
        },
        get: function (url, param, callback, dataType) {
            return learun.http.async('GET', url, param, callback, dataType);
        },
        webget: function (url, param, callback, dataType) {
            url = encodeURI(url);
            return $.ajax({
                url: url,
                data: param,
                type: 'GET',
                dataType: dataType || "json",
                async: true,
                cache: false,
                success: function (res) {
                    callback(res);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    callback(null);
                },
                beforeSend: function () {
                },
                complete: function () {
                }
            });
        },
        async: function (type, url, param, callback, dataType) {
            url = encodeURI(url);
			return $.ajax({
				url: url,
				data: param,
				type: type,
				dataType: dataType || "json",
				async: true,
				cache: false,
				success: function (res) {
					callback(res);
				},
				error: function (XMLHttpRequest, textStatus, errorThrown) {
					callback(null);
				},
				beforeSend: function () {
				},
				complete: function () {
				}
			});
        }
    };

})(window.jQuery, window.lrmui, window.fui, window);