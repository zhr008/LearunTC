/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开 发 框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软 信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.05.24
 * 描 述：lr-uploader 表单附件选择插件
 */
(function ($, learun) {
    "use strict";

    $.lrUploader = {
        init: function ($self) {
            var dfop = $self[0]._lrUploader.dfop;
            $.lrUploader.initRender($self, dfop);
        },
        initRender: function ($self, dfop) {
            $self.attr('type', 'lr-Uploader').addClass('lrUploader-wrap');
            var $wrap = $('<div class="lrUploader-input" ></div>');

            var $btnGroup = $('<div class="lrUploader-btn-group"></div>');
            var $uploadBtn = $('<a id="lrUploader_uploadBtn_' + dfop.id + '" class="btn btn-success lrUploader-input-btn">上传</a>');
            var $downBtn = $('<a id="lrUploader_downBtn_' + dfop.id + '" class="btn btn-danger lrUploader-input-btn">下载</a>');
            var $viewBtn = $('<a id="lrUploader_viewBtn_' + dfop.id + '" class="btn btn-primary lrUploader-input-btn">预览</a>');

            $self.append($wrap);
            var w = 0;
            if (dfop.isUpload) {
                $btnGroup.append($uploadBtn);
                w += 57;
            }
            if (dfop.isDown) {
                $btnGroup.append($downBtn);
                w += 57;
            }
            if (dfop.isView) {
                $btnGroup.append($viewBtn);
                w += 57;
            }
            $uploadBtn.on('click', $.lrUploader.openUploadForm);
            $downBtn.on('click', $.lrUploader.openDownForm);
            $viewBtn.on('click', $.lrUploader.openViewForm);

            $self.append($btnGroup);
            $self.css({ 'padding-right': w});

        },
        openUploadForm: function () {
            var $btn = $(this);
            var $self = $btn.parents('.lrUploader-wrap');
            var dfop = $self[0]._lrUploader.dfop;
            learun.layerForm({
                id: dfop.id,
                title: dfop.placeholder,
                url: top.$.rootUrl + '/LR_SystemModule/Annexes/UploadForm?keyVaule=' + dfop.value + "&extensions=" + dfop.extensions + "&filePath=" + dfop.filePath,
                width: 600,
                height: 400,
                maxmin: true,
                btn: null,
                end: function () {
                    learun.httpAsyncGet(top.$.rootUrl + '/LR_SystemModule/Annexes/GetFileNames?folderId=' + dfop.value, function (res) {
                        if (res.code == learun.httpCode.success) {
                            $('#' + dfop.id).find('.lrUploader-input').text(res.info);
                        }
                    });
                }
            });
        },
        openDownForm: function () {
            var $btn = $(this);
            var $self = $btn.parents('.lrUploader-wrap');
            var dfop = $self[0]._lrUploader.dfop;
            learun.layerForm({
                id: dfop.id,
                title: dfop.placeholder,
                url: top.$.rootUrl + '/LR_SystemModule/Annexes/DownForm?keyVaule=' + dfop.value,
                width: 600,
                height: 400,
                maxmin: true,
                btn: null
            });
        },
        openViewForm: function () {
            var $btn = $(this);
            var $self = $btn.parents('.lrUploader-wrap');
            var dfop = $self[0]._lrUploader.dfop;
            learun.layerForm({
                id: 'PreviewForm',
                title: '文件预览',
                url: top.$.rootUrl + '/LR_SystemModule/Annexes/PreviewFile?fileId=' + dfop.value,
                width: 1080,
                height: 850,
                btn: null
            });
        }
    };

    $.fn.lrUploader = function (op) {
        var $this = $(this);
        if (!!$this[0]._lrUploader) {
            return $this;
        }
        var dfop = {
            placeholder: '上传附件',
            isUpload: true,
            isDown: true,
            isView: true,
            extensions: '',
            filePath: ''//上传路径（配置文件）
        }

        $.extend(dfop, op || {});
        dfop.id = $this.attr('id');
        dfop.value = learun.newGuid();

        $this[0]._lrUploader = { dfop: dfop };
        $.lrUploader.init($this);
    };

    $.fn.lrUploaderSet = function (value) {
        if (value == null || value == 'null' || value == undefined || value == 'undefined' || value == '') {
            return;
        }

        var $self = $(this);
        var dfop = $self[0]._lrUploader.dfop;
        dfop.value = value;
        learun.httpAsyncGet(top.$.rootUrl + '/LR_SystemModule/Annexes/GetFileNames?folderId=' + dfop.value, function (res) {
            if (res.code == learun.httpCode.success) {
                $('#' + dfop.id).find('.lrUploader-input').text(res.info);
            }
        });
    }

    $.fn.lrUploaderGet = function () {
        var $this = $(this);
        var dfop = $this[0]._lrUploader.dfop;
        return dfop.value;
    }
})(jQuery, top.learun);