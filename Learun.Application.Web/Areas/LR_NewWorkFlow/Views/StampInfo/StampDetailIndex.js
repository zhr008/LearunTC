/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：
 * 日 期：2018.11.15
 * 描 述：印章列表	
 */
var keyword;
var acceptClick;
var path = '';
var bootstrap = function ($, learun) {
    "use strict";
    // 保存数据
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 订单产品信息
            $('#datagird').jfGrid({
                headData: [
                    { label: '名称', name: 'F_StampName', width: 150, align: "center" },
                    {
                        label: '印章', name: 'F_ImgFile', width: 110, align: "left",
                        formatter: function (value, row, op, $cell) {
                            return '<img src="' + top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/GetImg?keyValue=' + row.F_StampId + '"  style="position: absolute;height:100px;width:100px;top:5px;left:5px;" >';
                        }
                    }
                ],
                mainId: 'F_StampId',
                rowHeight: 110,
            });
            //查询
            $('#btn_Search').on('click', function () {
                keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
        },
        initData: function () {
            $.lrSetForm(top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/GetList?keyword=' + '', function (data) {
                $('.lr-layout-wrap').lrSetFormData(data.data);
                $('#datagird').jfGridSet('refreshdata', data);
            });
        },
        search: function (param) {
            param = param || {};
            $.lrSetForm(top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/GetList?keyword=' + keyword, function (data) {
                $('.lr-layout-wrap').lrSetFormData(data.data);
                $('#datagird').jfGridSet('refreshdata', data);

            });
        }
    };
    acceptClick = function (callBack) {
        var keyValue = $("#datagird").jfGridValue("F_StampId");
        if (!$('.lr-item').lrValidform()) {
            return false;
        }
        var postData = $('.lr-item').lrGetFormData();
        var F_Password = $.md5(postData.F_Password);
        learun.postForm(top.$.rootUrl + '/LR_NewWorkFlow/StampInfo/EqualForm', { keyValue: keyValue, Password: F_Password }, function (res) {
            console.log(keyValue);
            callBack(keyValue);
        });
    };
    page.init();
}