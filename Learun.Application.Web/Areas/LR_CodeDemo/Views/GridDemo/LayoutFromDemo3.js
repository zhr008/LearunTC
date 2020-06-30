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
            $('#id1').lrselect({
                data: [
                    {id:'1',text:'通用'}
                ],
                select: function (item) {
                    if (item) {
                        switch (item.id) {
                            case '1': // 通用
                                $('#gridtable').jfGridSet('refreshdata', [{ name: '商标' }, { name: '款号' }]);
                                break;
                        }

                       
                    }
                    else {
                        $('#gridtable').jfGridSet('refreshdata', []);
                    }

                    console.log(item);
                }
            });

            $('#id2').lrselect({
                data: [
                    { id: '1', text: '数据1' },
                    { id: '2', text: '数据2' },
                    { id: '3', text: '数据3' }
                ],
                type: 'multiple'

            });

            $('#gridtable').jfGrid({
                headData: [
                    { label: '属性', name: 'name', width: 100, align: 'left' }
                ],
                onSelectRow: function (row, isCheck) {
                    // 选中后触发事件
                    if (isCheck) {
                        
                    } else {
                       
                    }
                },
                isMultiselect: true,
                mainId: 'name'
            });

            $('#gridtable2').jfGrid({
                headData: [
                    { label: '属性', name: 'name', width: 100, align: 'left' },
                    { label: '方法编码', name: 'code', width: 100, align: 'left' },
                    {
                        label: '预处理方法', name: 'menth', width: 100, align: 'left',
                        formatter: function (cellvalue, row, op, $cell) {

                            $cell.on('click', function () {
                                learun.layerForm({
                                    title: '添加预处理方法',
                                    url: top.$.rootUrl + '/LR_CodeDemo/GridDemo/LayoutFromDemo4',
                                    width: 400,
                                    height: 200,
                                    callBack: function (id) {
                                        return top[id].acceptClick(function (data) {
                                            console.log(data);
                                        });
                                    }
                                });
                            });

                            $cell.css('text-align','right');
                            return '<i class="fa fa-ellipsis-h" ></i>';
                        }
                    },
                    {
                        label: '操作', name: 'F_Id', width: 200, align: 'left',
                        formatter: function (value, row, op, $cell) {
                            var $div = $('<div></div>');
                            for (var i = 0, l = 5; i < l; i++) {
                                var $hbtn = $('<span class=\"label label-info\" style=\"cursor: pointer;margin-right:8px;\">按钮' + (i + 1) + '</span>');
                                $hbtn.on('click', function () {
                                    var name = $(this).text();
                                    alert(name);
                                });
                                $div.append($hbtn);
                            }


                            return $div;
                        }
                    }
                ],
                mainId: 'name',
                isSubGrid: true,             // 是否有子表
                subGridExpanded: function (subid, rowdata) {
                    $('#' + subid).jfGrid({
                        headData: [
                            { label: "项目值", name: "f_value", width: 300, align: "left" },
                            { label: "备注", name: "f_des", width: 80, align: "center" }
                        ]
                    });


                    $('#' + subid).jfGridSet('refreshdata', [{ f_value: '项目值1', name: '测试' }]);
                }// 子表展开后调用函数
            });

            $('#gridtable2').jfGridSet('refreshdata', [{ name: '测试数据', code: 'iso 90001', menth: '' }, { name: '测试数据2', code: 'iso 90002', menth: '' }]);
            

        }
    };

    // 保存数据
    acceptClick = function (callBack) {
        callBack([{ F_Code: '0001', F_Name: '样品名称' }, { F_Code: '0002', F_Name: '样品名称2' }], dfopid);
        return true;
    };

    page.init();
}

