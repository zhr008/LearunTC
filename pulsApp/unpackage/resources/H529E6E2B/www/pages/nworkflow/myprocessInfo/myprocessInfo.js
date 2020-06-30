(function () {
    var processId = '';
    var taskId = '';

    var nodeMap = {};

    var page = {
        init: function ($page, param) {
            var _html = '<div class="lr-form-header-submit" style="display:block;" >操作</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            headText = $header.find('.f-page-title').text();

            processId = param.processId || '';
            taskId = param.taskId || '';

            $page.find('.lr-nmyprocessInfo-page').toptab(['表单信息', '流程信息']).each(function (index) {
                var $this = $(this);
                switch (index) {
                    case 0:
                        $this.html('<div class="container" id="nmyprocessInfocontainer1"></div>');
                        break;
                    case 1:
                        $this.html('<div class="container" id="nmyprocessInfocontainer2"></div>');
                        break;
                }
                $this = null;
            });
            processinfo(param);

            // 操作
            $header.find('.lr-form-header-submit').on('tap', function () {
                var actionsheetData = [];
                actionsheetData.push({
                    text: '催办审核',
                    group: '1',
                    event: function () {
                        learun.layer.confirm('是否确认催办审核？', function (_index) {
                            if (_index === '1') {
                                learun.layer.loading(true, "催办审核中");
                                learun.httppost(config.webapi + "learun/adms/newwf/urge", processId, function (data) {
                                    learun.layer.loading(false);
                                });
                            }
                        }, '力软提示', ['取消', '确定']);
                    }
                });

                if (param.isStart == 0) {
                    actionsheetData.push({
                        text: '撤销流程',
                        group: '2',
                        mark: true,
                        event: function () {
                            learun.layer.confirm('是否确认撤销流程？', function (_index) {
                                if (_index === '1') {
                                    learun.layer.loading(true, "撤销流程中");
                                    learun.httppost(config.webapi + "learun/adms/newwf/revoke", processId, function (data) {
                                        learun.layer.loading(false);
                                        var prepage = learun.nav.getpage('nworkflow/myflow');
                                        prepage.grid[0].reload();
                                        learun.nav.closeCurrent();

                                    });
                                }
                            }, '力软提示', ['取消', '确定']);
                        }
                    });
                }

                learun.actionsheet({
                    id: 'myprocessInfo2',
                    data: actionsheetData,
                    cancel: function () {
                    }
                });
            });
        }
    };
    // 流程发起初始化
    function processinfo(_param) {
        var req = {
            processId: _param.processId,
            taskId: _param.taskId
        };
        learun.layer.loading(true, "获取流程信息");
        learun.httpget(config.webapi + "learun/adms/newwf/processinfo", req, function (data) {
            if (data) {
                var info = data.info;
                var schemeObj = JSON.parse(info.Scheme);
                var currentNode;
                var formreq = [];
                var schemeIds = [];
                var authorizeFields = [];
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
                    $('#nmyprocessInfocontainer1').custmerform(scheme);

                    // 设置表单的可查看权限
                    $.each(authorizeFields, function (_index, _item) {
                        if (_item.isLook === 0) {
                            $('#nmyprocessInfocontainer1').find('#' + _item.fieldId).parents('.lr-form-row').remove();
                        }
                    });

                    $('#nmyprocessInfocontainer1').setFormRead();
                });

                // 获取下自定义表单数据
                learun.httpget(config.webapi + "learun/adms/form/data", formreq, function (data) {
                    if (data) {
                        // 设置自定义表单数据
                        $('#nmyprocessInfocontainer1').custmerformSet(data);
                    }
                });
                // 加载流程信息
                initTimeLine(info.TaskLogList);
            }
            learun.layer.loading(false);
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
                        $('#nmyprocessInfocontainer2').ftimeline(nodelist);
                    }
                });
            }
        });
    }
    return page;
})();