var bootstrap = function ($, learun) {
    var name = "";
    var $data;

    "use strict";    
    var page = {
        init: function () {

            $("#Xselect").append("<option value=\"F_Name\" selected>商品名称</option>");
            $("#Yselect").append("<option value=\"F_Count\" selected>商品数量</option>");

            initCharts();
            page.bind();
        },  
        bind: function () {
            $('#lr_left_tree').lrtree({
                url: top.$.rootUrl + '/LR_FormModule/Custmerform/GetModuleTree',
                nodeClick: function (item) {
                    
                    switch (item.id) {
                        case "90e359cb-c448-4bcb-8350-853f0d6146c1":
                            name = "LR_ERP_PaymentInfo";
                            break;
                        case "523457ce-2391-47a1-9f88-5833a7706816":
                            name = "LR_ERP_ReceiptInfo";
                            break;
                        case "214ca1b5-bb6a-47d8-bf1f-c4f91650cb49":
                            name = "LR_ERP_SalesReceipt";
                            break;
                        case "87eed284-d5a0-4979-abfb-488f4892adc6":
                            name = "LR_ERP_SalesOrder";
                            break;
                        case "1383093c-7d22-437e-bf28-af7dc74e2d60":
                            name = "LR_ERP_SalesOffer";
                            break;
                        case "cbb2cda6-4a90-486f-b2a9-3317ea3f6228":
                            name = "LR_ERP_PurchaseWarehousing";
                            break;
                        case "f085868a-0fd9-417d-a5d8-d261e6edcf6a":
                            name = "LR_ERP_PurchaseRequisition";
                            break;
                        case "4aafbc6b-8f16-4b68-a0e6-3e11ec7a42a7":
                            name = "LR_ERP_PurchaseOrder";
                            break;
                    }                    
                    page.search(name);
                    page.divBind();
                    page.selBind();
                }
            });
           
        },
        search: function (param) {
            param = param || {};
            param.name = name;

            learun.httpAsync('GET', top.$.rootUrl + '/LR_ReportModule/ReportShow/GetTableDate', { param: param }, function (data) {
                
                lineChart(data, "F_Name", "F_Count");
                barsChart(data, "F_Name", "F_Count");
                pieChart(data, "F_Name", "F_Count");
                gaugeChart(data, "F_Name", "F_Count");

                $data = data;
            });
        },
        divBind: function () {
            $("#Xselect").html("");
            $("#Yselect").html("");
            learun.httpAsync('GET', top.$.rootUrl + '/LR_ReportModule/ReportShow/GetTableField', {}, function (data) {
                $("#Xselect").append("<option value=\"0\" selected>请选择</option>");
                $("#Yselect").append("<option value=\"0\" selected>请选择</option>");
                for (var i = 0; i < data.length; i++) {
                    $("#Xselect").append("<option value=\"selX_" + i + "\" >" + data[i].column_name + "</option>");
                    $("#Yselect").append("<option value=\"selY_" + i + "\" >" + data[i].column_name + "</option>");
                }
            });
               
        },
        selBind: function () {
            $("#Xselect").on("click", function () {
                var value = $(this).find("option:selected").val();  //x轴 

                var x_text = $("#Xselect").find("option:selected").text();  //x轴
                var y_text = $("#Yselect").find("option:selected").text(); //y轴                

                if (y_text == "请选择" || y_text == "" || y_text == undefined) {
                    y_text = "F_Price";
                }

                if (x_text == "请选择" || x_text == "" || x_text == undefined) {
                    x_text = "F_Name";
                }

                if (value != "0" && value != "selX_0") {
                    lineChart($data, x_text, y_text);
                    barsChart($data, x_text, y_text);
                    pieChart($data, x_text, y_text);
                }
            });
            $("#Yselect").on("click", function () {
                var value = $(this).find("option:selected").val();  //x轴 

                var x_text = $("#Xselect").find("option:selected").text();  //x轴
                var y_text = $("#Yselect").find("option:selected").text(); //y轴
                
                if (y_text == "请选择" || y_text == "" || y_text == undefined) {
                    y_text = "F_Price";
                }

                if (x_text == "请选择" || x_text == "" || x_text == undefined) {
                    x_text = "F_Name";
                }

                if (value != "0" && value != "selY_0") {
                    lineChart($data, x_text, y_text);
                    barsChart($data, x_text, y_text);
                    pieChart($data, x_text, y_text);
                    gaugeChart($data, x_text, y_text);
                }

            });
        }
    };


    function initCharts() {
        learun.httpAsync('GET', top.$.rootUrl + '/LR_ReportModule/ReportShow/GetTableDate', {}, function (data) {
            lineChart(data, "F_Name", "F_Count");
            barsChart(data, "F_Name", "F_Count");
            pieChart(data, "F_Name", "F_Count");
            gaugeChart(data, "F_Name", "F_Count");
            $data = data;
        });
    }
    
    function lineChart(data,_x,_y) {
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
    function gaugeChart(data, _x, _y) {
        var myChart = echarts.init(document.getElementById('gauge'));
        var _data = eCharts.dataFormate(data, _x, _y);
        eCharts.optionTemplates.loadGauge(myChart, _data);
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
            },
            loadGauge: function (chart, data) {
                var option = {
                    tooltip: {
                        formatter: "{a} <br/>{b} : {c}%"
                    },
                    series: [
                        {
                            type: 'gauge',
                            detail: { formatter: '{value}%' },
                            data: data.data
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

