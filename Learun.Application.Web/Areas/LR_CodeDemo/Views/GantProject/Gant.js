/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：208.11.22
 * 描 述：甘特图
 */
var isMain = false;
var keyValue = '';
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGantt();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/LR_CodeDemo/GantProject/Form',
                    width: 800,
                    height: 600,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 删除
            $('#lr_delete').on('click', function () {
                if (learun.checkrow(keyValue)) {
                    var url = top.$.rootUrl + '/LR_CodeDemo/GantProject/DeleteForm';
                    if (!isMain) {
                        url = top.$.rootUrl + '/LR_CodeDemo/GantProject/DeleteDetail'
                    }
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(url, { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGantt: function () {
            $('#gridtable').lrGantt({
                url: top.$.rootUrl + '/LR_CodeDemo/GantProject/GetProjectList',
                childUrl: top.$.rootUrl + '/LR_CodeDemo/GantProject/GetProjectDetail',
                timebtns: ['month', 'week', 'day'],//'month', 'week', 'day', 'hour'
                timeClick: function (data, $self) {
                    if (data.item.hasChildren) {
                        isMain = true;
                    }
                    else {
                        isMain = false;
                    }
                    keyValue = data.item.id;
                },
                timeDoubleClick: function (data, $self) {
                    if (data.item.hasChildren) {
                        isMain = true;
                    }
                    keyValue = data.item.id;
                    if (isMain) {
                        learun.layerForm({
                            id: 'form',
                            title: '编辑',
                            url: top.$.rootUrl + '/LR_CodeDemo/GantProject/Project?keyValue=' + keyValue,
                            width: 600,
                            height: 400,
                            callBack: function (id) {
                                return top[id].acceptClick(location.reload());
                            }
                        });
                    }
                    else {
                        learun.layerForm({
                            id: 'form',
                            title: '编辑',
                            url: top.$.rootUrl + '/LR_CodeDemo/GantProject/ProjectDetail?keyValue=' + keyValue,
                            width: 600,
                            height: 400,
                            callBack: function (id) {
                                return top[id].acceptClick(location.reload());
                            }
                        });
                    }
                },
                click: function (item, $item) {
                    if (item.hasChildren) {
                        isMain = true;
                    }
                    else {
                        isMain = false;
                    }
                    keyValue = item.id;
                    if (isMain) {
                        learun.layerForm({
                            id: 'form',
                            title: '编辑',
                            url: top.$.rootUrl + '/LR_CodeDemo/GantProject/Form?keyValue=' + keyValue,
                            width: 600,
                            height: 400,
                            callBack: function (id) {
                                return top[id].acceptClick(location.reload());
                            }
                        });
                    }
                }
            }).lrGanttSet('reload');
        },
        search: function (param) {
            $('#gridtable').lrGanttSet('reload', param || {});
        }
    };
    page.init();
}


