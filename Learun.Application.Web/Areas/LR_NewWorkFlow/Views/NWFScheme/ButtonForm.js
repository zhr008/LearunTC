/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：表单权限添加	
 */
var id = request('id');
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#next').lrselect({// 是否可编辑1.是2.否
                placeholder: false,
                data: [{ 'id': '1', 'text': learun.language.getSyn('不能手动设置') }, { 'id': '2', 'text': learun.language.getSyn('能手动设置') }]
            }).lrselectSet('1');
            $('#isHide').lrselect({// 是否可编辑1.是2.否
                placeholder: false,
                data: [{ 'id': '1', 'text': learun.language.getSyn('是') }, { 'id': '2', 'text': learun.language.getSyn('否') }]
            }).lrselectSet('2');
            $('#isSign').lrselect({// 是否可编辑1.是2.否
                placeholder: false,
                data: [{ 'id': '1', 'text': '需要' }, { 'id': '2', 'text': '不需要' }]
            }).lrselectSet('2');


        },
        initData: function () {
            if (id) {
                var btnList = top.layer_NodeForm.btnList;
                $.each(btnList, function (_index, _item) {
                    if (_item.id == id) {
                        $('#form').lrSetFormData(_item);
                        if (id == '1' || id == '2') {
                            $('#name').attr('readonly', 'readonly');
                            $('#code').attr('readonly', 'readonly');
                        }
                        return false;
                    }
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();
        formData.id = id || learun.newGuid();
        callBack(formData);
        return true;
    };
    page.init();
}