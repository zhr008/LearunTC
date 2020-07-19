/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 力软信息技术（苏州）有限公司
 * 创建人：超级管理员
 * 日  期：2020-06-27 23:47
 * 描  述：供应商登记
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
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            $('#F_ManageType').lrDataItemSelect({ code: 'ManageType' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_CodeDemo/Applicant/Form',
                    width: 750,
                    height: 500,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_ApplicantId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_CodeDemo/Applicant/Form?keyValue=' + keyValue,
                        width: 750,
                        height: 500,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_ApplicantId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/Applicant/DeleteForm', { keyValue: keyValue }, function () {
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
                url: top.$.rootUrl + '/LR_CodeDemo/Applicant/GetPageList',
                headData: [
                    { label: "公司名称", name: "F_CompanyName", width: 260, align: "left" },
                    {
                        label: "供应商类型", name: "F_SupplyType", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'SupplyType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "工商注册号码", name: "F_RegistrationNo", width: 150, align: "center" },
                    //{ label: "省", name: "F_ProvinceCode", width: 100, align: "left"},
                    //{ label: "市", name: "F_CityCode", width: 100, align: "left"},
                    //{ label: "区", name: "F_AreaCode", width: 100, align: "left"},
                    //{ label: "详细地址", name: "F_Address", width: 100, align: "left"},
                    { label: "注册资金(万元)", name: "F_RegisteredCapital", width: 100, align: "left" },
                    { label: "法定代表人", name: "F_Representative", width: 100, align: "center" },
                    {
                        label: "成立日期", name: "F_EstablishDate", width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    {
                        label: "工商更新日期", name: "F_BusinessUpdateDate", width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },

                    {
                        label: "经营状态", name: "F_ManageType", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'ManageType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    //{
                    //    label: "单位类型", name: "F_ApplicantType", width: 100, align: "center",
                    //    formatterAsync: function (callback, value, row, op, $cell) {
                    //        learun.clientdata.getAsync('dataItem', {
                    //            key: value,
                    //            code: 'ApplicantType',
                    //            callback: function (_data) {
                    //                callback(_data.text);
                    //            }
                    //        });
                    //    }
                    //},
                    { label: "现有资质证书", name: "F_QualificationCert", width: 250, align: "left" },
                    //{ label: "负责人姓名", name: "F_LeaderUserName", width: 100, align: "left"},
                    //{ label: "负责人手机", name: "F_LeaderMobile", width: 100, align: "left"},
                    //{ label: "负责人其他联系方式", name: "F_LeaderOtherContact", width: 100, align: "left"},
                    //{ label: "办事人姓名", name: "F_ClerkUserName", width: 100, align: "left"},
                    //{ label: "办事人手机", name: "F_ClerkMobile", width: 100, align: "left"},
                    //{ label: "办事人其他联系方式", name: "F_ClerkOtherContact", width: 100, align: "left"},
                    //{ label: "办事人职务", name: "F_ClerkJob", width: 100, align: "left"},
                    //{ label: "单位开户行", name: "F_BankName", width: 100, align: "left"},
                    //{ label: "单位银行账户", name: "F_BankAccount", width: 100, align: "left"},

                    { label: "备注", name: "F_Description", width: 100, align: "left" },
                ],
                mainId: 'F_ApplicantId',
                isPage: true,

                isSubGrid: true,
                subGridExpanded: function (subid, rowdata) {
                    $('#' + subid).jfGrid({
                        url: top.$.rootUrl + '/LR_CodeDemo/Personnels/GetPageListByApplicantId',
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
                        ]
                    });

                    $('#' + subid).jfGridSet('reload', { param: { ApplicantId: rowdata.F_ApplicantId } });
                }



            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.F_ApplicantType = 1;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
