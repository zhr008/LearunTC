/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.07.30
 * 描 述：移动端logo设置
 */
var loaddfimg;
var baseinfo;
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            $('.lr-rblock').lrscroll();
            page.initData();
            page.bind();
        },
        bind: function () {
            function uploadImg(id, previewId) {//uploadFile//uploadPreview
                var f = document.getElementById(id).files[0]
                var src = window.URL.createObjectURL(f);
                document.getElementById(previewId).src = src;
            };

            $('#uploadFile').on('change', function () {
                uploadImg('uploadFile', 'uploadPreview');
            });
            $('#uploadFile1').on('change', function () {
                uploadImg('uploadFile1', 'uploadPreview1');
            });
            $('#uploadFile2').on('change', function () {
                uploadImg('uploadFile2', 'uploadPreview2');
            });
            $('#uploadFile3').on('change', function () {
                uploadImg('uploadFile3', 'uploadPreview3');
            });

            $('#lr_save_btn').on('click', function () {
                var f = document.getElementById('uploadFile').files[0];
                if (!!f) {
                    learun.loading(true, '正在保存...');
                    $.ajaxFileUpload({
                        url: top.$.rootUrl + "/LR_SystemModule/LogoImg/UploadFile?code=default",
                        secureuri: false,
                        fileElementId: 'uploadFile',
                        dataType: 'json',
                        success: function (data) {
                            learun.loading(false);
                            $('#uploadFile').on('change', function () {
                                uploadImg('uploadFile', 'uploadPreview');
                            });
                            if (data.code == 200) {
                                learun.alert.success('保存成功');
                            }
                        }
                    });
                }
            });
            $('#lr_save_btn1').on('click', function () {
                var f = document.getElementById('uploadFile1').files[0];
                if (!!f) {
                    learun.loading(true, '正在保存...');
                    $.ajaxFileUpload({
                        url: top.$.rootUrl + "/LR_SystemModule/LogoImg/UploadFile?code=accordion",
                        secureuri: false,
                        fileElementId: 'uploadFile1',
                        dataType: 'json',
                        success: function (data) {
                            learun.loading(false);
                            $('#uploadFile1').on('change', function () {
                                uploadImg('uploadFile1', 'uploadPreview1');
                            });
                            if (data.code == 200) {
                                learun.alert.success('保存成功');
                            }
                        }
                    });
                }
            });
            $('#lr_save_btn2').on('click', function () {
                var f = document.getElementById('uploadFile2').files[0];
                if (!!f) {
                    learun.loading(true, '正在保存...');
                    $.ajaxFileUpload({
                        url: top.$.rootUrl + "/LR_SystemModule/LogoImg/UploadFile?code=windows",
                        secureuri: false,
                        fileElementId: 'uploadFile2',
                        dataType: 'json',
                        success: function (data) {
                            learun.loading(false);
                            $('#uploadFile2').on('change', function () {
                                uploadImg('uploadFile2', 'uploadPreview2');
                            });
                            if (data.code == 200) {
                                learun.alert.success('保存成功');
                            }
                        }
                    });
                }
            });
            $('#lr_save_btn3').on('click', function () {
                var f = document.getElementById('uploadFile3').files[0];
                if (!!f) {
                    learun.loading(true, '正在保存...');
                    $.ajaxFileUpload({
                        url: top.$.rootUrl + "/LR_SystemModule/LogoImg/UploadFile?code=top",
                        secureuri: false,
                        fileElementId: 'uploadFile3',
                        dataType: 'json',
                        success: function (data) {
                            learun.loading(false);
                            $('#uploadFile3').on('change', function () {
                                uploadImg('uploadFile3', 'uploadPreview2');
                            });
                            if (data.code == 200) {
                                learun.alert.success('保存成功');
                            }
                        }
                    });
                }
            });
        },
        initData: function () {
            $('#file').prepend('<img id="uploadPreview"  src="' + top.$.rootUrl + '/LR_SystemModule/LogoImg/GetImg?code=default" >');
            $('#file1').prepend('<img id="uploadPreview1"  src="' + top.$.rootUrl + '/LR_SystemModule/LogoImg/GetImg?code=accordion" >');
            $('#file2').prepend('<img id="uploadPreview2"  src="' + top.$.rootUrl + '/LR_SystemModule/LogoImg/GetImg?code=windows" >');
            $('#file3').prepend('<img id="uploadPreview3"  src="' + top.$.rootUrl + '/LR_SystemModule/LogoImg/GetImg?code=top" >');
            
        }
    };
    page.init();
}