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
                            F_ApplicantId = item.parentid
                            page.search();
                        }
                        else {

                            F_PersonId = "";
                            F_UserName = "";
                            F_IDCardNo = "";
                            //F_ApplicantId = item.id
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
            $('#F_EducationType').lrDataItemSelect({ code: 'EducationType' });
            $('#F_Term').lrDataItemSelect({ code: 'Term' });
            $('#F_OriginalType').lrDataItemSelect({ code: 'OriginalType' });
            $('#F_MajorType').lrDataItemSelect({ code: 'MajorType' });
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
                        width: 770,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
                else {
                    learun.alert.warning('请选择人员信息人员!');
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
                        width: 770,
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
                                refreshGirdData();
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
                    { label: '姓名', name: 'F_UserName', width: 100, align: "center" },
                    { label: '身份证号码', name: 'F_IDCardNo', width: 200, align: "center" },
                    { label: '毕业专业', name: 'F_Major', width: 200, align: "center" },
                    {
                        label: '毕业日期', name: 'F_GradTime', width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    {
                        label: '学历', name: 'F_EducationType', width: 100, align: "center",
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
                        label: '学制类型', name: 'F_Term', width: 100, align: "center",
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
                        label: '专业序列', name: 'F_MajorType', width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'MajorType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: '毕业证保管', name: 'F_OriginalType', width: 100, align: "center",
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
                    {
                        label: '库存状态', name: 'F_CertStatus', width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'CertStatus',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: '入库时间', name: 'F_CheckInDate', width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
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
