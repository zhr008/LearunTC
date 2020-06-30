// 适配各个平台
(function(w,learun) {
	function plusReady() {
		console.log('test');
		if(plus.device.model.indexOf('iphone') != -1 || plus.device.model.indexOf('iPhone') != -1){
			// ios
			// 加载页面css代码
			learun.http.webget('css/ios.css', {}, function (res) {
			    if (res !== null) {
			        fui.includeCss(res);
			    }
			}, 'text');
			
		}
		else{
			// android
			learun.http.webget('css/android.css', {}, function (res) {
			    if (res !== null) {
			        fui.includeCss(res);
			    }
			}, 'text');
		}
	}
	if (w.plus) {
		plusReady();
	} else {
		document.addEventListener('plusready', plusReady, false);
	}
})(window,window.lrmui);


