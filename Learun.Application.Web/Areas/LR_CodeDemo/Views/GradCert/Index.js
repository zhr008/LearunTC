/* * 创建人：超级管理员
 * 日  期：2020-07-02 23:28
 * 描  述：毕业证书
 */
var selectedRow;
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
            page.initGird();
            page.bind();
        },
        bind: function () {
            // 初始化左侧树形数据
            $('#dataTree').lrtree({
                url: top.$.rootUrl + '/LR_CodeDemo/IDCard/GetTree?PersonId=' + F_PersonId + "&ApplicantId=" + F_ApplicantId,
                nodeClick: function (item) {
                    if (!!item.value) {
                        F_PersonId = item.id;
                        F_UserName = item.text;
                        F_IDCardNo = item.value;
                        F_ApplicantId = item.parentid
                        page.search();
                    }
                    else {

                        F_PersonId = "";
                        F_UserName = "";
                        F_IDCardNo = "";
                        F_ApplicantId = item.id
                        debugger
                        if (ParentDisable != "true") {
                            page.search();
                        }
                    }

                }
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                if (!!F_PersonId) {
                    learun.layerForm({
                        id: 'form',
                        title: '新增',
                        url: top.$.rootUrl + '/LR_CodeDemo/GradCert/Form?F_PersonId=' + F_PersonId + "&F_UserName=" + F_UserName + "&F_IDCardNo=" + F_IDCardNo,
                        width: 700,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
                else {
                    learun.alert.warning('请选择树形列表人员!');
                }


            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_GradCertId');
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_CodeDemo/GradCert/Form?keyValue=' + keyValue,
                        width: 700,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_GradCertId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/GradCert/DeleteForm', { keyValue: keyValue }, function () {
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_CodeDemo/GradCert/GetPageList',
                headData: [
                    { label: '姓名', name: 'F_UserName', width: 100, align: "left" },
                    { label: '身份证号码', name: 'F_IDCardNo', width: 200, align: "left" },
                    { label: '专业', name: 'F_Major', width: 100, align: "left" },
                    { label: '毕业时间', name: 'F_GradTime', width: 50, align: "left" },
                    {
                        label: '学历类型', name: 'F_EducationType', width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'EducationType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: '学制类型', name: 'F_Term', width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'Term',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: '保管方式', name: 'F_OriginalType', width: 100, align: "left",
                          formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'OriginalType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: '备注', name: 'F_Description', width: 200, align: "left" },

                ],
                mainId: 'F_GradCertId',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
