var bootstrap = function ($, learun) {
    "use strict";

    var yplist = [];

    var page = {
        init: function () {
            $('#id1').lrformselect({
                layerUrl: top.$.rootUrl + '/LR_CodeDemo/GridDemo/LayoutFromDemo1',
                layerUrlW: 500,
                layerUrlH: 400,
                dataUrl: '',
                select: function (item) {

                }
            });
            $('#id2').lrformselect({
                layerUrl: top.$.rootUrl + '/LR_CodeDemo/GridDemo/LayoutFromDemo2',
                layerUrlW: 600,
                layerUrlH: 400,
                dataUrl: '',
                select: function (item) {

                }
            });

            $('#id3').lrselect({
                data: [{ id: '1', text: '测试人员', code: '000001' }, { id: '2', text: '测试人员2', code: '000002' }],
                getText: function (item) {
                    return '<div class="selectdiv"  >' + item.code + '</div><div class="selectdiv">' + item.text + '</div>';
                },
                width: 300
            });

            $('#lr_form_tabs').lrFormTab();

            // 部门
            $('#F_DepartmentId').lrDepartmentSelect();
            // 部门
            $('#F_DepartmentId2').lrDepartmentSelect();
            // 性别
            $('#F_Gender').lrselect();

            
            $('#ypgridtable').jfGrid({
                headData: [
                    { label: '样品编号', name: 'F_Code', width: 100, align: 'left' },
                    { label: '样品名称', name: 'F_Name', width: 200, align: 'left' }
                ]
            });

            // 增样
            $('#lr_add').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '增样',
                    url: top.$.rootUrl + '/LR_CodeDemo/GridDemo/LayoutFromDemo3',
                    width: 1000,
                    height: 620,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            console.log(data);
                            $.each(data, function (_index, _item) {
                                yplist.push(_item);
                            });
                            $('#ypgridtable').jfGridSet('refreshdata', yplist);
                        });
                    }
                });
            });


            // 导入数据
            $('#lr_edit').on('click', function () {
                learun.layerForm({
                    id: 'form2',
                    title: '导入数据',
                    url: top.$.rootUrl + '/LR_CodeDemo/GridDemo/LayoutFromDemo5',
                    width: 1000,
                    height: 620,
                    maxmin: true,
                    btn:null
                });
            });
        },
        initGrid: function () {
        }
    };
    page.init();
}


