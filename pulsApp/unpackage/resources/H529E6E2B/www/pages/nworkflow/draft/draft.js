(function () {
    var processId = '';
    var fieldMap = {};
    var formMap = {};
    var formDataes;
    var formAllData;
    var formreq;

    var $header = null;
    var headText = '';

    var getFormData = function ($page, flag) {
        formDataes = $page.find('#draftflow').custmerformGet(flag);
        if (formDataes == null) {
            return false;
        }
        formreq = [];
        for (var id in formDataes) {
            if (!fieldMap[id]) {
                learun.layer.warning('未设置流程表单关联字段！', function () { }, '力软提示', '关闭');
                return false;
            }
            formDataes[id][fieldMap[id]] = processId;
            var point = {
                schemeInfoId: id,
                processIdName: fieldMap[id],
                formData: JSON.stringify(formDataes[id])
            }
            if (formMap[id]) {
                point.keyValue = processId;
            }

            formreq.push(point);
        }

        return true;
    };
    // 流程信息
    var info;
    var schemeObj;
    var currentNode;
    var nodeMap = {};

    var page = {
        isScroll: false,
        init: function ($page, param) {
            processId = param.processId;
            currentNode = null;

            var _html = '<div class="lr-form-header-submit" style="display:block;" >提交</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            headText = $header.find('.f-page-title').text();

          
            bootstraper($page);

            // 提交
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
                                // 获取表单数据
                                if (!getFormData($page)) {
                                    return false;
                                }
                                learun.nav.go({ path: 'nworkflow/draft/form', title: headText, type: 'right', param: { node: currentNode, nodelist: schemeObj.nodes, processId: processId } });
                            }
                        },
                        {
                            text: '保存草稿',
                            mark: true,
                            group: '2',
                            event: function () {
                                // 获取表单数据
                                if (!getFormData($page, true)) {
                                    return false;
                                }
                                // 创建草稿
                                page.saveDraft();

                            }
                        }
                    ],
                    cancel: function () {
                    }
                });

            });
        },
        create: function (info, auditers) {// 创建流程
            var flowreq = {
                processId: processId,
                title: info.title,
                level: info.level,
                auditors: JSON.stringify(auditers),
                formreq: JSON.stringify(formreq)
            };
            learun.layer.loading(true, "创建流程,请等待！");
            learun.httppost(config.webapi + "learun/adms/newwf/create", flowreq, function (data) {
                learun.layer.loading(false);
                var prepage = learun.nav.getpage('nworkflow/myflow');
                prepage.grid[0].reload();
                learun.nav.closeCurrent();
            });
        },
        saveDraft: function () {// 保存草稿
            var flowreq = {
                processId: processId,
                formreq: JSON.stringify(formreq)
            };
            learun.layer.loading(true, "保存草稿！");
            learun.httppost(config.webapi + "learun/adms/newwf/draft", flowreq, function (data) {
                learun.layer.loading(false);
                var prepage = learun.nav.getpage('nworkflow/myflow');
                prepage.grid[0].reload();
                learun.nav.closeCurrent();
            });
        }
    };
    // 流程发起初始化
    function bootstraper($page) {
        fieldMap = {};
        var req = {
            processId: processId
        };
        learun.layer.loading(true, "获取流程信息");
        learun.httpget(config.webapi + "learun/adms/newwf/processinfo", req, function (data) {
            learun.layer.loading(false);
            if (data) {
                var schemeIds = [];
                var authorizeFields = [];
                var formreq = [];
                info = data.info;
                schemeObj = JSON.parse(info.Scheme);
                // 获取当前节点
                $.each(schemeObj.nodes, function (_index, _item) {
                    if (_item.id == info.CurrentNodeId) {
                        currentNode = _item;
                    }
                    nodeMap[_item.id] = _item;
                });

                $.each(currentNode.wfForms, function (_index, _item) {
                    if (_item.formId) {
                        fieldMap[_item.formId] = _item.field;
                        schemeIds.push(_item.formId);
                        var point = {
                            schemeInfoId: _item.formId,
                            processIdName: _item.field,
                            keyValue: processId,
                        }
                        formreq.push(point);

                        $.each(_item.authorize, function (_jindex, _jitem) {
                            _jitem.fieldId = _jindex;
                            authorizeFields.push(_jitem);
                        });
                    }
                });
                learun.custmerform.loadScheme(schemeIds, function (scheme) {
                    $('#draftflow').custmerform(scheme);
                    // 设置表单的可查看权限
                    $.each(authorizeFields, function (_index, _item) {
                        if (_item.isLook === 0) {
                            $('#draftflow').find('#' + _item.fieldId).parents('.lr-form-row').remove();
                        }
                        else if (_item.isEdit === 0) {
                            $('#draftflow').find('#' + _item.fieldId).parents('.lr-form-row').attr('readonly', 'readonly');
                        }
                    });
                });

                formMap = {};
                // 获取下自定义表单数据
                learun.httpget(config.webapi + "learun/adms/form/data", formreq, function (data) {
                    if (data) {
                        // 设置自定义表单数据
                        $.each(data, function (_id, _item) {
                            $.each(_item, function (_j, _jitem) {
                                if (_jitem.length > 0) {
                                    formMap[_id] = true;
                                }
                            });
                        });
                        $('#draftflow').custmerformSet(data);
                    }
                });
            }
        });
    }

    return page;
})();