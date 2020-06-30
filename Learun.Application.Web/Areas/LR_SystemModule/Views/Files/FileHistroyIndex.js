var fileInfoId = request('fileInfoId');
var bootstrap = function ($, learun) {
    "use strict";

    //1 查看 2上传 3 下载  4 删除  5 复原
    var btnName = {
        '1': '查看',
        '2': '上传',
        '3': '下载',
        '4': '删除',
        '5': '复原',
        '6': '彻底删除'
    }
    var page = {
        init: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/Files/GetHistoryList?fileInfoId=' + fileInfoId,
                headData: [
                    {
                        label: '文件名', name: 'F_Name', width: 300, align: 'left',
                        formatter: function (cellvalue, row) {
                            return "<div style='cursor:pointer;'><div style='float: left;'><img src='" + top.$.rootUrl + "/Content/images/filetype/" + row.F_FileType + ".png' style='width:30px;height:30px;padding:5px;margin-left:-5px;margin-right:5px;' /></div><div style='float: left;line-height:35px;'>" + cellvalue + "</div></div>";
                        }
                    },
                    {
                        label: '文件版本', name: 'F_Ver', width: 60, align: 'center'
                    },
                    {
                        label: '大小', name: 'F_FileSize', width: 100, align: 'center',
                        formatter: function (cellvalue) {
                            return page.CountFileSize(cellvalue);
                        }
                    },
                    {
                        label: '操作', name: 'F_Id', width: 200, align: 'left',
                        formatter: function (value, row, op, $cell) {
                            var $div = $('<div></div>');
                            var btnlist = row.F_AuthType.split(',');
                            $.each(btnlist, function (_index, _item) {
                                if (_item != '6' && _item != '5' && _item != '4' && _item != '2' && $div.find('[data-value="' + _item + '"]').length == 0) {
                                    var $btn = $('<span class=\"label label-info\" data-value="' + _item + '" style=\"cursor: pointer; margin-right:8px;\">' + btnName[_item] + '</span>');
                                    $btn[0]._row = row;
                                    $btn.on('click', function () {
                                        var $this = $(this);
                                        var btnValue = $this.attr('data-value');
                                        var btnRow = $this[0]._row;
                                        switch (btnValue) {
                                            case '1':// 查看
                                                learun.layerForm({
                                                    id: 'PreviewForm',
                                                    title: '文件预览',
                                                    url: top.$.rootUrl + '/LR_SystemModule/Annexes/PreviewFile?fileId=' + btnRow.F_PFiled,
                                                    width: 1080,
                                                    height: 850,
                                                    btn: null
                                                });
                                                break;
                                            case '3':// 下载
                                                learun.download({ url: top.$.rootUrl + '/LR_SystemModule/Annexes/DownAnnexesFile', param: { fileId: btnRow.F_FileId }, method: 'POST' });
                                                break;
                                        }
                                    });
                                    $div.append($btn);
                                }

                            });

                            return $div;
                        }
                    }
                ],
                mainId: 'F_Id'
            });
            $('#gridtable').jfGridSet('reload');
        },
        CountFileSize: function (Size) {
            var m_strSize = "";
            var FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = page.ToDecimal(FactSize) + " 字节";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = page.ToDecimal(FactSize / 1024.00) + " KB";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = page.ToDecimal(FactSize / 1024.00 / 1024.00) + " MB";
            else if (FactSize >= 1073741824)
                m_strSize = page.ToDecimal(FactSize / 1024.00 / 1024.00 / 1024.00) + " GB";
            return m_strSize;
        },
        ToDecimal: function (x) {
            var f = parseFloat(x);
            if (isNaN(f)) {
                return 0;
            }
            f = Math.round(x * 100) / 100;
            return f;
        }

        
    };

    page.init();
}