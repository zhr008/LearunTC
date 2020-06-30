/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.12.13
 * 描 述：节点信息展示
 */
var bootstrap = function ($, learun) {
    "use strict";
    var lookNode = top.wflookNode;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#gridtable').jfGrid({
                headData: [
                    {
                        label: "执行人", name: "F_CreateUserName", width: 80, align: "left", frozen: true,
                        formatter: function (cellvalue, row) {
                            if (!cellvalue) {
                                return '系统处理';
                            }
                            return cellvalue;
                        }
                    },
                    {
                        label: "执行动作", name: "F_TaskType", width: 140, align: "left", frozen: true,
                        formatter: function (cellvalue, row) {
                            switch (cellvalue)//0创建1审批2传阅3加签审核4子流程5重新创建6.超时流转
                            {
                                case 0:
                                    return '<span class=\"label label-success \" >' + row.F_OperationName + '</span>';
                                    break;
                                case 1:
                                    return '<span class=\"label label-primary \" >审批-' + row.F_OperationName + '</span>';
                                    break;
                                case 2:
                                    return '<span class=\"label label-primary \" >已阅</span>';
                                    break;
                                case 3:
                                    return '<span class=\"label label-primary \" >' + row.F_OperationName + '</span>';
                                    break;
                                case 4:
                                    return '<span class=\"label label-info \" >发起子流程</span>';
                                    break;
                                case 5:
                                    return '<span class=\"label label-warning \" >重新发起</span>';
                                    break;
                                case 6:
                                    return '<span class=\"label label-danger \" >超时流转</span>';
                                    break;
                                case 7:
                                    return '<span class=\"label label-info \" >' + row.F_OperationName + '</span>';
                                    break;
                                case 8:
                                case 9:
                                    return '<span class=\"label label-info \" >' + row.F_OperationName + '</span>';
                                    break;
                                default:
                                    return '<span class=\"label label-info \" >' + row.F_OperationName + '</span>';
                                    
                            }
                        }
                    },
                    {
                        label: "执行时间", name: "F_CreateDate", width: 130, align: "center", frozen: true,
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    },
                    { label: "内容", name: "F_Des", width: 400, align: "left" }
                ]
            });
        },
        initData: function () {
            if (lookNode) {
                var list = [];
                $.each(lookNode.history, function (_index, _item) {
                    if (_item.namelist) {
                        $('#currentUser').text(_item.namelist).attr('title', _item.namelist);

                    }
                    else if (_item.namelist == undefined || _item.namelist == null) {
                        list.push(_item);
                    }
                });

                $('#gridtable').jfGridSet('refreshdata', list);
            }
        }
    };
    page.init();
}