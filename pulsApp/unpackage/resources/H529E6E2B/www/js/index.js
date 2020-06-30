// 有关“空白”模板的简介，请参阅以下文档:
// http://go.microsoft.com/fwlink/?LinkID=397704
// 若要在 cordova-simulate 或 Android 设备/仿真器上在页面加载时调试代码: 启动应用，设置断点，
// 然后在 JavaScript 控制台中运行 "window.location.reload()"。
(function ($, learun) {
    "use strict";
	
    // 初始化页面
        var tabdata = [
            {
                page: 'workspace',
                text: '首页',
                img: 'images/tab10.png',
                fillimg: 'images/tab11.png'
            },
            {
                page: 'message',
                text: '消息',
                img: 'images/tab20.png',
                fillimg: 'images/tab21.png'
            },
            {
                page: 'contacts',
                text: '通讯录',
                img: 'images/tab30.png',
                fillimg: 'images/tab31.png'
            },
            {
                page: 'my',
                text: '我的',
                img: 'images/tab40.png',
                fillimg: 'images/tab41.png'
            }
        ];
	
	function init(){
		if(window.lrmui == undefined){
			
			setTimeout(function(){
				init();
			},100);
		}
		else{
			 window.lrmui.init(function () {
			    window.lrmui.tab.init(tabdata);
			    var logininfo = learun.storage.get('logininfo');
				if (logininfo) {// 有登录的token
					learun.tab.go('workspace');
				}
				else {
					learun.nav.go({ path: 'login', isBack: false, isHead: false });
				}
			});
			
		}
	}
	
	
	init();
	
   
})(window.jQuery, window.lrmui);