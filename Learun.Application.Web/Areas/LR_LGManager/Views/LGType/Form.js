/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-04-10 15:08
 * 描  述：语言类型
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.initData();
        },
        bind: function () {
        },
        initData: function () {
            if (!!selectedRow) {
                $('#form').lrSetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData();
        postData.F_Code = postData.F_Code.toLowerCase();
        postData.F_IsMain = 0;
        postData['__RequestVerificationToken'] = $.lrToken;
        learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGType/GetEntityByCode?keyValue=' + postData.F_Code, function (res) {
            //判断编辑是否更改编码
            if (res.data) {
                learun.alert.warning("编码已存在");
                return false;
            }
            else {
                if (!(selectedRow && selectedRow.F_Code === postData.F_Code) && keyValue) {
                    selectedRow.F_Code = postData.F_Code;
                    selectedRow.F_Name = postData.F_Name;
                    $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/LGType/SaveForm?keyValue=' + keyValue, selectedRow, function (res) {
                        // 保存成功后才回调
                        if (!!callBack) {
                            callBack();
                        }
                    });
                }
                else if (!keyValue) {
                    $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/LGType/SaveForm?keyValue=' + keyValue, postData, function (res) {
                        // 保存成功后才回调
                        if (!!callBack) {
                            callBack();
                        }
                    });
                }
            }
        });
    };
    page.init();
}
