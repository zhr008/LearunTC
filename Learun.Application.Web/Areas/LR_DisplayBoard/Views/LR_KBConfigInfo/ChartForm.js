/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-09-20 09:41
 * 描  述：饼图/柱状图/折线图/仪表盘模块配置信息
 */
var acceptClick;
var type = request('type');

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
                allowSearch: true,
                select: function () {
                    var datasourceId = $('#F_DataSourceId').lrselectGet();
                    var sql = $("#F_Sql").val();
                    if (datasourceId && sql) {
                        page.sqldata(datasourceId, sql, function (data) {
                            var _v = $('#F_FullName').lrselectGet();
                            $('#F_FullName').lrselectRefresh({ data: data });
                            $('#F_FullName').lrselectSet(_v);

                            var _v1 = $('#F_Value').lrselectGet();
                            $('#F_Value').lrselectRefresh({ data: data });
                            $('#F_Value').lrselectSet(_v1);
                        });
                    }
                }

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

            // 数据名对应字段
            $('#F_FullName').lrselect({
                value: 'F_FullName',
                text: 'F_FullName',
                title: 'F_FullName',
                allowSearch: true
            });
            // 数据值对应字段
            $('#F_Value').lrselect({
                value: 'F_FullName',
                text: 'F_FullName',
                title: 'F_FullName',
                allowSearch: true
            });

            $("#F_Sql").on('blur', function () {
                var datasourceId = $('#F_DataSourceId').lrselectGet();
                var sql = $("#F_Sql").val();
                if (datasourceId && sql) {
                    page.sqldata(datasourceId, sql, function (data) {
                        var _v = $('#F_FullName').lrselectGet();
                        $('#F_FullName').lrselectRefresh({ data: data });
                        $('#F_FullName').lrselectSet(_v);

                        var _v1 = $('#F_Value').lrselectGet();
                        $('#F_Value').lrselectRefresh({ data: data });
                        $('#F_Value').lrselectSet(_v1);
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
                        var _v = $('#F_FullName').lrselectGet();
                        $('#F_FullName').lrselectRefresh({ data: data });
                        $('#F_FullName').lrselectSet(_v);

                        var _v1 = $('#F_Value').lrselectGet();
                        $('#F_Value').lrselectRefresh({ data: data });
                        $('#F_Value').lrselectSet(_v1);
                    });
                } else {
                    learun.alert.warning('请检查填写接口地址');
                }
            });

            $("#btn_finish").on('click', function () {
                acceptClick();
            })
        },
        initData: function () {
            if (selectedModel) {
                $('#step-1').lrSetFormData(selectedModel);
                if (selectedModel._configuration) {
                    $("#step-2").lrSetFormData(selectedModel._configuration);
                    if (selectedModel._configuration.F_DataSourceId && selectedModel._configuration.F_Sql) {
                        page.sqldata(selectedModel._configuration.F_DataSourceId, selectedModel._configuration.F_Sql, function (data) {
                            $('#F_FullName').lrselectRefresh({ data: data });
                            $('#F_Value').lrselectRefresh({ data: data });
                            $('#F_FullName').lrselectSet(selectedModel._configuration.F_FullName);
                            $('#F_Value').lrselectSet(selectedModel._configuration.F_Value);
                        });
                    }
                    else if (selectedModel._configuration.F_Interface) {
                        $("input[name=radio][value=api]").trigger('click');
                        page.interfaceData(selectedModel._configuration.F_Interface, function (data) {
                            $('#F_FullName').lrselectRefresh({ data: data });
                            $('#F_Value').lrselectRefresh({ data: data });
                            $('#F_FullName').lrselectSet(selectedModel._configuration.F_FullName);
                            $('#F_Value').lrselectSet(selectedModel._configuration.F_Value);
                        });
                    }
                    $('#F_LinkInfo').val(selectedModel._configuration.F_LinkInfo);
                }      
            }
        },

        sqldata: function (databaseLinkId, strSql, callback) {// 根据sql语句获取字段列表
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
        var configratoin = $('#step-2').lrGetFormData();

        if ($("input[type = 'radio']:checked").val() == "sql") {
            if (configratoin.F_DataSourceId == "" || configratoin.F_Sql == "") {
                learun.alert.error('请检查数据库或sql语句是否输入');
                return false;
            }
            configratoin.F_Interface = '';
        } else {
            if (configratoin.F_Interface == "") {
                learun.alert.error('请检查填写接口地址');
                return false;
            }
            configratoin.F_DataSourceId = '';
            configratoin.F_Sql = '';
        }
       
        var data = {
            F_ModeName: baseInfo.F_ModeName,
            F_TopValue: baseInfo.F_TopValue,
            F_LeftValue: baseInfo.F_LeftValue,
            F_WidthValue: baseInfo.F_WidthValue,
            F_HightValue: baseInfo.F_HightValue,
            F_RefreshTime: baseInfo.F_RefreshTime,
            F_Type: type,
            _configuration: {
                F_LinkInfo: baseInfo.F_LinkInfo,
                F_DataSourceId: configratoin.F_DataSourceId,
                F_Sql: configratoin.F_Sql,
                F_Interface: configratoin.F_Interface,
                F_FullName: configratoin.F_FullName,
                F_Value: configratoin.F_Value
            }
        };

        if (selectedModel) {
            selectedModel.F_ModeName = data.F_ModeName;
            selectedModel.F_TopValue = data.F_TopValue;
            selectedModel.F_LeftValue = data.F_LeftValue;
            selectedModel.F_WidthValue = data.F_WidthValue;
            selectedModel.F_HightValue = data.F_HightValue;
            selectedModel.F_RefreshTime = data.F_RefreshTime;
            selectedModel._configuration = data._configuration;

            top.layer_form.displayBoard.updateModel(selectedModel);
        }
        else {
            top.layer_form.displayBoard.addModel(data);
        }
        learun.layerClose(window.name);
        return true;
    };
    page.init();
}
