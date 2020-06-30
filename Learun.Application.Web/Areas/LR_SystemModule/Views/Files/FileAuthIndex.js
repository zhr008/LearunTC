var bootstrap = function ($, learun) {
    "use strict";
    var folderId = "0";
    var page = {
        init: function () {

            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            $('#lr_auth').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var name = $('#gridtable').jfGridValue('F_Name');
                var type = $('#gridtable').jfGridValue('Type');
                if (learun.checkrow(keyValue)) {
                    var isFolder = '0';
                    if (type == '2') {
                        isFolder = '1'
                    }
                    learun.layerForm({
                        id: 'FileAuthFrom',
                        title: '授权【' + name + '】',
                        url: top.$.rootUrl + '/LR_SystemModule/Files/FileAuthFrom?fileInfoId=' + keyValue + "&isFolder=" + isFolder,
                        width: 800,
                        height: 500,
                        maxmin: true,
                        btn: null
                    });
                }
              
            });
            $('#lr_main_auth').on('click', function () {
                learun.layerForm({
                    id: 'FileAuthFrom',
                    title: '授权主目录',
                    url: top.$.rootUrl + '/LR_SystemModule/Files/FileAuthFrom?fileInfoId=0&isFolder=1',
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: null,
                    end: function () {

                    }
                });

            });

            $('#pathList').on('click', '.path-item', function () {
                var $this = $(this);
                $this.nextAll().remove();
                folderId = $this.attr('data-fileid');
                page.openFolder();
            });
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/Files/GetAllPublishPageList',
                headData: [
                    {
                        label: '文件名', name: 'F_Name', width: 400, align: 'left',
                        formatter: function (cellvalue, row) {
                            return "<div style='cursor:pointer;'><div style='float: left;'><img src='" + top.$.rootUrl + "/Content/images/filetype/" + row.F_FileType + ".png' style='width:30px;height:30px;padding:5px;margin-left:-5px;margin-right:5px;' /></div><div style='float: left;line-height:35px;'>" + cellvalue + "</div></div>";
                        }
                    },
                    {
                        label: '文件编号', name: 'F_Code', width: 120, align: 'left'
                    },
                    {
                        label: '文件版本', name: 'F_Ver', width: 60, align: 'center'
                    },
                    {
                        label: '操作', name: 'F_Id', width: 200, align: 'left',
                        formatter: function (value, row, op, $cell) {
                            var $div = $('<div></div>');
                            if (row.Type == '2') {
                                var $openFolder = $('<span class=\"label label-success\" style=\"cursor: pointer;\">打开</span>');
                                $openFolder.on('click', function () {
                                    folderId = value;
                                    page.openFolder();
                                    $('#pathList').append('<span data-fileid="' + value + '"  class="path-item">/&nbsp;' + row.F_Name + '</span>');
                                });
                                $div.append($openFolder);
                            }

                            return $div;
                        }
                    }
                ],
                mainId: 'F_Id'
            });

            page.search();
        },
        openFolder: function () {
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.folderId = folderId || "0";

            if (param.folderId == "0") {
                $('#pathList [data-fileid="0"]').nextAll().remove();
            }

            $('#gridtable').jfGridSet('reload', param);
        }
    };

    page.init();
}