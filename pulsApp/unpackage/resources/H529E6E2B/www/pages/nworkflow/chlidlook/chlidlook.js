(function () {
    var processId = '';
    var taskId = '';

    var $header = null;
    var headText = '';

    var fieldMap = {};
    var formMap = {};
    var formDataes;
    var formreq;

    // 流程信息
    var info;
    var schemeObj;
    var currentNode;
    var nodeMap = {};

    var page = {
        init: function ($page, param) {
            $page.find('.lr-chlidlook-page').toptab(['表单信息', '父流程信息']).each(function (index) {
                var $this = $(this);
                switch (index) {
                    case 0:
                        $this.html('<div class="container" id="chlidlookcontainer"></div>');
                        break;
                    case 1:
                        $this.html('<div class="container" id="chlidlookcontainer2"></div>');
                        break;
                }
                $this = null;
            });
            processId = param.processId;
            taskId = param.taskId;
            taskinfo(param);

        },
        destroy: function (pageinfo) {
            $header = null;
        }
    };
    // 流程发起初始化
    function taskinfo(_param) {
        fieldMap = {};
        var req = {
            processId: _param.processId,
            taskId: _param.taskId
        };
        learun.layer.loading(true, "获取流程信息");
        learun.httpget(config.webapi + "learun/adms/newwf/processinfo", req, function (data) {
            if (data) {
                info = data.info;
                schemeObj = JSON.parse(info.Scheme);
                // 获取当前节点
                $.each(schemeObj.nodes, function (_index, _item) {
                    if (_item.id == info.CurrentNodeId) {
                        currentNode = _item;
                    }
                });

                learun.httpget(config.webapi + "learun/adms/newwf/scheme", currentNode.childFlow, function (cdata) {
                    learun.layer.loading(false);
                    if (cdata) {
                        var schemeObj3 = JSON.parse(cdata.F_Content);
                        var ccurrentNode;

                        // 获取开始节点
                        $.each(schemeObj3.nodes, function (_index, _item) {
                            if (_item.type == 'startround') {
                                ccurrentNode = _item;
                                return false;
                            }
                        });
                        var wfForms = ccurrentNode.wfForms;// 表单数据
                        var schemeIds = [];
                        var authorizeFields = [];
                        var formreq = [];

                        // 获取下关联字段
                        $.each(wfForms, function (_index, _item) {
                            if (_item.formId) {
                                fieldMap[_item.formId] = _item.field;
                                schemeIds.push(_item.formId);

                                var point = {
                                    schemeInfoId: _item.formId,
                                    processIdName: _item.field,
                                    keyValue: info.childProcessId,
                                }
                                formreq.push(point);

                                $.each(_item.authorize, function (_jindex, _jitem) {
                                    _jitem.fieldId = _jindex;
                                    authorizeFields.push(_jitem);
                                });
                            }
                        });
                        learun.custmerform.loadScheme(schemeIds, function (scheme) {
                            $('#chlidlookcontainer').custmerform(scheme);
                            // 设置表单的可查看权限
                            $.each(authorizeFields, function (_index, _item) {
                                if (_item.isLook === 0) {
                                    $('#chlidlookcontainer').find('#' + _item.fieldId).parents('.lr-form-row').remove();
                                }
                                else if (_item.isEdit === 0) {
                                    $('#chlidlookcontainer').find('#' + _item.fieldId).parents('.lr-form-row').attr('readonly', 'readonly');
                                }
                            });

                            $('#chlidlookcontainer').setFormRead();
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
                                $('#chlidlookcontainer').custmerformSet(data);
                            }
                        });
                    }
                });
            }
            else {
                learun.layer.loading(false);
            }
        });
        learun.httpget(config.webapi + "learun/adms/newwf/processinfo", {
            processId: _param.processId
        }, function (data) {
            learun.layer.loading(false);
            if (data) {
                var schemeObj2 = JSON.parse(data.info.Scheme);
                // 获取当前节点
                $.each(schemeObj2.nodes, function (_index, _item) {
                    nodeMap[_item.id] = _item;
                });
                // 加载流程信息
                initTimeLine(data.info.TaskLogList);
            }

        });

    }
    function initTimeLine(flowHistory) {
        var nodelist = [];
        learun.clientdata.getAll('department', {
            callback: function (departmentMap) {
                learun.clientdata.getAll('user', {
                    callback: function (userMap) {
                        for (var i = 0, l = flowHistory.length; i < l; i++) {
                            var item = flowHistory[i];
                            var name = (item.F_CreateUserName || '系统处理') + '：';
                            if (item.F_CreateUserId && userMap[item.F_CreateUserId]) {
                                var _department = departmentMap[userMap[item.F_CreateUserId].departmentId];
                                if (_department) {
                                    name = '【' + _department.name + '】' + name;
                                }
                            }
                            var content = item.F_OperationName;
                            if (item.F_Des) {
                                content += '【审批意见】' + item.F_Des;
                            }
                            var nodeName = '';
                            if (item.F_NodeId && nodeMap[item.F_NodeId]) {
                                nodeName = nodeMap[item.F_NodeId].name;
                            }

                            var point = {
                                title: item.F_NodeName || nodeName,
                                people: name,
                                content: content,
                                time: item.F_CreateDate
                            };

                            nodelist.push(point);
                        }
                        $('#chlidlookcontainer2').ftimeline(nodelist);
                    }
                });
            }
        });
    }
    return page;
})();