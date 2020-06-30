$(function () {
    $.jgrid.defaults.styleUI = 'Bootstrap';
    $("#table_list_1").jqGrid({
        url: '/LR_CodeDemo/Product/GetGridList',
        datatype: "json",
        viewrecords: true,
        colNames: ['商品编码', '商品名称', '条码', '产地', '规格', '型号', '批次号', '单位', '数量', '单价', '金额', '创建时间'],
        colModel: [
            { name: 'F_Code', index: 'F_Code', width: 120, sorttype: 'string', searchoptions: { sopt: ['eq', 'bw', 'bn', 'cn', 'nc', 'ew', 'en'] } },
            //合并行
            {
                name: 'F_Name', index: 'F_Name', width: 120, sorttype: 'string', searchoptions: { sopt: ['eq', 'bw', 'bn', 'cn', 'nc', 'ew', 'en'] },
                key: true,
                cellattr: function (rowId, value, rowObject, colModel, arrData) {
                    return ' colspan=2';
                },
                formatter: function (value, options, rData) {
                    return value + " - " + rData['F_BarCode'];
                }
            },
            {
                name: 'F_BarCode', index: 'F_BarCode', width: 120, sorttype: 'string', searchoptions: { sopt: ['eq', 'bw', 'bn', 'cn', 'nc', 'ew', 'en'] },
                cellattr: function (rowId, value, rowObject, colModel, arrData) {
                    return " style=display:none; ";
                }
            },
            { name: 'F_Place', index: 'F_Place', width: 120, sorttype: 'string', searchoptions: { sopt: ['eq', 'bw', 'bn', 'cn', 'nc', 'ew', 'en'] } },
            { name: 'F_Specification', index: 'F_Specification', width: 120, sorttype: 'string', searchoptions: { sopt: ['eq', 'bw', 'bn', 'cn', 'nc', 'ew', 'en'] } },
            { name: 'F_Type', index: 'F_Type', width: 120, sorttype: 'string', searchoptions: { sopt: ['eq', 'bw', 'bn', 'cn', 'nc', 'ew', 'en'] } },
            { name: 'F_Number', index: 'F_Number', width: 120, sorttype: 'string', searchoptions: { sopt: ['eq', 'bw', 'bn', 'cn', 'nc', 'ew', 'en'] } },
            { name: 'F_Unit', index: 'F_Unit', width: 120, sorttype: 'string', searchoptions: { sopt: ['eq', 'bw', 'bn', 'cn', 'nc', 'ew', 'en'] } },
            //合并列
            {
                name: 'F_Count', index: 'F_Count', width: 120, sorttype: 'integer', searchoptions: { sopt: ['eq', 'ne', 'le', 'lt', 'gt', 'ge'] },
                cellattr: function (rowId, tv, rawObject, cm, rdata) {
                    return 'id=\'F_Count' + rowId + "\'";
                }
            },
            {
                name: 'F_Price', index: 'F_Price', width: 120, sorttype: 'integer', searchoptions: { sopt: ['eq', 'ne', 'le', 'lt', 'gt', 'ge'] },
                cellattr: function (rowId, tv, rawObject, cm, rdata) {
                    return 'id=\'F_Price' + rowId + "\'";
                }
            },
            { name: 'F_Amount', index: 'F_Amount', width: 120, sorttype: 'integer', searchoptions: { sopt: ['eq', 'ne', 'le', 'lt', 'gt', 'ge'] } },
            { name: 'F_CreateDate', index: 'F_CreateDate', width: 120 }
        ],
        rowNum: 5,
        height: 200,
        autowidth: true,
        rowList: [5, 10, 20],
        pager: '#pager_list_1',
        sortname: 'F_Id',
        sortorder: 'desc',
        viewrecords: true,
        rownumbers: true,
        gridview: true,
        loadonce: true,
        //合并列
        gridComplete: function () {
            var gridName = "table_list_1";
            Merger(gridName, 'F_Count');
            Merger(gridName, 'F_Price');
        },
        caption: "列头查询",
        hidegrid: false
    });
    jQuery("#table_list_1").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false, searchOperators: true });
    function Merger(gridName, CellName) {
        var mya = $("#" + gridName + "").getDataIDs();
        var length = mya.length;
        for (var i = 0; i < length; i++) {
            var before = $("#" + gridName + "").jqGrid('getRowData', mya[i]);
            var rowSpanTaxCount = 1;
            for (j = i + 1; j <= length; j++) {
                var end = $("#" + gridName + "").jqGrid('getRowData', mya[j]);
                if (before[CellName] == end[CellName]) {
                    rowSpanTaxCount++;
                    $("#" + gridName + "").setCell(mya[j], CellName, '', { display: 'none' });
                } else {
                    rowSpanTaxCount = 1;
                    break;
                }
                $("#" + CellName + "" + mya[i] + "").attr("rowspan", rowSpanTaxCount);
            }
        }
    }

    $("#table_list_2").jqGrid({
        url: '/LR_CodeDemo/Product/GetGridList',
        datatype: "json",
        viewrecords: true,
        colNames: ['商品编码', '商品名称', '条码', '产地', '规格', '型号', '批次号', '单位', '数量', '单价', '金额', '创建时间'],
        colModel: [
            { name: 'F_Code', index: 'F_Code', width: 120, sorttype: 'string' },
            { name: 'F_Name', index: 'F_Name', width: 120, sorttype: 'string' },
            { name: 'F_BarCode', index: 'F_BarCode', width: 120, sorttype: 'string' },
            { name: 'F_Place', index: 'F_Place', width: 120, sorttype: 'string' },
            { name: 'F_Specification', index: 'F_Specification', width: 120, sorttype: 'string' },
            { name: 'F_Type', index: 'F_Type', width: 120, sorttype: 'string' },
            { name: 'F_Number', index: 'F_Number', width: 120, sorttype: 'string' },
            { name: 'F_Unit', index: 'F_Unit', width: 120, sorttype: 'string' },
            { name: 'F_Count', index: 'F_Count', width: 120, sorttype: 'int', formatter: "number", summaryType: 'sum' },
            { name: 'F_Price', index: 'F_Price', width: 120, sorttype: 'int', formatter: "number", summaryType: 'sum' },
            { name: 'F_Amount', index: 'F_Amount', width: 120, sorttype: 'int', formatter: "number", summaryType: 'sum' },
            { name: 'F_CreateDate', index: 'F_CreateDate', width: 120 }
        ],
        rowNum: 5,
        height: 200,
        autowidth: true,
        rowList: [5, 10, 20],
        pager: '#pager_list_2',
        sortname: 'F_Id',
        sortorder: 'desc',
        viewrecords: true,
        rownumbers: true,
        gridview: true,
        loadonce: true,
        //分组统计
        grouping: true,
        groupingView: {
            groupField: ['F_Specification'],
            groupSummary: [true],
            groupColumnShow: [true],
            groupText: ['<b>{0}</b>'],
            groupCollapse: false,
            groupOrder: ['desc']
        },
        caption: "列头查询",
        hidegrid: false
    });

    // Add selection
    //$("#table_list_2").setSelection(4, true);

    // Setup buttons
    //$("#table_list_2").jqGrid('navGrid', '#pager_list_2', {
    //    edit: true,
    //    add: true,
    //    del: true,
    //    search: true
    //}, {
    //        height: 200,
    //        reloadAfterSubmit: true
    //    });

    // Add responsive to jqGrid
    $(window).bind('resize', function () {
        var width = $('.jqGrid_wrapper').width();
        $('#table_list_1').setGridWidth(width);
        $('#table_list_2').setGridWidth(width);
    });
});