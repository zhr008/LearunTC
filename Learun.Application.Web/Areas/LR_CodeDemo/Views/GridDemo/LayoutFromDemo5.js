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

            $('#gridtable1').jfGrid({
                headData: [
                    { label: '编号', name: 'F_Code', width: 100, align: 'left' },
                    { label: '名称', name: 'F_Name', width: 200, align: 'left' }
                ]
            });

            $('#gridtable').jfGrid({
                headData: [
                    { label: "项目值名称", name: "name", width: 260, align: "left" },
                    {
                        label: "测试值", name: "test", width: 150, align: "left", edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "单位", name: "test2", width: 150, align: "left", edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "自定义名称", name: "test3", width: 80, align: "left", edit: {
                            type: 'input'
                        }
                    }
                ],
                isTree: true,
                mainId: 'id',
                parentId: 'pid'
            });

            var data = [
                { id: '1', pid: '0', name: '项目序号', lrnotedit: '1' },
                { id: '2', pid: '1', name: '组名称', lrnotedit: '1' },
                { id: '3', pid: '2', name: '复合次数：0', lrnotedit: '1' },
                { id: '4', pid: '3', name: '项目值排序值：测试结果', lrnotedit: '1' },
                { id: '5', pid: '4', name: '钙', test: '待定', test2: '%', test3:'' }
            ];


            $('#gridtable').jfGridSet('refreshdata',data);
        }
    };

    // 保存数据
    acceptClick = function (callBack) {
        callBack(citem, dfopid);
        return true;
    };

    page.init();
}

