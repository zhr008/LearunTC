/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-09-20 10:10
 * 描  述：看板信息
 */
var isFirst = true;

var selectedModel;  // 模块选中数据
var selectColData;  // 统计项选中数据

var keyValue = request('keyValue');
var configData = [];//配置信息数组
var containerHeight = 0;

var displayBoard;

var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('.container-left .content').lrscroll();
            // 加载导向
            $('#wizard').wizard().on('change', function (e, data) {
                var $finish = $("#btn_finish");
                var $next = $("#btn_next");
                if (data.direction === "next") {
                    if (!$('#step-1').lrValidform()) {
                        return false;
                    }
                    if (isFirst) {
                        isFirst = false;
                        if (keyValue) {
                            setTimeout(function () {
                                var $container = $(".container-right .content");
                                $container.find('.box').each(function () {
                                    var $this = $(this);
                                    var item = $this[0].lrdata;
                                    item.eChart && item.eChart.resize();
                                });
                            }, 100);
                        }
                        else {
                            setTimeout(function () {
                                $(".model").show();
                            }, 100);
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
            // 加载预设模板
            $('#typelayer .lr-cover-box').on('click', function () {
                $(".model").hide();
                var value = $(this).attr('data-value');
                if (value != 'six') {
                    configData = lrDisplayBoardTamplates[value];
                    displayBoard.init();
                }
            });

            ///模块风格
            $("#lr_statistics").on('click', function () {
                selectedModel = null;
                learun.layerForm({
                    id: 'statisticsForm',
                    title: '新增统计指标',
                    url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/StatisticsForm',
                    width: 500,
                    height: 330,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            displayBoard.add(data);
                        });
                    }
                });
            });
            $("#lr_table").on("click", function () {
                selectedModel = null;
                learun.layerForm({
                    id: 'tableForm',
                    title: '新增表格',
                    url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/TableForm',
                    width: 650,
                    height: 600,
                    btn: null
                });
            });
            $("#lr_pieChart").on("click", function () {
                selectedModel = null;
                learun.layerForm({
                    id: 'ChartForm',
                    title: '新增饼图',
                    url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/ChartForm?type=pieChart',
                    width: 650,
                    height: 600,
                    btn: null
                });
            });
            $("#lr_barChart").on("click", function () {
                selectedModel = null;
                learun.layerForm({
                    id: 'ChartForm',
                    title: '新增柱状图',
                    url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/ChartForm?showtype=barChart',
                    width: 650,
                    height: 600,
                    btn: null
                });

            });
            $("#lr_lineChart").on("click", function () {
                selectedModel = null;
                learun.layerForm({
                    id: 'ChartForm',
                    title: '新增折线图',
                    url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/ChartForm?type=lineChart',
                    width: 650,
                    height: 600,
                    btn: null
                });
            });
            $("#lr_gaugeChart").on("click", function () {
                selectedModel = null;
                learun.layerForm({
                    id: 'ChartForm',
                    title: '新增仪表盘',
                    url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/ChartForm?type=gaugeChart',
                    width: 650,
                    height: 600,
                    btn: null
                });

            });

            //模块编辑
            $(".container-right").on("click", ".edit", function () {
                selectedModel = $(this).parents('.box')[0].lrdata;
                switch (selectedModel.F_Type) {
                    case 'statistics':
                        learun.layerForm({
                            id: 'statisticsForm',
                            title: '编辑统计指标',
                            url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/StatisticsForm',
                            width: 500,
                            height: 330,
                            callBack: function (id) {
                                return top[id].acceptClick(function (data) {
                                    displayBoard.update(selectedModel);
                                });
                            }
                        });
                        break;
                    case 'table':
                        learun.layerForm({
                            id: 'tableForm',
                            title: '编辑表格',
                            url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/TableForm',
                            width: 650,
                            height: 600,
                            btn: null
                        });
                        break;
                    case 'pieChart':
                    case 'lineChart':
                    case 'barChart':
                    case 'gaugeChart':
                        learun.layerForm({
                            id: 'ChartForm',
                            title: '编辑图表',
                            url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/ChartForm?type=' + selectedModel.F_Type,
                            width: 650,
                            height: 600,
                            btn: null
                        });
                        break;
                }
            });
            //模块删除
            $(".container-right").on("click", ".del", function () {
                var $box = $(this).parents('.box');
                learun.layerConfirm('是否确认删除该项！', function (res, index) {
                    if (res) {
                        $box.remove();
                        top.layer.close(index); //再执行关闭 
                    }
                });
            });


            // 新增统计指标数据
            $(".container-right").on("click", ".add", function () {
                var component = $(this).parents('.box')[0].lrdata;
                selectColData = null;
                if (component._configuration.num < 5) {
                    learun.layerForm({
                        id: 'ColStatisForm',
                        title: '添加统计项配置',
                        url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/ColStatisForm',
                        width: 700,
                        height: 665,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                component._configuration.num++;
                                component._configuration.colData[data.id] = data;
                                $.lrDisplayBoardComponents.statistics.load(data, component);
                            });
                        }
                    });
                }
                else {
                    learun.alert.warning('最多添加五项');
                }
            });
            $(".container-right").on("click", ".edited", function () {
                selectColData = $(this).parents('.informationContentItem')[0].lrdata;
                learun.layerForm({
                    id: 'ColStatisForm',
                    title: '修改配置',
                    url: top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/ColStatisForm',
                    height: 665,
                    width: 700,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            $.lrDisplayBoardComponents.statistics.load(selectColData);
                        });
                    }
                });
            });
            $(".container-right").on("click", ".deleted", function (event) {
                var $item = $(this).parents('.informationContentItem');
                var item = $item[0].lrdata;
                var component = $item.parents('.box')[0].lrdata;
                var _id = item.id;
                learun.layerConfirm('是否确认删除该项！', function (res, index) {
                    if (res) {
                        component._configuration.num--;
                        delete component._configuration.colData[item.id];
                        $item.remove();
                        top.layer.close(index); //再执行关闭 
                    }
                });
            });

            ///完成方法
            $("#btn_finish").on('click', function () {
                if (!$('#step-1').lrValidform()) {
                    return false;
                }
                var kanbaninfo = $('#step-1').lrGetFormData();

                var _configData = [];
                var $container = $(".container-right .content");
                $container.find('.box').each(function () {
                    var $this = $(this);
                    var item = $this[0].lrdata;
                    var _item = {};
                    _item.F_ModeName = item.F_ModeName,
                    _item.F_TopValue = item.F_TopValue,
                    _item.F_LeftValue = item.F_LeftValue,
                    _item.F_WidthValue = item.F_WidthValue,
                    _item.F_HightValue = item.F_HightValue,
                    _item.F_RefreshTime = item.F_RefreshTime,
                    _item.F_Type = item.F_Type,
                    _item.F_Configuration = JSON.stringify(item._configuration);
                    _configData.push(_item);
                });

                var postData = {
                    kanbaninfo: JSON.stringify(kanbaninfo),
                    kbconfigInfo: JSON.stringify(_configData)
                };
                $.lrSaveForm(top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/SaveForm?keyValue=' + keyValue, postData, function (res) {
                    // 保存成功后才回调
                    learun.frameTab.currentIframe().refreshGirdData();//index页列表刷新
                });
            });
        },
        initData: function () {
            //编辑看板信息
            if (keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_DisplayBoard/LR_KBKanBanInfo/GetFormData?keyValue=' + keyValue, function (data) {
                    $('#step-1').lrSetFormData(data.baseinfo);
                    configData = data.configinfo;
                    displayBoard.init();
                    $.lrDisplayBoardComponents.load($(".container-right .content"));
                });
            }
        }
    };

    displayBoard = {
        init: function () {
            var $container = $(".container-right .content");
            $container.hide();
            $container.html("");
            $.each(configData || [], function (_Index, _Item) {
                if (_Item && _Item.F_Type) {
                    var _fn = $.lrDisplayBoardComponents[_Item.F_Type];
                    if (_fn) {
                        $container.append(_fn.init(_Item, true));
                        var _height = _Item._height + _Item._top;
                        if (containerHeight < _height) {
                            containerHeight = _height;
                        }
                    }
                }
            });
            if (containerHeight > 0) {
                $container.height(containerHeight);
            }
            $container.show();
            $container = null;
        },
        add: function (data) {
            var $container = $(".container-right .content");
            $container.hide();
            var _fn = $.lrDisplayBoardComponents[data.F_Type];
            if (_fn) {
                $container.append(_fn.init(data, true));
                var _height = data._height + data._top;
                if (containerHeight < _height) {
                    containerHeight = _height;
                }
            }
            $container.height(containerHeight);
            $container.show();
            $container = null;
        },
        update: function (data) {
            var _fn = $.lrDisplayBoardComponents[data.F_Type];
            if (_fn) {
                _fn.init(data, true);
                var _height = data._height + data._top;
                if (containerHeight < _height) {
                    containerHeight = _height;
                }
            }
            var $container = $(".container-right .content");
            $container.height(containerHeight);
            $container = null;
        },

        addModel: function (data) {
            displayBoard.add(data);
            $.lrDisplayBoardComponents[data.F_Type].load(data);
        },
        updateModel: function (data) {
            displayBoard.update(data);
            $.lrDisplayBoardComponents[data.F_Type].load(data);
        }
    }
    page.init();
};