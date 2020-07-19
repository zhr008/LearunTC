/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 力软信息技术（苏州）有限公司
 * 创建人：超级管理员
 * 日  期：2020-06-28 21:48
 * 描  述：个人基本信息
 */
var refreshGirdData;


var F_PersonId = request('F_PersonId');
var F_IDCardNo = request('F_IDCardNo');
var F_UserName = request('F_UserName');
var F_ApplicantId = request('F_ApplicantId');
var ParentDisable = request('ParentDisable');


var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
            page.initGird();
        },
        bind: function () {
            if (ParentDisable == "true") {
                $(".lr-layout-left").remove();
                $("#lr_layout").removeClass("lr-layout-left-center");
                $("#multiple_condition_query").remove();

            } else {
                // 初始化左侧树形数据
                $('#dataTree').lrtree({
                    url: top.$.rootUrl + '/LR_CodeDemo/IDCard/GetTree?PersonId=' + F_PersonId + "&ApplicantId=" + F_ApplicantId,
                    nodeClick: function (item) {
                        if (!!item.parentId) {
                            F_PersonId = item.id;
                            F_UserName = item.text;
                            F_IDCardNo = item.value;
                            F_ApplicantId = "";
                            page.search();
                        }
                        else {

                            F_PersonId = "";
                            F_UserName = "";
                            F_IDCardNo = "";
                            F_ApplicantId = item.id
                            if (ParentDisable != "true") {
                                page.search();
                            }
                        }

                    }
                });
            }
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 285, 400);
            $('#F_ApplicantId').lrDataSourceSelect({ code: 'Rptdata', value: 'f_id', text: 'f_name' });
            $('#F_SceneType').lrDataItemSelect({ code: 'SceneType' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            //新增其他信息
            $('#lr_addall').on('click', function () {
                var F_PersonId = $('#gridtable').jfGridValue('F_PersonId');
                var F_UserName = $('#gridtable').jfGridValue('F_UserName');
                var F_IDCardNo = $('#gridtable').jfGridValue('F_IDCardNo');
                var F_ApplicantId = $('#gridtable').jfGridValue('F_ApplicantId');
                if (learun.checkrow(F_PersonId)) {
                    learun.layerForm({
                        id: 'AllIndex',
                        title: '新增',
                        url: top.$.rootUrl + '/LR_CodeDemo/Personnels/AllIndex?F_PersonId=' + F_PersonId + "&F_IDCardNo=" + F_IDCardNo + "&F_UserName=" + F_UserName + "&F_ApplicantId=" + F_ApplicantId,
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
                    title: '新增人员',
                    url: top.$.rootUrl + '/LR_CodeDemo/Personnels/Form',
                    width: 750,
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
                        width: 750,
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
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/Personnels/DeleteForm', { keyValue: keyValue }, function () {
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
                    { label: "姓名", name: "F_UserName", width: 100, align: "center" },
                    { label: "身份证号码", name: "F_IDCardNo", width: 150, align: "center" },
                    {
                        label: "性别", name: "F_Gender", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'Gender',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }

                    },
                    { label: "年龄", name: "F_Age", width: 100, align: "center" },

                    { label: "存档编码", name: "F_PlaceCode", width: 100, align: "center" },
                    { label: "证书编码", name: "F_CertCode", width: 100, align: "center" },
                    {
                        label: "来源", name: "F_ApplicantId", width: 180, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('custmerData', {
                                url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'applicantdata',
                                key: value,
                                keyId: 'f_applicantid',
                                callback: function (_data) {
                                    callback(_data['f_companyname']);
                                }
                            });
                        }
                    },
                    {
                        label: "到场", name: "F_SceneType", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
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
                mainId: 'F_PersonId',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.F_PersonId = F_PersonId;
            param.F_ApplicantId = F_ApplicantId;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
