/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-09-25 13:15
 * 描  述：采购申请
 */
var keyValue = request('keyValue');

var refreshGirdData; // 更新数据
var bootstrap = function ($, learun) {
    "use strict";

    var nowpurchaseId = learun.frameTab.currentIframe().nowpurchaseId;

    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });

            // 更新到此版本
            $('#lr_update').on('click', function () {
                var purchaseId = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(purchaseId)) {
                    if (purchaseId != nowpurchaseId) {
                        learun.layerConfirm('是否要更新到该版本！', function (res) {
                            if (res) {
                                learun.postForm(top.$.rootUrl + '/ERPDemo/PurchaseWarehousing/Update', { PurchaseInfoId: keyValue, purchaseId: purchaseId }, function () {
                                    nowpurchaseId = purchaseId;
                                    learun.frameTab.currentIframe().nowpurchaseId = purchaseId;
                                    learun.frameTab.currentIframe().refreshGirdData();
                                    //refreshGirdData();

                                });
                            }
                        });
                    }
                    else {
                        learun.alert.warning('已更新到当前版本!');
                    }
                }
            });
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/ERPDemo/PurchaseWarehousing/GetPurchasePageList',
                headData: [
                    { label: "单据", name: "F_PurchaseNo", width: 160, align: "left" },
                    {
                        label: "申请时间", name: "F_ApplyDate", width: 160, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    },
                    {
                        label: "修改时间", name: "F_ModifyDate", width: 160, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    },
                    {
                        label: "修改人", name: "F_ModifyUserName", width: 140, align: "left",
                    },
                    //{
                    //    label: "状态", name: "F_Type", width: 80, align: "center",
                    //    formatter: function (cellvalue, row) {
                    //        if (row.F_Type == 1) {
                    //            return '<span class=\"label label-success\" style=\"cursor: pointer;\">正式</span>';
                    //        }
                    //        else {
                    //            return '<span class=\"label label-info\" style=\"cursor: pointer;\">草稿</span>';
                    //        }
                    //    }
                    //},
                    //{
                    //    label: "", name: "F_Id", width: 80, align: "center",
                    //    formatter: function (cellvalue) {
                    //        if (cellvalue == nowschemeId) {
                    //            return '<span class=\"label label-danger\" style=\"cursor: pointer;\">当前版本</span>';
                    //        }
                    //    }
                    //}
                ],
                mainId: 'F_Id',
                reloadSelected: true,
                isPage: true,
                sidx: 'F_CreateDate',
                sord: 'DESC'
            });
            page.search();
        },
        search: function (param) {
            $('#gridtable').jfGridSet('reload', { purchaseId: keyValue });
        }
    };

    // 保存数据后回调刷新
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    }

    page.init();
}
