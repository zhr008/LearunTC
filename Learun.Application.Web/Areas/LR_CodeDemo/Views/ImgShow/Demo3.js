var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
            page.loaddata();
        },
        bind: function () {
        },
        loaddata: function () {
            learun.httpAsyncGet(top.$.rootUrl + '/LR_CodeDemo/WorkOrder/GetList',function (data) {
                if (!!data) {
                    console.log('data', data.data[0]['01']);
                    $("#line1-1").html('未完成：' + data.data[0]['1-1']);
                    $("#line1-2").html('已完成：' + data.data[0]['1-2']);
                    $("#line2-1").html('未完成：' + data.data[0]['2-1']);
                    $("#line2-2").html('已完成：' + data.data[0]['2-2']);
                    $("#line3-1").html('未完成：' + data.data[0]['3-1']);
                    $("#line3-2").html('已完成：' + data.data[0]['3-2']);
                    $("#line4-1").html('未完成：' + data.data[0]['4-1']);
                    $("#line4-2").html('已完成：' + data.data[0]['4-2']);
                }
            });
        }
    };
    page.init();
}