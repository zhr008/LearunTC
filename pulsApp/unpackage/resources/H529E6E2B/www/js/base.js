/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 基础方法
 */
(function ($, fui, window) {
    "use strict";

    var backbuttonEvent = function () {
		console.log('testback');
		
		
        // 判断是否有图片预览
        if ($('#fui_imagePreview').length >0 && !$('#fui_imagePreview').is(':hidden')) {
            $('#fui_imagePreview').imagePreviewClose();
        }
        else if ($('.f-dialog').length > 0) {
            $('.f-dialog').remove();
            $('.f-dialog-mask').remove();
        }
        else {
            fui.loading(false);
            // 判断下当前页面是否有返回键
            var pageInfo = fui.nav.page.data[fui.nav.page.activeid];
            if (pageInfo.op.isBack) {
                window.lrmui.nav.closeCurrent();
            }
            else {
                window.lrmui.layer.confirm('是否退出APP', function (_index) {
                    if (_index === '1') {
						plus.runtime.quit();
                        //navigator.app.exitApp();
                    }
                }, '力软提示', ['否', '是']);
            }
        }
    }

    window.lrmui = {// 力软mobileui框架基础方法
        isreal: false,
        isready: false,
        init: function (callback) {
			
			
			
            //document.addEventListener("backbutton", backbuttonEvent, false);
            _init(callback);
        },
        guid: function (separator) {
            return fui.guid(separator);
        },
        pageid: function () {
            return fui.pageid();
        },
        deviceId: function () {// 设备id
            var deviceId = window.lrmui.storage.get('deviceId');
            if (!deviceId) {
                deviceId = window.device ? device.uuid : fui.guid('');
                window.lrmui.storage.set('deviceId', deviceId);
            }
            return deviceId;
        },
        type: function (obj) {
            return fui.type(obj);
        },
        date: {// 日期格式化操作
            parse: function (v) {
                return fui.date.parse(v);
            },
            format: function (v, format) {
                return fui.date.format(v, format);
            }
        },
        actionsheet: function (op) {
            /*id: '',
             data: [], //text:'名称'，group:'组名'，event:'点击事件'，mark:'' 标记后为红色
             cancel: false*/
            fui.actionsheet(op);
        },
        layer: {
            toast: function (msg) {// 消失提示框
                fui.dialog({ msg: msg });
            },
            warning: function (msg, callback, title, btn) {// 警告框
                fui.dialog({ type: 'warning', msg: msg, title: title, callback: callback, btn: btn });
            },
            confirm: function (msg, callback, title, btns) {// 确认消息框
                fui.dialog({ type: 'confirm', msg: msg, title: title, callback: callback, btns: btns });
            },
            popinput: function (msg, callback, title, btns, input) {// 输入消息框
                fui.dialog({ type: 'prompt', msg: msg, title: title, callback: callback, btns: btns });
            },
            loading: function (isShow, msg) {// 加载提示框
                fui.loading(isShow, msg);
            }
        },
        md5: function (str) {
            return $.md5(str);
        }
    };
	var openw;
	window.lrmui.createWithoutTitle=function(id, ws){
		if(openw){//避免多次打开同一个页面
			return null;
		}
		if(window.plus){
			ws=ws||{};
			ws.scrollIndicator||(ws.scrollIndicator='none');
			ws.scalable||(ws.scalable=false);
			ws.backButtonAutoControl||(ws.backButtonAutoControl='close');
			openw = plus.webview.create(id, id, ws);
			openw.addEventListener('close', function(){
				openw=null;
			}, false);
			return openw;
		}else{
			w.open(id);
		}
		return null;
	};
	
	
    function _init(callback) {// 框架初始化方法
        if (window.lrmui.isready) {
            // 开始执行初始化函数
            // 处理 Cordova 暂停并恢复事件
            //document.addEventListener('pause', onPause.bind(this), false);
            //document.addEventListener('resume', onResume.bind(this), false);
            callback();
        }
        else {
            setTimeout(function () {
                _init(callback);
            }, 200);
        }
    }
    function isPlatform() {// 调试浏览器模拟环境，浏览器环境，安卓，IOS
		if (!window.plus) {
			window.lrmui.platform = 'browser';
		}
		document.addEventListener('plusready', onDeviceReady, false);
		setTimeout(function () {
		    window.lrmui.isready = true;
		}, 500);
    }
    isPlatform();
    function onDeviceReady() {
		if(plus.device.model.indexOf('iphone') != -1 || plus.device.model.indexOf('iPhone') != -1){
			// ios
			window.lrmui.platform = 'ios';
		}
		else{
			// android
			window.lrmui.platform = 'android';
		}
		window.lrmui.isreal = true;
		window.lrmui.isready = true; // 表示设备加载完成      
		  
		  
		  plus.key.addEventListener('backbutton', function(){
		  	backbuttonEvent();
		  },false);
    }

    $(function () {
        $('body').delegate('input[type="password"],input[type="text"],textarea,.lrtextarea', 'tap', function () {
            var $this = $(this);
            if ($this.attr('readonly') || $this.parents('.lr-form-row').attr('readonly')) {
                return false;
            }

            if (!$this.is(":focus")) {
                if ($this.hasClass('lrtextarea')) {
                    var range;
                    if (window.getSelection) {//ie11 10 9 ff safari
                        range = window.getSelection();//创建range
                        range.selectAllChildren($this[0]);//range 选择obj下所有子内容
                        $this.focus(); //解决ff不获取焦点无法定位问题
                        range.collapseToEnd();//光标移至最后
                    }
                    else if (document.selection) {//ie10 9 8 7 6 5
                        range = document.selection.createRange();//创建选择对象
                        range.moveToElementText($this[0]);//range定位到obj
                        range.collapse(false);//光标移至最后
                        range.select();
                    }
                }
                else
                {
                    $this.focus();
                }
            }
        });
    });
})(window.jQuery, window.fui, window);