/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.12.20
 * 描 述：指派审核人	
 */
var processId = request('processId');
var acceptClick;

var bootstrap = function ($, learun) {
    "use strict";

    var taskMap = {};
    var userMap = {};
    var page = {
        init: function () {
            var userList = [];
            learun.clientdata.getAllAsync('department', {
                callback: function (departmentMap) {
                    learun.clientdata.getAllAsync('user', {
                        callback: function (data) {
                            userMap = data;
                            $.each(userMap, function (_id, _item) {
                                var name = '';
                                if (departmentMap[_item.departmentId]) {
                                    name = '【' + departmentMap[_item.departmentId].name + '】' + _item.name;
                                }
                                else {
                                    name = _item.name;

                                }
                               
                                var point = {
                                    id: _id,
                                    name: name
                                };
                                userList.push(point);
                            });
                        }
                    });
                }
            });
            // 获取当前任务的审核人
            learun.httpAsync('GET', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/GetTaskUserList', { processId: processId }, function (data) {
                if (data) {
                    var $form = $('#form .lr-scroll-box');
                    $.each(data, function (_index, _item) {
                        taskMap[_item.F_Id] = _item;
                        var nameList = [];
                        $.each(_item.nWFUserInfoList, function (_jindex,_jitem) {
                            if (userMap[_jitem.Id]) {
                                nameList.push(userMap[_jitem.Id].name);
                            } 
                        });

                        var _html = '<div class="col-xs-12 lr-form-item">\
                                        <div class="lr-form-item-title">' + _item.F_NodeName + '【当前】</div>\
                                        <input  type="text" class="form-control" value="' + String(nameList) + '"  readonly/>\
                                    </div>\
                                    <div class="col-xs-12 lr-form-item">\
                                        <div class="lr-form-item-title">' + _item.F_NodeName + '【设置】</div>\
                                        <div id="' + _item.F_Id + '" ></div>\
                                    </div>';

                        $form.append(_html);

                        $('#' + _item.F_Id).lrselect({
                            text: 'name',
                            value: 'id',
                            type: 'multiple',
                            data: userList,
                            allowSearch: true
                        });
                    });
                }
            });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        var formData = $('#form').lrGetFormData();
        var postData = [];
        $.each(formData || {}, function (_id, _item) {
            if (_item) {
                var point = taskMap[_id];
                if (point) {
                    var userList = _item.split(',');

                    point.nWFUserInfoList = [];
                    $.each(userList, function (_i, _u) {
                        point.nWFUserInfoList.push({ Id: _u, Name: userMap[_u].name });
                    });

                    postData.push(point);
                }
            }
        });
        if (postData.length > 0) {
            $.lrSaveForm(top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/AppointUser', { strList: JSON.stringify(postData) }, function (res) {
                // 保存成功后才回调
                callBack && callBack();
            });
        }
        else {
            return true;
        }
    };
    page.init();
}