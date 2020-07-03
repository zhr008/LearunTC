/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：个人中心	
 */
var baseinfo;

var F_PersonId = request('F_PersonId');
var F_IDCardNo = request('F_IDCardNo');
var F_UserName = request('F_UserName');
var F_ApplicantId = request('F_ApplicantId');
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.initleft();
            page.bind();
            //page.initData();
        },
        bind: function () {

        },
        initleft: function () {
            $('#lr_left_list li').on('click', function () {
                debugger
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    var $parent = $this.parent();
                    $parent.find('.active').removeClass('active');
                    $this.addClass('active');
                    var _type = $this.attr('data-value');
                    $('.lr-layout-wrap-item').removeClass('active');
                    $('#lr_layout_item' + _type).addClass('active');

                    var baseUrl = "?F_PersonId=" + F_PersonId + "&F_IDCardNo=" + F_IDCardNo + "&F_UserName=" + F_UserName + "&F_ApplicantId=" + F_ApplicantId + "&ParentDisable=" + true;

                    switch (_type) {
                        case '2':
                            $("#lr_iframe_item2").attr("src", "/LR_CodeDemo/IDCard/Index" + baseUrl)
                            break;
                        case '3':
                            $("#lr_iframe_item3").attr("src", "/LR_CodeDemo/Credentials/Index" + baseUrl)
                            break;
                        case '4':
                            $("#lr_iframe_item4").attr("src", "/LR_CodeDemo/GradCert/Index" + baseUrl)
                            break;
                        case '5':
                            $("#lr_iframe_item5").attr("src", "/LR_CodeDemo/IDCard/Index" + baseUrl)
                            break;
                        case '6':
                            $("#lr_iframe_item6").attr("src", "/LR_CodeDemo/IDCard/Index" + baseUrl)
                            break;
                        case '7':
                            $("#lr_iframe_item7").attr("src", "/LR_CodeDemo/IDCard/Index" + baseUrl)
                            break;
                    }

                }
            });
        },
        initData: function () {
            learun.httpAsyncGet(top.$.rootUrl + '/UserCenter/GetUserInfo', function (res) {
                if (res.code == 200) {
                    baseinfo = res.data.baseinfo;
                    /*基础信息*/
                    $('#F_Account').val(baseinfo.account);
                    $('#F_EnCode').val(baseinfo.enCode);
                    $('#F_RealName').val(baseinfo.realName);
                    $('#F_Gender').val(baseinfo.gender == 0 ? '女' : '男');
                    learun.clientdata.getAsync('company', {
                        key: baseinfo.companyId,
                        callback: function (_data) {
                            $('#F_Company').val(_data.name);
                        }
                    });

                    learun.clientdata.getAsync('department', {
                        key: baseinfo.departmentId,
                        callback: function (_data) {
                            $('#F_Department').val(_data.name);
                        }
                    });
                    var post = [], role = [];
                    $.each(res.data.post, function (id, item) {
                        post.push(item.F_Name);
                    });
                    $.each(res.data.role, function (id, item) {
                        role.push(item.F_FullName);
                    });
                    $('#Post').val(String(post));
                    $('#Role').val(String(role));
                    $('#F_Description').val(baseinfo.description);
                }
                else {
                    learun.alert.error('数据加载失败');
                }
            });


        }
    };



    page.init();
}


