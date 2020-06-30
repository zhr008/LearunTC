/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-04-10 15:00
 * 描  述：语言映照
 */
var acceptClick;
var mainType;
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            var data = [];
            //获取语言类型
            learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGType/GetList', function (res) {
                if (res.data) {
                    for (var i = 0; i < res.data.length; i++) {
                        var html = '<div class="col-xs-12 lr-form-item"> <div class="lr-form-item-title">' + res.data[i].F_Name + '<font face="宋体">*</font></div> <input id="' + res.data[i].F_Code.toLowerCase() + '" type="text" class="form-control" isvalid="yes" checkexpession="NotNull" /> </div>';
                        data.push(html);
                    }
                    mainType = res.data[0].F_Code.toLowerCase();
                    //根据类型添加表单
                    $('#form .lr-form-item:last').parent().append(data);
                    $('#form .lr-form-item:first').remove();
                }
                if (!!selectedRow) {
                    $('#form').lrSetFormData(selectedRow);
                }
            });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var nameList = "";
        var code = "";
        if (!!selectedRow) {
            //原始值
            code = selectedRow["f_code"];
            delete selectedRow.f_code;
            nameList = JSON.stringify(selectedRow);
        }
        //表单值
        var formData = $('#form').lrGetFormData();
        var newNameList = JSON.stringify(formData);
        if (!keyValue) {
            //判断主语言是否重复
            learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGMap/GetListByNameAndType?keyValue=' + formData[mainType] + '&typeCode=' + mainType, function (res) {
                if (res.data.length != 0) {
                    learun.alert.warning("主语言项不能重复！");
                    return false;
                }
                else {
                    $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/LGMap/SaveForm?nameList=' + nameList + "&newNameList=" + newNameList + "&code=" + code, {}, function (res) {
                        // 保存成功后才回调
                        if (!!callBack) {
                            callBack();
                        }
                    });
                }
            });
        }
        else {
            if (formData[mainType] != selectedRow[mainType]) {
                //判断主语言是否重复
                learun.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGMap/GetListByNameAndType?keyValue=' + formData[mainType] + '&typeCode=' + mainType, function (res) {
                    if (res.data.length != 0) {
                        learun.alert.warning("主语言项不能重复！");
                        return false;
                    }
                    else {
                        $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/LGMap/SaveForm?nameList=' + nameList + "&newNameList=" + newNameList + "&code=" + code, {}, function (res) {
                            // 保存成功后才回调
                            if (!!callBack) {
                                callBack();
                            }
                        });
                    }
                });
            }
            else {
                $.lrSaveForm(top.$.rootUrl + '/LR_LGManager/LGMap/SaveForm?nameList=' + nameList + "&newNameList=" + newNameList + "&code=" + code, {}, function (res) {
                    // 保存成功后才回调
                    if (!!callBack) {
                        callBack();
                    }
                });
            }
        }
    };
    page.init();
}
