/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.05
 * 描 述：节点设置	
 */
var layerId = request('layerId');
var isPreview = request('isPreview');

var acceptClick;

// 审核者
var auditors = [];
// 表单
var workforms = [];
var workformMap = {};
// 按钮
var btnList = [{ id: '1', name: '同意', code: 'agree', file: '1', next: '1' }, { id: '2', name: '不同意', code: 'disagree', file: '1', next: '1' }];
// 条件节点
var conditions = [];

var bootstrap = function ($, learun) {
    "use strict";
    var currentNode = top[layerId].currentNode;
    var formcomponts = {};
    function isRepeat(id) {
        var res = false;
        for (var i = 0, l = auditors.length; i < l; i++) {
            if (auditors[i].auditorId == id) {
                learun.alert.warning('重复添加审核人员信息');
                res = true;
                break;
            }
        }
        return res;
    }
    var page = {
        init: function () {
            page.nodeInit();
            page.bind();
            page.initData();

            if (isPreview) {
                $('input,textarea').attr('readonly', 'readonly');
                $('.lr-form-jfgrid-btns').remove();
            }
        },
        nodeInit: function () {
            switch (currentNode.type) {
                case 'startround':     // 开始节点
                    $('#lr_form_tabs li a[data-value="auditor"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="timeout"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="btnSetting"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="conditionField"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="conditionSqlDiv"]').parent().remove();

                    $('#lr_form_tabs li a[data-value="operation"]').parent().remove();
                    $('.div_notice').show();
                    $('.div_next').show();
                    break;
                case 'stepnode':       // 审核节点
                    $('#lr_form_tabs li a[data-value="conditionField"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="conditionSqlDiv"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="operation"]').parent().remove();
                    $('.div_notice').show();
                    $('.div_auditor').show();
                    $('.div_sign').show();
                    $('.div_batchAudit').show();
                    $('.div_agreeGz').show();
                    $('.div_noPeopleGz').show();
                    
                    $('#name').removeAttr('readonly');
                    break;
                case 'auditornode':    // 传阅节点
                    $('#lr_form_tabs li a[data-value="timeout"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="btnSetting"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="conditionField"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="conditionSqlDiv"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="operation"]').parent().remove();
                    $('.div_batchAudit').show();
                    $('.div_notice').show();
                    $('#name').removeAttr('readonly');
                    break;
                case 'conditionnode':  // 条件节点
                    $('#lr_form_tabs li a[data-value="workform"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="auditor"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="timeout"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="btnSetting"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="operation"]').parent().remove();
                    $('#name').removeAttr('readonly');
                    break;
                case 'confluencenode': // 会签节点
                    $('#lr_form_tabs li a[data-value="workform"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="auditor"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="timeout"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="btnSetting"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="conditionField"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="conditionSqlDiv"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="operation"]').parent().remove();
                    $('.div_confluence').show();
                    $('#name').removeAttr('readonly');
                    break;
                case 'childwfnode':    // 子流程节点
                    $('#lr_form_tabs li a[data-value="workform"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="timeout"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="btnSetting"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="conditionField"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="conditionSqlDiv"]').parent().remove();

                    $('.div_notice').show();
                    $('.div_child').show();
                    $('#name').removeAttr('readonly');
                    break;
            };
        },
        /*绑定事件和初始化控件*/
        bind: function () {
            $('#lr_form_tabs').lrFormTab();

            /*基础配置*/
            // 通知消息策略选择
            $('#notice').lrselect({
                text: 'F_StrategyName',
                value: 'F_StrategyCode',
                url: top.$.rootUrl + '/LR_Message/LR_StrategyInfo/GetList'
            });

            $('input[name="isAllAuditor"]').on('click', function () {
                var value = $(this).val();
                if (value == "2") {
                    if (currentNode.type == 'stepnode') {
                        $('.div_auditor_type').show();
                    }
                }
                else {
                    $('.div_auditor_type').hide();
                }
            });

            // 子流程选择
            $('#childFlow').lrselect({
                text: 'F_Name',
                value: 'F_Code',
                url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/GetList',
                allowSearch: true
            });
            // 会签设置
            $('#confluenceType').lrselect({//会签类型:1-100%通过，2-一个通过即可，3-按百分比
                placeholder: false,
                data: [{ id: '1', text: '所有步骤通过' }, { id: '2', text: '一个步骤通过即可' }, { id: '3', text: '按百分比计算' }]
            }).lrselectSet('1');

            // 自动同意规则
            $('#agreeGz').lrselect({
                data: [{ id: '1', text: '处理人就是提交人' }, { id: '2', text: '处理人和上一步的处理人相同' }, { id: '3', text: '处理人审批过' }],
                type: 'multiple'
            });

            /*审核者*/
            $('#auditor_girdtable').jfGrid({
                headData: [
                    {
                        label: "类型", name: "type", width: 100, align: "center",
                        formatter: function (cellvalue) {//审核者类型1.岗位2.角色3.用户
                            switch (cellvalue) {
                                case '1':
                                    return '岗位';
                                    break;
                                case '2':
                                    return '角色';
                                    break;
                                case '3':
                                    return '用户';
                                    break;
                                case '4':
                                    return '上下级';
                                    break;
                                case '5':
                                    return '表字段';
                                    break;
                                case '6':
                                    return '某节点执行人';
                                    break;
                            }
                        }
                    },
                    { label: "名称", name: "auditorName", width: 260, align: "left" },
                    {
                        label: "附加条件", name: "condition", width: 150, align: "left",
                        formatter: function (cellvalue) {// 1.同一个部门2.同一个公司
                            switch (cellvalue) {
                                case '1':
                                    return '同一个部门';
                                    break;
                                case '2':
                                    return '同一个公司';
                                    break;
                                case '3':
                                    return '发起人上级';
                                    break;
                                case '4':
                                    return '发起人下级';
                                    break;
                            }
                        }
                    }
                ]
            });
            // 岗位添加
            $('#lr_post_auditor').on('click', function () {
                learun.layerForm({
                    id: 'AuditorPostForm',
                    title: '添加审核岗位',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/PostForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }

                        });
                    }
                });
            });
            // 角色添加
            $('#lr_role_auditor').on('click', function () {
                learun.layerForm({
                    id: 'AuditorRoleForm',
                    title: '添加审核角色',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/RoleForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }
                        });
                    }
                });
            });
            // 人员添加
            $('#lr_user_auditor').on('click', function () {
                learun.layerForm({
                    id: 'AuditorUserForm',
                    title: '添加审核人员',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/UserForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }
                        });
                    }
                });
            });
            // 审核人员移除
            $('#lr_delete_auditor').on('click', function () {
                var _id = $('#auditor_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该审核人员！', function (res, index) {
                        if (res) {
                            for (var i = 0, l = auditors.length; i < l; i++) {
                                if (auditors[i].id == _id) {
                                    auditors.splice(i, 1);
                                    $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });
            // 添加上下级
            $('#lr_level_auditor').on('click', function () {
                learun.layerForm({
                    id: 'AuditorLevelForm',
                    title: '添加上下级',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/LevelForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }
                        });
                    }
                });
            });
            // 添加某节点执行人
            $('#lr_node_auditor').on('click', function () {
                learun.layerForm({
                    id: 'AuditorNodeForm',
                    title: '添加某节点执行人',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/AuditorNodeForm?layerId=' + layerId,
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }
                        });
                    }
                });
            });
            // 添加表字段作为审核人
            $('#lr_form_auditor').on('click', function () {
                learun.layerForm({
                    id: 'AuditorFieldForm',
                    title: '添加表字段',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/AuditorFieldForm?layerId=' + layerId,
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }
                        });
                    }
                });
            });

            /*表单添加*/
            $('#workform_girdtable').jfGrid({
                headData: [
                    { label: "名称", name: "name", width: 160, align: "left" },
                    {
                        label: "类型", name: "type", width: 85, align: "left",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == 1) {
                                return '<span class=\"label label-success \" style=\"cursor: pointer;\">自定义表单</span>';
                            } else if (cellvalue == 0) {
                                return '<span class=\"label label-warning \" style=\"cursor: pointer;\">系统表单</span>';
                            }
                        }
                    },
                    { label: "地址", name: "url", width: 200, align: "left" }
                ],
                isSubGrid: true,
                subGridExpanded: function (subid, rowdata) {
                    $('#' + subid).jfGrid({
                        headData: [
                           {
                               label: "字段名称", name: "fieldName", width: 240, align: "left",
                               edit: {
                                   type: rowdata.type == '0' ? 'input' : 'label',
                                   change: function (data, num) {// 行数据和行号
                                       workformMap[rowdata.id].authorize[data.id] = data;
                                   }
                               }
                           },
                           {
                               label: "字段ID", name: "fieldId", width: 240, align: "left",
                               edit: {
                                   type: rowdata.type == '0' ? 'input' : 'label',
                                   change: function (data, num) {// 行数据和行号
                                       workformMap[rowdata.id].authorize[data.id] = data;
                                   }
                               }
                           },
                           {
                               label: "查看", name: "isLook", width: 70, align: "center",
                               formatter: function (cellvalue, row, dfop, $dcell) {
                                   $dcell.on('click', function () {

                                       if (row.isLook == 1) {
                                           if (dfop.isEdit) {// 系统表单
                                               workformMap[rowdata.id].authorize[row.id].isLook = 0;
                                           }
                                           else {// 自定义表单
                                               var _formAuthorize = workformMap[row.formId].authorize;
                                               _formAuthorize[row.fieldId] = _formAuthorize[row.fieldId] || { isLook: 1, isEdit: 1 };
                                               _formAuthorize[row.fieldId].isLook = 0;
                                           }
                                           row.isLook = 0;
                                           $(this).html('<span class=\"label label-default \" style=\"cursor: pointer;\">否</span>');
                                       }
                                       else {
                                           if (dfop.isEdit) {// 系统表单
                                               workformMap[rowdata.id].authorize[row.id].isLook = 1;
                                           }
                                           else {// 自定义表单
                                               var _formAuthorize = workformMap[row.formId].authorize;
                                               _formAuthorize[row.fieldId] = _formAuthorize[row.fieldId] || { isLook: 1, isEdit: 1 };
                                               _formAuthorize[row.fieldId].isLook = 1;
                                           }

                                           row.isLook = 1;
                                           $(this).html('<span class=\"label label-success \" style=\"cursor: pointer;\">是</span>');
                                       }
                                   });
                                   if (cellvalue == 1) {
                                       return '<span class=\"label label-success \" style=\"cursor: pointer;\">是</span>';
                                   } else if (cellvalue == 0) {
                                       return '<span class=\"label label-default \" style=\"cursor: pointer;\">否</span>';
                                   }
                               }
                           },
                           {
                               label: "编辑", name: "isEdit", width: 70, align: "center",
                               formatter: function (cellvalue, row, dfop, $dcell) {
                                   $dcell.on('click', function () {
                                       if (row.isEdit == 1) {
                                           if (dfop.isEdit) {// 系统表单
                                               workformMap[rowdata.id].authorize[row.id].isEdit = 0;
                                           }
                                           else {// 自定义表单
                                               var _formAuthorize = workformMap[row.formId].authorize;
                                               _formAuthorize[row.fieldId] = _formAuthorize[row.fieldId] || { isLook: 1, isEdit: 1 };
                                               _formAuthorize[row.fieldId].isEdit = 0;
                                           }

                                           row.isEdit = 0;
                                           $(this).html('<span class=\"label label-default \" style=\"cursor: pointer;\">否</span>');
                                       }
                                       else {
                                           if (dfop.isEdit) {// 系统表单
                                               workformMap[rowdata.id].authorize[row.id].isEdit = 1;
                                           }
                                           else {// 自定义表单
                                               var _formAuthorize = workformMap[row.formId].authorize;
                                               _formAuthorize[row.fieldId] = _formAuthorize[row.fieldId] || { isLook: 1, isEdit: 1 };
                                               _formAuthorize[row.fieldId].isEdit = 1;
                                           }

                                           row.isEdit = 1;
                                           $(this).html('<span class=\"label label-success \" style=\"cursor: pointer;\">是</span>');
                                       }
                                   });
                                   if (cellvalue == 1) {
                                       return '<span class=\"label label-success \" style=\"cursor: pointer;\">是</span>';
                                   } else if (cellvalue == 0) {
                                       return '<span class=\"label label-default \" style=\"cursor: pointer;\">否</span>';
                                   }
                               }
                           }
                        ],
                        onAddRow: function (row, rows) {//行数据和所有行数据
                            row.isLook = 0;
                            row.isEdit = 0;
                            row.id = learun.newGuid();
                            workformMap[rowdata.id].authorize[row.id] = row;
                        },
                        onMinusRow: function (row, rows) {//行数据和所有行数据
                            if (workformMap[rowdata.id].authorize[row.jfgridRowData.id]) {
                                delete workformMap[rowdata.id].authorize[row.jfgridRowData.id];
                            }
                        },
                        isEdit: rowdata.type == '0' ? true : false
                    });
                    if (rowdata.type == '1') {
                        // 自定义表单添加自定义表单字段
                        var _formAuthorize = workformMap[rowdata.formId].authorize;
                        $.lrSetForm(top.$.rootUrl + '/LR_FormModule/Custmerform/GetFormData?keyValue=' + rowdata.formId, function (data) {
                            var scheme = JSON.parse(data.schemeEntity.F_Scheme);
                            var _authorize = [];
                            for (var i = 0, l = scheme.data.length; i < l; i++) {
                                var componts = scheme.data[i].componts;
                                for (var j = 0, jl = componts.length; j < jl; j++) {
                                    var compont = componts[j];
                                    if (compont.type == 'gridtable' || compont.type == 'girdtable') {
                                        $.each(compont.fieldsData, function (_i, _item) {
                                            if (_item.type != 'guid') {
                                                var point = { formId: rowdata.formId, fieldName: compont.title + '-' + _item.name, fieldId: compont.id + '|' + _item.id, isLook: '1', isEdit: '1' };
                                                if (_formAuthorize[point.fieldId]) {
                                                    point.isLook = _formAuthorize[point.fieldId].isLook;
                                                    point.isEdit = _formAuthorize[point.fieldId].isEdit;
                                                }
                                                _authorize.push(point);
                                            }
                                        });
                                    }
                                    else {
                                        var point = { formId: rowdata.formId, fieldName: compont.title, fieldId: compont.id, isLook: '1', isEdit: '1' };
                                        if (_formAuthorize[point.fieldId]) {
                                            point.isLook = _formAuthorize[point.fieldId].isLook;
                                            point.isEdit = _formAuthorize[point.fieldId].isEdit;
                                        }
                                        _authorize.push(point);
                                    }
                                }
                            }
                            $('#' + subid).jfGridSet('refreshdata', _authorize);
                        });
                    }
                    else {
                        var _authorize = [];
                        $.each(workformMap[rowdata.id].authorize, function (_index, _item) {
                            _authorize.push(_item);
                        });

                        $('#' + subid).jfGridSet('refreshdata', _authorize);
                    }



                }// 子表展开后调用函数
            });
            $('#lr_add_workform').on('click', function () {
                learun.layerForm({
                    id: 'WorkformForm',
                    title: '添加表单',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/WorkformForm',
                    width: 400,
                    height: 320,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            //需要判断表单的重复性
                            for (var i = 0, l = workforms.length; i < l; i++) {
                                if (data.formId != "") {
                                    if (data.formId == workforms[i].formId) {
                                        learun.alert.error('重复添加表单');
                                        return false;
                                    }
                                }
                                else {
                                    if (data.url == workforms[i].url) {
                                        learun.alert.error('重复添加表单');
                                        return false;
                                    }
                                }
                            }

                            data.id = learun.newGuid();
                            workforms.push(data);
                            data.authorize = {};
                            if (data.type == '0') {// 系统表单
                                workformMap[data.id] = data;
                            }
                            else {// 自定义表单
                                workformMap[data.formId] = data;
                            }
                            $('#workform_girdtable').jfGridSet('refreshdata', workforms);
                        });
                    }
                });
            });
            $('#lr_edit_workform').on('click', function () {
                var _id = $('#workform_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerForm({
                        id: 'WorkformForm',
                        title: '编辑表单',
                        url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/WorkformForm?id=' + _id,
                        width: 400,
                        height: 320,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                for (var i = 0, l = workforms.length; i < l; i++) {
                                    if (workforms[i].id != _id) {
                                        if (data.formId != "") {
                                            if (data.formId == workforms[i].formId) {
                                                learun.alert.error('重复添加表单');
                                                return false;
                                            }
                                        }
                                        else {
                                            if (data.url == workforms[i].url) {
                                                learun.alert.error('重复添加表单');
                                                return false;
                                            }
                                        }
                                    }
                                }

                                for (var i = 0, l = workforms.length; i < l; i++) {
                                    if (workforms[i].id == _id) {
                                        if (workforms[i].formId != data.formId) {
                                            delete workformMap[workforms[i].formId];
                                            data.authorize = {};
                                            workformMap[data.formId] = data;
                                        }
                                        workforms[i] = data;
                                        $('#workform_girdtable').jfGridSet('refreshdata', workforms);
                                        break;
                                    }
                                }
                            });
                        }
                    });
                }
            });
            $('#lr_delete_workform').on('click', function () {
                var _id = $('#workform_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该表单！', function (res, index) {
                        if (res) {
                            for (var i = 0, l = workforms.length; i < l; i++) {
                                if (workforms[i].id == _id) {
                                    if (workforms[i].type == '0') {
                                        delete workformMap[workforms[i].id];
                                    }
                                    else {
                                        delete workformMap[workforms[i].formId];
                                    }

                                    workforms.splice(i, 1);
                                    $('#workform_girdtable').jfGridSet('refreshdata', workforms);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });

            /*超时设置*/
            // 超时通知策略
            $('#timeoutStrategy').lrselect({
                text: 'F_StrategyName',
                value: 'F_StrategyCode',
                url: top.$.rootUrl + '/LR_Message/LR_StrategyInfo/GetList'
            });
            
            /*按钮设置*/
            $('#btn_girdtable').jfGrid({
                headData: [
                    {
                        label: "名称", name: "name", width: 180, align: "left"
                    },
                    {
                        label: "编码", name: "code", width: 180, align: "left"
                    },
                    {
                        label: "是否隐藏", name: "isHide", width: 80, align: "left",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == '1') {
                                return '是';
                            } else {
                                return '否';
                            }
                        }
                    },
                    {
                        label: "需要签名或盖章", name: "isSign", width: 100, align: "left",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == '1') {
                                return '需要';
                            } else {
                                return '不需要';
                            }
                        }
                    },
                    {
                        label: "下一节点审核人", name: "next", width: 100, align: "left",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == '1') {
                                return '不能手动设置';
                            } else if (cellvalue == '2') {
                                return '能手动设置';
                            }
                        }
                    }
                    
                ]
            });
            $('#btn_girdtable').jfGridSet('refreshdata', btnList);
            $('#lr_add_btns').on('click', function () {
                learun.layerForm({
                    id: 'ButtonForm',
                    title: '添加按钮',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/ButtonForm',
                    width: 400,
                    height: 320,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            var _flag = true;
                            $.each(btnList, function (_index,_item) {
                                if (_item.code == data.code) {
                                    learun.alert.error('按钮编码重复！');
                                    _flag = false;
                                    return false;
                                }
                                else if (_item.name == data.name) {
                                    learun.alert.error('按钮名字重复！');
                                    _flag = false;
                                    return false;
                                }
                            });
                            if (_flag) {
                                btnList.push(data);
                                $('#btn_girdtable').jfGridSet('refreshdata', btnList);
                            }
                            return _flag;
                        });
                    }
                });
            });
            $('#lr_edit_btns').on('click', function () {
                var _id = $('#btn_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerForm({
                        id: 'ButtonForm',
                        title: '编辑按钮',
                        url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/ButtonForm?id=' + _id,
                        width: 400,
                        height: 320,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                for (var i = 0, l = btnList.length; i < l; i++) {
                                    if (btnList[i].id != _id) {
                                        if (btnList[i].code == data.code) {
                                            learun.alert.error('按钮编码重复！');
                                            return false;
                                        }
                                        else if (btnList[i].name == data.name) {
                                            learun.alert.error('按钮名字重复！');
                                            return false;
                                        }
                                    }
                                }

                                for (var i = 0, l = btnList.length; i < l; i++) {
                                    if (btnList[i].id == _id) {
                                        btnList[i] = data;
                                        $('#btn_girdtable').jfGridSet('refreshdata', btnList);
                                        break;
                                    }
                                }
                            });
                        }
                    });
                }
            });
            $('#lr_delete_btns').on('click', function () {
                var _id = $('#btn_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    if (_id == '1' || _id == '2') {
                        learun.alert.error('同意和不同意按钮不允许删除！');
                        return false;
                    }
                    learun.layerConfirm('是否确认删除该按钮！', function (res, index) {
                        if (res) {
                            for (var i = 0, l = btnList.length; i < l; i++) {
                                if (btnList[i].id == _id) {
                                    btnList.splice(i, 1);
                                    $('#btn_girdtable').jfGridSet('refreshdata', btnList);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });

            /*条件节点*/
            // 条件节点字段条件设置
            $('#condition_girdtable').jfGrid({
                headData: [
                    {
                        label: "数据库", name: "dbId", width: 100, align: "left",
                        formatterAsync: function (callback, value, row) {
                            if (value == 'systemdb') {
                                callback('本地数据库');
                            }
                            else {
                                learun.clientdata.getAsync('db', {
                                    key: value,
                                    callback: function (item) {
                                        callback(item.alias);
                                    }
                                });
                            }
                        }
                    },
                    { label: "数据表", name: "table", width: 100, align: "left" },
                    { label: "关联字段", name: "field1", width: 100, align: "left" },
                    { label: "比较字段", name: "field2", width: 100, align: "left" },
                    {
                        label: "比较类型", name: "compareType", width: 80, align: "center",
                        formatter: function (cellvalue, row) {
                            switch (cellvalue)// 比较类型1.等于2.不等于3.大于4.大于等于5.小于6.小于等于7.包含8.不包含9.包含于10.不包含于
                            {
                                case '1':
                                    return '等于';
                                    break;
                                case '2':
                                    return '不等于';
                                    break;
                                case '3':
                                    return '大于';
                                    break;
                                case '4':
                                    return '大于等于';
                                    break;
                                case '5':
                                    return '小于';
                                    break;
                                case '6':
                                    return '小于等于';
                                    break;
                                case '7':
                                    return '包含';
                                    break;
                                case '8':
                                    return '不包含';
                                    break;
                                case '9':
                                    return '包含于';
                                    break;
                                case '10':
                                    return '不包含于';
                                    break;
                            }
                        }
                    },
                    { label: "数据值", name: "value", width: 100, align: "left" }
                ]
            });
            $('#lr_add_condition').on('click', function () {
                learun.layerForm({
                    id: 'AuthorizeForm',
                    title: '添加条件字段',
                    url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/ConditionFieldForm',
                    width: 400,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            conditions.push(data);
                            $('#condition_girdtable').jfGridSet('refreshdata', conditions);
                        });
                    }
                });
            });
            $('#lr_edit_condition').on('click', function () {
                var _id = $('#condition_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerForm({
                        id: 'AuthorizeForm',
                        title: '编辑条件字段',
                        url: top.$.rootUrl + '/LR_NewWorkFlow/NWFScheme/ConditionFieldForm?id=' + _id,
                        width: 400,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                for (var i = 0, l = conditions.length; i < l; i++) {
                                    if (conditions[i].id == _id) {
                                        conditions[i] = data;
                                        $('#condition_girdtable').jfGridSet('refreshdata', conditions);
                                        break;
                                    }
                                }
                            });
                        }
                    });
                }
            });
            $('#lr_delete_condition').on('click', function () {
                var _id = $('#condition_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该条件字段！', function (res, index) {
                        if (res) {
                            for (var i = 0, l = conditions.length; i < l; i++) {
                                if (conditions[i].id == _id) {
                                    conditions.splice(i, 1);
                                    $('#condition_girdtable').jfGridSet('refreshdata', conditions);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });
            // 条件节点设置
            $('#dbConditionId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true
            });

            // 子节点
            $('input[name="operationType"]').on('click', function () {
                var value = $(this).val();
                $('.operationDiv').hide();
                $('#' + value).show();
            });

            $('#dbId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true
            });
        },
        /*初始化数据*/
        initData: function () {
            $('#baseInfo').lrSetFormData(currentNode);
            $('#timeout').lrSetFormData(currentNode);

            $('#dbConditionId').lrselectSet(currentNode.dbConditionId);
            $('#conditionSql').val(currentNode.conditionSql || '');
            if (currentNode.wfForms) {
                workforms = currentNode.wfForms;
                $.each(workforms, function (_index, _item) {
                    _item.authorize = _item.authorize || {};
                    if (_item.type == '0') { // 系统表单
                        workformMap[_item.id] = _item;
                    }
                    else {// 自定义表单
                        workformMap[_item.formId] = _item;
                    }
                });
            }
            if (currentNode.auditors) {
                auditors = currentNode.auditors;
            }
            if (currentNode.btnList && currentNode.btnList.length >0) {
                btnList = currentNode.btnList;
            }
            if (currentNode.conditions) {
                conditions = currentNode.conditions;
            }

            $('#workform_girdtable').jfGridSet('refreshdata', workforms);
            $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
            $('#btn_girdtable').jfGridSet('refreshdata', btnList);
            $('#condition_girdtable').jfGridSet('refreshdata', conditions);


            $('#operationTypeDiv').lrSetFormData(currentNode);
            switch (currentNode.operationType) {
                case 'sql':
                    $('#dbId').lrselectSet(currentNode.dbId);
                    $('#strSql').val(currentNode.strSql);
                    break;
                case 'interface':
                    $('#strInterface').val(currentNode.strInterface);
                    break;
                case 'ioc':
                    $('#iocName').val(currentNode.iocName);
                    break;
            }
          
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#baseInfo').lrValidform()) {
            return false;
        }
        var baseInfo = $('#baseInfo').lrGetFormData();
        switch (currentNode.type) {
            case 'startround':// 开始节点
                currentNode.notice = baseInfo.notice;
                currentNode.isNext = baseInfo.isNext;
                currentNode.isTitle = baseInfo.isTitle;

                currentNode.wfForms = workforms;
                break;
            case 'stepnode':
                currentNode.name = baseInfo.name;
                currentNode.notice = baseInfo.notice;
                currentNode.isAllAuditor = baseInfo.isAllAuditor;
                currentNode.auditorAgainType = baseInfo.auditorAgainType;
                currentNode.auditorType = baseInfo.auditorType;
                currentNode.auditExecutType = baseInfo.auditExecutType;
                

                currentNode.isSign = baseInfo.isSign;
                currentNode.isBatchAudit = baseInfo.isBatchAudit;
                currentNode.auditors = auditors;


                currentNode.agreeGz = baseInfo.agreeGz;
                currentNode.noPeopleGz = baseInfo.noPeopleGz;
                currentNode.wfForms = workforms;

                var timeout = $('#timeout').lrGetFormData();

                currentNode.timeoutNotice = timeout.timeoutNotice;
                currentNode.timeoutInterval = timeout.timeoutInterval;
                currentNode.timeoutStrategy = timeout.timeoutStrategy;
                currentNode.timeoutAction = timeout.timeoutAction;

                currentNode.btnList = btnList;
                break;
            case 'auditornode':
                currentNode.name = baseInfo.name;
                currentNode.notice = baseInfo.notice;
                currentNode.isBatchAudit = baseInfo.isBatchAudit;
                currentNode.auditors = auditors;

                currentNode.wfForms = workforms;
                break;
            case 'confluencenode':
                currentNode.name = baseInfo.name;
                currentNode.confluenceType = baseInfo.confluenceType;
                currentNode.confluenceRate = baseInfo.confluenceRate;

                break;
            case 'conditionnode':
                currentNode.name = baseInfo.name;
                currentNode.conditions = conditions;

                currentNode.dbConditionId = $('#dbConditionId').lrselectGet();
                currentNode.conditionSql = $('#conditionSql').val();
                break;
            case 'childwfnode':
                currentNode.name = baseInfo.name;
                currentNode.notice = baseInfo.notice;
                currentNode.childFlow = baseInfo.childFlow;
                currentNode.childType = baseInfo.childType;
                currentNode.auditors = auditors;
                var operation = $('#operationTypeDiv').lrGetFormData();
                currentNode.operationType = operation.operationType;
                switch (currentNode.operationType) {
                    case 'sql':
                        currentNode.dbId = $('#dbId').lrselectGet();
                        currentNode.strSql = $('#strSql').val();
                        break;
                    case 'interface':
                        currentNode.strInterface = $('#strInterface').val();
                        break;
                    case 'ioc':
                        currentNode.iocName = $('#iocName').val();
                        break;
                }
                break;
        };

        callBack();
        return true;
    };
    page.init();
}