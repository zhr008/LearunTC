/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-01-03 09:35
 * 描  述：设置模块2
 */
var sort = request('sort');
var tabName = '';
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var keyValue = '';
    var categoryId = '';
    var currentList = [];
    var currentMap = {};

    var currentModule = learun.frameTab.currentIframe().currentModule;

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 比例选择
            $('#prop').lrselect({
                data: [{ id: '0.5', text: '1/2' }, { id: '0.333333', text: '1/3' }, { id: '0.66666', text: '2/3' }, { id: '1', text: '1' }],
                placeholder: false
            }).lrselectSet('1');

            // 页面选择
            $('#F_Url').lrselect({
                text: 'F_Title',
                value: 'F_Id',
                url: top.$.rootUrl + '/LR_PortalSite/Page/GetList',
                allowSearch: true
            });

            $('#F_Category').lrDataItemSelect({ code: 'PortalSiteType' });
            $('#F_Category').on('change', function () {
                var v = $(this).lrselectGet();
                categoryId = v;
                page.search();
            });
            $('#select_grid').jfGrid({
                url: top.$.rootUrl + '/LR_PortalSite/Article/GetPageList',
                headData: [
                    { label: '标题', name: 'F_Title', width: 330, align: "left" },
                    {
                        label: "分类", name: "F_Category", width: 100, align: "center",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'PortalSiteType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: '发布时间', name: 'F_PushDate', width: 80, align: "center",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    }
                ],
                mainId: 'F_Id',
                isPage: true,
                isMultiselect: true,
                multiselectfield: 'isCheck',
                sidx: 'F_PushDate desc',
                onRenderBefore: function (datas) {
                    $.each(datas, function (_index, _item) {
                        if (currentMap[_item.F_Id]) {
                            _item.isCheck = 1;
                        }
                    });
                },
                onSelectRow: function (row, isCheck) {
                    if (isCheck) {
                        var _row = { F_Id: row.F_Id, F_Title: row.F_Title, F_Category: row.F_Category, F_PushDate: row.F_PushDate };
                        $('#selected_grid').jfGridSet('addRow', _row);
                        currentMap[row.F_Id] = true;

                    } else {
                        $('#selected_grid').jfGridSet('removeRow', row.F_Id);
                        currentMap[row.F_Id] = false;
                    }
                }
            });

            $('#selected_grid').jfGrid({
                headData: [
                    {
                        label: "", name: "btn", width: 60, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                $('#selected_grid').jfGridSet('removeRow', row.F_Id);
                                $('#select_grid').jfGridSet('nocheck', row.F_Id);
                                currentMap[row.F_Id] = false;
                                return false;
                            });
                            return '<span class=\"label label-danger \" style=\"cursor: pointer;\">移除</span>';
                        }
                    },
                    { label: '标题', name: 'F_Title', width: 300, align: "left" },
                    {
                        label: "分类", name: "F_Category", width: 100, align: "center",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'PortalSiteType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: '发布时间', name: 'F_PushDate', width: 80, align: "center",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    }
                ],
                mainId: 'F_Id'
            });

            $('#left_list').on('click', '.tab-item', function () {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    $this.parent().find('.active').removeClass('active');
                    $this.addClass('active');

                    $('#txt_Keyword').val('');

                    currentList = $this[0].data.list;
                    currentMap = {};
                    $.each(currentList, function (_index, _item) {
                        currentMap[_item.F_Id] = true;
                    });

                    // 加载已经选择文章数据
                    $('#selected_grid').jfGridSet('refreshdata', currentList);

                    page.search();

                }
            });


            $('#add_item').on('click', function () {
                tabName = '';
                learun.layerForm({
                    id: 'settingTitle',
                    title: '添加tab标签项',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/AddTabForm',
                    width: 350,
                    height: 140,
                    callBack: function (id) {
                        return top[id].acceptClick(function (name) {
                            var $item = $('<div class="left-list-item  tab-item"><div class="btn-list" ><span>上移</span><span>下移</span><span>编辑</span><span>删除</span></div><span class="itemname" >' + name + '</span></div>');
                            $item[0].data = {
                                name: name,
                                list:[]
                            }
                            $('#add_item').before($item);
                            $item.trigger('click');
                        });
                    }
                });
            });

            // 编辑分类项
            $('#left_list').on('click', '.btn-list>span', function () {
                var $this = $(this);
                var $item = $this.parents('.left-list-item');
                var text = $this.text();
                switch (text) {
                    case '上移':
                        $item.prev().before($item);
                        break;
                    case '下移':
                        if (!$item.next().hasClass('active2')) {
                            $item.next().after($item);
                        }
                        break;
                    case '编辑':
                        tabName = $item.find('.itemname').text();
                        learun.layerForm({
                            id: 'settingTitle',
                            title: '编辑tab标签项',
                            url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/AddTabForm',
                            width: 350,
                            height: 140,
                            callBack: function (id) {
                                return top[id].acceptClick(function (name) {
                                    $item.find('.itemname').text(name);
                                });
                            }
                        });
                        break;
                    case '删除':
                        if ($item.prev().length > 0) {
                            $item.prev().trigger('click');
                        }
                        else{
                            $item.next().trigger('click');
                        }
                       
                        $item.remove();
                        break;
                }

                return false;
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var queryJson = {};
                var keyword = $('#txt_Keyword').val();
                queryJson.F_Category = categoryId;
                queryJson.F_Title = keyword;
                $('#select_grid').jfGridSet('reload', { queryJson: JSON.stringify(queryJson) });
            });
        },
        search: function (param) {
            param = param || {};
            param.F_Category = categoryId;
            $('#select_grid').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        },
        initData: function () {
            if (currentModule) {
                var schemeObj = JSON.parse(currentModule.F_Scheme);
                keyValue = currentModule.F_Id;
                currentModule.prop = schemeObj.prop;
                $('#form1').lrSetFormData(currentModule);

                $.each(schemeObj.list, function (_index, _item) {
                    var $item = $('<div class="left-list-item  tab-item"><div class="btn-list" ><span>上移</span><span>下移</span><span>编辑</span><span>删除</span></div><span class="itemname" >' + _item.name + '</span></div>');
                    $item[0].data = _item;
                    $('#add_item').before($item);
                });
            }
        }
    };

    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form1').lrValidform()) {
            return false;
        }
        var formData = $('#form1').lrGetFormData(keyValue);

        var list = [];

        $('.tab-item').each(function () {
            var point = $(this)[0].data;
            list.push(point);
        });
        if (list.length == 0) {
            learun.alert.warning('请设置tab标签项！');
            return false;
        }

        var postData = {
            F_Name: '模块3',
            F_Type: 9,
            F_Scheme: JSON.stringify({ list: list, prop: formData.prop, type: "3" }),
            F_Sort: sort
        };

        $.lrSaveForm(top.$.rootUrl + '/LR_PortalSite/HomeConfig/SaveForm?keyValue=' + keyValue, postData, function (res) {
            postData.F_Id = res.data;
            callBack && callBack(postData);
        });
    };
    page.init();
}
