/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.11.06
 * 描 述：上下级添加	
 */
var layerId = request('layerId');

var acceptClick;
var auditorName = '';
var bootstrap = function ($, learun) {
    "use strict";

    var nodeList = top[layerId].nodeList;



    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('#auditorId').lrselect({
                text: 'name',
                value: 'id',
                data: nodeList,//
                select: function (item) {
                    if (item) {
                        auditorName = item.name;
                    }
                    else {
                        auditorName = '';
                    }
                },
            });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();
        formData.auditorName = auditorName;
        formData.type = '6';//审核者类型1.岗位2.角色3.用户4.上下级5.表单指定字段6.某一个节点执行人
        callBack(formData);
        return true;
    };
    page.init();
}