$(function () {
    "use strict";
    var targetMap = {};
    var listMap = {};
    // 统计指标显示
    function target(data) {
        if (data.length > 0) {// 计算每块统计区域宽度
            // 滚动条优化
            $('#lr_target_content').lrscroll();
            var itemW = 250;
            var allw = 0;

            var w = $('#lr_target_content').width();
            var itemW = w / data.length;
            if (itemW < 250) {
                itemW = 250;
            }
            allw = itemW * data.length;
            var $target = $('#lr_target_content .lr-scroll-box');
            $target.css('width', allw);
            $.each(data, function (_index, _item) {
                targetMap[_item.F_Id] = _item;
                var _html = '\
                    <div class="lr-item-20" data-Id="'+ _item.F_Id +'">\
                        <div class="task-stat">\
                            <div class="visual">\
                                <i class="'+ _item.F_Icon + '"></i>\
                            </div>\
                            <div class="number" data-value="'+ _item.F_Id +'"></div>\
                            <div class="desc">'+ _item.F_Name + '</div>\
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
            $target.find('.lr-item-20').on('click', function () {
                var Id = $(this).attr('data-Id');
                top.learun.frameTab.open({ F_ModuleId: Id, F_FullName: targetMap[Id].F_Name, F_UrlAddress: targetMap[Id].F_Url });
                return false;
            });

            $target.find('.lr-item-20').css('width', itemW);
            $('#lr_target_content').resize(function () {
                var w = $('#lr_target_content').width();
                var itemW = w / data.length;
                if (itemW < 250) {
                    itemW = 250;
                }
                allw = itemW * data.length;
                $target.css('width', allw);
                $target.find('.lr-item-20').css('width', itemW);
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
                        '+ _item.F_Name +'\
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
                                <div class="text">'+ _item.f_title+'</div>\
                                <div class="date">'+ _item.f_time+'</div>\
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
            var $chartTitle = $('.lr-desktop-chat-panel-title');
            var $chartCotent = $('.lr-desktop-chat-panel-content');

            $.each(data, function (_index, _item) {
                var _titleHtml = '<div class="title-item ' + (_index == 0 ? 'active' : '') + '" data-value="' + _item.F_Id + '" >' + _item.F_Name + '</div>';
                $chartTitle.append(_titleHtml);
                var _contentHtml = '<div class="content-item ' + (_index == 0 ? 'active' : '') + '" id="' + _item.F_Id + '"  data-type="' + _item.F_Type + '"></div >';
                $chartCotent.append(_contentHtml);

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

            $('.lr-desktop-chat-panel-title .title-item').on('click', function (e) {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    var $p = $this.parent();
                    $p.find('.active').removeClass('active');
                    $this.addClass('active');
                    var v = $this.attr('data-value');
                    $('.lr-desktop-chat-panel-content .active').removeClass('active');
                    $('#' + v).addClass('active');

                    chartMap[v].resize(e);
                }
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