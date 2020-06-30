/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-10-19 16:07
 * 描  述：任务计划模板
 */
//var flg = 1;//给查看（收起）预置表达式做标记
var acceptClick;
var keyValue = request('keyValue');



var queryDataList = [];
var BaseData;
var cron;
var bootstrap = function ($, learun) {
    "use strict";
    var refreshData = function (label, data, rowid) {// 刷新条件
        if (rowid != "") {
            queryDataList[rowid] = data;
        }
        else {
            rowid = queryDataList.length;
            queryDataList.push(data);
            var $item = $('<div class="lr-query-item add-more-list" id="lr_query_item_' + rowid + '"><div class="list-text"></div><div class="edit-del"><a class="btn-edit">编辑</a>|<a class="btn-delete">删除</a></div></div>');
            $item.find('.btn-edit')[0].rowid = rowid;
            $item.find('.btn-delete')[0].rowid = rowid;
            $('#querylist').append($item);
        }
        $('#lr_query_item_' + rowid).find('.list-text').html('<i class="list-num">' + (rowid + 1) + '</i>' + label);
    };
    var loadData = function () {
        $('#querylist').html("");
        for (var i = 0, l = queryDataList.length; i < l; i++) {
            var _item = queryDataList[i];
            var lablevalue = "每";
            var datam = _item.F_CarryMounth.split(',');
            for (var h = 0; h < datam.length; h++) {
                if (h > 0) {
                    lablevalue += ',';
                }
                lablevalue += ToChinesNum(datam[h]);
            }
            lablevalue += "月";
            lablevalue += _item.F_SelectMinute;
            if (_item.F_CarryDate != "") {
                var datac = _item.F_CarryDate.split(',');
                for (var c = 0; c < datac.length; c++) {
                    if (c > 0) {
                        lablevalue += ',';
                    }
                    lablevalue += ToChinesNum(datac[c]);
                }
            }
            lablevalue += _item.F_Hour + "时" + _item.F_Minute + "分执行";
            var $item = $('<div class="lr-query-item add-more-list" id="lr_query_item_' + i + '"><div class="list-text"></div><div class="edit-del"><a class="btn-edit">编辑</a>|<a class="btn-delete">删除</a></div></div>');
            $item.find('.btn-edit')[0].rowid = i;
            $item.find('.btn-delete')[0].rowid = i;
            $('#querylist').append($item);
            $('#lr_query_item_' + i).find('.list-text').html('<i class="list-num">' + (i + 1) + '</i>' + lablevalue);
        }
    }
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
                    BaseData = $('#form').lrGetFormData();
                    ///开始时间处理
                    BaseData["F_IsStartTime"] = $("input[name=radio-start-time][type=radio]:checked").val();
                    if (BaseData.F_IsStartTime == "2") {//判断开始时间
                        if (BaseData.F_StartTime == "") {
                            learun.alert.error('请选择开始时间');
                            return false;
                        }
                    }
                    ////执行频率处理
                    BaseData["F_ExecutionType"] = $("input[name=radio-execute][type=radio]:checked").val();
                    if (BaseData.F_ExecutionType == "1") {
                        if (BaseData.F_SimpleValue == "") {
                            learun.alert.error('请检查间隔时间');
                            return false;
                        }
                    }
                    else if (BaseData.F_ExecutionType == "2") {
                        if (queryDataList.length <= 0) {
                            learun.alert.error('请添加明细条件');
                            return false;
                        } else {
                            BaseData["F_DetailFrequency"] = JSON.stringify(queryDataList);
                        }
                    }
                    else if (BaseData.F_ExecutionType == "3") {
                        if (BaseData.F_CornValue == "") {
                            learun.alert.error('请填写表达式');
                            return false;
                        }
                    }
                    ////结束时间处理
                    BaseData["F_IsEndTime"] = $("input[name=radio-end-time][type=radio]:checked").val();
                    if (BaseData.F_IsEndTime == "1") {
                        if (BaseData.F_EndTime == "") {
                            learun.alert.error('请选择结束时间');
                            return false;
                        }
                    }
                    ///任务重启处理
                    if (BaseData.F_IsRestart == "1") {
                        if (BaseData.F_MinuteValue == "" || BaseData.F_RestratValue == "") {
                            learun.alert.error('请检查重启时间或重启次数是否输入');
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
            $("#start_time").find('input[type="radio"]').each(function () {
                $(this).on("click", function () {
                    if ($(this).val() == 2 && $(this).is(':checked')) {
                        $(this).parent().parent().next().show();
                    } else {
                        $(this).parent().parent().next().hide();
                    }
                })
            });

            $(".btn-select-express").on("click", function () {
                cron = $('#F_CornValue').val();
                learun.layerForm({
                    id: 'SelectExpressForm',
                    title: '查看预置表达式',
                    url: top.$.rootUrl + '/LR_TaskScheduling/TS_TaskDesign/SelectExpressForm',
                    width: 600,
                    height: 540,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            $('#F_CornValue').val(data);
                        });
                    }
                });
            });
            //执行频率
            $("#execute").find('input[type="radio"]').each(function (index) {
                $(this).on("click", function () {
                    $("#execute-repeat").hide();
                    $("#ticket_set").hide();
                    $("#express_set").hide();
                    $('#querylist').hide();
                    $("#select_CronExpression").hide();
                    if ($(this).is(':checked')) {
                        if (index == 1) {
                            $("#execute-repeat").show()
                        }
                        if (index == 2) {
                            $("#ticket_set").show();
                            $('#querylist').show();
                        } else if (index == 3) {
                            $("#express_set").show();
                        } else { }

                    }
                })
            });

            //结束时间
            $("#end_time").find('input[type="radio"]').each(function () {
                $(this).on("click", function () {
                    if ($(this).val() == 1 && $(this).is(':checked')) {
                        $(this).parent().parent().next().show();
                    } else {
                        $(this).parent().parent().next().hide();
                    }
                })
            });
            //任务重启
            $("#F_IsRestart").on('click', function () {
                if (!$(this).is(':checked')) {
                    $("#isShowRestartModule").hide();
                } else {
                    $("#isShowRestartModule").show()
                }
            });
            //SQL语句,存储过程,dll文件
            $("#F_ScheduleType").find("input[type='radio']").each(function (index, item) {
                $(item).on("click", function () {
                    $("#db").hide();
                    $("#sql").hide();
                    $("#stored").hide();
                    $("#dll").hide();
                    $("#intertype").hide();
                    $("#inter").hide();
                    if (index == 0) {
                        $("#db").show();
                        $("#sql").show();
                    } else if (index == 1) {
                        $("#db").show();
                        $("#stored").show();
                    } else if (index == 2) {
                        $("#intertype").show();
                        $("#inter").show();
                    }
                    else {
                        $("#dll").show();
                    }
                })
            });
           
            //数据库
            $('#F_DataSourceId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true
            });
            $('#F_CronExpression').lrselect().on('change', function () {
                var corn = $('#F_CronExpression').lrselectGet();
                $("#F_CornValue").val(corn);
                $(".btn-select-express").text("查看预置表达式")
                $(".select-express").hide();
            });
            // 条件行
            $('#querylist').on('click', function (e) {
                var et = e.target || e.srcElement;
                var $et = $(et);
                if ($et.hasClass('btn-edit')) {
                    var _rowid = $et[0].rowid;
                    learun.layerForm({
                        id: 'AddDetailedForm',
                        title: '编辑明细频率',
                        url: top.$.rootUrl + '/LR_TaskScheduling/TS_TaskDesign/AddDetailedForm?rowid=' + _rowid,
                        width: 500,
                        height: 300,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshData);
                        }
                    });
                }
                else if ($et.hasClass('btn-delete')) {
                    var _rowid = $et[0].rowid;
                    queryDataList.splice(_rowid, 1);
                    loadData();
                }
            });

            // 添加条件
            $('#lr_query_add').on('click', function () {
                learun.layerForm({
                    id: 'AddDetailedForm',
                    title: '添加明细频率',
                    url: top.$.rootUrl + '/LR_TaskScheduling/TS_TaskDesign/AddDetailedForm',
                    width: 400,
                    height: 390,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshData);
                    }
                });
            });
            $("#btn_finish").on('click', function () {
                acceptClick();
            })
        },
        initData: function () {
            if (!!selectedRow) {
                $('#step-1').lrSetFormData(selectedRow);
                BaseData = JSON.parse(selectedRow.F_SchedulingPlan);
                $('#form').lrSetFormData(BaseData);
                ///开始时间处理
                $("input[name='radio-start-time'][value=" + BaseData.F_IsStartTime + "]").attr("checked", true);
                if (BaseData.F_IsStartTime == "2") {
                    $("#start_time_set").show();
                    $("#F_StartTime").val(BaseData.F_StartTime);

                }
                ///执行频率处理
                $("input[name='radio-execute'][value=" + BaseData.F_ExecutionType + "]").attr("checked", true);
                if (BaseData.F_ExecutionType == "1") {
                    $("#execute-repeat").show();
                    $("#ticket_set").hide();
                    $("#express_set").hide();
                }
                else if (BaseData.F_ExecutionType == "2") {
                    $("#execute-repeat").hide();
                    $("#ticket_set").show();
                    $("#express_set").hide();
                    $('#querylist').show();
                    queryDataList = JSON.parse(BaseData.F_DetailFrequency);
                    loadData();
                }
                else if (BaseData.F_ExecutionType == "3") {
                    $("#execute-repeat").hide();
                    $("#ticket_set").hide();
                    $("#express_set").show();
                }
                ///结束时间处理
                $("input[name='radio-end-time'][value=" + BaseData.F_IsEndTime + "]").attr("checked", true);
                if (BaseData.F_IsEndTime == "1") {
                    $("#end_time_set").show();
                    $("#F_EndTime").val(BaseData.F_EndTime);
                }
                $("#step-2").lrSetFormData(selectedRow);
                ///任务类型处理
                $("#db").hide();
                $("#sql").hide();
                $("#stored").hide();
                $("#dll").hide();
                $("#intertype").hide();
                $("#inter").hide();
                $("input[name='radio'][value=" + selectedRow.F_ScheduleType + "]").attr("checked", true);
                if (selectedRow.F_ScheduleType == "sql") {
                    $("#db").show();
                    $("#sql").show();
                } else if (selectedRow.F_ScheduleType == "stored") {
                    $("#db").show();
                    $("#stored").show();
                } else if (selectedRow.F_ScheduleType == "Interface") {
                    $("#intertype").show();
                    $("#inter").show();
                    $("input[name='inter'][value=" + selectedRow.F_InterfaceType + "]").attr("checked", true);
                } else {
                    $("#dll").show();
                }
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#step-1').lrValidform()) {
            return false;
        }
        var base = $('#step-1').lrGetFormData();
        var excu = $("#step-2").lrGetFormData();
        var postData = {
            F_TaskName: base.F_TaskName,
            F_TaskCode: base.F_TaskCode,
            F_SchedulingPlan: JSON.stringify(BaseData),
            F_DataSourceId: excu.F_DataSourceId,
            F_SQL: excu.F_SQL,
            F_StoredProcedure: excu.F_StoredProcedure,
            F_DllName: excu.F_DllName,
            F_InterfacePath: excu.F_InterfacePath,
            F_Description: excu.F_Description
        };
        postData["F_ScheduleType"] = $("input[name=radio][type=radio]:checked").val();
        if (postData.F_ScheduleType == "Interface") {
            postData["F_InterfaceType"] = $("input[name=inter][type=radio]:checked").val();
        }
        $.lrSaveForm(top.$.rootUrl + '/LR_TaskScheduling/TS_TaskDesign/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            learun.frameTab.currentIframe().refreshGirdData();//index页列表刷新

        });
    };

    page.init();
}
///数字转成汉字的方法num为输入的数字
function ToChinesNum(section) {
    var chnNumChar = ["零", "一", "二", "三", "四", "五", "六", "七", "八", "九"];
    var chnUnitSection = ["", "万", "亿", "万亿", "亿亿"];
    var chnUnitChar = ["", "十", "百", "千"];
    var strIns = '', chnStr = '';
    var unitPos = 0;
    var zero = true;
    while (section > 0) {
        var v = section % 10;
        if (v === 0) {
            if (!zero) {
                zero = true;
                chnStr = chnNumChar[v] + chnStr;
            }
        } else {
            zero = false;
            strIns = chnNumChar[v];
            strIns += chnUnitChar[unitPos];
            chnStr = strIns + chnStr;
        }
        unitPos++;
        section = Math.floor(section / 10);
    }
    return chnStr;
}