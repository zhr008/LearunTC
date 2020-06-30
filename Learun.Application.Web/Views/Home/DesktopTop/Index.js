$(function () {
    "use strict";

    var targetMap = {};
    var listMap = {};
    // 统计指标显示
    function target(data) {
        if (data.length > 0) {// 计算每块统计区域宽度
            // 滚动条优化
            $('#lr_target_content').lrscroll();
            var $target = $('#lr_target_content .lr-scroll-box');
            $.each(data, function (_index, _item) {
                targetMap[_item.F_Id] = _item;
                var _html = '\
                    <div class="target-item" data-Id="'+ _item.F_Id + '">\
                        <div class="count"><span data-value="'+ _item.F_Id + '"></span></div>\
                        <div class="point"><span></span></div>\
                        <div class="content">\
                            <i class="'+ _item.F_Icon + '"></i>\
                            <div class="text">'+ _item.F_Name + '</div>\
                        </div>\
                    </div>';

                $target.append(_html);
                // 向后台请求数据
                top.learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTTarget/GetSqlData', { Id: _item.F_Id }, function (data) {
                    if (data) {
                        $target.find('[data-value="' + data.Id + '"]').text(data.value);
                    }
                });
            });
            $target.find('.target-item').on('click', function () {
                var Id = $(this).attr('data-Id');
                top.learun.frameTab.open({ F_ModuleId: Id, F_FullName: targetMap[Id].F_Name, F_UrlAddress: targetMap[Id].F_Url });
                return false;
            });
        }
    }
    // 消息列表
    function list(data) {
        if (data.length > 0) {
            $('#lr_desktop_list').lrscroll();
            var $list = $('#lr_desktop_list .lr-scroll-box');

            $.each(data, function (_index, _item) {
                listMap[_item.F_Id] = _item;
                var _html = '\
                <div class="lr-desktop-list"  data-Id="'+ _item.F_Id + '">\
                    <div class="title">\
                        '+ _item.F_Name + '\
                        <span class="menu" title="更多">\
                            <span class="point"></span>\
                            <span class="point"></span>\
                            <span class="point"></span>\
                        </span>\
                    </div>\
                    <div class="content" data-value="'+ _item.F_Id + '">\
                    </div>\
                </div>';
                $list.append(_html);
                // 向后台请求数据
                top.learun.httpAsync('GET', top.$.rootUrl + '/LR_Desktop/DTList/GetSqlData', { Id: _item.F_Id }, function (data) {
                    if (data) {
                        var $content = $list.find('[data-value="' + data.Id + '"]');
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

                        $content.find('.lr-list-line').on('click', function () {
                            var $p = $(this).parent();
                            var id = $p.attr('data-value');
                            var item = $(this)[0].item;

                            if (listMap[id].F_ItemUrl) {
                                top.learun.frameTab.open({ F_ModuleId: 'dtlist' + item.f_id, F_FullName: item.f_title, F_UrlAddress: listMap[id].F_ItemUrl + item.f_id });
                            }
                            else {
                                top['dtlist' + item.f_id] = item;
                                top.learun.frameTab.open({ F_ModuleId: 'dtlist' + item.f_id, F_FullName: item.f_title, F_UrlAddress: '/Utility/ListContentIndex?id=' + item.f_id });
                            }
                            return false;
                        });
                    }
                });
            });
            $list.find('.lr-desktop-list .menu').on('click', function () {
                var $p = $(this).parents('.lr-desktop-list');
                var id = $p.attr('data-Id');
                top.learun.frameTab.open({ F_ModuleId: id, F_FullName: listMap[id].F_Name, F_UrlAddress: listMap[id].F_Url });
                return false;
            });
        }
    }
    var chartMap = {};
    // 图表
    function chart(data) {
        if (data.length > 0) {
            $('#lr_desktop_chart').lrscroll();
            var $chart = $('#lr_desktop_chart>.lr-scroll-box');


            $.each(data, function (_index, _item) {
                var _html = '\
                <div class="col-xs-'+ (12 / parseInt(_item.F_Proportion1)) + '">\
                    <div class="lr-desktop-chart">\
                        <div class="title">'+ _item.F_Name + '</div>\
                        <div class="content"  id="'+ _item.F_Id + '"  data-type="' + _item.F_Type + '"></div>\
                    </div>\
                </div>';
                $chart.append(_html);

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
        }
    }
    // 获取配置数据
    top.learun.clientdata.getAsync('desktop', {
        callback: function (data) {

            target(data.target || []);
            list(data.list || []);
            chart(data.chart || []);
        }
    });
});