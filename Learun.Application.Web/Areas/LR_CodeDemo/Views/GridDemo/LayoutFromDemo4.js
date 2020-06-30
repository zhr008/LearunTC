var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var citem = {};
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
        }
    };

    // 保存数据
    acceptClick = function (callBack) {
        callBack({});
        return true;
    };

    page.init();
}

