/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-01-03 09:35
 * 描  述：设置页面内容
 */
var type = request('type');
var bootstrap = function ($, learun) {
    "use strict";
    var categoryId = '';
    var currentList = [];
    var currentMap = {};
    var allList = learun.frameTab.currentIframe().allList;

    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
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
                        label: "分类", name: "F_Category", width: 150, align: "center",
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
                        label: '发布时间', name: 'F_PushDate', width: 100, align: "center",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    }
                ],
                mainId: 'F_Id',
                isPage: true,
                isMultiselect: true,
                multiselectfield:'isCheck',
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
                        label: "分类", name: "F_Category", width: 150, align: "center",
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
                        label: '发布时间', name: 'F_PushDate', width: 100, align: "center",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    }
                ],
                mainId: 'F_Id'
            });

            var $list = $('#left_list');
            $list.on('click', '.left-list-item', function () {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    $this.parent().find('.active').removeClass('active');
                    $this.addClass('active');

                    $('#txt_Keyword').val('');

                    var _data = $this[0].data;
                    currentList = _data.list;
                    currentMap = {};
                    $.each(currentList, function (_index, _item) {
                        currentMap[_item.F_Id] = true;
                    });

                    // 加载已经选择文章数据
                    $('#selected_grid').jfGridSet('refreshdata', currentList);

                    page.search();
                   
                }
            });
            $.each(allList, function (_index, _item) {
                var $item = $('<div class="left-list-item" >' + _item.name + '</div>');
                $item[0].data = _item;
                $list.append($item);

                if (_index == 0) {
                    $item.trigger('click');
                }
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
        }
    };
    page.init();
}
