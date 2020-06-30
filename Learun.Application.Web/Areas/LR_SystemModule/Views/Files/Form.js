// 设置权限
var setAuthorize;
// 设置表单数据
var setFormData;
// 验证数据是否填写完整
var validForm;
// 保存数据
var save;

var isUpdate = false;

var bootstrap = function ($, learun) {
    "use strict";

    var fileInfoId = '';

    // 设置权限
    setAuthorize = function (data, isLook) {
        $('#F_Folder').lrselect({
            url: top.$.rootUrl + '/LR_SystemModule/Files/GetFolderTree',
            type: 'tree',
            allowSearch: true,
            maxHeight: 225,
            select: function (item) {
                if (item) {
                    if (item.id == '-1') {
                        learun.alert.warning("请选择文件夹！");
                    }
                    else {
                        if (!$('#F_Folder')[0]._setflag) {
                            if (item.checkstate != 2) {
                                learun.alert.warning("当前文件夹没有写入权限请重新选择！");
                                $('#F_Folder').lrselectSet("");
                            }
                        } else {
                            var text = $('#F_Folder .lr-select-placeholder').text().replace('【无权限】', '');
                            $('#F_Folder .lr-select-placeholder').text(text);
                            $('#F_Folder')[0]._setflag = false;
                        }
                    }
                }
            }
        });
        if (isLook) {
            $('input,#F_Folder').attr('readonly', 'readonly');
            $('#F_FileId').lrUploader({ isDown: true, isView: false, isUpload: false });
            $('#F_PFiled').lrUploader({ isDown: false, isView: true, isUpload: false });
        }
        else {
            for (var i = 0, l = data.length; i < l; i++) {
                var field = data[i];
                if (field.isLook != 1) {// 如果没有查看权限就直接移除
                    $('#' + field.fieldId).parent().remove();
                }
                else {
                    if (field.isEdit != 1) {
                        $('#' + field.fieldId).attr('disabled', 'disabled');
                        if ($('#' + field.fieldId).hasClass('lrUploader-wrap')) {
                            $('#' + field.fieldId).css({ 'padding-right': '58px' });
                            $('#' + field.fieldId).find('.btn-success').remove();
                        }
                    }
                }
            }

            $('#F_FileId').lrUploader({ isDown: false, isView: false });
            $('#F_PFiled').lrUploader({ isDown: false, isView: false });
        }



       
        
    };
    // 设置表单数据
    setFormData = function (processId, _fileInfoId, callback, userId) {
        if (processId) {
            fileInfoId = _fileInfoId;
            $.lrSetForm(top.$.rootUrl + '/LR_SystemModule/Files/GetFileInfoByWF?processId=' + processId + "&fileInfoId=" + _fileInfoId, function (data) {//
                console.log(data);
                $('#F_Code').val(data.code);
                if (data.fileInfoEntity != null) {
                    data.fileInfoEntity.F_Folder = null;
                    $('#form').lrSetFormData(data.fileInfoEntity);
                }
                if (data.fileListEntity != null) {
                    isUpdate = true;
                    fileInfoId = data.fileListEntity.F_FileInfoId;
                    $('#F_FileId').lrUploaderSet(data.fileListEntity.F_FileId);
                    $('#F_PFiled').lrUploaderSet(data.fileListEntity.F_PFiled);

                    $('#F_Name').val(data.fileListEntity.F_Name);
                    if (data.fileListEntity.F_Folder != '' || data.fileListEntity.F_Folder != null)
                    {
                        $('#F_Folder')[0]._setflag = true;
                        $('#F_Folder').lrselectSet(data.fileListEntity.F_Folder);
                    }
                   
                   

                    $('#F_KeyWord').val(data.fileListEntity.F_KeyWord);
                }
                $('#F_Ver').val(data.ver);
                setPbtn();
                callback();

            });
        }
        else {
            setPbtn();
            callback();
        }
    };
    // 验证数据是否填写完整
    validForm = function (code) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        if (code == 'create') {
            if ($('#F_FileId').find('.lrUploader-input').text() == '') {
                learun.alert.error("请上传【原始文件】");
                return false;
            }
            if ($('#F_PFiled').find('.lrUploader-input').text() == '') {
                learun.alert.error("请上传【预览文件】");
                return false;
            }
        }
       
        return true;
    };
    // 保存调用函数
    save = function (processId, callBack, i) {
        var keyValue = "";
        
        if (isUpdate) {
            keyValue = processId;
        }

        var formData = $('#form').lrGetFormData(fileInfoId);

        var infoEntity = {
            F_Id: fileInfoId,
            F_Code: formData.F_Code,
            //F_Name: formData.F_Name,
            //F_KeyWord: formData.F_KeyWord,
            //F_Folder: formData.F_Folder || '0'
        }


        var listEntity = {
            F_Id: processId,
            F_FileId: formData.F_FileId,
            F_PFiled: formData.F_PFiled,
            F_Ver: formData.F_Ver,

            F_Name: formData.F_Name,
            F_KeyWord: formData.F_KeyWord,
            F_Folder: formData.F_Folder || '0'
        }

        if (listEntity.F_Folder == '&nbsp;') {
            listEntity.F_Folder = '0';
        }


        var postData = {
            strInfoEntity: JSON.stringify(infoEntity),
            strListEntity: JSON.stringify(listEntity)
        };


        $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/Files/SaveFile?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack(res, i);
            }
        });
    };


    // 设置父级窗口的按钮
    function setPbtn() {
        $('#release', window.parent.document).text('提交文档审核');
        $('#savedraft', window.parent.document).hide();
        //$('#print', window.parent.document).hide();
        
    }
   
}