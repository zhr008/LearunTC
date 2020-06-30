/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.11
 * 描 述：统计指标单个配置列表字段添加	
 */
var acceptClick;

var sqlFieldMap = {};
var urlFieldMap = {};

var bootstrap = function ($, learun) {
    "use strict";
    var selectColData = top.layer_form.selectColData;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            sqlOrAPiIsShow();
            $("input[type = 'radio']").click(function () {
                sqlOrAPiIsShow();
            });
            function sqlOrAPiIsShow() {
                if ($("input[type = 'radio'][name='radio']:checked").val() == "sql") {
                    $("#db").show();
                    $("#sql").show();
                    $("#api").hide();
                }
                else {
                    $("#db").hide();
                    $("#sql").hide();
                    $("#api").show();
                }

                if ($("input[type = 'radio'][name='radiot']:checked").val() == "sql") {
                    $("#dbt").show();
                    $("#sqlt").show();
                    $("#apit").hide();
                } else {
                    $("#dbt").hide();
                    $("#apit").hide();
                    $("#sqlt").show();
                }
            }

            ///显示值数据库
            $('#F_TBaseData').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true,
                select: function () {
                    var datasourceId = $('#F_TBaseData').lrselectGet();
                    var sql = $("#F_TSQL").val();
                    if (datasourceId && sql) {
                        page.sqldata(datasourceId, sql, function (data) {
                            var _v = $('#F_TitleValue').lrselectGet();
                            $('#F_TitleValue').lrselectRefresh({ data: data });
                            $('#F_TitleValue').lrselectSet(_v);
                        });
                    }
                }
            });
            ///副显示值数据库
            $('#F_SBaseData').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true,
                select: function () {
                    var datasourceId = $('#F_SBaseData').lrselectGet();
                    var sql = $("#F_SubSQL").val();
                    if (datasourceId && sql) {
                        page.sqldata(datasourceId, sql, function (data) {
                            var _v = $('#F_SubtitleValue').lrselectGet();
                            $('#F_SubtitleValue').lrselectRefresh({ data: data });
                            $('#F_SubtitleValue').lrselectSet(_v);
                        });
                    }
                }
            });

            $("#F_TSQL").on('blur', function () {
                var datasourceId = $('#F_TBaseData').lrselectGet();
                var sql = $("#F_TSQL").val();
                if (datasourceId && sql) {
                    page.sqldata(datasourceId, sql, function (data) {
                        var _v = $('#F_TitleValue').lrselectGet();
                        $('#F_TitleValue').lrselectRefresh({ data: data });
                        $('#F_TitleValue').lrselectSet(_v);
                    });
                }
                else {
                    learun.alert.warning('请检查数据库或sql语句是否输入');
                }
            });
            $("#F_TInterface").on('blur', function () {
                var path = $("#F_TInterface").val();
                if (!!path) {
                    page.InterfaceData(path, function (data) {
                        var _v = $('#F_TitleValue').lrselectGet();
                        $('#F_TitleValue').lrselectRefresh({ data: data });
                        $('#F_TitleValue').lrselectSet(_v);
                    });
                } else {
                    learun.alert.warning('请检查填写接口地址');
                }
            });
            $("#F_SubSQL").on('blur', function () {
                var datasourceId = $('#F_SBaseData').lrselectGet();
                var sql = $("#F_SubSQL").val();
                if (datasourceId && sql) {
                    page.sqldata(datasourceId, sql, function (data) {
                        var _v = $('#F_SubtitleValue').lrselectGet();
                        $('#F_SubtitleValue').lrselectRefresh({ data: data });
                        $('#F_SubtitleValue').lrselectSet(_v);
                    });
                }
                else {
                    learun.alert.warning('请检查数据库或sql语句是否输入');
                }
            });
            $("#F_SInterface").on('blur', function () {
                var path = $("#F_SInterface").val();
                if (!!path) {
                    page.InterfaceData(path, function (data) {
                        var _v = $('#F_SubtitleValue').lrselectGet();
                        $('#F_SubtitleValue').lrselectRefresh({ data: data });
                        $('#F_SubtitleValue').lrselectSet(_v);
                    });
                } else {
                    learun.alert.warning('请检查填写接口地址');
                }
            });

            // 显示值对应字段
            $('#F_TitleValue').lrselect({
                value: 'F_FullName',
                text: 'F_FullName',
                allowSearch: true
            });
            // 副显示值对应字段
            $('#F_SubtitleValue').lrselect({
                value: 'F_FullName',
                text: 'F_FullName',
                allowSearch: true
            });
        },
        initData: function () {
            if (selectColData) {
                $('#form').lrSetFormData(selectColData);
                if (selectColData.F_TBaseData && selectColData.F_TSQL) {
                    page.sqldata(selectColData.F_TBaseData, selectColData.F_TSQL, function (data) {
                        $('#F_TitleValue').lrselectRefresh({ data: data });
                        $('#F_TitleValue').lrselectSet(selectColData.F_TitleValue);
                    });
                }
                if (selectColData.F_SBaseData && selectColData.F_SubSQL) {
                    page.sqldata(selectColData.F_SBaseData, selectColData.F_SubSQL, function (data) {
                        $('#F_SubtitleValue').lrselectRefresh({ data: data });
                        $('#F_SubtitleValue').lrselectSet(selectColData.F_SubtitleValue);
                    });
                }

                if (selectColData.F_TInterface) {
                    $("input[name=radio][value=api]").trigger('click');
                    page.InterfaceData(selectColData.F_TInterface, function (data) {
                        $('#F_TitleValue').lrselectRefresh({ data: data });
                        $('#F_TitleValue').lrselectSet(selectColData.F_TitleValue);
                    });
                }
                if (selectColData.F_SInterface) {
                    $("input[name=radiot][value=api]").trigger('click');
                    page.InterfaceData(selectColData.F_SInterface, function (data) {
                        $('#F_SubtitleValue').lrselectRefresh({ data: data });
                        $('#F_SubtitleValue').lrselectSet(selectColData.F_SubtitleValue);
                    });
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
        rfaceData: function (path, callback) {
            if (urlFieldMap[path]) {
                callback(urlFieldMap[path]);
            } else {
                learun.httpAsync('GET', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetApiData', { path: path }, function (data) {
                    if (data) {
                        urlFieldMap[path] = [];
                        $.each(data, function (_index, _item) {
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
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();

        if ($("input[type = 'radio'][name='radio']:checked").val() == "sql") {
            if (formData.F_TBaseData == "" || formData.F_TSQL == "") {
                learun.alert.error('请检查数据库或sql语句是否输入');
                return false;
            }
            formData.F_TInterface = '';
        }
        else {
            if (formData.F_TInterface == "") {
                learun.alert.error('请检查填写接口地址');
                return false;
            }
            formData.F_TBaseData = '';
            formData.F_TSQL = '';
        }

        if ($("input[type = 'radio'][name='radiot']:checked").val() == "sql") {
            if (formData.F_SBaseData == "" || formData.F_SubSQL == "") {
                learun.alert.error('请检查数据库或sql语句是否输入');
                return false;
            }
            formData.F_SInterface = '';
        }
        else {
            if (formData.F_SInterface == "") {
                learun.alert.error('请检查填写接口地址');
                return false;
            }
            formData.F_SBaseData = '';
            formData.F_SubSQL = '';
        }
        if (selectColData) {
            $.extend(selectColData, formData);
        }
        formData.id = learun.newGuid();

        callBack(formData);
        return true;
    };
    page.init();
}