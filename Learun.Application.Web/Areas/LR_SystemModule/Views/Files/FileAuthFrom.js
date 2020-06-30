var fileInfoId = request('fileInfoId');
var isFolder = request('isFolder');
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'FolderForm',
                    title: '添加授权对象',
                    url: top.$.rootUrl + '/LR_SystemModule/Files/FileAuthAddFrom?fileInfoId=' + fileInfoId + "&isFolder=" + isFolder,
                    width: 500,
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
                        id: 'FileAuthAddFrom',
                        title: '编辑授权对象',
                        url: top.$.rootUrl + '/LR_SystemModule/Files/FileAuthAddFrom?keyValue=' + keyValue + '&fileInfoId=' + fileInfoId + "&isFolder=" + isFolder,
                        width: 500,
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
                    learun.layerConfirm('是否确认删除该授权对象！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_SystemModule/Files/DeleteAuth', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });



            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/Files/GetAuthList',
                headData: [
                    { label: '角色名称', name: 'F_ObjName', width: 100, align: 'left' },
                    {
                        label: '到期时间', name: 'F_Time', width: 120, align: 'left',
                        formatter: function (cellvalue) {
                            if (cellvalue == '9999-12-31 00:00:00') {
                                return '<span class=\"label label-success\" style=\"cursor: pointer;\">永久</span>';
                            } else if (cellvalue == 0) {
                                return learun.formatDate(cellvalue,'yyyy-MM-dd');
                            }
                        }
                    },
                    {
                        label: '权限类型', name: 'F_AuthType', width: 300, align: 'left', formatter: function (cellvalue) {
                            cellvalue = cellvalue || '';
                            return cellvalue.replace('1', '查看').replace('2', '上传').replace('3', '下载').replace('4', '删除').replace('5', '复原');
                        }
                    }
                ],
                mainId: 'F_Id'
            });
            page.search();
        },
        search: function (param) {
            $('#gridtable').jfGridSet('reload', { fileInfoId: fileInfoId});
        }
    };


    // 保存数据后回调刷新
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    }

    page.init();
}