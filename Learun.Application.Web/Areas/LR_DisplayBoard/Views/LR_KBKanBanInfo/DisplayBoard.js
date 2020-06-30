/*看板组件*/
    (function ($, learun) {
        "use strict";
        // 方法：将分数转换为百分比
        function percent (score) {
            score == null ? 0 : parseInt(score);
            return eval(score) * 100 + '%';
        }
        // 计算模块位置和大小
        function setOffset(component) {
            component._top = component.F_TopValue ? parseInt(component.F_TopValue) : 0;
            component._height = component.F_HightValue ? parseInt(component.F_HightValue) : 0;
            component._width = component.F_WidthValue ? percent(component.F_WidthValue) : '100%';
            component._left = component.F_LeftValue ? percent(component.F_LeftValue) : 0;
        }

        function openWindowForm(url) {
            if (url) {
                if (url.indexOf("http") != -1 || url.indexOf("www") != -1) {
                    if (url.indexOf("http") == -1) {
                        url = "http://" + url;
                    }
                }
                else {
                    url = top.$.rootUrl + url
                }
                learun.layerForm({
                    id: 'more',
                    title: '更多内容',
                    url: url,
                    width: 1000,
                    height: 900,
                    btn: null
                });
            }
        }

        var _displayBoard = {
            getContainer: function (component, isEdit) {
                setOffset(component);
                var $component = null;
                if (!component.id) {
                    component.id = learun.newGuid();

                    var $component = $('<div id="' + component.id + '" style="width:' + component._width + ';left:' + component._left + ';top: ' + component._top + 'px;height:' + component._height + 'px;" class="box"></div>');
                    var templateStr = "";
                    templateStr += '<div class="box-module ' + (isEdit ? 'box-module-edit' : '') + '">';
                    templateStr += '<div class="edit-del">';
                    templateStr += '<a href="#" class="edit">编辑</a>｜<a href="#" class="del">删除</a>';
                    templateStr += '</div>';

                    templateStr += '<h3 class="title title-marBottom">' + component.F_ModeName + '<i class="arrow"></i></h3>';

                    switch (component.F_Type) {
                        case 'statistics':
                            templateStr += '<div class="information"><div class="informationContent" >';
                            templateStr += '<div class="edit-add">';
                            templateStr += '<a href="#" class="add">添加统计项</a></a>';
                            templateStr += '</div>';
                            templateStr += '<img class="informationImg" src="' + top.$.rootUrl + '/Content/images/kanban/bg.png' + '">';
                            templateStr += '<div class="informationContentList"></div>';
                            templateStr += '</div></div>';
                            break;
                        case 'table':
                        case 'pieChart':
                        case 'lineChart':
                        case 'barChart':
                        case 'gaugeChart':
                            if (component._configuration.F_LinkInfo) {
                                templateStr += '<div class="box-module-more">更多</div>';
                            }
                            templateStr += '<div class="box-module-container"><div class="box-module-content" >';
                            templateStr += '<div class="box-module-content-text" >鼠标移入模块点击编辑数据</div>';
                            templateStr += '</div></div>';
                            if (!isEdit) {
                                $component.on('click', '.box-module-more', function () {
                                    var $box = $(this).parents('.box');
                                    var url = $box[0].lrdata._configuration.F_LinkInfo;
                                    openWindowForm(url);
                                });
                            }
                          
                            break;
                    }
                    templateStr += '</div>';
                    $component.append(templateStr);

                    $component[0].lrdata = component;
                }
                else {
                    $component = $('#' + component.id);
                    $component.css({
                        top: component._top,
                        left: component._left,
                        width: component._width,
                        height: component._height
                    });
                    $component.find('.title-marBottom').html(component.F_ModeName + '<i class="arrow"></i>');
                }
                return $component;
            }
        };

        function refreshDataByTime(id, time) {
            var $component = $('#' + id);
            if ($component.length > 0) {
                var configInfoList = [];
                var component = $component[0].lrdata;
                switch (component.F_Type) {
                    case 'statistics':
                        $.each(component._configuration.colData, function (_index, _item) {
                            configInfoList.push({
                                id: _item.id,
                                type: _item.F_TInterface == '' ? '1' : '2',
                                dataType: 'main',
                                modelType: 'statistics',
                                dbId: _item.F_TBaseData,
                                sql: _item.F_TSQL,
                                url: _item.F_TInterface
                            });
                            configInfoList.push({
                                id: _item.id,
                                type: _item.F_TInterface == '' ? '1' : '2',
                                dataType: 'sub',
                                modelType: 'statistics',
                                dbId: _item.F_TBaseData,
                                sql: _item.F_SubSQL,
                                url: _item.F_TInterface
                            });
                        });
                        break;
                    case 'table':
                    case 'pieChart':
                    case 'lineChart':
                    case 'barChart':
                    case 'gaugeChart':
                        if (component._configuration.F_DataSourceId || component._configuration.F_Interface) {
                            configInfoList.push({
                                id: component.id,
                                type: component._configuration.F_Interface == '' ? '1' : '2',
                                modelType: component.F_Type,
                                dbId: component._configuration.F_DataSourceId,
                                sql: component._configuration.F_Sql,
                                url: component._configuration.F_Interface
                            });
                        }
                        break;
                }

                learun.httpAsync('Post', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetConfigData', { configInfoList: JSON.stringify(configInfoList) }, function (data) {
                    if (data) {
                        $.each(data, function (_index, _item) {
                            if (_item.modelType == 'statistics') {
                                var point = {};
                                point.id = _item.id;
                                var colData = $('#' + _item.id)[0].lrdata;
                                if (_item.type == '1') {
                                    if (_item.dataType == 'main') {
                                        point.titleValue = _item.data[0][colData.F_TitleValue] || '';
                                    }
                                    else {
                                        point.subtTitleValue = _item.data[0][colData.F_SubtitleValue] || '';
                                    }
                                }
                                else {
                                    if (_item.dataType == 'main') {
                                        point.titleValue = _item.data[colData.F_TitleValue] || '';
                                    }
                                    else {
                                        point.subtTitleValue = _item.data[colData.F_SubtitleValue] || '';
                                    }
                                }
                                $.lrDisplayBoardComponents.statistics.renderData(point);
                            }
                            else {
                                var $component = $('#' + _item.id);
                                if ($component.length > 0) {
                                    var _component = $component[0].lrdata;
                                    $.lrDisplayBoardComponents[_component.F_Type].renderData(_item.data, _component);
                                    $component = null;
                                }
                            }
                        });
                        $('.top-title-time').text(learun.formatDate(new Date(), 'yyyy-MM-dd hh:mm:ss'));
                    }
                    setTimeout(function () {
                        refreshDataByTime(id, time);
                    }, time);
                });
               
            }
        }

        $.lrDisplayBoardComponents = {
            load: function ($container, isRefresh, time) {
                var configInfoList = [];
                $container.find('.box').each(function () {
                    var $this = $(this);
                    var component = $this[0].lrdata;
                   
                    if (isRefresh) {
                        component._refreshTime = component.F_RefreshTime || time;
                        if (component._refreshTime) {
                            component._refreshTime = parseInt(component._refreshTime) * 60 * 1000;
                        }
                    }

                    switch (component.F_Type) {
                        case 'statistics':
                            $.each(component._configuration.colData, function (_index, _item) {
                                configInfoList.push({
                                    id: _item.id,
                                    type: _item.F_TInterface == '' ? '1' : '2',
                                    dataType: 'main',
                                    modelType: 'statistics',
                                    dbId: _item.F_TBaseData,
                                    sql: _item.F_TSQL,
                                    url: _item.F_TInterface
                                });
                                configInfoList.push({
                                    id: _item.id,
                                    type: _item.F_TInterface == '' ? '1' : '2',
                                    dataType: 'sub',
                                    modelType: 'statistics',
                                    dbId: _item.F_TBaseData,
                                    sql: _item.F_SubSQL,
                                    url: _item.F_TInterface
                                });
                                var _data = {
                                    id: _item.id,
                                    title: _item.F_Title,
                                    subTitle: _item.F_Subtitle
                                };
                                $.lrDisplayBoardComponents.statistics.renderData(_data, component);
                            });
                            break;
                        case 'table':
                        case 'pieChart':
                        case 'lineChart':
                        case 'barChart':
                        case 'gaugeChart':
                            if (component._configuration.F_DataSourceId || component._configuration.F_Interface) {
                                configInfoList.push({
                                    id: component.id,
                                    type: component._configuration.F_Interface == '' ? '1' : '2',
                                    modelType: component.F_Type,
                                    dbId: component._configuration.F_DataSourceId,
                                    sql: component._configuration.F_Sql,
                                    url: component._configuration.F_Interface
                                });
                            }
                            break;
                    }
                  
                });
                learun.httpAsync('Post', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetConfigData', { configInfoList: JSON.stringify(configInfoList) }, function (data) {
                    if (data) {
                        $.each(data, function (_index, _item) {
                            if (_item.modelType == 'statistics') {
                                var point = {};
                                point.id = _item.id;
                                var colData = $('#' + _item.id)[0].lrdata;
                                if (_item.type == '1') {
                                    if (_item.dataType == 'main') {
                                        point.titleValue = _item.data[0][colData.F_TitleValue] || '';
                                    }
                                    else {
                                        point.subtTitleValue = _item.data[0][colData.F_SubtitleValue] || '';
                                    }
                                }
                                else {
                                    if (_item.dataType == 'main') {
                                        point.titleValue = _item.data[colData.F_TitleValue] || '';
                                       
                                    }
                                    else {
                                        point.subtTitleValue = _item.data[colData.F_SubtitleValue] || '';
                                    }
                                    
                                }
                                if (_item.dataType == 'main') {
                                    var _component2 = $('#' + _item.id).parents('.box')[0].lrdata;
                                    if (!_component2._isRefreshTime) {
                                        _component2._isRefreshTime = true;
                                        if (_component2._refreshTime > 0) {

                                            setTimeout(function () {
                                                refreshDataByTime(_item.id, _component2._refreshTime);
                                            }, _component2._refreshTime);

                                        }
                                    }
                                }

                                $.lrDisplayBoardComponents.statistics.renderData(point);
                            }
                            else {
                                var $component = $('#' + _item.id);
                                if ($component.length > 0) {
                                    var _component = $component[0].lrdata;
                                    $.lrDisplayBoardComponents[_component.F_Type].renderData(_item.data, _component);
                                    $component = null;
                                    if (_component._refreshTime > 0) {
                                        setTimeout(function () {
                                            refreshDataByTime(_item.id, _component._refreshTime);
                                        }, _component._refreshTime);
                                    }
                                    
                                }
                            }
                        });
                    }
                    $('.top-title-time').text(learun.formatDate(new Date(), 'yyyy-MM-dd hh:mm:ss'));
                });
            },
            statistics: {
                init: function (component, isEdit) {
                    var $component = _displayBoard.getContainer(component, isEdit);
                    if (component.F_Configuration && !component._configuration) {
                        component._configuration = JSON.parse(component.F_Configuration);
                    }
                    else {
                        component._configuration = component._configuration || {
                            num: 0,
                            colData: {}
                        };
                    }
                    return $component;
                },
                renderData: function (data, component) {
                    var $item = $('#' + data.id);
                    if ($item.length == 0) {
                        var $list = $('#' + component.id + ' .informationContentList');
                        $item = $('<div class="informationContentItem" id="' + data.id + '" ></div>');
                        $item.append('<div class="edited-del"><a href="#"  class="edited">编辑</a>｜<a href="#" class="deleted">删除</a></div>');
                        $item.append('<div class="title2" >' + data.title + '</div>');
                        $item.append('<div class="value" >' + (data.titleValue || '') + '</div>');
                        $item.append('<div class="sub-title" ><span class="sub-title-text" >' + data.subTitle + '</span><span class="sub-value" >' + (data.subtTitleValue || '') + '</span></div>');
                        $item[0].lrdata = component._configuration.colData[data.id];
                        $list.append($item);
                        $item.on('click', function () {
                            var $this = $(this);
                            if ($this.parents('.box-module-edit').length == 0) {
                                var url = $this[0].lrdata.F_Link;
                                openWindowForm(url);
                            }
                        });
                    }
                    else {
                        if (data.title != null && data.title != undefined) {
                            $item.find('.title2').text(data.title);
                        }
                        if (data.titleValue != null && data.titleValue != undefined) {
                            $item.find('.value').text(data.titleValue);
                        }
                        if (data.subTitle != null && data.subTitle != undefined) {
                            $item.find('.sub-title-text').text(data.subTitle);
                        }
                        if (data.subtTitleValue != null && data.subtTitleValue != undefined) {
                            $item.find('.sub-value').text(data.subtTitleValue);
                        }
                    }
                },
                load: function (data, component) {
                    var configInfoList = [];
                    configInfoList.push({
                        id: data.id,
                        type: data.F_TInterface == '' ? '1' : '2',
                        dataType: 'main',
                        modelType: 'statistics',
                        dbId: data.F_TBaseData,
                        sql: data.F_TSQL,
                        url: data.F_TInterface
                    });
                    configInfoList.push({
                        id: data.id,
                        type: data.F_TInterface == '' ? '1' : '2',
                        dataType:'sub',
                        modelType: 'statistics',
                        dbId: data.F_TBaseData,
                        sql: data.F_SubSQL,
                        url: data.F_TInterface
                    });
                    var _data = {
                        id:data.id,
                        title:data.F_Title,
                        subTitle:data.F_Subtitle
                    };
                    $.lrDisplayBoardComponents.statistics.renderData(_data, component);
                    learun.httpAsync('GET', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetConfigData', { configInfoList: JSON.stringify(configInfoList) }, function (data) {
                        if (data) {
                            $.each(data, function (_index, _item) {
                                var point = {};
                                point.id = _item.id;
                                var colData = $('#' + _item.id)[0].lrdata;

                                if (_item.type == '1') {
                                    if (_item.dataType == 'main') {
                                        point.titleValue = _item.data[0][colData.F_TitleValue] || '';
                                    }
                                    else {
                                        point.subtTitleValue = _item.data[0][colData.F_SubtitleValue] || '';
                                    }
                                }
                                else {
                                    if (_item.dataType == 'main') {
                                        point.titleValue = _item.data[colData.F_TitleValue] || '';
                                    }
                                    else {
                                        point.subtTitleValue = _item.data[colData.F_SubtitleValue] || '';
                                    }
                                }
                                $.lrDisplayBoardComponents.statistics.renderData(point);
                            });
                        }
                    });
                }
            },
            table: {
                init: function (component, isEdit) {
                    if (component.F_Configuration && !component._configuration) {
                        component._configuration = JSON.parse(component.F_Configuration);
                    }
                    else {
                        component._configuration = component._configuration || {};
                    }
                    var $component = _displayBoard.getContainer(component, isEdit);
                    
                  
                    if (component._configuration.grid && component._configuration.grid.length > 0) {
                        var $content = $component.find('.box-module-content');
                        $content.hide();
                        $content.html('');
                        // 表头
                        var $head = $('<div class="table-head" ></div>');
                        $.each(component._configuration.grid || [], function (_index, _item) {
                            var $item = $('<div class="table-head-item" title="' + _item.F_HeadTitle + '" >' + _item.F_HeadTitle + '</div>').css({ width: _item.F_Width + '%', 'text-align': (_item.F_Align || 'left') });
                            $head.append($item);
                        });
                        $content.append($head);
                        $content.append('<div class="table-body-container"><div class="table-body"></div></div>');
                        $content.css({ 'padding-top': '30px' });

                        $content.find('.table-body-container').Carousel();

                        $content.show();
                    }
                  
                    return $component;
                },
                renderData: function (data, component) {
                    var $list = $('#' + component.id + ' .table-body');
                    $list.html('');
                    $.each(data || [], function (_index, _item) {
                        var $row = $('<div class="table-body-row" ></div>');
                        $.each(component._configuration.grid || [], function (_jindex,_jitem) {
                            var $item = $('<div class="table-body-item" title="' + (_item[_jitem.F_FullName] || '') + '"  >' + (_item[_jitem.F_FullName] || '') + '</div>').css({ width: _jitem.F_Width + '%', 'text-align': (_jitem.F_Align || 'left') });
                            $row.append($item);
                        });
                        $list.append($row);
                    });
                },
                load: function (component) {
                    var configInfoList = [];
                    configInfoList.push({
                        id: component.id,
                        type: component._configuration.F_Interface == '' ? '1' : '2',
                        modelType: 'table',
                        dbId: component._configuration.F_DataSourceId,
                        sql: component._configuration.F_Sql,
                        url: component._configuration.F_Interface
                    });
                    learun.httpAsync('GET', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetConfigData', { configInfoList: JSON.stringify(configInfoList) }, function (data) {
                        if (data) {
                            $.each(data, function (_index, _item) {
                                var $component = $('#' + _item.id);
                                if ($component.length > 0) {
                                    var _component = $component[0].lrdata;
                                    $.lrDisplayBoardComponents.table.renderData(_item.data, _component);
                                    $component = null;
                                }
                            });
                        }
                    });
                }
            },
            pieChart: {
                init: function (component, isEdit) {
                    if (component.F_Configuration && !component._configuration) {
                        component._configuration = JSON.parse(component.F_Configuration);
                    }
                    else {
                        component._configuration = component._configuration || {};
                    }
                    var $component = _displayBoard.getContainer(component, isEdit);
                   
                    return $component;
                },
                renderData: function (data, component) {
                    if (!component.eChart) {
                        component.eChart = echarts.init($('#' + component.id + ' .box-module-content')[0]);
                        $(window).resize(function () {
                            component.eChart.resize();
                        });
                    }
                    var _data = eCharts.dataFormate(data, component._configuration.F_FullName, component._configuration.F_Value);
                    eCharts.optionTemplates.loadPie(component.eChart, _data);
                },
                load: function (component) {
                    var configInfoList = [];
                    configInfoList.push({
                        id: component.id,
                        type: component._configuration.F_Interface == '' ? '1' : '2',
                        modelType: 'pieChart',
                        dbId: component._configuration.F_DataSourceId,
                        sql: component._configuration.F_Sql,
                        url: component._configuration.F_Interface
                    });
                    learun.httpAsync('GET', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetConfigData', { configInfoList: JSON.stringify(configInfoList) }, function (data) {
                        if (data) {
                            $.each(data, function (_index, _item) {
                                var $component = $('#' + _item.id);
                                if ($component.length > 0) {
                                    var _component = $component[0].lrdata;
                                    $.lrDisplayBoardComponents.pieChart.renderData(_item.data, _component);
                                    $component = null;
                                }
                            });
                        }
                    });
                }
            },
            lineChart: {
                init: function (component, isEdit) {
                    if (component.F_Configuration && !component._configuration) {
                        component._configuration = JSON.parse(component.F_Configuration);
                    }
                    else {
                        component._configuration = component._configuration || {};
                    }
                    var $component = _displayBoard.getContainer(component, isEdit);
                    return $component;
                },
                renderData: function (data, component) {
                    if (!component.eChart) {
                        component.eChart = echarts.init($('#' + component.id + ' .box-module-content')[0]);
                        $(window).resize(function () {
                            component.eChart.resize();
                        });
                    }
                    var _data = eCharts.dataFormate(data, component._configuration.F_FullName, component._configuration.F_Value);
                    eCharts.optionTemplates.loadLines(component.eChart, _data);
                },
                load: function (component) {
                    var configInfoList = [];
                    configInfoList.push({
                        id: component.id,
                        type: component._configuration.F_Interface == '' ? '1' : '2',
                        modelType: 'lineChart',
                        dbId: component._configuration.F_DataSourceId,
                        sql: component._configuration.F_Sql,
                        url: component._configuration.F_Interface
                    });
                    learun.httpAsync('GET', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetConfigData', { configInfoList: JSON.stringify(configInfoList) }, function (data) {
                        if (data) {
                            $.each(data, function (_index, _item) {
                                var $component = $('#' + _item.id);
                                if ($component.length > 0) {
                                    var _component = $component[0].lrdata;
                                    $.lrDisplayBoardComponents.lineChart.renderData(_item.data, _component);
                                    $component = null;
                                }
                            });
                        }
                    });
                }
            },
            barChart: {
                init: function (component, isEdit) {
                    if (component.F_Configuration && !component._configuration) {
                        component._configuration = JSON.parse(component.F_Configuration);
                    }
                    else {
                        component._configuration = component._configuration || {};
                    }
                    var $component = _displayBoard.getContainer(component, isEdit);
                    return $component;
                },
                renderData: function (data, component) {
                    if (!component.eChart) {
                        component.eChart = echarts.init($('#' + component.id + ' .box-module-content')[0]);
                        $(window).resize(function () {
                            component.eChart.resize();
                        });
                    }
                    var _data = eCharts.dataFormate(data, component._configuration.F_FullName, component._configuration.F_Value);
                    eCharts.optionTemplates.loadBars(component.eChart, _data);
                },
                load: function (component) {
                    var configInfoList = [];
                    configInfoList.push({
                        id: component.id,
                        type: component._configuration.F_Interface == '' ? '1' : '2',
                        modelType: 'barChart',
                        dbId: component._configuration.F_DataSourceId,
                        sql: component._configuration.F_Sql,
                        url: component._configuration.F_Interface
                    });
                    learun.httpAsync('GET', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetConfigData', { configInfoList: JSON.stringify(configInfoList) }, function (data) {
                        if (data) {
                            $.each(data, function (_index, _item) {
                                var $component = $('#' + _item.id);
                                if ($component.length > 0) {
                                    var _component = $component[0].lrdata;
                                    $.lrDisplayBoardComponents.barChart.renderData(_item.data, _component);
                                    $component = null;
                                }
                            });
                        }
                    });
                }
            },
            gaugeChart: {
                init: function (component, isEdit) {
                    if (component.F_Configuration && !component._configuration) {
                        component._configuration = JSON.parse(component.F_Configuration);
                    }
                    else {
                        component._configuration = component._configuration || {};
                    }
                    var $component = _displayBoard.getContainer(component, isEdit);
                    return $component;
                },
                renderData: function (data, component) {
                    if (!component.eChart) {
                        component.eChart = echarts.init($('#' + component.id + ' .box-module-content')[0]);
                        $(window).resize(function () {
                            component.eChart.resize();
                        });
                    }
                    var _data = eCharts.dataFormate(data, component._configuration.F_FullName, component._configuration.F_Value);
                    eCharts.optionTemplates.loadGauge(component.eChart, _data);
                },
                load: function (component) {
                    var configInfoList = [];
                    configInfoList.push({
                        id: component.id,
                        type: component._configuration.F_Interface == '' ? '1' : '2',
                        modelType: 'gaugeChart',
                        dbId: component._configuration.F_DataSourceId,
                        sql: component._configuration.F_Sql,
                        url: component._configuration.F_Interface
                    });
                    learun.httpAsync('GET', top.$.rootUrl + '/LR_DisplayBoard/LR_KBConfigInfo/GetConfigData', { configInfoList: JSON.stringify(configInfoList) }, function (data) {
                        if (data) {
                            $.each(data, function (_index, _item) {
                                var $component = $('#' + _item.id);
                                if ($component.length > 0) {
                                    var _component = $component[0].lrdata;
                                    $.lrDisplayBoardComponents.gaugeChart.renderData(_item.data, _component);
                                    $component = null;
                                }
                            });
                        }
                    });
                }
            }
        };

        $.fn.Carousel = function (options) {
            //默认配置
            var defaults = {
                speed: 40,  //滚动速度,值越大速度越慢
                rowHeight: 30 //每行的高度
            };

            var opts = $.extend({}, defaults, options), intId = [];

            function marquee(obj, step) {
                obj.find(".table-body").animate({
                    marginTop: '-=1'
                }, 0, function () {
                    var s = Math.abs(parseInt($(this).css("margin-top")));
                    if (s >= step) {

                        $(this).find("table-body-row").slice(0, 1).appendTo($(this));
                        $(this).css("margin-top", 0);
                    }
                });
            }

            this.each(function (i) {
                var sh = opts["rowHeight"], speed = opts["speed"], _this = $(this);
                var _fn = function () {
                    if (_this.find(".table-body").height() > _this.height()) {
                        marquee(_this, sh);
                    }
                    intId[i] = setTimeout(_fn, speed);
                };
                _fn();
                _this.hover(function () {
                    clearInterval(intId[i]);
                }, function () {
                    _fn();
                });
            });
        };

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
                            axisLine:{
                                lineStyle:{
                                    color:'#909399'
                                }
                            },
                            splitLine:{
                                show:true,
                                lineStyle:{
                                    color:'#F2F6FC'
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
    })(jQuery, top.learun);