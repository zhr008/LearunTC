(function () {
    var processId = '';
    var schemeCode = '';
    var fieldMap = {};
    var formDataes;
    var formAllData;
    var formreq;

    var $header = null;
    var headText = '';
	
	/* 系统表单 */
	var sysList = [];
	
	
    var getFormData = function ($page,flag,callback) {
        formDataes = $page.find('#createflow').custmerformGet(flag);
        if (formDataes == null) {
            callback(false);
        }
        formreq = [];
        for (var id in formDataes) {
            if (!fieldMap[id]) {
                learun.layer.warning('未设置流程表单关联字段！', function () { }, '力软提示', '关闭');
                 callback(false);
            }
            formDataes[id][fieldMap[id]] = processId;
            var point = {
                schemeInfoId: id,
                processIdName: fieldMap[id],
                formData: JSON.stringify(formDataes[id])
            }

            formreq.push(point);
        }
		
		/* 系统表单 */
		$page.find('.sysForm').each(function(){
			var $sysForm = $(this);
			var url = $(this).attr('data-url');
			sysList.push(learun.nav.data[url]);
		})
		// 验证表单数据
		if(!flag){
			for(var i=0;i<sysList.length;i++){
				var sysPage = sysList[i];
				var validRes = sysPage.jsObj.validForm('create');
				if(!validRes){
					callback(validRes);
					return;
				}
			}
		}

		callback(true);
    };
    var currentNode;
    var schemeObj;

    var page = {
        isScroll: false,
        init: function ($page, param) {
            schemeCode = param.schemeCode;
            currentNode = null;
            var _html = '<div class="lr-form-header-submit" style="display:block;" >提交</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            headText = $header.find('.f-page-title').text();

            processId = learun.guid('-');;
            bootstraper($page);

            // 发起流程
            $header.find('.lr-form-header-submit').on('tap', function () {
                if (currentNode == null) {
                    learun.layer.toast('还未加载完流程模板');
                    return;
                }
                learun.actionsheet({
                    id: 'createflow1',
                    data: [
                        {
                            text: '创建流程',
                            group: '1',
                            event: function () {
								getFormData($page,false,function(res){
									if(res){
										learun.nav.go({ path: 'nworkflow/createflow/form', title: headText, type: 'right', param: { node: currentNode, nodelist: schemeObj.nodes, schemeCode: schemeCode, processId: processId } });
									}
								})
                            }
                        },
                        {
                            text: '保存草稿',
                            mark: true,
                            group: '2',
                            event: function () {
                                // 获取表单数据
								getFormData($page,true,function(res){
									if(res){
										// 创建草稿
										page.saveDraft();
									}
								})
                            }
                        }
                    ],
                    cancel: function () {
                    }
                });
                
            });
        },
        create: function (info, auditers) {// 提交创建流程
            var flowreq = {
                code: schemeCode,
                processId: processId,
                title: info.title,
                level: info.level,
                auditors: JSON.stringify(auditers),
                formreq: JSON.stringify(formreq)
            };
			learun.sysFormSave(processId,sysList,function(formSaveRes){
				if(formSaveRes){
					learun.layer.loading(true, "创建流程,请等待！");
					learun.httppost(config.webapi + "learun/adms/newwf/create", flowreq, function (data) {
					    learun.layer.loading(false);
					    learun.nav.closeCurrent();
					});
				}
			})
        },
        saveDraft: function () {
            var flowreq = {
                code: schemeCode,
                processId: processId,
                formreq: JSON.stringify(formreq)
            };
			learun.sysFormSave(processId,sysList,function(formSaveRes){
				if(formSaveRes){
					learun.layer.loading(true, "保存草稿！");
					learun.httppost(config.webapi + "learun/adms/newwf/draft", flowreq, function (data) {
					    learun.layer.loading(false);
					    learun.nav.closeCurrent();
					});
				}
			})
        }
    };
    // 流程发起初始化
    function bootstraper($page) {
        learun.layer.loading(true, "获取流程模板信息");
        learun.httpget(config.webapi + "learun/adms/newwf/scheme", schemeCode, function (data) {
            learun.layer.loading(false);
            if (data) {
                schemeObj = JSON.parse(data.F_Content);
                // 获取开始节点
                $.each(schemeObj.nodes, function (_index, _item) {
                    if (_item.type == 'startround') {
                        currentNode = _item;
                        return false;
                    }
                });
                var wfForms = currentNode.wfForms;// 表单数据
                var schemeIds = [];

                var authorizeFields = [];
                // 获取下关联字段
                $.each(wfForms, function (_index, _item) {
                    if (_item.formId) {
                        fieldMap[_item.formId] = _item.field;
                        schemeIds.push(_item.formId);
                        $.each(_item.authorize, function (_jindex, _jitem) {
                            _jitem.fieldId = _jindex;
                            authorizeFields.push(_jitem);
                        });
                    }
					else{
						// 加载系统表单
						learun.sysForm(_item,$page.find('#createflow'),function(jsObj){
							jsObj.setAuthorize(_item.authorize);
						});
					}
                });
                learun.custmerform.loadScheme(schemeIds, function (scheme) {
                    $page.find('#createflow').custmerform(scheme);
                    // 设置表单的可查看权限
                    $.each(authorizeFields, function (_index, _item) {
                        if (_item.isLook === 0) {
                            $('#createflow').find('#' + _item.fieldId).parents('.lr-form-row').remove();
                        }
                        else if (_item.isEdit === 0) {
                            $('#createflow').find('#' + _item.fieldId).parents('.lr-form-row').attr('readonly', 'readonly');
                        }
                    });
                });
            }
        });
    }

    return page;
})();