/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.7.12
 * 描 述：力软移动端框架(ADMS) 流程我的任务
 */
(function () {
    var begin = '';
    var end = '';

    var $header = null;

    var page = {
        currentPage: 0,
        grid: [],
        init: function ($page) {
            page.currentPage = 0;
            page.grid = [];
            // 添加头部按钮列表
            var _html = '\
                <div class="lr-form-header-btnlist" style="display:block;" >\
                    <div class="lr-form-header-more" ><i class="iconfont icon-searchlist" ></i></div>\
                    <div class="lr-form-header-edit" ><i class="iconfont icon-time" ></i></div>\
                </div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);

            // 设置查询条件
            $header.find('.lr-form-header-edit').searchdate({
                callback: function (_begin, _end) {
                    begin = _begin;
                    end = _end;
                    page.grid[page.currentPage].reload();
                }
            });
            // 点击搜索按钮
            $header.find('.lr-form-header-more').on('tap', function () {
                learun.nav.go({ path: 'search', title: '', isBack: true, isHead: true, param: 'nworkflow/myflow' });// 告诉搜索页本身所在的地址
            });
            $page.find('#myflow_tab').toptab(['我的', '待办', '已办'], function (_index) {
                page.currentPage = parseInt(_index);
                begin = "";
                end = "";
                page.grid[page.currentPage].reload();
            }).each(function (index) {
                var $this = $(this);
                switch (index) {
                    case 0:
                        page.grid[index] = $this.lrpagination({
                            lclass: "lr-list lr-flow-list",
                            rows: 10,                            // 每页行数
                            getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                                param.begin = begin;
                                param.end = end;
                                page.loadData(param, callback, $page);
                            },
                            renderData: function (_index, _item, _$item) {// 渲染数据模板
                                return page.rowRender(_index, _item, _$item, $page);
                            },
                            click: function (item, $item) {// 列表行点击事件
                                page.click(item, $item, $page);
                            }
                        });
                        break;
                    case 1:
                        page.grid[index] = $this.lrpagination({
                            lclass: "lr-list lr-flow-list",
                            rows: 10,                            // 每页行数
                            getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                                param.begin = begin;
                                param.end = end;
                                page.loadData(param, callback, $page);
                            },
                            renderData: function (_index, _item, _$item) {// 渲染数据模板
                                return page.rowRender(_index, _item, _$item, $page);
                            },
                            click: function (item, $item) {// 列表行点击事件
                                page.click(item, $item, $page);
                            }
                        });
                        break;
                    case 2:
                        page.grid[index] = $this.lrpagination({
                            lclass: "lr-list lr-flow-list",
                            rows: 10,                            // 每页行数
                            getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                                param.begin = begin;
                                param.end = end;
                                page.loadData(param, callback, $page);
                            },
                            renderData: function (_index, _item, _$item) {// 渲染数据模板
                                return page.rowRender(_index, _item, _$item, $page);
                            },
                            click: function (item, $item) {// 列表行点击事件
                                page.click(item, $item, $page);
                            }
                        });
                        break;
                }
                $this = null;
            });
        },
        lclass: 'lr-list lr-flow-list',
        loadData: function (param, callback, $page) {// 列表加载后台数据
            var _postParam = {
                pagination: {
                    rows: param.rows,
                    page: param.page,
                    sidx: 'F_CreateDate',
                    sord: 'DESC'
                },
                queryJson: '{}'
            };
            if (param.keyword) {
                _postParam.queryJson = JSON.stringify({ keyword: param.keyword });
            }
            if (param.begin && param.end) {
                _postParam.queryJson = JSON.stringify({ StartTime: param.begin, EndTime: param.end });
            }
            var url = '';
            var mypage = learun.nav.getpage('nworkflow/myflow');
            switch (mypage.currentPage) {
                case 0:
                    url = config.webapi + "learun/adms/newwf/mylist";
                    break;
                case 1:
                    url = config.webapi + "learun/adms/newwf/mytask";
                    break;
                case 2:
                    url = config.webapi + "learun/adms/newwf/mytaskmaked";
                    break;
            }

            learun.httpget(url, _postParam, function (data) {
                if (data) {
                    callback(data.rows, parseInt(data.records));
                }
                else {
                    callback([], 0);
                }
            });
        },
        rowRender: function (_index, _item, _$item, $page) {// 渲染列表行数据
            var mypage = learun.nav.getpage('nworkflow/myflow');

            var levelText = '';
            var levelbg = '';
            switch (_item.F_Level) {
                case 0:
                    levelText = '普通';
                    levelbg = 'bgcblue1';
                    break;
                case 1:
                    levelText = '重要';
                    levelbg = 'bgcyellow';
                    break;
                case 2:
                    levelText = '紧急';
                    levelbg = 'bgcpink';
                    break;
                default:
                    levelText = '普通';
                    levelbg = 'bgcblue1';
                    break;
            }
            var statusText = '';
            if (mypage.currentPage != 2) {
                statusText = '待审批';
            }
            
            if (_item.F_TaskName) {
                statusText = '【' + _item.F_TaskName + '】' + statusText;
            }

            if (_item.F_IsFinished === 1) {
                statusText = '结束';
            }
            else if (_item.F_EnabledMark === 3) {
                statusText = '<span style="color:red;" >作废</span>';
            }
            else if (_item.F_EnabledMark === 2) {
                statusText = '草稿';
                _item.F_Title = '草稿需要编辑!';
            }

            if (_item.F_IsAgain === 1 && mypage.currentPage !== 2) {
                statusText = '<span style="color:red;" >重新发起</span>';
            }

            if (_item.F_TaskType == 3 && mypage.currentPage === 1) {
                statusText = '【加签】' + statusText;
            }

            if (_item.F_IsUrge == "1" && mypage.currentPage === 1) {
                statusText = '<span style="color:red;" >【催办加急】</span>' + statusText;
            }

            var _html = '';
            
            if (mypage.currentPage === 0) {
                _html = '<div class="lr-list-item">\
                    <div class="left" >\
                        <span class="circle '+ levelbg + '">' + levelText + '</span>\
                    </div >\
                    <div class="middle">\
                        <div class="title">' + _item.F_Title + '</div>\
                        <div class="text">'+ _item.F_SchemeName + '</div>\
                        <div class="status">'+ statusText + '</div>\
                    </div>\
                    <div class="right">'+ learun.date.format(_item.F_CreateDate, 'yyyy-MM-dd') + '</div>\
                </div>';
            }
            else {
                _html = '<div class="lr-list-item">\
                    <div class="left" >\
                        <span class="circle '+ levelbg + '">' + levelText + '</span>\
                    </div >\
                    <div class="middle">\
                        <div class="title">' + _item.F_Title + '</div>\
                        <div class="text">'+ _item.F_CreateUserName + '/' + _item.F_SchemeName + '</div>\
                        <div class="status">'+ statusText + '</div>\
                    </div>\
                    <div class="right">'+ learun.date.format(_item.F_CreateDate, 'yyyy-MM-dd') + '</div>\
                </div>';
            }
            return _html;
        },
        click: function (item, $item, $page) {// 列表行点击触发方法
            var mypage = learun.nav.getpage('nworkflow/myflow');

            if (item.F_IsAgain === 1 && mypage.currentPage !== 2) {// 重新发起流程
                learun.nav.go({ path: 'nworkflow/againcreateflow', title: item.F_SchemeName, type: 'right', param: { processId: item.F_Id } });
                return false;
            }
            switch (mypage.currentPage) {
                case 0:
                    if (item.F_EnabledMark === 2) {// 草稿
                        learun.actionsheet({
                            id: 'myflow0',
                            data: [
                                {
                                    text: '编辑草稿',
                                    group: '1',
                                    event: function () {
                                        learun.nav.go({ path: 'nworkflow/draft', title: item.F_SchemeName, type: 'right', param: { processId: item.F_Id } });
                                    }
                                },
                                {
                                    text: '删除草稿',
                                    mark: true,
                                    group: '2',
                                    event: function () {
                                        learun.layer.confirm('确定要删除流程草稿？', function (_index) {
                                            if (_index === '1') {
                                                learun.layer.loading(true, "正在删除该流程草稿");
                                                learun.httppost(config.webapi + "learun/adms/newwf/deldraft", item.F_Id, function (data) {
                                                    learun.layer.loading(false);
                                                    if (data) {// 删除数据成功
                                                        page.grid[0].reload();
                                                    }

                                                });
                                            }
                                        }, '力软提示', ['取消', '确定']);
                                    }
                                }
                            ],
                            cancel: function () { }
                        });
                        return false;
                    }

                    learun.nav.go({ path: 'nworkflow/myprocessInfo', title: item.F_SchemeName, type: 'right', param: { processId: item.F_Id, isStart: item.F_IsStart } });
                    break;
                case 1:
                    if (item.F_TaskType == 3) {// 加签
                        learun.nav.go({ path: 'nworkflow/signAudit', title: item.F_SchemeName, type: 'right', param: { processId: item.F_Id, taskId: item.F_TaskId } });
                        return false;
                    }
                    else if (item.F_TaskType == 4 || item.F_TaskType == 6) {// 子流程发起
                        learun.nav.go({ path: 'nworkflow/chlidaudit', title: item.F_SchemeName, type: 'right', param: { processId: item.F_Id, taskId: item.F_TaskId } });
                        return false;
                    }
                    else if (item.F_TaskType == 2) {// 查阅
                        learun.nav.go({ path: 'nworkflow/refer', title: item.F_SchemeName, type: 'right', param: { processId: item.F_Id, taskId: item.F_TaskId } });
                        return false;
                    }
                    learun.nav.go({ path: 'nworkflow/audit', title: item.F_SchemeName, type: 'right', param: { processId: item.F_Id, taskId: item.F_TaskId } });
                    break;
                case 2:
                    if (item.F_TaskType == 4 || item.F_TaskType == 6) {// 子流程发起
                        learun.nav.go({ path: 'nworkflow/chlidlook', title: item.F_SchemeName, type: 'right', param: { processId: item.F_Id, taskId: item.F_TaskId } });
                        return false;
                    }
                    learun.nav.go({ path: 'nworkflow/processInfo', title: item.F_SchemeName, type: 'right', param: { processId: item.F_Id, taskId: item.F_TaskId } });
                    break;
            }
            return false;
        },
        destroy: function (pageinfo) {
            page.currentPage = 0;
            page.grid = [];
        }
    };
    return page;
})();