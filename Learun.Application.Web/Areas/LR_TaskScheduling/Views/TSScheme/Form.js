/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-10-19 16:07
 * 描  述：任务计划模板
 */

var keyValue = request('keyValue');
var acceptClick;
var frequencyList = [];
var setCorn;

var bootstrap = function ($, learun) {
    "use strict";

    var baseData;

    setCorn = function (value) {
        $('#cornValue').val(value);
    };

    var loadFrequencyData = function (data, rowid) {// 刷新条件
        if (data) {
            if (rowid != "") {
                frequencyList[rowid] = data;
            } else {
                frequencyList.push(data);
            }
        }
       

        var $list = $('#frequencyList');
        $list.html('');
        $.each(frequencyList, function (_index, _item) {
            var text = '每' + _item.carryMounth + '月的';
            switch (_item.type) {
                case 'day':
                    text += '每天'
                    break;
                case 'week':
                    text += '每周' + _item.carryDate + '天';
                    break;
                case 'month':
                    text += _item.carryDate+'号';
                    break;
            }

            text += _item.hour + '时' + _item.minute + '分执行';


            var $item = $('<div class="frequency-item">\
                                <div class="text">\
                                    <i class="num">' + (_index + 1) + '.</i>' + text + '\
                                </div>\
                                <div class="edit-del">\
                                    <a class="btn-edit" data-value="' + _index + '" >编辑</a>|<a class="btn-delete" data-value="' + _index + '">删除</a>\
                                </div>\
                            </div>');
            $list.append($item);
        });
    };

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            //加载导向
            $('#wizard').wizard().on('change', function (e, data) {
                var $finish = $("#btn_finish");
                var $next = $("#btn_next");
                if (data.direction == "next") {
                    if (!$('#step-1').lrValidform()) {
                        return false;
                    }
                    baseData = $('#form').lrGetFormData();

                    if (baseData.startType == '2' && baseData.startTime == '') {
                        learun.alert.error('请选择开始时间');
                        return false;
                    }
                    switch (baseData.executeType) {
                        case '1':// 只执行一次
                            break;
                        case '2':// 简单重复执行
                            if (baseData.simpleValue == "") {
                                learun.alert.error('请填写间隔时间');
                                return false;
                            }
                            else if (!(/^[-+]?\d+$/).test(baseData.simpleValue)) {
                                learun.alert.error('间隔时间必须为数字');
                                return false;
                            }
                            break;
                        case '3':// 明细频率设置
                            if (frequencyList.length <= 0) {
                                learun.alert.error('请添加明细频率');
                                return false;
                            } else {
                                baseData["frequencyList"] = frequencyList;
                            }
                            break;
                        case '4':// 表达式设置
                            if (baseData.cornValue == "") {
                                learun.alert.error('请设置表达式');
                                return false;
                            }
                            break;
                    }
                    if (baseData.endType == '2' && baseData.endTime == '') {
                        learun.alert.error('请选择结束时间');
                        return false;
                    }
                    if (baseData.isRestart == "1") {
                        if (baseData.restartMinute == "") {
                            learun.alert.error('请填写重启间隔时间');
                            return false;
                        }

                        if (baseData.restartNum == "") {
                            learun.alert.error('请填写重启次数');
                            return false;
                        }
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

            //开始时间
            $("#startType").find('input[type="radio"]').on('click', function () {
                var $this = $(this);
                var v = $this.val();
                if (v == 2) {
                    $('#start_time_set').show();
                }
                else {
                    $('#start_time_set').hide();
                }
            });

            //执行频率
            $("#executeType").find('input[type="radio"]').on('click', function () {
                var $this = $(this);
                var v = $this.val();
                $("#execute-repeat").hide();
                $("#lr_frequency_set").hide();
                $("#express_set").hide();
                switch (v) {
                    case '1':// 只执行一次
                        break;
                    case '2':// 简单重复执行
                        $("#execute-repeat").show();
                        break;
                    case '3':// 明细频率设置
                        $("#lr_frequency_set").show();
                        break;
                    case '4':// 表达式设置
                        $("#express_set").show();
                        break;

                }

            });
            // 简单重复执行
            $('#simpleType').lrselect({
                data: [{ id: 'minute', text: '分钟' }, { id: 'hours', text: '小时' }, { id: 'day', text: '天' }, { id: 'week', text: '周' }],
                placeholder:false
            }).lrselectSet('minute');

            // 明细频率设置
            $('#lr_frequency_add').on('click', function () {
                learun.layerForm({
                    id: 'AddDetailedForm',
                    title: '添加明细频率',
                    url: top.$.rootUrl + '/LR_TaskScheduling/TSScheme/AddDetailedForm',
                    width: 600,
                    height: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(loadFrequencyData);
                    }
                });
            });
            $('#frequencyList').on('click', '.edit-del a', function () {
                var $this = $(this);
                var _rowid = $this.attr('data-value');
                if ($this.hasClass('btn-edit')) {
                    learun.layerForm({
                        id: 'AddDetailedForm',
                        title: '编辑明细频率',
                        url: top.$.rootUrl + '/LR_TaskScheduling/TSScheme/AddDetailedForm?rowid=' + _rowid,
                        width: 600,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(loadFrequencyData);
                        }
                    });
                }
                else if ($this.hasClass('btn-delete')) {
                    frequencyList.splice(_rowid, 1);
                    loadFrequencyData();
                }
            });
    
            // 添加预置表达式
            $(".btn-select-express").on("click", function () {
                learun.layerForm({
                    id: 'SelectExpressForm',
                    title: '添加预置表达式',
                    url: top.$.rootUrl + '/LR_TaskScheduling/TSScheme/SelectExpressForm',
                    width: 600,
                    height: 500,
                    btn: null
                });
            });

            //结束时间
            $("#endType").find('input[type="radio"]').on('click', function () {
                var $this = $(this);
                var v = $this.val();
                if (v == 2) {
                    $('#end_time_set').show();
                }
                else {
                    $('#end_time_set').hide();
                }
            });
            //任务重启
            $("#isRestart").on('click', function () {
                if (!$(this).is(':checked')) {
                    $("#isShowRestartModule").hide();
                } else {
                    $("#isShowRestartModule").show()
                }
            });



            //SQL语句,存储过程,dll文件
            $("#methodType").find("input[type='radio']").on('click', function () {
                var v = $(this).val();

                $('#dbId').parent().hide();
                $('#strSql').parent().hide();
                $('#procName').parent().hide();
                $('#iocName').parent().hide();
                $('#urlType').parent().hide();
                $('#url').parent().hide();

                switch (v) {
                    case '1':// SQL语句
                        $('#dbId').parent().show();
                        $('#strSql').parent().show();
                        break;
                    case '2':// 存储过程
                        $('#dbId').parent().show();
                        $('#procName').parent().show();
                        break;
                    case '3':// 接口
                        $('#urlType').parent().show();
                        $('#url').parent().show();
                        break;
                    case '4':// Ioc依赖注入
                        $('#iocName').parent().show();
                        break;
                }
            });
            //数据库
            $('#dbId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true
            });
          
           

            $("#btn_finish").on('click', function () {
                acceptClick();
            })
        },
        initData: function () {
            if (keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_TaskScheduling/TSScheme/GetFormData?keyValue=' + keyValue, function (data) {//
                    $('#wizard-steps').lrSetFormData(data.schemeInfoEntity);
                    var scheme = JSON.parse(data.schemeEntity.F_Scheme);
                    $('#wizard-steps').lrSetFormData(scheme);
                    frequencyList = scheme.frequencyList || [];
                    loadFrequencyData();
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        var postData = {
            keyValue: keyValue,
            strSchemeInfo: JSON.stringify({
                F_Name: baseData.F_Name,
                F_Description: baseData.F_Description
            }),
        }

        var strScheme = {
            startType: baseData.startType,
            startTime: baseData.startTime,
            endType: baseData.endType,
            endTime: baseData.endTime,
            executeType: baseData.executeType,
            isRestart: baseData.isRestart
        };
        switch (strScheme.executeType) {
            case '1':// 只执行一次
                break;
            case '2':// 简单重复执行
                strScheme.simpleValue = baseData.simpleValue;
                strScheme.simpleType = baseData.simpleType;
                break;
            case '3':// 明细频率设置
                strScheme.frequencyList = baseData.frequencyList;
                break;
            case '4'://  表达式设置
                strScheme.cornValue = baseData.cornValue;
                break;
        }

        if (strScheme.isRestart == '1') {
            strScheme.restartMinute = baseData.restartMinute;
            strScheme.restartNum = baseData.restartNum;
        }


        var excu = $("#step-2").lrGetFormData();
       
        strScheme.methodType = excu.methodType;

        switch (excu.methodType) {
            case '1':// SQL语句
                strScheme.dbId = excu.dbId;
                strScheme.strSql = excu.strSql;
                break;
            case '2':// 存储过程
                strScheme.dbId = excu.dbId;
                strScheme.procName = excu.procName;
                break;
            case '3':// 接口
                strScheme.urlType = excu.urlType;
                strScheme.url = excu.url;
                break;
            case '4':// Ioc依赖注入
                strScheme.iocName = excu.iocName;
                break;
        }

        postData.strScheme = JSON.stringify(strScheme);

        
        $.lrSaveForm(top.$.rootUrl + '/LR_TaskScheduling/TSScheme/SaveForm', postData, function (res) {
            // 保存成功后才回调
            learun.frameTab.currentIframe().refreshGirdData();//index页列表刷新
        });
    };

    page.init();
}