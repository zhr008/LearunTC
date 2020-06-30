/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.11.06
 * 描 述：审核人表单字段添加
 */


var acceptClick;
var auditorName = '';
var fieldName = '';
var formName = '';

var dbId = '';
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('#AuditorFieldId').lrselect({
                value: 'f_column',
                text: 'f_column',
                title: 'f_remark',
                allowSearch: true,
                select: function (item) {
                    if (item) {
                        fieldName = item.f_column;
                    }
                    else {
                        fieldName = '';
                    }
                }
            });
            $('#ReFieldId').lrselect({
                value: 'f_column',
                text: 'f_column',
                title: 'f_remark',
                allowSearch: true
            });
            $('#ReFormId').lrselect({
                value: 'name',
                text: 'name',
                title: 'tdescription',
                allowSearch: true,
                select: function (item) {
                    if (item) {
                        learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: dbId, tableName: item.name }, function (data) {
                            if (data) {
                                $('#ReFieldId').lrselectRefresh({
                                    data: data
                                });
                                $('#AuditorFieldId').lrselectRefresh({
                                    data: data
                                });
                            }
                        });
                        formName = item.name;
                    }
                    else {
                        formName = '';
                        $('#ReFieldId').lrselectRefresh({
                            data: []
                        });
                        $('#AuditorFieldId').lrselectRefresh({
                            data: []
                        });
                    }
                    
                }
            });
            $('#DbId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true,
                select: function (item) {
                    if (item && !item.hasChildren) {
                        dbId = item.id;
                        $('#ReFormId').lrselectRefresh({
                            url: top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetList',
                            param: { databaseLinkId: item.id }
                        });
                    }
                    else {
                        dbId = '';
                        $('#ReFormId').lrselectRefresh({
                            url: false,
                            data: [],
                            param: {}
                        });
                    }
                }
            });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();
       
        var res = {};
        res.auditorId = formData.DbId + "|" + formData.ReFormId + "|" + formData.ReFieldId + "|" + formData.AuditorFieldId;
        res.auditorName = "【" + formName + "】" + fieldName;
        res.type = '5';//审核者类型1.岗位2.角色3.用户4.上下级5.表单指定字段6.某一个节点执行人
        callBack(res);
        return true;
    };
    page.init();
}