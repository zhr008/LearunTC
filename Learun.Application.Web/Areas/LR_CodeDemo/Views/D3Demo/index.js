var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            initData();
        }
    };

    function initData() {
        var d = { "code": 200, "info": "响应成功", "data": [{ "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 80.000000, "F_Amount": 180000.000000, "F_Profit": 100000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-20 11:19:41", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 70.000000, "F_Amount": 1600000.000000, "F_Profit": 400000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-20 10:57:21", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 12.000000, "F_Amount": 1900000.000000, "F_Profit": 1000000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2018-12-27 10:15:04", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 80.000000, "F_Amount": 330000.000000, "F_Profit": 60000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-20 11:09:13", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 90.000000, "F_Amount": 110000.000000, "F_Profit": 30000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-20 10:59:29", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 30.000000, "F_Amount": 150000.000000, "F_Profit": 100000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-20 11:06:20", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 60.000000, "F_Amount": 59000.000000, "F_Profit": 11000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-23 11:16:32", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 90.000000, "F_Amount": 65000.000000, "F_Profit": 20000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-23 10:06:22", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 60.000000, "F_Amount": 400000.000000, "F_Profit": 200000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-20 11:02:55", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 30.000000, "F_Amount": 600000.000000, "F_Profit": 300000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-23 09:27:35", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 80.000000, "F_Amount": 36900.000000, "F_Profit": 12000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-23 09:34:47", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 90.000000, "F_Amount": 260000.000000, "F_Profit": 200000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-20 11:12:01", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 90.000000, "F_Amount": 8600.000000, "F_Profit": 8600.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-23 09:48:53", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 90.000000, "F_Amount": 84000.000000, "F_Profit": 20000.0, "F_ChanceTypeId": "产品类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-05-20 11:29:50", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }, { "F_ChanceId": null, "F_EnCode": null, "F_FullName": null, "F_SourceId": null, "F_StageId": null, "F_SuccessRate": 78.000000, "F_Amount": 20000.899800, "F_Profit": 1500000.0, "F_ChanceTypeId": "服务类", "F_SaleCost": null, "F_DealDate": null, "F_IsToCustom": null, "F_CompanyName": null, "F_CompanyNatureId": null, "F_CompanyAddress": null, "F_CompanySite": null, "F_CompanyDesc": null, "F_Province": null, "F_City": null, "F_Contacts": null, "F_Mobile": null, "F_Tel": null, "F_Fax": null, "F_QQ": null, "F_Email": null, "F_Wechat": null, "F_Hobby": null, "F_TraceUserId": null, "F_TraceUserName": null, "F_ChanceState": null, "F_AlertDateTime": null, "F_AlertState": null, "F_SortCode": null, "F_DeleteMark": null, "F_EnabledMark": null, "F_Description": null, "F_CreateDate": "2016-03-22 20:46:03", "F_CreateUserId": null, "F_CreateUserName": null, "F_ModifyDate": null, "F_ModifyUserId": null, "F_ModifyUserName": null }] }
        dataFormate(d.data, 'F_Profit');
    }

    function dataFormate(data, value) {
        var datas = [];
        for (var i = 0; i < data.length; i++) {
            datas.push(data[i][value]);
        }
        drawBar(datas);
        drawForce();
    }

    function drawBar(dataset) {
        //画布大小
        var width = 800;
        var height = 500;

        //在 body 里添加一个 SVG 画布
        var svg = d3.select("div#bars")
            .append("svg")
            .attr("width", width)
            .attr("height", height);

        //画布周边的空白
        var padding = { left: 50, right: 30, top: 50, bottom: 20 };

        //定义一个数组
        //dataset = [10, 20, 30, 40, 33, 24, 12, 5];


        //x轴的比例尺
        var xScale = d3.scale.ordinal()
            .domain(d3.range(dataset.length))
            .rangeRoundBands([0, width - padding.left - padding.right]);

        //y轴的比例尺
        var yScale = d3.scale.linear()
            .domain([0, d3.max(dataset)])
            .range([height - padding.top - padding.bottom, 0]);

        //定义x轴
        var xAxis = d3.svg.axis()
            .scale(xScale)
            .orient("bottom");

        //定义y轴
        var yAxis = d3.svg.axis()
            .scale(yScale)
            .orient("left");

        //矩形之间的空白
        var rectPadding = 4;

        //添加矩形元素
        var rects = svg.selectAll(".MyRect")
            .data(dataset)
            .enter()
            .append("rect")
            .attr("class", "MyRect")
            .attr("transform", "translate(" + padding.left + "," + padding.top + ")")
            .attr("x", function (d, i) {
                return xScale(i) + rectPadding / 2;
            })
            .attr("width", xScale.rangeBand() - rectPadding)
            .attr("y", function (d) {
                var min = yScale.domain()[0];
                return yScale(min);
            })
            .attr("height", function (d) {
                return 0;
            })
            .transition()
            .delay(function (d, i) {
                return i * 200;
            })
            .duration(2000)
            .ease("bounce")
            .attr("y", function (d) {
                return yScale(d);
            })
            .attr("height", function (d) {
                return height - padding.top - padding.bottom - yScale(d);
            });

        //添加文字元素
        var texts = svg.selectAll(".MyText")
            .data(dataset)
            .enter()
            .append("text")
            .attr("class", "MyText")
            .attr("transform", "translate(" + padding.left + "," + padding.top + ")")
            .attr("x", function (d, i) {
                return xScale(i) + rectPadding / 2;
            })
            .attr("dx", function () {
                return (xScale.rangeBand() - rectPadding) / 2;
            })
            .attr("dy", function (d) {
                return 20;
            })
            .text(function (d) {
                return d;
            })
            .attr("y", function (d) {
                var min = yScale.domain()[0];
                return yScale(min);
            })
            .transition()
            .delay(function (d, i) {
                return i * 200;
            })
            .duration(2000)
            .ease("bounce")
            .attr("y", function (d) {
                return yScale(d);
            });

        //添加x轴
        svg.append("g")
            .attr("class", "axis")
            .attr("transform", "translate(" + padding.left + "," + (height - padding.bottom) + ")")
            .call(xAxis);

        //添加y轴
        svg.append("g")
            .attr("class", "axis")
            .attr("transform", "translate(" + padding.left + "," + padding.top + ")")
            .call(yAxis);
    }

    function drawForce() {
        var nodes = [{ name: "桂林" }, { name: "广州" },
        { name: "厦门" }, { name: "杭州" },
        { name: "上海" }, { name: "青岛" },
        { name: "天津" }];

        var edges = [{ source: 0, target: 1 }, { source: 0, target: 2 },
        { source: 0, target: 3 }, { source: 1, target: 4 },
        { source: 1, target: 5 }, { source: 1, target: 6 }];

        var width = 800;
        var height = 500;

        var svg = d3.select("div#force")
            .append("svg")
            .attr("width", width)
            .attr("height", height);

        var force = d3.layout.force()
            .nodes(nodes)		//指定节点数组
            .links(edges)		//指定连线数组
            .size([width, height])	//指定范围
            .linkDistance(150)	//指定连线长度
            .charge(-400);	//相互之间的作用力

        force.start();	//开始作用

        console.log(nodes);
        console.log(edges);

        //添加连线		
        var svg_edges = svg.selectAll("line")
            .data(edges)
            .enter()
            .append("line")
            .style("stroke", "#ccc")
            .style("stroke-width", 1);

        var color = d3.scale.category20();

        //添加节点			
        var svg_nodes = svg.selectAll("circle")
            .data(nodes)
            .enter()
            .append("circle")
            .attr("r", 20)
            .style("fill", function (d, i) {
                return color(i);
            })
            .call(force.drag);	//使得节点能够拖动

        //添加描述节点的文字
        var svg_texts = svg.selectAll("text")
            .data(nodes)
            .enter()
            .append("text")
            .style("fill", "black")
            .attr("dx", 20)
            .attr("dy", 8)
            .text(function (d) {
                return d.name;
            });


        force.on("tick", function () {	//对于每一个时间间隔

            //更新连线坐标
            svg_edges.attr("x1", function (d) { return d.source.x; })
                .attr("y1", function (d) { return d.source.y; })
                .attr("x2", function (d) { return d.target.x; })
                .attr("y2", function (d) { return d.target.y; });

            //更新节点坐标
            svg_nodes.attr("cx", function (d) { return d.x; })
                .attr("cy", function (d) { return d.y; });

            //更新文字坐标
            svg_texts.attr("x", function (d) { return d.x; })
                .attr("y", function (d) { return d.y; });
        });
    }

    page.init();
}