(function () {
    var processId = '';

    var $header = null;
    var headText = '';

    var fieldMap = {};
    var formMap = {};
    var formDataes;
    var formAllData;
    var formreq;

    // 流程信息
    var info;
    var schemeObj;
    var currentNode;
    var nodeMap = {};

    var getFormData = function ($page) {
        formDataes = $page.find('#againcreateflowcontainer').custmerformGet();
        if (formDataes == null) {
            return false;
        }
        formreq = [];
        formAllData = {};
        for (var id in formDataes) {
            if (!fieldMap[id]) {
                learun.layer.warning('未设置流程表单关联字段！', function () { }, '力软提示', '关闭');
                return false;
            }
            $.extend(formAllData, formDataes[id]);
            if (!formMap[id]) {
                formDataes[id][fieldMap[id]] = processId;
            }
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

    var page = {
        init: function ($page, param) {
            var _html = '<div class="lr-form-header-submit" style="display:block;" >发起</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            headText = $header.find('.f-page-title').text();

            $page.find('.lr-againcreateflow-page').toptab(['表单信息', '流程信息']).each(function (index) {
                var $this = $(this);
                switch (index) {
                    case 0:
                        $this.html('<div class="container" id="againcreateflowcontainer"></div>');
                        break;
                    case 1:
                        $this.html('<div class="container" id="againcreateflowcontainer2"></div>');
                        break;
                }
                $this = null;
            });
            processId = param.processId;
            taskinfo(param);

            // 发起
            $header.find('.lr-form-header-submit').on('tap', function () {
                // 获取表单数据
                if (!getFormData($page)) {
                    return false;
                }
                learun.layer.confirm('是否重新发起？', function (_index) {
                    if (_index === '1') {
                        var flowreq = {
                            processId: processId,
                            formreq: JSON.stringify(formreq)
                        };
                        learun.layer.loading(true, "重新发起中");
                        learun.httppost(config.webapi + "learun/adms/newwf/againcreate", flowreq, function (data) {
                            learun.layer.loading(false);
                            var prepage = learun.nav.getpage('nworkflow/myflow');
                            prepage.grid[0].reload();
                            prepage.grid[1].reload();
                            learun.nav.closeCurrent();

                        });
                    }
                }, '力软提示', ['取消', '确定']);
            });
        },
        destroy: function (pageinfo) {
            $header = null;
        }
    };
    // 流程发起初始化
    function taskinfo(_param) {
        fieldMap = {};
        var req = {
            processId: _param.processId
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
                        schemeIds.push(_item.formId);
                        var point = {
                            schemeInfoId: _item.formId,
                            processIdName: _item.field,
                            keyValue: _param.processId,
                        }
                        formreq.push(point);

                        $.each(_item.authorize, function (_jindex, _jitem) {
                            _jitem.fieldId = _jindex;
                            authorizeFields.push(_jitem);
                        });
                    }
                });
                learun.custmerform.loadScheme(schemeIds, function (scheme) {
                    $('#againcreateflowcontainer').custmerform(scheme);
                    // 设置表单的可查看权限
                    $.each(authorizeFields, function (_index, _item) {
                        if (_item.isLook === 0) {
                            $('#againcreateflowcontainer').find('#' + _item.fieldId).parents('.lr-form-row').remove();
                        }
                        else if (_item.isEdit === 0) {
                            $('#againcreateflowcontainer').find('#' + _item.fieldId).parents('.lr-form-row').attr('readonly', 'readonly');
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
                        $('#againcreateflowcontainer').custmerformSet(data);
                    }
                });
                // 加载流程信息
                initTimeLine(info.TaskLogList);
            }
            console.log(data);
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
                        $('#againcreateflowcontainer2').ftimeline(nodelist);
                    }
                });
            }
        });
    }
    return page;
})();