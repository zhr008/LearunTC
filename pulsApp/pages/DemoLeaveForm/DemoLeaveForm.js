(function () {
    var keyValue = '';
	var $cpage = null;
    var page = {
		isUpdate:false,
        init: function ($page, param) {
			$cpage = $page;
			page.isUpdate = false;
			
			$page.find('#F_Begin').lrdate();
			$page.find('#F_End').lrdate();
        },
		setAuthorize:function(data,isLook){
			// 设置系统表单的权限, data是流程模板设置的权限数据,isLook表示是否是查看状态
			
		},
		setFormData:function(processId){
			console.log(processId);
			// processId流程实例主键
			// 获取表单数据
			learun.layer.loading(true, '获取表单数据');
			learun.httpget(config.webapi + "learun/adms/demo/wfsys/form", processId, function (data) {
			    if (data) {
					page.isUpdate = true;
			        $cpage.find('.lr-form-container').lrformSet(data);
			    }
			    learun.layer.loading(false);
			});
			
		},
		validForm:function(code){
			// 验证表单数据 code 是流程当前执行的动作
			if (!$cpage.find('.lr-form-container').lrformValid()) {
			    return false;
			}
			return true;
		},
		save: function(processId,callBack,i){
			var formData = $cpage.find('.lr-form-container').lrformGet();
			// 保存表单数据
			var keyValue = "";     
			if (page.isUpdate) {
				keyValue = processId;
			}
			else {
				formData.F_Id = processId;
			}
			
			
			learun.layer.loading(true, "正在提交数据");
			
			var _postData = {
			    keyValue: keyValue,
			    strEntity: JSON.stringify(formData)
			};
			learun.httppost(config.webapi + "learun/adms/demo/wfsys/save", _postData, function (data) {
			    learun.layer.loading(false);
				callBack && callBack(data, i);
			});
		}
    };
    return page;
})();