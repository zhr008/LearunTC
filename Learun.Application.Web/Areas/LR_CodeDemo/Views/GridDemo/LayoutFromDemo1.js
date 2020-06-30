var dfopid = request('dfopid');
var selectValue = request('selectValue');

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";


    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {

            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 新增
            $('#lr_add').on('click', function () {
                selectedRow = null;//新增前请清空已选中行
                learun.layerForm({
                    id: 'form',
                    title: '新增客户',
                    url: top.$.rootUrl + '/LR_CRMModule/Customer/Form',
                    width: 1000,
                    height: 620,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });


            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_CRMModule/Customer/GetPageListJson',
                headData: [
                    { label: '客户编号', name: 'F_EnCode', width: 100, align: 'left' },
                    { label: '客户名称', name: 'F_FullName', width: 200, align: 'left' },
                    {
                        label: '客户级别', name: 'F_CustLevelId', width: 100, align: 'left',
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'Client_Level',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: '客户类别', name: 'F_CustTypeId', width: 100, align: 'left' },
                    { label: '客户程度', name: 'F_CustDegreeId', width: 100, align: 'left' },
                    { label: '公司行业', name: 'F_CustIndustryId', width: 100, align: 'left' },
                    { label: '联系人', name: 'F_Contact', width: 100, align: 'left' },
                    { label: '跟进人员', name: 'F_TraceUserName', width: 100, align: 'left' },
                    {
                        label: "最后更新", name: "F_ModifyDate", width: 140, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm');
                        }
                    },
                    { label: '备注', name: 'F_Description', width: 200, align: 'left' },
                ],
                mainId: 'F_CustomerId',
                reloadSelected: true,
                isPage: true,
                sidx: 'F_CreateDate'
            });

            page.search();
        }
        , search: function (param) {
            $('#gridtable').jfGridSet('reload', param);
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        var selectedRow = $('#gridtable').jfGridGet('rowdata');
        selectedRow.value = selectedRow.F_CustomerId;
        selectedRow.text = selectedRow.F_FullName;

        if (!selectedRow.value) {
            learun.alert.warning("请选择");
            return false;
        }

        callBack(selectedRow, dfopid);
        return true;
    };
    page.init();
}

