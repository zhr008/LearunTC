/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.11.06
 * 描 述：上下级添加	
 */
var acceptClick;
var auditorName = '';
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('#auditorId').lrselect({// 限制条件1.同一个部门2.同一个公司
                data: [{ id: '1', text: '上一级' }, { id: '2', text: '上二级' }, { id: '3', text: '上三级' }, { id: '4', text: '上四级' }, { id: '5', text: '上五级' }, { id: '6', text: '下一级' }, { id: '7', text: '下二级' }, { id: '8', text: '下三级' }, { id: '9', text: '下四级' }, { id: '10', text: '下五级' }],//
                 select: function (item) {
                     if (item) {
                         auditorName = item.text;
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
        formData.type = '4';//审核者类型1.岗位2.角色3.用户4.上下级5.表单指定字段6.某一个节点执行人
        callBack(formData);
        return true;
    };
    page.init();
}