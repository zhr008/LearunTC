var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";

    var tabType = 'releaseFile';
    var wfType = '1';
    var folderId = "0";
    var folderAuth = '';

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
            page.initleft();
            page.initGrid();
            page.bind();
        },
        bind: function () {
            $('#lr_form_tabs').lrFormTab(function (id) {
                wfType = id;
                page.search.verifyFile && page.search.verifyFile();
            });

            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search[tabType]({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });


            // 文件夹管理
            $('#lr-folder').on('click', function () {
                learun.layerForm({
                    id: 'FolderIndex',
                    title: '文件夹管理',
                    url: top.$.rootUrl + '/LR_SystemModule/Files/FolderIndex',
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: null,
                    end: function () {
                        
                    }
                });
            });
            // 文件授权
            $('#lr-fileAuth').on('click', function () {
                learun.layerForm({
                    id: 'FileAuthIndex',
                    title: '文件授权',
                    url: top.$.rootUrl + '/LR_SystemModule/Files/FileAuthIndex',
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: null,
                    end: function () {

                    }
                });
            });

            //上传文件
            $('#lr-uploadify').on('click', function () {
                learun.frameTab.open({ F_ModuleId: 'filesuploadify', F_Icon: 'fa magic', F_FullName: '上传文件审核', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?shcemeCode=lr_files_manager&tabIframeId=filesuploadify&type=create' });
                //if (folderId == "0") {// 获取主目录的权限，有上传权限才允许上传
                //    learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/Files/IsUPLoad', { folderId: folderId }, function (res) {
                //        if (res == true) {
                //            learun.frameTab.open({ F_ModuleId: 'filesuploadify', F_Icon: 'fa magic', F_FullName: '上传文件审核', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?shcemeCode=lr_files_manager&tabIframeId=filesuploadify&type=create' });
                //        }
                //        else {
                //            learun.alert.warning('你没有权限在当前目录下创建文件！');
                //        }
                //    })
                //}
                //else if (folderAuth == '2') {
                //}
                //else {
                //    learun.alert.warning('你没有权限在当前目录下创建文件！');
                //}
            });

            // 
            $('#pathList').on('click', '.path-item', function () {
                var $this = $(this);
                var data = $this[0]._data;
                if (data) {
                    folderAuth = data.F_AuthType;
                }
                $this.nextAll().remove();
                folderId = $this.attr('data-fileid');
                page.openFolder();
            });

        },
        initleft: function () {
            $('#lr_left_list li').on('click', function () {
                var $this = $(this);
                $this.parent().find('.active').removeClass('active');
                $this.addClass('active');

                var v = $this.attr('data-value');
                $('.lr-layout-body>.active').removeClass('active');
                $('#' + v).addClass('active');
                tabType = v;
                page.search[v] && page.search[v]();
                //console.log(tabType)
                //lr-uploadify

            });
        },
        //加载表格
        initGrid: function () {
            $('#wfgridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/Files/GetWFPageList',
                headData: [
                    {
                        label: "任务", name: "F_TaskName", width: 160, align: "left",
                        formatter: function (cellvalue, row, dfop, $cell) {
                            if (row.F_EnabledMark == 3) {
                                if (wfType == '1') {
                                    return '本人发起';
                                }
                                else {
                                    cellvalue;
                                }
                            }

                            // 草稿
                            if (row.F_EnabledMark == 2) {
                                $cell.on('click', '.create', function () {// 创建
                                    // 关闭草稿页
                                    learun.frameTab.closeByParam('tabProcessId', row.F_Id);

                                    learun.frameTab.open({ F_ModuleId: row.F_Id, F_Icon: 'fa magic', F_FullName: '创建流程-' + row.F_SchemeName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?processId=' + row.F_Id + '&tabIframeId=' + row.F_Id + '&type=draftCreate' });
                                    return false;
                                });
                                $cell.on('click', '.delete', function () {// 删除
                                    learun.layerConfirm('是否确认删除该草稿？', function (res) {
                                        if (res) {
                                            // 关闭草稿页

                                            learun.frameTab.closeByParam('tabProcessId', row.F_Id);
                                            learun.deleteForm(top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/DeleteDraft', { processId: row.F_Id }, function () {
                                                refreshGirdData();
                                            });
                                        }
                                    });
                                    return false;
                                });
                                return '<span class="label label-success create" title="编辑创建">编辑创建</span><span class="label label-danger delete" style="margin-left:5px;" title="删除草稿" >删除草稿</span>';
                            }



                            var isaAain = false;
                            if (wfType == '1') {
                                if (row.F_IsAgain == 1) {
                                    isaAain = true;
                                }
                                else if (row.F_IsFinished == 0) {
                                    // 加入催办和撤销按钮
                                    $cell.on('click', '.urge', function () {// 催办审核
                                        learun.layerConfirm('是否确认催办审核？', function (res, _index) {
                                            if (res) {
                                                learun.loading(true, '催办审核...');
                                                var postData = {
                                                    processId: row.F_Id,
                                                };
                                                learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/UrgeFlow', postData, function (data) {
                                                    learun.loading(false);
                                                });
                                                top.layer.close(_index);
                                            }
                                        });
                                        return false;
                                    });
                                    $cell.on('click', '.revoke', function () {// 删除
                                        learun.layerConfirm('是否确认撤销流程？', function (res, _index) {
                                            if (res) {

                                                learun.loading(true, '撤销流程...');
                                                var postData = {
                                                    processId: row.F_Id,
                                                };
                                                learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/RevokeFlow', postData, function (data) {
                                                    learun.loading(false);
                                                    refreshGirdData();
                                                });
                                                top.layer.close(_index);
                                            }
                                        });
                                        return false;
                                    });


                                    var _btnHtml = '<span class="label label-primary urge" title="催办审核" >催办审核</span>';
                                    if (row.F_IsStart == 0) {
                                        _btnHtml += '<span class="label label-warning revoke" style="margin-left:5px;" title="撤销流程" >撤销流程</span>';
                                    }
                                    return _btnHtml;
                                }
                                else {
                                    return '本人发起';
                                }
                            }
                            if (row.F_TaskType == 3) {
                                return "【加签】" + cellvalue;
                            }
                            else if (row.F_TaskType == 5 && wfType == '2') {
                                isaAain = true;
                            }
                            else if (row.F_TaskType == 5) {
                                return '重新发起';
                            }

                            if (isaAain) {
                                $cell.on('click', '.AgainCreate', function () {
                                    var title = "";
                                    if (row.F_SchemeName != row.F_Title && row.F_Title) {
                                        title = row.F_SchemeName + "(" + row.F_Title + ")";
                                    }
                                    else {
                                        title = row.F_SchemeName;
                                    }
                                    learun.frameTab.open({ F_ModuleId: row.F_Id, F_Icon: 'fa magic', F_FullName: '重新发起-' + title, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?processId=' + row.F_Id + '&tabIframeId=' + row.F_Id + '&type=againCreate' });
                                    return false;
                                });
                                return '<span class="label label-danger AgainCreate" title="重新发起">重新发起</span>';
                            }


                            if (wfType == '3' && row.F_TaskType == 1 && row.F_IsFinished == 0) {// 已完成任务，添加一个撤销按钮
                                cellvalue = '<span class="label label-warning revoke2" style="margin-left:5px;margin-right:5px;" title="撤销审核" >撤销审核</span>' + cellvalue;

                                $cell.on('click', '.revoke2', function () {// 删除
                                    learun.layerConfirm('是否确认撤销审核？', function (res, _index) {
                                        if (res) {

                                            learun.loading(true, '撤销审核...');
                                            var postData = {
                                                processId: row.F_Id,
                                                taskId: row.F_TaskId,
                                            };
                                            learun.httpAsync('Post', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/RevokeAudit', postData, function (data) {
                                                learun.loading(false);
                                                refreshGirdData();
                                            });
                                            top.layer.close(_index);
                                        }
                                    });
                                    return false;
                                });
                            }

                            return cellvalue;
                        }
                    },
                    {
                        label: "文件名", name: "FileName", width: 120, align: "left",
                    },
                    {
                        label: "文件编号", name: "FileCode", width: 120, align: "left",
                    },
                    {
                        label: "文件版本", name: "FileVer", width: 60, align: "center",
                    },
                    {
                        label: "等级", name: "F_Level", width: 60, align: "center",
                        formatter: function (cellvalue) {
                            switch (cellvalue) {
                                case 0:
                                    return '普通';
                                    break;
                                case 1:
                                    return '重要';
                                    break;
                                case 2:
                                    return '紧急';
                                    break;
                                default:
                                    return '普通';
                                    break;
                            }
                        }
                    },
                    {
                        label: "状态", name: "F_EnabledMark", width: 70, align: "center",
                        formatter: function (cellvalue, row) {
                            if (row.F_TaskType == 4) {
                                if (row.F_IsUrge == "1" && wfType == '2') {
                                    return "<span class=\"label label-danger\">催办加急</span>";
                                }
                                return "<span class=\"label label-success\">运行中</span>";
                            }
                            if (row.F_IsFinished == 0) {
                                if (cellvalue == 1) {
                                    if (row.F_IsUrge == "1" && wfType == '2') {
                                        return "<span class=\"label label-danger\">催办加急</span>";
                                    }
                                    return "<span class=\"label label-success\">运行中</span>";
                                } else if (cellvalue == 2) {
                                    return "<span class=\"label label-primary\">草稿</span>";
                                } else {
                                    return "<span class=\"label label-danger\">作废</span>";
                                }
                            }
                            else {
                                return "<span class=\"label label-warning\">结束</span>";
                            }

                        }
                    },
                    { label: "发起者", name: "F_CreateUserName", width: 70, align: "center" },
                    {
                        label: "时间", name: "F_CreateDate", width: 150, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    }
                ],
                mainId: 'F_Id',
                isPage: true,
                sidx: 'F_CreateDate DESC',
                onSelectRow: function (row) {
                    if (wfType == '2') {
                        if (row.F_TaskType == 5) {
                            $('#lr_verify span').text("重新发起");
                        }
                        else if (row.F_TaskType == 3) {
                            $('#lr_verify span').text("【加签】" + row.F_TaskName);
                        }
                        else {
                            $('#lr_verify span').text(row.F_TaskName);
                        }
                    }
                },
                dblclick: function () {
                    if (wfType == '2') {
                        page.verify();
                    }
                    else {
                        page.eye();
                    }
                }
            });
            $('#gridTable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/Files/GetPublishList',
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
                        label: '大小', name: 'F_FileSize', width: 100, align: 'center',
                        formatter: function (cellvalue, row) {
                            if (row.Type == '2') {
                                return '';
                            }
                            else {
                                return page.CountFileSize(cellvalue);
                            }
                        }
                    },
                    {
                        label: '操作', name: 'F_Id', width: 200, align: 'left',
                        formatter: function (value, row, op, $cell) {
                            var $div = $('<div></div>');
                            if (row.Type == '2') {
                                var $openFolder = $('<span class=\"label label-success\" style=\"cursor: pointer;\">打开</span>');
                                $openFolder.on('click', function () {
                                    folderId = value;
                                    folderAuth = row.F_AuthType;
                                    page.openFolder();
                                    var $span = $('<span data-fileid="' + value + '"  class="path-item">/&nbsp;' + row.F_Name + '</span>');
                                    $span[0]._data = row;
                                    $('#pathList').append($span);
                                });
                                $div.append($openFolder);

                            }
                            else {
                                var btnlist = row.F_AuthType.split(',');
                                $.each(btnlist, function (_index, _item) {
                                    if (_item != '6' && _item != '5' && $div.find('[data-value="' + _item + '"]').length == 0) {
                                        var btnClass = 'label-info';
                                        if (_item == '4') {
                                            btnClass = 'label-warning';
                                        }
                                        var $btn = $('<span class=\"label ' + btnClass +'\" data-value="' + _item + '" style=\"cursor: pointer; margin-right:8px;\">' + btnName[_item] + '</span>');
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
                                                case '2':// 上传
                                                    learun.frameTab.open({ F_ModuleId: btnRow.F_Id, F_Icon: 'fa magic', F_FullName: '上传文件审核', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?shcemeCode=lr_files_manager&tabIframeId=' + btnRow.F_Id + '&type=create&wfFormParam=' + btnRow.F_Id  });
                                                    break;
                                                case '3':// 下载
                                                    learun.download({ url: top.$.rootUrl + '/LR_SystemModule/Annexes/DownAnnexesFile', param: { fileId: btnRow.F_FileId}, method: 'POST' });
                                                    break;
                                                case '4':// 删除
                                                    learun.layerConfirm('是否确认删除该文件！', function (res) {
                                                        if (res) {
                                                            learun.deleteForm(top.$.rootUrl + '/LR_SystemModule/Files/VDeleteFile', { keyValue: btnRow.F_Id }, function () {
                                                                refreshGirdData();
                                                            });
                                                        }
                                                    });
                                                    break;
                                            }
                                        });
                                        $div.append($btn);
                                    }
                                   
                                });

                                // 查看
                                var $hbtn = $('<span class=\"label label-primary\" style=\"cursor: pointer;\">查看历史</span>');
                                $hbtn.on('click', function () {
                                    learun.layerForm({
                                        id: 'FileHistroyIndex',
                                        title: '文件历史【'+row.F_Name+'】',
                                        url: top.$.rootUrl + '/LR_SystemModule/Files/FileHistroyIndex?fileInfoId=' + row.F_Id ,
                                        width: 800,
                                        height: 500,
                                        maxmin: true,
                                        btn: null,
                                        end: function () {

                                        }
                                    });
                                });
                                $div.append($hbtn);
                            }

                            //console.log();

                            return $div;
                        }
                    }
                ],
                mainId: 'F_Id'
            });

            $('#dgridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/Files/GetDeleteList',
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
                                if (_item != '2' && _item != '4' && $div.find('[data-value="' + _item + '"]').length == 0) {
                                    var btnClass = 'label-info';
                                    if (_item == '6') {
                                        btnClass = 'label-danger';
                                    }
                                    else if (_item == '5') {
                                        btnClass = 'label-warning';
                                    }
                                    var $btn = $('<span class=\"label ' + btnClass +'\" data-value="' + _item + '" style=\"cursor: pointer; margin-right:8px;\">' + btnName[_item] + '</span>');
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
                                            case '5':// 复原
                                                learun.layerConfirm('是否确认还原该文件！', function (res) {
                                                    if (res) {
                                                        learun.postForm(top.$.rootUrl + '/LR_SystemModule/Files/RecoveryFile', { keyValue: btnRow.F_Id }, function () {
                                                            refreshGirdData();
                                                        });
                                                    }
                                                });
                                                break;
                                            case '6':// 彻底删除
                                                learun.layerConfirm('是否确认彻底删除该文件！', function (res) {
                                                    if (res) {
                                                        learun.deleteForm(top.$.rootUrl + '/LR_SystemModule/Files/DeleteFile', { keyValue: btnRow.F_Id }, function () {
                                                            refreshGirdData();
                                                        });
                                                    }
                                                });
                                                break;
                                        }
                                    });
                                    $div.append($btn);
                                }

                            });

                            // 查看
                            var $hbtn = $('<span class=\"label label-primary\" style=\"cursor: pointer;\">查看历史</span>');
                            $hbtn.on('click', function () {
                                learun.layerForm({
                                    id: 'FileHistroyIndex',
                                    title: '文件历史【' + row.F_Name + '】',
                                    url: top.$.rootUrl + '/LR_SystemModule/Files/FileHistroyIndex?fileInfoId=' + row.F_Id,
                                    width: 800,
                                    height: 500,
                                    maxmin: true,
                                    btn: null,
                                    end: function () {

                                    }
                                });
                            });
                            $div.append($hbtn);
                            return $div;
                        }
                    }
                ],
                mainId: 'F_Id'
            });


            page.search.releaseFile();
        },
        search: {
            verifyFile: function (param) {
                param = param || {};
                param.wfType = wfType;
                $('#wfgridtable').jfGridSet('reload', param);
            },
            releaseFile: function (param) {
                param = param || {};
                param.folderId = folderId || "0";

                if (param.folderId == "0") {
                    $('#pathList [data-fileid="0"]').nextAll().remove();
                }

                $('#gridTable').jfGridSet('reload', param);
            },
            recycledFile: function (param) {
                param = param || {};
                $('#dgridtable').jfGridSet('reload', param);
            }
        },
        search2: {
            verifyFile: function () {
                $('#wfgridtable').jfGridSet('reload');
            },
            releaseFile: function () {
                $('#gridTable').jfGridSet('reload');
            },
            recycledFile: function () {
                $('#dgridtable').jfGridSet('reload');
            }
        },
        verify: function () {
            var processId = $('#wfgridtable').jfGridValue('F_Id');
            var taskId = $('#wfgridtable').jfGridValue('F_TaskId');
            var title = $('#wfgridtable').jfGridValue('F_Title');
            var schemeName = $('#wfgridtable').jfGridValue('F_SchemeName');
            var taskName = $('#wfgridtable').jfGridValue('F_TaskName');
            var taskType = $('#wfgridtable').jfGridValue('F_TaskType');

            if (schemeName != title && title) {
                title = schemeName + "(" + title + ")";
            }
            else {
                title = schemeName;
            }

            //1审批2传阅3加签4子流程5重新创建
            switch (taskType) {
                case 1:// 审批
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '审批-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=audit' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
                case 2:// 传阅
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '查阅-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=refer' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
                case 3:// 加签
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '加签审核-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=signAudit' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
                case 4:// 子流程
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '子流程-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=chlid' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
                case 5:// 重新创建
                    learun.frameTab.open({ F_ModuleId: processId, F_Icon: 'fa magic', F_FullName: '重新发起-' + title, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?processId=' + processId + '&tabIframeId=' + processId + '&type=againCreate' });
                    break;
                case 6:// 重新创建
                    learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '子流程-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=againChild' + "&processId=" + processId + "&taskId=" + taskId });
                    break;
            }
        },
        eye: function () {
           

            var processId = $('#wfgridtable').jfGridValue('F_Id') || '';
            var taskId = $('#wfgridtable').jfGridValue('F_TaskId') || '';
            var title = $('#wfgridtable').jfGridValue('FileName');
            var taskType = $('#wfgridtable').jfGridValue('F_TaskType');


            var enabledMark = $('#wfgridtable').jfGridValue('F_EnabledMark');
            if (enabledMark == 2) {// 草稿不允许查看进度
                learun.alert.warning("草稿不能查看进度");
                return;
            } 
            if (learun.checkrow(processId)) {
                if (taskType == '4' || taskType == '6') {
                    learun.frameTab.open({ F_ModuleId: processId + taskId, F_Icon: 'fa magic', F_FullName: '查看流程进度【' + title + '】', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + processId + taskId + '&type=childlook' + "&processId=" + processId + "&taskId=" + taskId });
                }
                else {
                    learun.frameTab.open({ F_ModuleId: processId + taskId, F_Icon: 'fa magic', F_FullName: '查看流程进度【' + title + '】', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + processId + taskId + '&type=look' + "&processId=" + processId + "&taskId=" + taskId });
                }
            }
        },

        openFolder: function () {
            page.search.releaseFile();
        },

        //计算文件大小函数(保留两位小数),Size为字节大小
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
        //保留两位小数
        //功能：将浮点数四舍五入，取小数点后2位
        ToDecimal: function (x) {
            var f = parseFloat(x);
            if (isNaN(f)) {
                return 0;
            }
            f = Math.round(x * 100) / 100;
            return f;
        },




    };

    refreshGirdData = function () {
        page.search2[tabType]();
    };

    page.init();
}