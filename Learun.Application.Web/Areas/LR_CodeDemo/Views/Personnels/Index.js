﻿/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 力软信息技术（苏州）有限公司
 * 创建人：超级管理员
 * 日  期：2020-06-28 21:48
 * 描  述：个人基本信息
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 初始化左侧树形数据
            $('#dataTree').lrtree({
                url: top.$.rootUrl + '/LR_CodeDemo/Personnels/GetTree',
                nodeClick: function (item) {
                    page.search({ F_ApplicantId: item.value });
                }
            });
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            $('#F_ApplicantId').lrDataSourceSelect({ code: 'Rptdata',value: 'f_id',text: 'f_name' });
            $('#F_SceneType').lrDataItemSelect({ code: 'SceneType' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            //新增其他信息
            $('#lr_addall').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_PersonId');
                var ApplicantId = $('#gridtable').jfGridValue('F_ApplicantId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'AllIndex',
                        title: '新增',
                        url: top.$.rootUrl + '/LR_CodeDemo/Personnels/AllIndex?keyValue=' + keyValue + "&ApplicantId=" + ApplicantId,
                        width: 1200,
                        height: 700,
                        btn: null,
                        maxmin: true,
                        //callBack: function (id) {
                        //    return top[id].acceptClick(refreshGirdData);
                        //}
                    });
                }
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_CodeDemo/Personnels/Form',
                    width: 600,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_PersonId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_CodeDemo/Personnels/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_PersonId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/Personnels/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_CodeDemo/Personnels/GetPageList',
                headData: [
                    { label: "姓名", name: "F_UserName", width: 100, align: "left"},
                    { label: "身份证号码", name: "F_IDCardNo", width: 100, align: "left"},
                    { label: "性别", name: "F_Gender", width: 100, align: "left"},
                    { label: "年龄", name: "F_Age", width: 100, align: "left"},
                    { label: "存档编号", name: "F_PlaceCode", width: 100, align: "left"},
                    { label: "证书编码", name: "F_CertCode", width: 100, align: "left"},
                    { label: "供应商", name: "F_ApplicantId", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('custmerData', {
                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'Rptdata',
                                 key: value,
                                 keyId: 'f_id',
                                 callback: function (_data) {
                                     callback(_data['f_name']);
                                 }
                             });
                        }},
                    { label: "到场", name: "F_SceneType", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'SceneType',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }
                    },
                    { label: "备注", name: "F_Description", width: 100, align: "left" },
                ],
                mainId:'F_PersonId',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}