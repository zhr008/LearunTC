/* * 创建人：超级管理员
 * 日  期：2020-07-07 13:32
 * 描  述：projectmanage
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
                    url: top.$.rootUrl + '/LR_CodeDemo/projectmanage/Form',
                    width: 900,
                    height: 500,
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
                        url: top.$.rootUrl + '/LR_CodeDemo/projectmanage/Form?keyValue=' + keyValue,
                        width: 900,
                        height: 500,
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
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/projectmanage/DeleteForm', { keyValue: keyValue}, function () {
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
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_CodeDemo/projectmanage/GetPageList',
                headData: [
                    { label: "单位名称", name: "ApplicantId", width: 100, align: "left",
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
                    { label: "注册地址", name: "RegisterAddress", width: 100, align: "left"},
                    { label: "资质类别", name: "ProjectType", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'projecttype',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "社保交费", name: "SocialSecurities", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'SocialSecurities',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "资质专业", name: "Major", width: 100, align: "left"},
                    { label: "资质等级", name: "Rank", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'rank',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                    { label: "需求发布日期", name: "PublishDate", width: 100, align: "left"},
                    { label: "配置完成日期", name: "ConfigDate", width: 100, align: "left"},
                    { label: "项目结束日期", name: "CompleteDate", width: 100, align: "left"},
                    { label: "项目状态", name: "Status", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op,$cell) {
                             learun.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'projectstatus',
                                 callback: function (_data) {
                                     callback(_data.text);
                                 }
                             });
                        }},
                ],
                mainId:'ProjectId',
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
