/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：
 * 日 期：2018.11.10
 * 描 述：电子签章
 */
var selectedRow;
var refreshGirdData;
var keyValue;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('.add').on('click', function () {
                learun.layerForm({
                    id: 'StampDetailIndex',
                    title: '印章列表',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/StampDetailIndex',
                    width: 1050,
                    height: 600,
                    callBack: function (id) {
                        return top[id].acceptClick(function (imgutl) {
                            keyValue = imgutl;
                            $(".price-box").lrSign({//电子签章调用插件
                                img: top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/GetImg?keyValue=' + imgutl
                            });
                        });
                    }
                });
            });
            $(".price-box").on("click", ".sure", function () {
                learun.layerForm({
                    id: 'EqualForm',
                    title: '密码验证',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/EqualForm?keyValue=' + keyValue,
                    width: 360,
                    height: 140,
                    callBack: function (id) {
                        return top[id].acceptClick(function () {
                            $("div.sign").addClass('ok').off('mousedown').find('.btn').remove();
                           
                        });
                    }
                });
            });
            $('.print').on('click', function () {//电子签章打印
                $('.price-box').jqprint();
            });
        }
    }
    page.init();
}