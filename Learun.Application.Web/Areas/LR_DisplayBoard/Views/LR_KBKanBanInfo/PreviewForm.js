/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-09-20 10:10
 * 描  述：看板信息
 */
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var containerHeight = 0;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {            
            $("#refresh").on('click', function () {
                location.reload();
            });
        },
        initData: function () {
            //编辑看板信息
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/GetFormData?keyValue=' + keyValue, function (data) {
                    if (data.baseinfo.F_KanBanName) {
                        $('.top-title').text(data.baseinfo.F_KanBanName);
                    }
                    displayBoard.init(data.configinfo);
                    $.lrDisplayBoardComponents.load($(".content"),true, data.baseinfo.F_RefreshTime);
                });
            }
        }
    };


    var displayBoard = {
        init: function (configData, time) {
            var $container = $(".content");
            $container.hide();
            $container.html("");
            $.each(configData || [], function (_Index, _Item) {
                if (_Item && _Item.F_Type) {
                    var _fn = $.lrDisplayBoardComponents[_Item.F_Type];
                    if (_fn) {
                        $container.append(_fn.init(_Item));
                        var _height = _Item._height + _Item._top;
                        if (containerHeight < _height) {
                            containerHeight = _height;
                        }
                    }
                }
            });
            if (containerHeight > 0) {
                $container.height(containerHeight);
            }
            $container.show();
            $container = null;
        }
    }
    page.init();
}
