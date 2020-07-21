/* * 创建人：超级管理员
 * 日  期：2020-07-05 19:59
 * 描  述：培训记录
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
            page.buildTree();
            page.bind();
            page.initGird();
        },
        bind: function () {
            $('#btn_BuildSearch').on('click', function () {
                F_UserName = $("#txt_BuildKeyword").val();
                page.buildTree();
            });
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 400, 400);
            $('#F_CertStatus').lrDataItemSelect({ code: 'CertStatus' });
            $('#F_TrainPayStatus').lrDataItemSelect({ code: 'TrainPayStatus' }); 

            $('#F_CertType').lrDataItemSelect({ code: 'CertType' });
            $('#F_MajorType').lrDataItemSelect({ code: 'MajorType' });

            $('#F_TrainCollectStatus').lrDataItemSelect({ code: 'TrainCollectStatus' });
            $('#F_FeeStandard').lrDataItemSelect({ code: 'FeeStandard' });

            $('#F_TrainStatus').lrDataItemSelect({ code: 'TrainStatus' });
            
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
                        url: top.$.rootUrl + '/LR_CodeDemo/PersonnelTrain/Form?F_PersonId=' + F_PersonId + "&F_UserName=" + F_UserName + "&F_IDCardNo=" + F_IDCardNo,
                        width: 800,
                        height: 580,
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
                var keyValue = $('#gridtable').jfGridValue('F_PersonnelTrainId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_CodeDemo/PersonnelTrain/Form?keyValue=' + keyValue,
                        width: 800,
                        height: 580,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_PersonnelTrainId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/PersonnelTrain/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            $('#lr_synchro').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_PersonnelTrainId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认同步人才库！', function (res, index) {
                        if (res) {
                            learun.loading(true, '正在同步人才库！');
                            learun.httpAsyncPost(top.$.rootUrl + '/LR_CodeDemo/PersonnelTrain/SyncToCredentials', { keyValue: keyValue }, function (res) {
                                learun.loading(false);
                                if (res.code == learun.httpCode.success) {
                                    learun.alert.success(res.info);
                                    refreshGirdData();
                                }
                                else {
                                    learun.alert.error(res.info);
                                    learun.httpErrorLog(res.info);
                                }
                                top.layer.close(index)
                            });
                        }
                    })
                }
            })



            // 打印
            $('#lr_print').on('click', function () {
                $('#gridtable').jqprintTable();
            });
        },
        buildTree: function () {
            if (ParentDisable == "true") {
                $(".lr-layout-left").remove();
                $("#lr_layout").removeClass("lr-layout-left-center");
                $("#multiple_condition_query").remove();

            } else {

                // 初始化左侧树形数据
                $('#dataTree').lrtree({
                    url: top.$.rootUrl + '/LR_CodeDemo/IDCard/GetTree?PersonId=' + F_PersonId + "&ApplicantId=" + F_ApplicantId + "&UserName=" + F_UserName,
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


        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_CodeDemo/PersonnelTrain/GetPageList',
                headData: [
                    { label: '姓名', name: 'F_UserName', width: 100, align: "center" },
                    { label: '身份证号码', name: 'F_IDCardNo', width: 200, align: "center" },
                    {
                        label: "培训证书名称", name: "F_CertType", width: 100, align: "center",
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

                    {
                        label: "培训专业序列", name: "F_MajorType", width: 100, align: "center",
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
                    { label: "报名专业/工种 ", name: "F_Major", width: 100, align: "center" },
                    { label: "预定发证机构", name: "F_CertOrganization", width: 100, align: "center" },
                    {
                        label: "预定发证日期", name: "F_CertDateBegin", width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    {
                        label: "证书失效日期", name: "F_CertDateEnd", width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    {
                        label: "资格证保管", name: "F_CertStyle", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'CertStyle',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: "库存状态", name: "F_CertStatus", width: 100, align: "center",
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
                        label: "报名培训状态", name: "F_TrainStatus", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'TrainStatus',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "登记日期", name: "F_CheckInDate", width: 100, align: "center" },
                    {
                        label: "报名截止日期", name: "F_ApplyDate", width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    {
                        label: "预计培训日期", name: "F_ExpectedTrainDate", width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },
                    { label: "报考收费标准", name: "F_FeeStandard", width: 100, align: "center" },
                    {
                        label: "报考收费状态", name: "F_TrainCollectStatus", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'TrainCollectStatus',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "付费代理机构", name: "F_TrainOrgName", width: 100, align: "center" },
                    { label: "代理机构开户名", name: "F_TrainOrgAccountName", width: 100, align: "center" },
                    { label: "代理机构开户行", name: "F_TrainOrgBankName", width: 100, align: "center" },
                    { label: "代理机构银行账号", name: "F_TrainOrgBankAccount", width: 100, align: "center" },
                    { label: "付费金额", name: "F_TrainPayAmount", width: 100, align: "center" },
                    {
                        label: "付费凭证", name: "F_TrainPayVoucher", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'TrainPayVoucher',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: "付费状态", name: "F_TrainPayStatus", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'TrainPayStatus',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "培训备注", name: "F_Description", width: 100, align: "left" },
                ],
                mainId: 'F_PersonnelTrainId',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.F_PersonId = F_PersonId;
            //param.F_ApplicantId = F_ApplicantId;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };
    page.init();
}
