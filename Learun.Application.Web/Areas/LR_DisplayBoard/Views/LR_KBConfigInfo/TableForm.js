/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-09-20 09:41
 * 描  述：表格模块配置信息
 */
var acceptClick;
var fields = [];

var sqlFieldMap = {};
var urlFieldMap = {};

var bootstrap = function ($, learun) {
    "use strict";
    var selectedModel = top.layer_form.selectedModel;

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            ///显示比例
            $('#F_WidthValue').lrselect();
            $('#F_LeftValue').lrselect();
            //数据库
            $('#F_DataSourceId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true
            });
            // 接口提示
            $('#lr-info').hover(function () { $('#lr-message').show(); }, function () { $('#lr-message').hide(); });         
            //选中的sql或api的操作
            sqlOrAPiIsShow();
            $("input[type = 'radio']").click(function () {
                sqlOrAPiIsShow();
            });
            function sqlOrAPiIsShow() {
                if ($("input[type = 'radio']:checked").val() == "sql") {
                    $("#db").show();
                    $("#sql").show();
                    $("#api").hide();
                }
                else {
                    $("#db").hide();
                    $("#sql").hide();
                    $("#api").show();
                }
            }

            // 加载导向
            $('#wizard').wizard().on('change', function (e, data) {
                var $finish = $("#btn_finish");
                var $next = $("#btn_next");
                if (data.direction == "next") {
                    if (!$('#step-1').lrValidform()) {
                        return false;
                    }
                    $next.attr('disabled', 'disabled');
                    $finish.removeAttr('disabled');
                }
                else if (data.direction == "previous") {
                    $finish.attr('disabled', 'disabled');
                    $next.removeAttr('disabled');
                }
                else {
                    $finish.attr('disabled', 'disabled');
                    $next.removeAttr('disabled');
                }
            });

            $("#F_Sql").on('blur', function () {
                var datasourceId = $('#F_DataSourceId').lrselectGet();
                var sql = $("#F_Sql").val();
                if (datasourceId && sql) {
                    page.sqldata(datasourceId, sql, function (data) {
                        fields = data;
                    });
                }
                else {
                    learun.alert.warning('请检查数据库或sql语句是否输入');
                }
            });
            $("#F_Interface").on('blur', function () {
                var path = $("#F_Interface").val();
                if (!!path) {
                    page.interfaceData(path, function (data) {
                        fields = data;
                    });
                } else {
                    learun.alert.warning('请检查填写接口地址');
                }
            });

            $('#btns_girdtable').jfGrid({
                headData: [
                    {
                        label: "", name: "btn1", width: 50, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#btns_girdtable').jfGridSet('moveUp', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-info\" style=\"cursor: pointer;\">上移</span>';
                        }
                    },
                    {
                        label: "", name: "btn2", width: 50, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#btns_girdtable').jfGridSet('moveDown', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-success\" style=\"cursor: pointer;\">下移</span>';
                        }
                    },
                    {
                        label: "字段项名称", name: "F_FullName", width: 150, align: "left",
                        formatter: function (value, row, op, $cell) {
                            return row.F_FullName || '';
                        },
                        edit: {
                            type: 'select',
                            init: function (row, $self) {// 选中单元格后执行
                                $self.lrselectRefresh({
                                    data: fields
                                });
                            },
                            op: {
                                value: 'F_FullName',
                                text: 'F_FullName',
                                allowSearch: true
                            },
                            change: function (rowData, rowIndex, item) {
                                if (item != null) {
                                    rowData.id = item.table + item.F_FullName;
                                }
                                else {
                                    rowData.id = '';
                                }
                            }
                        }
                    },
                    {
                        label: "标题", name: "F_HeadTitle", width: 120, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "宽度(百分比%)", name: "F_Width", width: 100, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: '对齐', name: 'F_Align', width: 100, align: 'left',
                        edit: {
                            type: 'select',
                            op: {// 下拉框设置参数 和 lrselect一致
                                data: [
                                    { 'id': 'left', 'text': '左对齐' },
                                    { 'id': 'center', 'text': '居中' },
                                    { 'id': 'right', 'text': '右对齐' }
                                ]
                            }
                        }
                    },
                ],
                mainId: 'id',
                isEdit: true,
                isMultiselect: true
            });
            $("#btn_finish").on('click', function () {
                acceptClick();
            })
        },
        initData: function () {
            if (selectedModel) {
                $('#step-1').lrSetFormData(selectedModel);
                if (selectedModel._configuration) {
                    var configuration = selectedModel._configuration;
                    $("#tabconfigration").lrSetFormData(configuration);
                    if (configuration.F_DataSourceId && configuration.F_Sql) {
                        page.sqldata(configuration.F_DataSourceId, configuration.F_Sql, function (data) {
                            fields = data;
                        });
                    }
                    else if (configuration.F_Interface) {
                        $("input[name=radio][value=api]").trigger('click');
                        page.interfaceData(configuration.F_Interface, function (data) {
                            fields = data;
                        });
                    }
                    $('#F_LinkInfo').val(configuration.F_LinkInfo);
                    $('#btns_girdtable').jfGridSet('refreshdata', configuration.grid);
                }
            }
        },

        sqldata: function (databaseLinkId, strSql,callback) {// 根据sql语句获取字段列表
            if (sqlFieldMap[strSql]) {
                callback(sqlFieldMap[strSql]);
            } else {
                learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetSqlColName', { databaseLinkId: databaseLinkId, strSql: strSql }, function (data) {
                    if (data) {
                        sqlFieldMap[strSql] = [];
                        $.each(data, function (_index, _item) {
                            sqlFieldMap[strSql].push({ F_FullName: _item });
                        });
                    }
                    callback(sqlFieldMap[strSql] || []);
                });
            }
        },
        interfaceData: function (path, callback) {
            if (urlFieldMap[path]) {
                callback(urlFieldMap[path]);
            } else {
                learun.httpAsync('GET', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetApiData', { path: path }, function (data) {
                    if (data) {
                        urlFieldMap[path] = [];
                        $.each(data[0] || [], function (_index, _item) {
                            urlFieldMap[path].push({ F_FullName: _index });
                        });
                    }
                    callback(urlFieldMap[path] || []);
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#step-1').lrValidform()) {
            return false;
        }
        var baseInfo = $('#step-1').lrGetFormData();

        var tabconfigration = $("#tabconfigration").lrGetFormData();
        if ($("input[type = 'radio']:checked").val() == "sql") {
            if (tabconfigration.F_DataSourceId == "" || tabconfigration.F_Sql == "") {
                learun.alert.error('请检查数据库或sql语句是否输入');
                return false;
            }
            tabconfigration.F_Interface = '';
        } else {
            if (tabconfigration.F_Interface == "") {
                learun.alert.error('请检查填写接口地址');
                return false;
            }
            tabconfigration.F_DataSourceId = '';
            tabconfigration.F_Sql = '';
        }

        var tabcorrespond = $('#btns_girdtable').jfGridGet('rowdatas');
        if (tabcorrespond.length <= 0) {
            learun.alert.error('请新增显示字段');
            return false;
        }

        var data = {
            F_ModeName: baseInfo.F_ModeName,
            F_TopValue: baseInfo.F_TopValue,
            F_LeftValue: baseInfo.F_LeftValue,
            F_WidthValue: baseInfo.F_WidthValue,
            F_HightValue: baseInfo.F_HightValue,
            F_RefreshTime: baseInfo.F_RefreshTime,
            F_Type: 'table',
            _configuration: {
                F_LinkInfo: baseInfo.F_LinkInfo,
                F_DataSourceId: tabconfigration.F_DataSourceId,
                F_Sql: tabconfigration.F_Sql,
                F_Interface: tabconfigration.F_Interface,
                grid: tabcorrespond
            }
        };

        if (selectedModel) {
            selectedModel.F_ModeName = data.F_ModeName;
            selectedModel.F_TopValue = data.F_TopValue;
            selectedModel.F_LeftValue = data.F_LeftValue;
            selectedModel.F_WidthValue = data.F_WidthValue;
            selectedModel.F_HightValue = data.F_HightValue;
            selectedModel.F_RefreshTime = data.F_RefreshTime;
            selectedModel.F_Type = data.F_Type;
            selectedModel._configuration = data._configuration;

            top.layer_form.displayBoard.updateModel(selectedModel);
        }
        else {
            top.layer_form.displayBoard.addModel(data);
        }
        learun.layerClose(window.name);
    };
    page.init();
}
