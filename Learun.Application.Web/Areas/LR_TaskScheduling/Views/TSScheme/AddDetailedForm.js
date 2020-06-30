/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-10-19 16:07
 * 描  述：任务计划模板
 */
var acceptClick;
var rowid = request('rowid');
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 执行日
            $('#carryDate').lrselect({
                type: 'multiple'
            });
            // 执行月
            $('#carryMounth').lrselect({
                value: "id",
                text: "text",
                type: 'multiple',
                data: [
                    { id: 1, text: '一月' },
                    { id: 2, text: '二月' },
                    { id: 3, text: '三月' },
                    { id: 4, text: '四月' },
                    { id: 5, text: '五月' },
                    { id: 6, text: '六月' },
                    { id: 7, text: '七月' },
                    { id: 8, text: '八月' },
                    { id: 9, text: '九月' },
                    { id: 10, text: '十月' },
                    { id: 11, text: '十一' },
                    { id: 12, text: '十二' }
                ]
            });
            // 类型
            $('#type').lrselect({
                placeholder: false,
                select: function (item) {
                    if (item) {
                        switch (item.id) {
                            case 'day':
                                $('#carryDate').hide();
                                break;
                            case 'week':
                                $('#carryDate').show();
                                $('#carryDate').lrselectRefresh({
                                    data: [{ id: '1', text: '周一' },
                                        { id: '2', text: '周二' },
                                        { id: '3', text: '周三' },
                                        { id: '4', text: '周四' },
                                        { id: '5', text: '周五' },
                                        { id: '6', text: '周六' },
                                        { id: '7', text: '周日' }]
                                });
                                break;
                            case 'month':
                                $('#carryDate').show();
                                $('#carryDate').lrselectRefresh({
                                    data: [{ id: '1', text: '1号' },
                                        { id: '2', text: '2号' },
                                        { id: '3', text: '3号' },
                                        { id: '4', text: '4号' },
                                        { id: '5', text: '5号' },
                                        { id: '6', text: '6号' },
                                        { id: '7', text: '7号' },
                                        { id: '8', text: '8号' },
                                        { id: '9', text: '9号' },
                                        { id: '10', text: '10号' },
                                        { id: '11', text: '11号' },
                                        { id: '12', text: '12号' },
                                        { id: '13', text: '13号' },
                                        { id: '14', text: '14号' },
                                        { id: '15', text: '15号' },
                                        { id: '16', text: '16号' },
                                        { id: '17', text: '17号' },
                                        { id: '18', text: '18号' },
                                        { id: '19', text: '19号' },
                                        { id: '20', text: '20号' },
                                        { id: '21', text: '21号' },
                                        { id: '22', text: '22号' },
                                        { id: '23', text: '23号' },
                                        { id: '24', text: '24号' },
                                        { id: '25', text: '25号' },
                                        { id: '26', text: '26号' },
                                        { id: '27', text: '27号' },
                                        { id: '28', text: '28号' },
                                        { id: '29', text: '29号' },
                                        { id: '30', text: '30号' },
                                        { id: '31', text: '31号' }
                                    ]
                                });
                                break;
                        }
                    }
                }
            }).lrselectSet('day');
        },
        initData: function () {
            if (rowid != "") {
                var _data = top.layer_form.frequencyList[rowid];
                $('#form').lrSetFormData(_data);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();

        if (parseInt(formData.hour) > 23 || parseInt(formData.hour) < 0) {
            learun.alert.warning('执行时间小时必须在0-23内');
            return false;
        }
        if (parseInt(formData.minute) > 59 || parseInt(formData.minute) < 0) {
            learun.alert.warning('执行时间小时必须在0-59内');
            return false;
        }
        callBack && callBack(formData, rowid);
        return true;
    };
    page.init();
}
