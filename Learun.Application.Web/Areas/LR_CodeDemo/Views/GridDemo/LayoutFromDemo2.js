var dfopid = request('dfopid');
var selectValue = request('selectValue');

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var citem = {};
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            // 部门
            $('#F_DepartmentId').lrDepartmentSelect();

            $('#tree').lrtree({
                data: [{
                    id:'1',
                    text:'委托方',
                    value: '1',
                    showcheck: true,
                    checkstate: 0,
                    hasChildren: false,
                    isexpand: false,
                    complete: true
                }],
                nodeCheck: function (item) {
                    citem = item;
                }
            });
        }
    };

    // 保存数据
    acceptClick = function (callBack) {
        callBack(citem, dfopid);
        return true;
    };

    page.init();
}

