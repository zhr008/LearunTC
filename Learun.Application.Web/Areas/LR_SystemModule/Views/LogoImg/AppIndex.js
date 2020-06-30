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
            page.initData();
            page.bind();
        },
        bind: function () {
            function uploadImg() {
                var f = document.getElementById('uploadFile').files[0]
                var src = window.URL.createObjectURL(f);
                document.getElementById('uploadPreview').src = src;
            };

            $('#uploadFile').on('change', uploadImg);

            $('#lr_save_btn').on('click', function () {
                var f = document.getElementById('uploadFile').files[0];
                if (!!f) {
                    learun.loading(true, '正在保存...');
                    $.ajaxFileUpload({
                        url: top.$.rootUrl + "/LR_SystemModule/LogoImg/UploadFile?code=applogo",
                        secureuri: false,
                        fileElementId: 'uploadFile',
                        dataType: 'json',
                        success: function (data) {
                            learun.loading(false);
                            $('#uploadFile').on('change', uploadImg);
                            if (data.code == 200) {
                                learun.alert.success('保存成功');
                            }
                        }
                    });
                }
            });
        },
        initData: function () {
            $('.file').prepend('<img id="uploadPreview"  src="' + top.$.rootUrl + '/LR_SystemModule/LogoImg/GetImg?code=applogo" >');
        }
    };
    page.init();
}