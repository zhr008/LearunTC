/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.11.11
 * 描 述：桌面统计配置
 */
var keyValue = '';
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 加载导向
            $('#wizard').wizard().on('change', function (e, data) {
                var $finish = $("#btn_finish");
                var $next = $("#btn_next");
                if (data.direction == "next") {
                    if (data.step == 1) {
                        if (!$('#step-1').lrValidform()) {
                            return false;
                        }
                        $finish.removeAttr('disabled');
                        $next.attr('disabled', 'disabled');
                    }
                } else {
                    $finish.attr('disabled', 'disabled');
                    $next.removeAttr('disabled');
                }
            });
            // 选择图标
            $('#selectIcon').on('click', function () {
                learun.layerForm({
                    id: 'iconForm',
                    title: '选择图标',
                    url: top.$.rootUrl + '/Utility/Icon',
                    height: 700,
                    width: 1000,
                    btn: null,
                    maxmin: true,
                    end: function () {
                        if (top._learunSelectIcon != '') {
                            $('#F_Icon').val(top._learunSelectIcon);
                        }
                    }
                });
            });
            //数据库
            $('#F_DataSourceId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
            });

            //完成事件
            $("#btn_finish").click(function () {
                if (!$('#wizard-steps').lrValidform()) {
                    return false;
                }
                var postData = $('#wizard-steps').lrGetFormData(keyValue);
                
                $.lrSaveForm(top.$.rootUrl + '/LR_Desktop/DTTarget/SaveForm?keyValue=' + keyValue, postData, function (res) {
                    learun.frameTab.currentIframe().refreshGirdData();
                });
            })
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_Id;
                $('#wizard-steps').lrSetFormData(selectedRow);
            }
        }
    };
    page.init();
}


