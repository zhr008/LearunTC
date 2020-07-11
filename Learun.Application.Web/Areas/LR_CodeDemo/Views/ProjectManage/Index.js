/* * 创建人：超级管理员
 * 日  期：2020-07-10 17:09
 * 描  述：项目管理
 */
var refreshGirdData;
var selectedRow;


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
                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetTree?code=recruitdata&parentId=parentId&Id=f_applicantid&showId=f_companyname',
                nodeClick: function (item) {
                    page.search({ ApplicantId: item.value });
                }
            });
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 400);
            $('#ApplicantId').lrDataSourceSelect({ code: 'recruitdata',value: 'f_applicantid',text: 'f_companyname' });
            $('#ProjectType').lrDataItemSelect({ code: 'projecttype' });
            $('#Status').lrDataItemSelect({ code: 'projectstatus' });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_CodeDemo/ProjectManage/Form',
                    width: 750,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('ProjectId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_CodeDemo/ProjectManage/Form?keyValue=' + keyValue,
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
                var keyValue = $('#gridtable').jfGridValue('ProjectId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/ProjectManage/DeleteForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 打印
            $('#lr_print').on('click', function () {
                $('#gridtable').jqprintTable();
            });
            //   需求
            $('#lr_adddetail').on('click', function () {
                var ProjectId = $('#gridtable').jfGridValue('ProjectId');
                if (learun.checkrow(ProjectId)) {
                    learun.layerForm({
                        id: 'detail',
                        title: '新增',
                        url: top.$.rootUrl + '/LR_CodeDemo/ProjectDetail/Form?ProjectId=' + ProjectId,
                        width: 750,
                        height: 450,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            
            
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_CodeDemo/ProjectManage/GetPageList',
                headData: [
                    {
                        label: "单位名称", name: "ApplicantId", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('custmerData', {
                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'recruitdata',
                                 key: value,
                                 keyId: 'f_applicantid',
                                 callback: function (_data) {
                                     callback(_data['f_companyname']);
                                 }
                             });
                        }},
                    { label: "注册地", name: "RegisterAddress", width: 100, align: "center"},
                    {
                        label: "资质类别", name: "ProjectType", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'projecttype',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    {
                        label: "社保交费", name: "SocialSecurities", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'SocialSecurities',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "资质专业", name: "Major", width: 100, align: "center"},
                    {
                        label: "资质等级", name: "Rank", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'rank',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "需求发布日期", name: "PublishDate", width: 100, align: "center"},
                    { label: "配置完成日期", name: "ConfigDate", width: 100, align: "center"},
                    { label: "项目结束日期", name: "CompleteDate", width: 100, align: "center"},
                    {
                        label: "项目状态", name: "Status", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'projectstatus',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "备注", name: "F_Description", width: 100, align: "left"},
                ],
                mainId:'ProjectId',
                isSubGrid: true,
                subGridExpanded: function (subid, rowdata) {
                    $('#' + subid).jfGrid({
                        url: top.$.rootUrl + '/LR_CodeDemo/ProjectDetail/GetPageListByProjectId',
                        headData: [
                            {
                                label: "证书类型", name: "CertType", width: 100, align: "center",
                                formatterAsync: function (callback, value, row, op, $cell) {
                                    learun.clientdata.getAsync('dataItem', {
                                        key: value,
                                        code: 'CertType',
                                        callback: function (_data) {
                                            callback(_data.text);
                                        }
                                    });
                                }
                            },
                            { label: "证书专业", name: "CertMajor", width: 100, align: "center" },
                            { label: "标准数量", name: "StandardNum", width: 100, align: "right" },
                            {
                                label: "社保要求", name: "SocialSecurityRequire", width: 100, align: "center",
                                formatterAsync: function (callback, value, row, op, $cell) {
                                    learun.clientdata.getAsync('dataItem', {
                                        key: value,
                                        code: 'SocialSecurityRequire',
                                        callback: function (_data) {
                                            callback(_data.text);
                                        }
                                    });
                                }
                            },
                            {
                                label: "资格证要求", name: "CertRequire", width: 100, align: "center",
                                formatterAsync: function (callback, value, row, op, $cell) {
                                    learun.clientdata.getAsync('dataItem', {
                                        key: value,
                                        code: 'CertRequire',
                                        callback: function (_data) {
                                            callback(_data.text);
                                        }
                                    });
                                }
                            },
                            {
                                label: "身份证要求", name: "IDCardRequire", width: 100, align: "center",
                                formatterAsync: function (callback, value, row, op, $cell) {
                                    learun.clientdata.getAsync('dataItem', {
                                        key: value,
                                        code: 'CertRequire',
                                        callback: function (_data) {
                                            callback(_data.text);
                                        }
                                    });
                                }
                            },
                            {
                                label: "毕业证要求", name: "GradCertRequire", width: 100, align: "center",
                                formatterAsync: function (callback, value, row, op, $cell) {
                                    learun.clientdata.getAsync('dataItem', {
                                        key: value,
                                        code: 'CertRequire',
                                        callback: function (_data) {
                                            callback(_data.text);
                                        }
                                    });
                                }
                            },
                            {
                                label: "到场要求", name: "SceneRequire", width: 100, align: "center",
                                formatterAsync: function (callback, value, row, op, $cell) {
                                    learun.clientdata.getAsync('dataItem', {
                                        key: value,
                                        code: 'SceneRequire',
                                        callback: function (_data) {
                                            callback(_data.text);
                                        }
                                    });
                                }
                            },
                            { label: "其他要求", name: "OtherRequire", width: 100, align: "center" },
                            { label: "甲方提供数量", name: "AlreadyNum", width: 100, align: "right" },
                            { label: "我方配置数量", name: "NeedNum", width: 100, align: "right" },
                            {
                                label: "配置状态", name: "Status", width: 100, align: "center",
                                formatterAsync: function (callback, value, row, op, $cell) {
                                    learun.clientdata.getAsync('dataItem', {
                                        key: value,
                                        code: 'CurrentCertStatus',
                                        callback: function (_data) {
                                            callback(_data.text);
                                        }
                                    });
                                }
                            },
                            { label: "配置说明", name: "F_Description", width: 100, align: "left" },
                        ]
                    });
                    $('#' + subid).jfGridSet('reload', { param: { ProjectId: rowdata.ProjectId } });
                }
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
