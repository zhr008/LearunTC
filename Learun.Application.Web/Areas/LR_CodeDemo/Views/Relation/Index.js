/* * 创建人：超级管理员
 * 日  期：2020-07-14 23:25
 * 描  述：1231
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
                    url: top.$.rootUrl + '/LR_CodeDemo/Relation/Form',
                    width: 700,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_RelationId');
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_CodeDemo/Relation/Form?keyValue=' + keyValue,
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
                var keyValue = $('#gridtable').jfGridValue('F_RelationId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CodeDemo/Relation/DeleteForm', { keyValue: keyValue}, function () {
                            });
                        }
                    });
                }
            });
        },
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_CodeDemo/Relation/GetPageList',
                headData: [
                        { label: 'F_RelationId', name: 'F_RelationId', width: 200, align: "left" },
                        { label: 'ProjectId', name: 'ProjectId', width: 200, align: "left" },
                        { label: 'ProjectDetailId', name: 'ProjectDetailId', width: 200, align: "left" },
                        { label: 'F_CertId', name: 'F_CertId', width: 200, align: "left" },
                        { label: 'F_PersonId', name: 'F_PersonId', width: 200, align: "left" },
                        { label: 'F_Description', name: 'F_Description', width: 200, align: "left" },
                        { label: 'F_CreateDate', name: 'F_CreateDate', width: 200, align: "left" },
                        { label: 'F_CreateUserName', name: 'F_CreateUserName', width: 200, align: "left" },
                        { label: 'F_CreateUserId', name: 'F_CreateUserId', width: 200, align: "left" },
                        { label: 'F_ModifyDate', name: 'F_ModifyDate', width: 200, align: "left" },
                        { label: 'F_ModifyUserName', name: 'F_ModifyUserName', width: 200, align: "left" },
                        { label: 'F_ModifyUserId', name: 'F_ModifyUserId', width: 200, align: "left" },
                        { label: 'F_DeleteMark', name: 'F_DeleteMark', width: 200, align: "left" },
                ],
                mainId:'F_RelationId',
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
