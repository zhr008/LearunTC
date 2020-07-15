/* * 创建人：超级管理员
 * 日  期：2020-07-05 20:42
 * 描  述：合同结算
 */
var refreshGirdData;
var selectedRow;
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
                        if (ParentDisable != "true") {
                            page.search();
                        }
                    }

                }
            });
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 360, 400);
            $('#F_ContractStatus').lrDataItemSelect({ code: 'ContractStatus' });
            $('#F_PayStatus').lrDataItemSelect({ code: 'PayStatus' });
            $('#F_ApplicantId').lrDataSourceSelect({ code: 'applicant', value: 'f_applicantid', text: 'f_companyname' });
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
                        url: top.$.rootUrl + '/LR_CodeDemo/Settlements/Form?F_PersonId=' + F_PersonId + "&F_UserName=" + F_UserName + "&F_IDCardNo=" + F_IDCardNo,
                        width: 750,
                        height: 520,
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
                var keyValue = $('#gridtable').jfGridValue('F_SettlementsId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_CodeDemo/Settlements/Form?keyValue=' + keyValue,
                        width: 750,
                        height: 520,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_SettlementsId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/Settlements/DeleteForm', { keyValue: keyValue }, function () {
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
            //  结算详情
            $('#lr_adddetails').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_SettlementsId');
                var F_PersonId = $('#gridtable').jfGridValue('F_PersonId');
                var F_UserName = $('#gridtable').jfGridValue('F_UserName');
                var F_IDCardNo = $('#gridtable').jfGridValue('F_IDCardNo');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '新增',
                        url: top.$.rootUrl + '/LR_CodeDemo/SettlementsDetails/Form?F_PersonId=' + F_PersonId + "&F_UserName=" + F_UserName + "&F_IDCardNo=" + F_IDCardNo + "&F_SettlementsId=" + keyValue,
                        width: 600,
                        height: 400,
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
                url: top.$.rootUrl + '/LR_CodeDemo/Settlements/GetPageList',
                headData: [
                    { label: '姓名', name: 'F_UserName', width: 100, align: "center" },
                    { label: '身份证号码', name: 'F_IDCardNo', width: 200, align: "center" },
                    {
                        label: "合同状态", name: "F_ContractStatus", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'ContractStatus',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "合同起日", name: "F_ContractStartDate", width: 100, align: "center" },
                    { label: "合同止日", name: "F_ContractEndDate", width: 100, align: "center" },
                    { label: "维护代表", name: "F_Maintain", width: 100, align: "center" },
                    { label: "手机", name: "F_Mobile", width: 100, align: "center" },
                    { label: "联系地址", name: "F_Address", width: 100, align: "center" },
                    { label: "其它方式", name: "F_OtherContact", width: 100, align: "center" },
                    {
                        label: "人才签约代表", name: "F_ApplicantId", width: 100, align: "center",
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
                    { label: "收款人", name: "F_payee", width: 100, align: "center" },
                    { label: "开户行", name: "F_BankName", width: 100, align: "center" },
                    { label: "银行账号", name: "F_BankAccount", width: 100, align: "center" },
                    { label: "人员薪酬", name: "F_PersonAmount", width: 100, align: "center" },
                    { label: "中介费用", name: "F_ApplicantAmount", width: 100, align: "center" },
                    { label: "签约金额", name: "F_ContractAmount", width: 100, align: "center" },
                    {
                        label: "付款状态", name: "F_PayStatus", width: 100, align: "center",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'PayStatus',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "累计支付次数", name: "F_PayNumber", width: 100, align: "left" },
                    { label: "累计支付金额", name: "F_PayTotalAmount", width: 100, align: "left" },
                ],

                reloadSelected: true,
                mainId: 'F_SettlementsId',
                isPage: true,
                isSubGrid: true,  
                subGridExpanded: function (subid, rowdata) {
                    $('#' + subid).jfGrid({
                        url: top.$.rootUrl + '/LR_CodeDemo/SettlementsDetails/GetPageListBySettlementsId',
                        headData: [
                            { label: '姓名', name: 'F_UserName', width: 100, align: "center" },
                            { label: '身份证号码', name: 'F_IDCardNo', width: 200, align: "center" },
                            { label: "批次号", name: "F_BatchNumber", width: 100, align: "center" },
                            { label: "支付金额", name: "F_PayAmount", width: 100, align: "left" },
                            //{
                            //    label: "支付状态", name: "F_PayStatus", width: 100, align: "center",
                            //    formatterAsync: function (callback, value, row, op, $cell) {
                            //        learun.clientdata.getAsync('dataItem', {
                            //            key: value,
                            //            code: 'TrainPayStatus',
                            //            callback: function (_data) {
                            //                callback(_data.text);
                            //            }
                            //        });
                            //    }
                            //},
                            { label: "支付条件", name: "F_PayCondition", width: 100, align: "left" },
                        ]
                    });

                    $('#' + subid).jfGridSet('reload', { param: { F_SettlementsId: rowdata.F_SettlementsId } });
                }


            })
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
