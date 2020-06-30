/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-09-26 15:11
 * 描  述：单据历史
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
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
                selectedRow = null;
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/ERPDemo/DataHistory/Form',
                    width: 700,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/ERPDemo/DataHistory/Form?keyValue=' + keyValue,
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
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/ERPDemo/DataHistory/DeleteForm', { keyValue: keyValue}, function () {
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/ERPDemo/DataHistory/GetPageList',
                headData: [
                        { label: 'F_Id', name: 'F_Id', width: 200, align: "left" },
                        { label: '单据编号', name: 'F_PurchaseNo', width: 200, align: "left" },
                        { label: '申请人', name: 'F_Appler', width: 200, align: "left" },
                        { label: '申请单位', name: 'F_Department', width: 200, align: "left" },
                        { label: '报价类别', name: 'F_PurchaseType', width: 200, align: "left" },
                        { label: '申请日期', name: 'F_ApplyDate', width: 200, align: "left" },
                        { label: '创建时间', name: 'F_CreateDate', width: 200, align: "left" },
                        { label: '备注', name: 'F_Description', width: 200, align: "left" },
                        { label: '附件', name: 'F_File', width: 200, align: "left" },
                        { label: '主题', name: 'F_Theme', width: 200, align: "left" },
                        { label: '商品编号', name: 'F_Code', width: 200, align: "left" },
                        { label: '商品名称', name: 'F_Name', width: 200, align: "left" },
                        { label: '条码', name: 'F_BarCode', width: 200, align: "left" },
                        { label: '产地', name: 'F_Place', width: 200, align: "left" },
                        { label: '规格', name: 'F_Specification', width: 200, align: "left" },
                        { label: '型号', name: 'F_Type', width: 200, align: "left" },
                        { label: '批次号', name: 'F_Number', width: 200, align: "left" },
                        { label: '单位', name: 'F_Unit', width: 200, align: "left" },
                        { label: '数量', name: 'F_Count', width: 200, align: "left" },
                        { label: '单价', name: 'F_Price', width: 200, align: "left" },
                        { label: '金额', name: 'F_Amount', width: 200, align: "left" },
                        { label: '状态', name: 'F_Status', width: 200, align: "left" },
                        { label: '采购员', name: 'F_Purchaser', width: 200, align: "left" },
                        { label: '支付方式', name: 'F_PaymentType', width: 200, align: "left" },
                        { label: '我方代表', name: 'F_We', width: 200, align: "left" },
                        { label: '对方代表', name: 'F_Your', width: 200, align: "left" },
                        { label: '总价', name: 'F_Total', width: 200, align: "left" },
                        { label: '交货日期', name: 'F_DeliveryDate', width: 200, align: "left" },
                        { label: '发货人', name: 'F_PurchaseWarehousinger', width: 200, align: "left" },
                        { label: '点收日期', name: 'F_PurchaseWarehousingDate', width: 200, align: "left" },
                        { label: '发货地址', name: 'F_FromAddress', width: 200, align: "left" },
                        { label: '收获地址', name: 'F_ToAddress', width: 200, align: "left" },
                        { label: '订单号', name: 'F_Order', width: 200, align: "left" },
                        { label: 'F_DataID', name: 'F_DataId', width: 200, align: "left" },
                ],
                mainId:'F_Id',
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
