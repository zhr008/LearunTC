(function () {

    var custmerform = {};
    var $scroll = '';

    // 统计数据
    function target(data, $desktop) {
        if (data.length > 0) {
            var _html = '\
            <div class="lr-black-panel">\
                <div class="lr-title">统计数据</div>\
                <div class="lr-content lr-flex-content">\
                </div>\
            </div>';
            $desktop.append(_html);
            var $content = $desktop.find('.lr-flex-content');
            $.each(data, function (_index, _item) {
                var _itemHtml = '\
                    <div class="targetItem">\
                        <div class="name">'+ _item.F_Name + '</div>\
                        <div class="number" data-number="'+ _item.F_Id + '" ></div>\
                    </div>';
                $content.append(_itemHtml);
                // 获取后台数据
                learun.httpget(config.webapi + "learun/adms/desktop/data", { type: 'Target', id: _item.F_Id }, function(data){
                    if (data) {
                        $('[data-number="' + data.Id + '"]').text(data.value);
                    }
                });

            });

        }
    }
    // 列表数据
    function list(data, $desktop) {
        if (data.length > 0) {
            $.each(data, function (_index, _item) {
                var _html = '\
                <div class="lr-black-panel">\
                    <div class="lr-title">'+ _item.F_Name +'</div>\
                    <div class="lr-content" data-desktop="'+ _item.F_Id + '" ></div>\
                </div>';
                $desktop.append(_html);
                learun.httpget(config.webapi + "learun/adms/desktop/data", { type: 'list', id: _item.F_Id }, function(data) {
                    if (data) {
                        var $list = $('[data-desktop="' + data.Id + '"]');
                        $.each(data.value, function (_j, _jitem) {
                            var _itemHtml = '\
                                <div class="lr-list-item lr-dtlist-item">\
                                    <div class="lr-ellipsis">'+ _jitem.f_title + '</div>\
                                    <div class="date">'+ learun.date.format(_jitem.f_time, 'yyyy-MM-dd') + '</div>\
                                </div>';
                            var _$itemHtml = $(_itemHtml);
                            _$itemHtml[0].item = _jitem;
                            $list.append(_$itemHtml);
                        });
                        $list.find('.lr-dtlist-item').on('tap', function () {
                            var item = $(this)[0].item;
                            learun.nav.go({ path: 'workspace/listdetaile', title: '详情', param: item, type: 'right' });
                        });

                        $list = null;
                    }
                });
            });
        }
    }
    var chartMap = {};
    // 图表数据
    function chart(data, $desktop) {
        if (data.length > 0) {
            chartMap = {};
            $.each(data, function (_index, _item) {
                var _html = '\
                <div class="lr-black-panel">\
                    <div class="lr-title">'+ _item.F_Name + '</div>\
                    <div class="lr-content lr-chart-content">\
                        <div class="lr-chart-container" id="'+ _item.F_Id + '"  data-desktop="' + _item.F_Type + '" ></div>\
                    </div>\
                </div>';

                $desktop.append(_html);
                chartMap[_item.F_Id] = echarts.init(document.getElementById(_item.F_Id));

                // 获取后台数据
                learun.httpget(config.webapi + "learun/adms/desktop/data", { type: 'chart', id: _item.F_Id }, function(data) {
                    if (data) {
                        var type = $('#' + data.Id).attr('data-desktop');
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
                                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                                };

                                option.legend = {
                                    orient: 'vertical',
                                    left: 'left',
                                    data: legendData
                                };
                                option.series = [{
                                    name: '占比',
                                    type: 'pie',
                                    radius: ['50%', '70%'],
                                    avoidLabelOverlap: false,
                                    label: {
                                        normal: {
                                            show: false,
                                            position: 'center'
                                        },
                                        emphasis: {
                                            show: true,
                                            textStyle: {
                                                fontSize: '30',
                                                fontWeight: 'bold'
                                            }
                                        }
                                    },
                                    labelLine: {
                                        normal: {
                                            show: false
                                        }
                                    },
                                    data: data.value
                                }];
                                option.color = ['#df4d4b', '#304552', '#52bbc8', 'rgb(224,134,105)', '#8dd5b4', '#5eb57d', '#d78d2f'];
                                break;
                            case '1'://折线图
                            case '2'://柱状图
                                option = {
                                    grid: {
                                        top: '20px',
                                        bottom: '10px',
                                        left: '15px',
                                        right: '15px',
                                        containLabel: true
                                    },
                                    xAxis: {
                                        type: 'category',
                                        data: legendData
                                    },
                                    yAxis: {
                                        type: 'value'
                                    },
                                    series: [{
                                        data: valueData,
                                        type: type === '1' ? 'line' : 'bar'
                                    }]
                                };
                                break;
                        }
                        chartMap[data.Id].setOption(option);
                    }
                });
            });
        }
    }
    function refreshDeskTop(self) {
        learun.clientdata.get('desktop', {
            callback: function (data) {
                var $desktop = $('#lr_desktop_msg_content');
                $desktop.html('');
                target(data.target || [], $desktop);
                list(data.list || [], $desktop);
                chart(data.chart || [], $desktop);

                if (self) {
                    self.refresh(true);
                    self.endPulldownToRefresh();
                }
            }
        });
    }

    var page = {
        init: function ($page) {
            var _html = '';
            _html += '<div class="scanner">';
            _html += '<i class="iconfont icon-scan"></i>';
            _html += '</div>';

            _html += '\
                <div class="searchBox">\
                    <i class="iconfont icon-search"></i>\
                    <div class="search" >搜索应用</div>\
                </div>';

            _html += '<div class="message">';
            _html += '<i class="iconfont icon-mail"></i>';
            _html += '<span class="red"></span>';
            _html += '</div>';
            $page.parent().find('.f-page-header').addClass('lr-workspace-header').html(_html);
            // 点击搜索框
            $page.parent().find('.searchBox').on('tap', function () {
                learun.nav.go({ path: 'workspace/search', title: '', isBack: true, isHead: true });
            });
            // 点击消息图标
            $page.parent().find('.message').on('tap', function () {
                learun.nav.go({ path: 'message', title: '消息', isBack: true, isHead: true,type:'right' });
            });
            // 注册扫描
            $page.parent().find('.scanner').on('tap', function () {
                learun.code.scan(function (res) {
                    if (res.status === 'success') {
                        learun.layer.toast(res.msg);
                    }
                    else {
                        learun.layer.toast('扫描失败:' + res.msg);
                    }
                });
            });
            // 图片加载
            learun.httpget(config.webapi + "learun/adms/desktop/imgid", null, function (data) {
                if (data) {
                    var _list = [];
                    $.each(data, function (_index, _item) {
                        _list.push(config.webapi + "learun/adms/desktop/img?data=" + _item);
                    });
                    $page.find('.banner').slider({ data: _list, indicator: true, interval: 10000 });
                }
            });

            // 基础数据初始化
            learun.clientdata.init();
            refreshDeskTop();
           
            $scroll = $page.find('#lr_desktop_msg').pullRefresh({
                down: {
                    height: 30,
                    contentinit: '下拉可以刷新',
                    contentdown: '下拉可以刷新',
                    contentover: '松开立即刷新',
                    contentrefresh: '正在刷新...',
                    callback: function () {
                        refreshDeskTop(this);
                    }
                }
            });

            // 加载功能列表
            learun.clientdata.get('module', {
                callback: function (data) {
                    learun.myModule.get(data, function (myModules) {
                        console.log(myModules);

                        var mylen = parseInt((myModules.length + 1) / 4) + ((myModules.length + 1) % 4 > 0 ? 1 : 0);
                        switch (mylen) {
                            case 1:
                                $page.find('.lr-workspace-page').css('padding-top', '210px');
                                break;
                            case 2:
                                $page.find('.lr-workspace-page').css('padding-top', '290px');
                                break;
                            case 3:
                                $page.find('.lr-workspace-page').css('padding-top', '370px');
                                break;
                        }

                        var map = {};
                        $.each(data, function (_index, _item) {
                            map[_item.F_Id] = _item;
                        });
                        var $appbox = $page.find('.appbox');
                        var $last = null;
                        $.each(myModules, function (_index, _id) {
                            var item = map[_id];
                            if (item) {
                                var _html = '\
                                        <div class="appitem appitem2" data-value="'+ item.F_Id + '">\
                                            <div><i class="'+ item.F_Icon + '"></i></div>\
                                            <span>'+ item.F_Name + '</span>\
                                        </div>';
                                var _$html = $(_html);
                                _$html[0].item = item;
                                if ($last === null) {
                                    $appbox.prepend(_$html);
                                }
                                else {
                                    $last.after(_$html);
                                }
                                $last = _$html;

                            }
                        });
                        $last = null;
                    });
                }
            });
            // 注册更多功能按钮
            $page.find('#lr_more_app').on('tap', function () {
                learun.nav.go({ path: 'workspace/modulelist', title: "", type: 'right' });
            });
            // 点击功能按钮
            $page.delegate('.appitem2', 'tap', function () {
                var $this = $(this);
                var item = $this[0].item;
                if (item.F_IsSystem === 1) {// 代码开发功能
                    learun.nav.go({ path: item.F_Url, title: item.F_Name, isBack: true, isHead: true, type: 'right' });
                }
                else {// 自定义表单开发功能
                    learun.nav.go({ path: 'custmerform', title: item.F_Name, param: { formSchemeId: item.F_FormId, girdScheme: item.F_Scheme }, isBack: true, isHead: true, type: 'right' });
                }
                return false;
            });
        },
        reload: function ($page, pageinfo) {
            if (learun.isOutLogin) {// 如果是重新登录的情况刷新下桌面数据
                learun.isOutLogin = false;
                refreshDeskTop();
                learun.clientdata.clear('module');
                learun.myModule.states = -1;
                // 图片加载
                learun.httpget(config.webapi + "learun/adms/desktop/imgid", null, function (data) {
                    if (data) {
                        var _list = [];
                        $.each(data, function (_index, _item) {
                            _list.push(config.webapi + "learun/adms/desktop/img?data=" + _item);
                        });
                        $page.find('.banner').after('<div class="banner"></div>').remove();
                        $page.find('.banner').slider({ data: _list, indicator: true, interval: 10000 });
                    }
                });
            }
            // 加载功能列表
            learun.clientdata.get('module', {
                callback: function (data) {
                    learun.myModule.get(data, function (myModules) {
                        var mylen = parseInt((myModules.length + 1) / 4) + ((myModules.length + 1) % 4 > 0 ? 1 : 0);
                        switch (mylen) {
                            case 1:
                                $page.find('.lr-workspace-page').css('padding-top', '210px');
                                break;
                            case 2:
                                $page.find('.lr-workspace-page').css('padding-top', '290px');
                                break;
                            case 3:
                                $page.find('.lr-workspace-page').css('padding-top', '370px');
                                break;
                        }

                        var map = {};
                        $.each(data, function (_index, _item) {
                            map[_item.F_Id] = _item;
                        });
                        var $appbox = $page.find('.appbox');
                        var $last = null;
                        $appbox.find(".appitem2").remove();
                        $.each(myModules, function (_index, _id) {
                            var item = map[_id];
                            if (item) {
                                var _html = '\
                                        <div class="appitem appitem2" data-value="'+ item.F_Id + '">\
                                            <div><i class="'+ item.F_Icon + '"></i></div>\
                                            <span>'+ item.F_Name + '</span>\
                                        </div>';
                                var _$html = $(_html);
                                _$html[0].item = item;
                                if ($last === null) {
                                    $appbox.prepend(_$html);
                                }
                                else {
                                    $last.after(_$html);
                                }
                                $last = _$html;

                            }
                        });
                        $last = null;
                    });
                }
            });
            $.each(chartMap, function (id, obj) {
                obj.resize();
            });
        }
    };
    return page;
})();