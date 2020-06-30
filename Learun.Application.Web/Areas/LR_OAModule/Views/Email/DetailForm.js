/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.06.05
 * 描 述：写邮件	
 */
var keyValue = request('keyValue');

var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_Files').lrUploader({ isUpload: false });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_OAModule/Email/GetEntity?keyValue=' + keyValue, function (data) {//
                    console.log(data);
                    $('#F_Theme').val(data.F_Theme);
                    $('#F_SenderName').val(data.F_SenderName);
                    
                    $('#F_Files').lrUploaderSet(data.F_Files);
                    $('#F_SenderTime').val(learun.formatDate(data.F_SenderTime, 'yyyy-MM-dd hh:mm:ss'));
                    var loginInfo = learun.clientdata.get(['userinfo']);
                    if (data.F_BccsendHtml.indexOf(loginInfo.userId) == -1) {
                        learun.clientdata.getsAsync('user', {
                            key: data.F_AddresssHtml,
                            callback: function (name) {
                                $('#addresssIds').val(name);
                            }
                        });
                        if (data.F_CopysendHtml == '') {
                            $('#copysendIds').parent().remove();
                        }
                        learun.clientdata.getsAsync('user', {
                            key: data.F_CopysendHtml,
                            callback: function (name) {
                                $('#copysendIds').val(name);
                            }
                        });
                    }
                    else {
                        $('#copysendIds').parent().remove();
                        $('#addresssIds').val(loginInfo.realName);
                    }

                    var arrEntities = { 'lt': '<', 'gt': '>', 'nbsp': ' ', 'amp': '&', 'quot': '"' };
                    var str = data.F_EmailContent.replace(/&(lt|gt|nbsp|amp|quot);/ig, function (all, t) { return arrEntities[t]; });
                    $('#F_EmailContent').html(str);

                   
                });
            }
        }
    };
    page.init();
}