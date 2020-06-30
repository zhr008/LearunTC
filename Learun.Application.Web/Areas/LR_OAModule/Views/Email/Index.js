/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.06.04
 * 描 述：邮件管理	
 */

var refreshGirdData;
var selectedRow = null;
var bootstrap = function ($, learun) {
    "use strict";
    var currentPage='1';
    refreshGirdData = function () {
        page.search();
    };

    var page = {
        init: function () {
            page.initleft();
            page.initGrid();
            page.bind();
            page.search();
        },
        bind: function () {
            $('#lr_deleteDraft').hide();
            $('#lr_delete').show();
            $('#lr_clear').show();
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search(keyword);
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 发邮件
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'sendform',
                    title: '发送邮件',
                    url: top.$.rootUrl + '/LR_OAModule/Email/Form',
                    width: 800,
                    height: 700,
                    btn: null
                });
            });

            // 查看
            $('#lr_detail').on('click', function () {
                var keyValue = '';
                if (currentPage == '2') {
                    keyValue = $('#receivetable').jfGridValue('F_Id');
                }
                else {
                    keyValue = $('#sendtable').jfGridValue('F_Id');
                }
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'detailform',
                        title: '查看',
                        url: top.$.rootUrl + '/LR_OAModule/Email/DetailForm?keyValue=' + keyValue + '&type=' + currentPage,
                        width: 800,
                        height: 700,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });

            // 删除
            $('#lr_delete').on('click', function () {
                keyValue = $('#grid' + currentPage).jfGridValue('F_ContentId');
                if (learun.checkrow(keyValue)) {
                    learun.deleteForm(top.$.rootUrl + '/LR_OAModule/Email/DeleteForm', { keyValue: keyValue,type: currentPage }, function () {
                        refreshGirdData();
                    });
                }
            });
            // 彻底删除
            $('#lr_clear').on('click', function () {
                keyValue = $('#grid' + currentPage).jfGridValue('F_ContentId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('删除后邮件将无法恢复，您确定要删除吗？', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OAModule/Email/ThoroughRemoveForm', { keyValue: keyValue,type: currentPage  }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 删除草稿
            $('#lr_deleteDraft').on('click', function () {
                keyValue = $('#grid2').jfGridValue('F_ContentId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('删除后草稿将无法恢复，您确定要删除吗？', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OAModule/Email/DeleteDraftForm', { keyValue: keyValue}, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initleft: function () {
            $('#lr_left_list li').on('click', function () {
                var $this = $(this);
                var $parent = $this.parent();
                $parent.find('.active').removeClass('active');
                $this.addClass('active');
                currentPage = $this.context.dataset.value;
                switch (currentPage) {
                    case "1":// 收件箱
                        $('.gridtable').removeClass('active');
                        $('#grid1').parent().addClass('active');
                        $('#lr_deleteDraft').hide();
                        $('#lr_delete').show();
                        $('#lr_clear').show();
                        break;
                    case "2":// 草稿箱
                        $('.gridtable').removeClass('active');
                        $('#grid2').parent().addClass('active');
                        $('#lr_deleteDraft').show();
                        $('#lr_delete').hide();
                        $('#lr_clear').hide();
                        break;
                    case "3":// 已发送
                        $('.gridtable').removeClass('active');
                        $('#grid3').parent().addClass('active');
                        $('#lr_deleteDraft').hide();
                        $('#lr_delete').show();
                        $('#lr_clear').show();
                        break;
                    case "4":// 垃圾箱
                        $('.gridtable').removeClass('active');
                        $('#grid4').parent().addClass('active');
                        $('#lr_deleteDraft').hide();
                        $('#lr_delete').hide();
                        $('#lr_clear').show();
                        break;
                }
                page.search();
            });
        },
        initGrid: function () {
            $('#grid1').jfGrid({
                url: top.$.rootUrl + '/LR_OAModule/Email/GetPageList',
                headData: [
                    { label: "发件人", name: "F_SenderName", width: 150, align: "left" },
                    {
                        label: "收件时间", name: "F_SenderTime", width: 120, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm');
                        }
                    },
                    { label: "主题", name: "F_Theme", width: 450, align: "left" }
                ],
                mainId: 'F_ContentId',
                isPage: true,
                sidx: 'F_SenderTime',
                sord: 'DESC',
                dblclick: function (item) {
                    learun.layerForm({
                        id: 'lookform',
                        title: '查看邮件',
                        url: top.$.rootUrl + '/LR_OAModule/Email/DetailForm?keyValue=' + item.F_ParentId,
                        width: 800,
                        height: 600,
                        btn: null
                    });
                }
            });
            $('#grid4').jfGrid({
                url: top.$.rootUrl + '/LR_OAModule/Email/GetPageList',
                headData: [
                    { label: "发件人", name: "F_SenderName", width: 150, align: "left" },
                    {
                        label: "时间", name: "F_SenderTime", width: 120, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm');
                        }
                    },
                    { label: "主题", name: "F_Theme", width: 450, align: "left" }
                ],
                mainId: 'F_Id',
                isPage: true,
                sidx: 'F_SenderTime',
                sord: 'DESC',
                dblclick: function (item) {
                    var _keyValue = item.F_ParentId || item.F_ContentId;
                    learun.layerForm({
                        id: 'lookform',
                        title: '查看邮件',
                        url: top.$.rootUrl + '/LR_OAModule/Email/DetailForm?keyValue=' + _keyValue,
                        width: 800,
                        height: 600,
                        btn: null
                    });
                }
            });
            $('#grid2').jfGrid({
                url: top.$.rootUrl + '/LR_OAModule/Email/GetPageList',
                headData: [
                    {
                        label: "收件人", name: "F_AddresssHtml", width: 220, align: "left",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getsAsync('user', {
                                key: value,
                                callback: function (name) {
                                    callback(name);
                                }
                            });
                        }
                    },
                    {
                        label: "时间", name: "F_CreateDate", width: 120, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm');
                        }
                    },
                    { label: "主题", name: "F_Theme", width: 450, align: "left" }
                ],
                mainId: 'F_ContentId',
                isPage: true,
                sidx: 'F_CreateDate',
                dblclick: function (item) {
                    selectedRow = item;
                    learun.layerForm({
                        id: 'sendform',
                        title: '发送邮件',
                        url: top.$.rootUrl + '/LR_OAModule/Email/Form?keyValue=' + item.F_ContentId,
                        width: 800,
                        height: 700,
                        btn: null
                    });
                }
            });
            $('#grid3').jfGrid({
                url: top.$.rootUrl + '/LR_OAModule/Email/GetPageList',
                headData: [
                    {
                        label: "收件人", name: "F_AddresssHtml", width: 220, align: "left",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getsAsync('user', {
                                key: value,
                                callback: function (name) {
                                    callback(name);
                                }
                            });
                        }
                    },
                    {
                        label: "发件时间", name: "F_SenderTime", width: 120, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm');
                        }
                    },
                    { label: "主题", name: "F_Theme", width: 450, align: "left" }
                ],
                mainId: 'F_ContentId',
                isPage: true,
                sidx: 'F_SenderTime',
                dblclick: function (item) {
                    learun.layerForm({
                        id: 'lookform',
                        title: '查看邮件',
                        url: top.$.rootUrl + '/LR_OAModule/Email/DetailForm?keyValue=' + item.F_ContentId,
                        width: 800,
                        height: 600,
                        btn: null
                    });
                }
            });
        },
        search: function (keyword) {
            $('#grid' + currentPage).jfGridSet('reload', { keyword: keyword, type: currentPage });
        }
    };
    page.init();
}
