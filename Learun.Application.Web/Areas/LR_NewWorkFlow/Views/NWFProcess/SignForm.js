/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.12.09
 * 描 述：签字盖章
 */
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var stampUrl = '';
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            var $sigdiv = $("#signature").jSignature({ 'UndoButton': false, height:'100%',width:'100%' });
            console.log($sigdiv.jSignature("getSettings")); 

            $('#btn_reset').on('click', function () {
                $sigdiv.jSignature("reset");
                $('#stamp').hide();
                stampUrl = '';
            });
            $('#btn_stamp').on('click', function () {
                learun.layerForm({
                    id: 'StampDetailIndex',
                    title: '印章列表',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/StampDetailIndex',
                    width: 600,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(function (imgutl) {
                            $('#stamp').find('img').attr('src', top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/GetImg?keyValue=' + imgutl);
                            $('#stamp').show();
                            stampUrl = imgutl;
                            console.log(stampUrl);
                        });
                    }
                });
            });

            $('#btn_finish').on('click', function () {
                var datapair = $sigdiv.jSignature("getData");
                top.flowAuditfn(datapair,stampUrl);
                learun.layerClose(window.name);
            });

        }
    };
    // 保存数据
    acceptClick = function () {
    };
    page.init();
}