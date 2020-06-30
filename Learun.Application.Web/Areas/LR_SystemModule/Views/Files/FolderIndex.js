var refreshGirdData; // 更新数据
var selectedRow;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGrid();
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
                var f_Id = $('#gridtable').jfGridValue('F_Id');
                learun.layerForm({
                    id: 'FolderForm',
                    title: '添加文件夹',
                    url: top.$.rootUrl + '/LR_SystemModule/Files/FolderForm?parentId=' + f_Id,
                    width: 400,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'FolderForm',
                        title: '编辑文件夹',
                        url: top.$.rootUrl + '/LR_SystemModule/Files/FolderForm?keyValue=' + keyValue,
                        width: 400,
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
                    learun.layerConfirm('是否确认删除该文件夹！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_SystemModule/Files/DeleteFolder', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/Files/GetFolderList',
                headData: [
                    { label: '名称', name: 'F_Name', width: 400, align: 'left' },
                    {
                        label: "修改时间", name: "F_Time", width: 120, align: "left"
                    }
                ],
                isTree: true,
                mainId: 'F_Id',
                parentId: 'F_PId',
                reloadSelected: true
            });
            page.search();
        },
        search: function (param) {
            $('#gridtable').jfGridSet('reload', param);
        }
    };

    // 保存数据后回调刷新
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    }

    page.init();
}


