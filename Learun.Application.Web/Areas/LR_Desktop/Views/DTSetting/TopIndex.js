/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.05.28
 * 描 述：PC端桌面配置
 */
var refreshGirdData;
var selectedRow = null;
var switchbtn = 1;

var bootstrap = function ($, learun) {
    "use strict";

    var targetMap = {};
    var listMap = {};
    var chartMap = {};
    var chartMap2 = {};

    var page = {
        init: function () {
            $('#lr_target_content').lrscroll();
            $('#lr_desktop_list').lrscroll();
            $('#lr_desktop_chart').lrscroll();
            page.bind();

            learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTTarget/GetPageList', {}, function (data) {
                page.target(data || []);
            });

            learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTList/GetPageList', {}, function (data) {
                page.list(data || []);
            });

            learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTChart/GetPageList', {}, function (data) {
                page.chart(data || []);
            });
        },
        target: function (data) {
            var $target = $('#lr_target_content .lr-scroll-box');
            $.each(data, function (_index, _item) {
                targetMap[_item.F_Id] = _item;
                var _html = '\
                    <div class="target-item targetItem" data-Id="'+ _item.F_Id + '">\
                        <div class="count"><span data-value="'+ _item.F_Id + '"></span></div>\
                        <div class="point"><span></span></div>\
                        <div class="content">\
                            <i class="'+ _item.F_Icon + '"></i>\
                            <div class="text">'+ _item.F_Name + '</div>\
                        </div>\
                        <div class="targetItemtool" ><div class="tool" data-Id="' + _item.F_Id + '">\
                            <div>编辑</div>\
                            <div>删除</div>\
                        </div></div>\
                    </div>';

                $target.append(_html);
                // 向后台请求数据
                top.learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTTarget/GetSqlData', { Id: _item.F_Id }, function (data) {
                    if (data) {
                        $target.find('[data-value="' + data.Id + '"]').text(data.value);
                    }
                });
            });
        },
        list: function (data) {
            var $btn = $('#lr-add-list');
            listMap = {};
            $.each(data, function (_index, _item) {
                listMap[_item.F_Id] = _item;
                var _html = '\
                <div class="lr-desktop-list listItem"  data-Id="'+ _item.F_Id + '">\
                    <div class="title">\
                        '+ _item.F_Name + '<div class="tool"><div>编辑</div><div>删除</div></div>\
                    </div>\
                    <div class="content" data-value="'+ _item.F_Id + '">\
                    </div>\
                </div>';
                $btn.before(_html);
                // 向后台请求数据
                top.learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTList/GetSqlData', { Id: _item.F_Id }, function (data) {
                    if (data) {
                        var $content = $('[data-value="' + data.Id + '"]');
                        $.each(data.value, function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-line">\
                                <div class="point"></div>\
                                <div class="text">'+ _item.f_title + '</div>\
                                <div class="date">'+ _item.f_time + '</div>\
                            </div>';

                            var _$html = $(_html);
                            _$html[0].item = _item;
                            $content.append(_$html);
                        });
                    }
                });
            });
        },
        chart: function (data) {
            var $btn = $('#lr-add-chart');
            chartMap = {};
            chartMap2 = {};
            $.each(data, function (_index, _item) {
                chartMap2[_item.F_Id] = _item;

                var _html = '\
                <div class="col-xs-'+ (12 / parseInt(_item.F_Proportion1)) + ' chartItem"  data-Id="' + _item.F_Id + '" >\
                    <div class="lr-desktop-chart">\
                        <div class="title">'+ _item.F_Name + '<div class="tool"><div>编辑</div><div>删除</div></div></div>\
                        <div class="content"  id="'+ _item.F_Id + '"  data-type="' + _item.F_Type + '"></div>\
                    </div>\
                </div>';
                $btn.before(_html);

                chartMap[_item.F_Id] = echarts.init(document.getElementById(_item.F_Id));

                // 向后台请求数据
                top.learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTChart/GetSqlData', { Id: _item.F_Id }, function (data) {
                    if (data) {
                        var type = $('#' + data.Id).attr('data-type');
                        var legendData = [];
                        var valueData = [];
                        $.each(data.value, function (_index, _item) {
                            legendData.push(_item.name);
                            valueData.push(_item.value);
                        });

                        var option = {};
                        switch (type) {
                            case '0'://饼图
                                option.tooltip = {
                                    trigger: 'item',
                                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                                };

                                option.legend = {
                                    bottom: 'bottom',
                                    data: legendData
                                };
                                option.series = [{
                                    name: '占比',
                                    type: 'pie',
                                    radius: '75%',
                                    center: ['50%', '50%'],
                                    label: {
                                        normal: {
                                            formatter: '{b}:{c}: ({d}%)',
                                            textStyle: {
                                                fontWeight: 'normal',
                                                fontSize: 12,
                                                color: '#333'
                                            }
                                        }
                                    },
                                    data: data.value,
                                    itemStyle: {
                                        emphasis: {
                                            shadowBlur: 10,
                                            shadowOffsetX: 0,
                                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                                        }
                                    }
                                }];
                                option.color = ['#df4d4b', '#304552', '#52bbc8', 'rgb(224,134,105)', '#8dd5b4', '#5eb57d', '#d78d2f'];
                                break;
                            case '1'://折线图
                                option.tooltip = {
                                    trigger: 'axis'
                                };
                                option.grid = {
                                    bottom: '8%',
                                    containLabel: true
                                };
                                option.xAxis = {
                                    type: 'category',
                                    boundaryGap: false,
                                    data: legendData
                                };
                                option.yAxis = {
                                    type: 'value'
                                }
                                option.series = [{
                                    type: 'line',
                                    data: valueData
                                }];


                                break;
                            case '2'://柱状图
                                option.tooltip = {
                                    trigger: 'axis'
                                };
                                option.grid = {
                                    bottom: '8%',
                                    containLabel: true
                                };
                                option.xAxis = {
                                    type: 'category',
                                    boundaryGap: false,
                                    data: legendData
                                };
                                option.yAxis = {
                                    type: 'value'
                                }
                                option.series = [{
                                    type: 'bar',
                                    data: valueData
                                }];
                                break;
                        }
                        chartMap[data.Id].setOption(option);
                    }
                });

            });

            window.onresize = function (e) {
                $.each(chartMap, function (id, obj) {
                    obj.resize(e);
                });
            }
        },
        bind: function () {
            // 统计
            $('#lr_target_content').on('click', '.tool>div', function () {
                var $this = $(this);
                var text = $this.text();
                var id = $this.parent().attr('data-Id');
                var item = targetMap[id];
                selectedRow = item;
                switchbtn = 1;
                if (text === '编辑') {
                    learun.layerForm({
                        id: 'Form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_Desktop/DTTarget/Form',
                        width: 600,
                        height: 500,
                        btn: null
                    });
                }
                else {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_Desktop/DTTarget/DeleteForm', { keyValue: item.F_Id }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            $('#lr-add-target').on('click', function () {
                selectedRow = null;
                switchbtn = 1;
                learun.layerForm({
                    id: 'Form',
                    title: '添加',
                    url: top.$.rootUrl + '/LR_Desktop/DTTarget/Form',
                    width: 600,
                    height: 500,
                    btn: null
                });
            });

            // 列表
            $('.lr-desktop-panel').on('click', '.listItem .tool>div', function () {
                var $this = $(this);
                var text = $this.text();
                var id = $this.parents('.listItem').attr('data-Id');
                var item = listMap[id];
                selectedRow = item;
                switchbtn = 2;
                if (text === '编辑') {
                    learun.layerForm({
                        id: 'Form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_Desktop/DTList/Form',
                        width: 600,
                        height: 500,
                        btn: null
                    });
                }
                else {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_Desktop/DTList/DeleteForm', { keyValue: item.F_Id }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }

            });
            $('#lr-add-list').on('click', function () {
                selectedRow = null;
                switchbtn = 2;
                learun.layerForm({
                    id: 'Form',
                    title: '添加',
                    url: top.$.rootUrl + '/LR_Desktop/DTList/Form',
                    width: 600,
                    height: 500,
                    btn: null
                });
            });

            // 图表
            $('.lr-desktop-panel').on('click', '.chartItem .tool>div', function () {
                var $this = $(this);
                var text = $this.text();
                var id = $this.parents('.chartItem').attr('data-Id');
                var item = chartMap2[id];
                selectedRow = item;
                switchbtn = 3;
                if (text === '编辑') {
                    learun.layerForm({
                        id: 'Form',
                        title: '编辑',
                        url: top.$.rootUrl + '/LR_Desktop/DTChart/Form',
                        width: 600,
                        height: 500,
                        btn: null
                    });
                }
                else {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_Desktop/DTChart/DeleteForm', { keyValue: item.F_Id }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }

            });
            $('#lr-add-chart').on('click', function () {
                selectedRow = null;
                switchbtn = 3;
                learun.layerForm({
                    id: 'Form',
                    title: '添加',
                    url: top.$.rootUrl + '/LR_Desktop/DTChart/Form',
                    width: 600,
                    height: 500,
                    btn: null
                });
            });
        }
    };

    refreshGirdData = function () {
        switch (switchbtn) {
            case 1:
                learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTTarget/GetPageList', {}, function (data) {
                    $('.targetItem').remove();
                    page.target(data || []);
                });
                break;
            case 2:
                learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTList/GetPageList', {}, function (data) {
                    $('.listItem').remove();
                    page.list(data || []);
                });
                break;
            case 3:
                learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTChart/GetPageList', {}, function (data) {
                    $('.chartItem').remove();
                    page.chart(data || []);
                });
                break;
        }
    }

    page.init();
}