/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-09-29 19:18
 * 描  述：采购订单报表
 */
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var startTime;
    var endTime;
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            page.initCharts();
        },
        bind: function () {
            //时间
            $('#datesearch').lrdate({
                dfdata: [
                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                    //startTime = begin;
                    //endTime = end;
                    //page.search();
                }
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var F_PurchaseNo = $('#txt_PurchaseNo').val();
                var F_Theme = $('#txt_Theme').val();
                var F_Appler = $('#txt_Appler').val();
                var F_Department = $('#txt_Department').val();
                page.search({ F_PurchaseNo: F_PurchaseNo, F_Theme: F_Theme, F_Appler: F_Appler, F_Department: F_Department });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 打印
            $('#lr_print').on('click', function () {
                $('#gridtable').jqprintTable();
            });
        },
        // 初始化列表
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/ERPDemo/SalesReceipt/GetPageList',
                headData: [
                    { label: "单据编号", name: "F_PurchaseNo", width: 100, align: "left" },
                    { label: "报价日期", name: "F_ApplyDate", width: 150, align: "left" },
                    { label: "主题", name: "F_Theme", width: 200, align: "left" },
                    {
                        label: "报价类别", name: "F_PurchaseType", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'menuTrage',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: "报价人", name: "F_Appler", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('custmerData', {
                                url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'userdata',
                                key: value,
                                keyId: 'f_userid',
                                callback: function (_data) {
                                    callback(_data['f_realname']);
                                }
                            });
                        }
                    },
                    {
                        label: "报价单位", name: "F_Department", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('custmerData', {
                                url: '/LR_SystemModule/DataSource/GetDataTable?code=' + 'company',
                                key: value,
                                keyId: 'f_companyid',
                                callback: function (_data) {
                                    callback(_data['f_shortname']);
                                }
                            });
                        }
                    },
                    {
                        label: "支付方式", name: "F_PaymentType", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'Client_PaymentMode',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "总价", name: "F_Total", width: 100, align: "left", statistics: true },
                    { label: "出库日期", name: "F_DeliveryDate", width: 150, align: "left" },
                    { label: "点收人", name: "F_PurchaseWarehousinger", width: 100, align: "left" },
                    { label: "点收日期", name: "F_PurchaseWarehousingDate", width: 150, align: "left" },
                    { label: "备注", name: "F_Description", width: 200, align: "left" },
                ],
                mainId:'F_Id',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.StartTime = startTime;
            param.EndTime = endTime;
            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });
        },
        initCharts: function (param) {
            param = "LR_ERP_SalesReceipt";
            learun.httpAsync('GET', top.$.rootUrl + '/LR_ReportModule/ReportShow/GetTableDate', { param: param }, function (data) {
                lineChart(data, "F_Name", "F_Count");
                barsChart(data, "F_Name", "F_Count");
                pieChart(data, "F_Name", "F_Count");
            });
        }
    };
    refreshGirdData = function () {
        $('#gridtable').jfGridSet('reload');
    };

    function lineChart(data, _x, _y) {
        var myChart = echarts.init(document.getElementById('line'));
        var _data = eCharts.dataFormate(data, _x, _y);
        eCharts.optionTemplates.loadLines(myChart, _data);
    }

    function barsChart(data, _x, _y) {
        var myChart = echarts.init(document.getElementById('bars'));
        var _data = eCharts.dataFormate(data, _x, _y);
        eCharts.optionTemplates.loadBars(myChart, _data);
    }

    function pieChart(data, _x, _y) {
        var myChart = echarts.init(document.getElementById('pie'));
        var _data = eCharts.dataFormate(data, _x, _y);
        eCharts.optionTemplates.loadPie(myChart, _data);
    }

    var eCharts = {
        dataFormate: function (data, text, value) {
            var categories = [];
            var datas = [];
            for (var i = 0, l = data.length; i < l; i++) {
                categories.push(data[i][text] || "");
                datas.push({ name: data[i][text], value: data[i][value] || 0 });
            }
            return { category: categories, data: datas };
        },
        optionTemplates: {
            loadLines: function (chart, data) {
                var option = {
                    color: ['#33A1FF'],//在这里设置colorList，是一个数组，图片颜色会按顺序选取
                    tooltip: {
                        trigger: 'axis'
                    },
                    grid: {
                        show: false,
                        left: '60px',
                        right: '30px',
                        top: '60px',
                        bottom: '40px',
                        borderColor: '#C0C4CC'
                    },
                    xAxis: {
                        type: 'category',
                        boundaryGap: true,
                        axisLine: {
                            lineStyle: {
                                color: '#909399'
                            }
                        },
                        splitLine: {
                            show: true,
                            lineStyle: {
                                color: '#F2F6FC'
                            }
                        },
                        axisTick: {
                            alignWithLabel: true
                        },
                        data: data.category,
                    },
                    yAxis: {
                        type: 'value',
                        axisLine: {
                            lineStyle: {
                                color: '#909399'
                            }
                        },
                        splitLine: {
                            show: true,
                            lineStyle: {
                                color: '#F2F6FC'
                            }
                        }
                    },
                    series: [{
                        data: data.data,
                        type: 'line'
                    }]
                };
                chart.setOption(option);
            },
            loadBars: function (chart, data) {
                var option = {
                    color: ['#33A1FF'],
                    tooltip: {
                        trigger: 'axis'
                    },
                    grid: {
                        show: false,
                        left: '60px',
                        right: '30px',
                        top: '60px',
                        bottom: '40px',
                        borderColor: '#C0C4CC'
                    },
                    xAxis: {
                        type: 'category',
                        boundaryGap: true,
                        axisLine: {
                            lineStyle: {
                                color: '#909399'
                            }
                        },
                        splitLine: {
                            show: true,
                            lineStyle: {
                                color: '#F2F6FC'
                            }
                        },
                        axisTick: {
                            alignWithLabel: true
                        },
                        data: data.category,
                    },
                    yAxis: {
                        type: 'value',
                        axisLine: {
                            lineStyle: {
                                color: '#909399'
                            }
                        },
                        splitLine: {
                            show: true,
                            lineStyle: {
                                color: '#F2F6FC'
                            }
                        }
                    },
                    series: [{
                        data: data.data,
                        type: 'bar',
                        barWidth: '60%',
                    }]
                };
                chart.setOption(option);
            },
            loadPie: function (chart, data) {
                var option = {
                    tooltip: {
                        trigger: 'item',
                        formatter: "{b} : {c} ({d}%)"
                    },
                    legend: {
                        bottom: '3%',
                        data: data.category
                    },
                    series: [
                        {
                            name: "占比",
                            type: 'pie',
                            radius: '60%',
                            center: ['50%', '50%'],
                            data: data.data,
                            itemStyle: {
                                emphasis: {
                                    shadowBlur: 10,
                                    shadowOffsetX: 0,
                                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                                }
                            }

                        }
                    ]
                };
                option.color = ['#9558E1', '#48D4D7', '#33A1FF', '#df4d4b', '#304552', '#52bbc8', 'rgb(224,134,105)', '#8dd5b4', '#5eb57d', '#d78d2f'];//在这里设置colorList，是一个数组，图片颜色会按顺序选取
                chart.setOption(option);
            }
        }
    };

    page.init();
}
