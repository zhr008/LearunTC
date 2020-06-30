/*
 * 版 本 Learun-ADMS V7.0.6 力软 敏捷 开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力 软- 前端开发组
 * 日 期：2017.03.16
 * 描 述：admin顶层页面操作方法
 */

var loaddfimg;
(function ($, learun) {
    "use strict";
    
    var page = {
        init: function () {
            /*判断当前浏览器是否是IE浏览器*/
            if ($('body').hasClass('IE') || $('body').hasClass('InternetExplorer')) {
                $('#lr_loadbg').append('<img imgback="imgjk" src="' + top.$.rootUrl + '/Content/images/ie-loader.gif" style="position: absolute;top: 0;left: 0;right: 0;bottom: 0;margin: auto;vertical-align: middle;">');
                Pace.stop();
            }
            else {
                Pace.on('done', function () {
                    $('#lr_loadbg').fadeOut();
                    Pace.options.target = '#learunpacenone';
                });
            }
            // 通知栏插件初始化设置
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": true,
                "progressBar": false,
                "positionClass": "toast-top-center",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "3000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
            // 打开首页模板
            learun.frameTab.open({ F_ModuleId: '0', F_Icon: 'fa fa-desktop', F_FullName: '我的桌面', F_UrlAddress: '/Home/AdminDesktop' }, true);
            learun.frameTab.open({ F_ModuleId: 'AdminDesktopTemp1', F_Icon: 'fa fa-desktop', F_FullName: '首页', F_UrlAddress: '/Home/AdminDesktopTemp' }, true);
            learun.frameTab.open({ F_ModuleId: 'DesktopIndex', F_Icon: 'fa fa-desktop', F_FullName: '热门功能', F_UrlAddress: '/Home/DesktopIndex' }, true);
            

            learun.clientdata.init(function () {
                page.userInit();
                // 初始页面特例
                bootstrap($, learun);
                if ($('body').hasClass('IE') || $('body').hasClass('InternetExplorer')) {
                    $('#lr_loadbg').fadeOut();
                }

                // 3.具体某一个页面
                var moduleCode = request("moduleCode");
                if (moduleCode) {
                    var modulesCodeMap = learun.clientdata.get(['modulesCodeMap']);
                    if (modulesCodeMap[moduleCode]) {
                        learun.frameTab.open(modulesCodeMap[moduleCode]);
                    }
                }

                // 4.具体某一个页面
                var moduleId = request("moduleId");
                if (moduleId) {
                    var modulesMap = learun.clientdata.get(['modulesMap']);
                    if (modulesMap[moduleId]) {
                        learun.frameTab.open(modulesMap[moduleId]);
                    }
                }
            });

            // 加载数据进度
            page.loadbarInit();
            // 全屏按钮
            page.fullScreenInit();
            // 主题选择初始化
            page.uitheme();


            // 监听页面传递参数：支持打开流程页面
            // 1.流程发起
            var _schemeCode = request("schemeCode");
            if (_schemeCode) {
                learun.frameTab.open({ F_ModuleId: _schemeCode, F_Icon: 'fa magic', F_FullName: '创建流程', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?shcemeCode=' + _schemeCode + '&tabIframeId=' + _schemeCode + '&type=create' });
            }
            // 2.流程审核
            var _taskId = request("taskId");
            if (_taskId) {
                // 获取下流程任务
                learun.httpAsync('GET', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/GetTask', { taskId: _taskId }, function (data) {
                    if (data) {
                        //1审批2传阅3加签4子流程5重新创建
                        switch (data.F_Type) {
                            case 1:// 审批
                                learun.frameTab.open({ F_ModuleId: _taskId, F_Icon: 'fa magic', F_FullName: '审批-' + data.F_NodeName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + _taskId + '&type=audit' + "&processId=" + data.F_ProcessId + "&taskId=" + _taskId });
                                break;
                            case 2:// 传阅
                                learun.frameTab.open({ F_ModuleId: _taskId, F_Icon: 'fa magic', F_FullName: '查阅-' + data.F_NodeName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + _taskId + '&type=refer' + "&processId=" + data.F_ProcessId + "&taskId=" + _taskId });
                                break;
                            case 3:// 加签
                                learun.frameTab.open({ F_ModuleId: _taskId, F_Icon: 'fa magic', F_FullName: '加签审核-' + data.F_NodeName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + _taskId + '&type=signAudit' + "&processId=" + data.F_ProcessId + "&taskId=" + _taskId });
                                break;
                            case 4:// 子流程
                                learun.frameTab.open({ F_ModuleId: _taskId, F_Icon: 'fa magic', F_FullName: '子流程-' + data.F_NodeName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + _taskId + '&type=chlid' + "&processId=" + data.F_ProcessId + "&taskId=" + _taskId });
                                break;
                            case 5:// 重新创建
                                learun.frameTab.open({ F_ModuleId: data.F_ProcessId, F_Icon: 'fa magic', F_FullName: '重新发起', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?processId=' + data.F_ProcessId + '&tabIframeId=' + data.F_ProcessId + '&type=againCreate' });
                                break;
                            case 6:// 重新创建
                                learun.frameTab.open({ F_ModuleId: _taskId, F_Icon: 'fa magic', F_FullName: '子流程-' + data.F_NodeName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + _taskId + '&type=againChild' + "&processId=" + data.F_ProcessId + "&taskId=" + _taskId });
                                break;
                        }
                    }
                });
            }
          
        },

        // 登录头像和个人设置
        userInit: function () {
            var loginInfo = learun.clientdata.get(['userinfo']);
            var _html = '<div class="lr-frame-personCenter"><a href="javascript:void(0);" class="dropdown-toggle" data-toggle="dropdown">';
            _html += '<img id="userhead"src="' + top.$.rootUrl + '/LR_OrganizationModule/User/GetImg?userId=' + loginInfo.userId + '" >';
            _html += '<span>' + loginInfo.realName + '</span>';
            _html += '</a>';
            _html += '<ul class="dropdown-menu pull-right">';
            _html += '<li><a href="javascript:void(0);" id="lr_userinfo_btn"><i class="fa fa-user"></i><span>个人信息</span></a></li>';
            _html += '<li><a href="javascript:void(0);" id="lr_schedule_btn"><i class="fa fa-calendar"></i><span>我的日程</span></a></li>';
            if (loginInfo.isSystem) {
                _html += '<li><a href="javascript:void(0);" id="lr_clearredis_btn"><i class="fa fa-refresh"></i><span>清空缓存</span></a></li>';
            }
            _html += '<li><a href="javascript:void(0);" id="lr_loginout_btn"><i class="fa fa-power-off"></i><span>安全退出</span></a></li>';
            _html += '</ul></div>';
            $('body').append(_html);

            // 语言包翻译
            $('.lr-frame-personCenter span').each(function () {
                var $this = $(this);
                var text = $this.text();
                learun.language.get(text, function (text) {
                    $this.text(text);
                });
            });

            $('#lr_loginout_btn').on('click', page.loginout);
            $('#lr_userinfo_btn').on('click', page.openUserCenter);
            $('#lr_clearredis_btn').on('click', page.clearredis);
        },
        loginout: function () { // 安全退出
            learun.layerConfirm("注：您确定要安全退出本次登录吗？", function (r) {
                if (r) {
                    learun.loading(true, '退出系统中...');
                    learun.httpAsyncPost($.rootUrl + '/Login/OutLogin', {}, function (data) {
                        window.location.href = $.rootUrl + "/Login/Index";
                    });
                }
            });
        },
        clearredis: function () {
            learun.layerConfirm("注：您确定要清空全部后台缓存数据吗？", function (r) {
                if (r) {
                    learun.loading(true, '清理缓存数据中...');
                    learun.httpAsyncPost($.rootUrl + '/Home/ClearRedis', {}, function (data) {
                        window.location.href = $.rootUrl + "/Login/Index";
                    });
                }
            });
        },
        openUserCenter: function () {
            // 打开个人中心
            learun.frameTab.open({ F_ModuleId: '1', F_Icon: 'fa fa-user', F_FullName: '个人中心', F_UrlAddress: '/UserCenter/Index' });
        },

        // 全屏按钮
        fullScreenInit: function () {
            var _html = '<div class="lr_frame_fullscreen"><a href="javascript:void(0);" id="lr_fullscreen_btn" title="全屏"><i class="fa fa-arrows-alt"></i></a></div>';
            $('body').append(_html);
            $('#lr_fullscreen_btn').on('click', function () {
                if (!$(this).attr('fullscreen')) {
                    $(this).attr('fullscreen', 'true');
                    page.requestFullScreen();
                } else {
                    $(this).removeAttr('fullscreen');
                    page.exitFullscreen();
                }
            });
        },
        requestFullScreen: function () {
            var de = document.documentElement;
            if (de.requestFullscreen) {
                de.requestFullscreen();
            } else if (de.mozRequestFullScreen) {
                de.mozRequestFullScreen();
            } else if (de.webkitRequestFullScreen) {
                de.webkitRequestFullScreen();
            }
        },
        exitFullscreen: function () {
            var de = document;
            if (de.exitFullscreen) {
                de.exitFullscreen();
            } else if (de.mozCancelFullScreen) {
                de.mozCancelFullScreen();
            } else if (de.webkitCancelFullScreen) {
                de.webkitCancelFullScreen();
            }
        },

        // 加载数据进度
        loadbarInit: function () {
            var _html = '<div class="lr-loading-bar" id="lr_loading_bar" >';
            _html += '<div class="lr-loading-bar-bg"></div>';
            _html += '<div class="lr-loading-bar-message" id="lr_loading_bar_message"></div>';
            _html += '</div>';
            $('body').append(_html);
        },

        // 皮肤主题设置
        uitheme: function () {
            var uitheme = top.$.cookie('Learn_ADMS_V7_UItheme') || '5';
            var $setting = $('<div class="lr-theme-setting"></div>');
            var $btn = $('<button class="btn btn-default"><i class="fa fa-spin fa-gear"></i></button>');
            var _html = '<div class="panel-heading lrlg">界面风格</div>';
            _html += '<div class="panel-body">';
            _html += '<div><label><input type="radio" name="ui_theme" value="1" ' + (uitheme == '1' ? 'checked' : '') + '><span class="lrlg" >经典版</span></label></div>';
            _html += '<div><label><input type="radio" name="ui_theme" value="2" ' + (uitheme == '2' ? 'checked' : '') + '><span class="lrlg" >风尚版</span></label></div>';
            _html += '<div><label><input type="radio" name="ui_theme" value="3" ' + (uitheme == '3' ? 'checked' : '') + '><span class="lrlg" >炫动版</span></label></div>';
            _html += '<div><label><input type="radio" name="ui_theme" value="4" ' + (uitheme == '4' ? 'checked' : '') + '><span class="lrlg" >飞扬版</span></label></div>';
            _html += '<div><label><input type="radio" name="ui_theme" value="5" ' + (uitheme == '5' ? 'checked' : '') + '><span class="lrlg" >主题五</span></label></div>';
            _html += '</div>';
            $setting.append($btn);
            $setting.append(_html);
            $('body').append($setting);

            $('.lr-theme-setting .lrlg').each(function () {
                var $this = $(this);
                var text = $this.text();
                learun.language.get(text, function (text) {
                    $this.text(text);
                });
            });


            $btn.on('click', function () {
                var $parent = $(this).parent();
                if ($parent.hasClass('opened')) {
                    $parent.removeClass('opened');
                }
                else {
                    $parent.addClass('opened');
                }
            });
            $setting.find('input').click(function () {
                var value = $(this).val();
                top.$.cookie('Learn_ADMS_V7_UItheme', value, { path: "/", expires: 30 });
                location.reload();
            });

        },
    };

    $(function () {
        page.init();
    });
})(window.jQuery, top.learun);