
$(function () {



    "use strict";
    var learun = top.learun;
    var data = [];
    var isupdate = false;

    // 生产计划
    // 初始化数据
    for (var i = 0; i < 10; i++) {
        var day = new Date();
        day = day.DateAdd('d', i * 2);
        var ponit = {
            id: top.learun.newGuid(),
            text: '计划任务' + (i + 1),
            isexpand: false,
            complete: true,
            timeList: [{
                beginTime: learun.formatDate(day, 'yyyy-MM-dd'),
                endTime: learun.formatDate(day.DateAdd('d', 8), 'yyyy-MM-dd'),
                color: '#3286ed',
                overtime: true,
                text: '执行时间9天'
            }],
            hasChildren: true,
            children: [{
                id: learun.newGuid(),
                text: '计划任务' + (i + 1) + '.1',
                isexpand: false,
                complete: true,
                timeList: [{
                    beginTime: learun.formatDate(day, 'yyyy-MM-dd'),
                    endTime: learun.formatDate(day.DateAdd('d', 3), 'yyyy-MM-dd'),
                    color: '#1bb99a',
                    overtime: true,
                    text: '执行时间4天'
                }]
            },
            {
                id: learun.newGuid(),
                text: '计划任务' + (i + 1) + '.2',
                isexpand: false,
                complete: true,
                timeList: [{
                    beginTime: learun.formatDate(day.DateAdd('d', 4), 'yyyy-MM-dd'),
                    endTime: learun.formatDate(day.DateAdd('d', 8), 'yyyy-MM-dd'),
                    color: '#E4474D',
                    overtime: true,
                    text: '执行时间5天'
                }]
            }]
        }
        data.push(ponit);
    }

    $('#gridtable').lrGantt({
        data: data,
        timebtns: ['month', 'week', 'day'],//'month', 'week', 'day', 'hour'
    });

    //######流程任务
    $('#flowmore').on('click', function () {
        top.learun.frameTab.open({ F_ModuleId: 'lr_iframe_56ce34c2-882e-47d1-b12d-5036e3b79fcf', F_FullName: '流程任务', F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/Index' });
    });

    // 加载代办任务列表
    var pagination = { "rows": 5, "page": 1, "sidx": "F_CreateDate DESC", "sord": "ASC", "records": 0, "total": 0 }
    top.learun.httpAsync('GET', top.$.rootUrl + '/LR_NewWorkFlow/NWFProcess/GetTaskPageList', {
        queryJson: "{}", categoryId: 2, pagination: JSON.stringify(pagination)
    }, function (data) {
        if (data) {
            var $list = $('#flowlist');
            $list.html("");
            $.each(data.rows, function (_index, _item) {
                var title = "";
                if (_item.F_SchemeName != _item.F_Title && _item.F_Title) {
                    title = _item.F_SchemeName + "(" + _item.F_Title + ")";
                }
                else {
                    title = _item.F_SchemeName;
                }
                var html = '<div class="lr-msg-line flowitem">\
                        <a href = "#" style="text-decoration:none;">[审批]&nbsp;&nbsp;&nbsp;'+ _item.F_CreateUserName + '的' + title + '</a>\
                            <label>'+ top.learun.formatDate(_item.F_CreateDate, 'yyyy-MM-dd') + '</label>\
                        </div>';

                var $item = $(html);
                $item[0].item = _item;
                $list.append($item);
            });
            $list.find('.flowitem').on('click', function () {
                var item = $(this)[0].item;

                var processId = item.F_Id;
                var taskId = item.F_TaskId;
                var title = item.F_Title;
                var schemeName = item.F_SchemeName;
                var taskName = item.F_TaskName;
                var taskType = item.F_TaskType;

                if (schemeName != title && title) {
                    title = schemeName + "(" + title + ")";
                }
                else {
                    title = schemeName;
                }

                //1审批2传阅3加签4子流程5重新创建
                switch (taskType) {
                    case 1:// 审批
                        top.learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '审批-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=audit' + "&processId=" + processId + "&taskId=" + taskId });
                        break;
                    case 2:// 传阅
                        top.learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '查阅-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=refer' + "&processId=" + processId + "&taskId=" + taskId });
                        break;
                    case 3:// 加签
                        top.learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '加签审核-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=signAudit' + "&processId=" + processId + "&taskId=" + taskId });
                        break;
                    case 4:// 子流程
                        top.learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '子流程-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=chlid' + "&processId=" + processId + "&taskId=" + taskId });
                        break;
                    case 5:// 重新创建
                        top.learun.frameTab.open({ F_ModuleId: processId, F_Icon: 'fa magic', F_FullName: '重新发起-' + title, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?processId=' + processId + '&tabIframeId=' + processId + '&type=againCreate' });
                        break;
                    case 6:// 重新创建
                        top.learun.frameTab.open({ F_ModuleId: taskId, F_Icon: 'fa magic', F_FullName: '子流程-' + title + '/' + taskName, F_UrlAddress: '/LR_NewWorkFlow/NWFProcess/NWFContainerForm?tabIframeId=' + taskId + '&type=againChild' + "&processId=" + processId + "&taskId=" + taskId });
                        break;
                }
            });
        }
    });
    

    // 基于准备好的dom，初始化echarts实例
    var pieChart = echarts.init(document.getElementById('piecontainer1'));
    // 指定图表的配置项和数据
    var pieoption = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            bottom: 'bottom',
            data: ['枢纽楼', 'IDC中心', '端局', '模块局', '营业厅', '办公大楼', 'C网基站']
        },
        series: [
            {
                name: '用电占比',
                type: 'pie',
                radius: '75%',
                center: ['50%', '50%'],
                label: {
                    normal: {
                        formatter: '{b}:{c}: ({d}%)',
                        textStyle: {
                            fontWeight: 'normal',
                            fontSize: 12,
                            color: '#333'
                        }
                    }
                },
                data: [
                    { value: 10, name: '枢纽楼' },
                    { value: 10, name: 'IDC中心' },
                    { value: 10, name: '端局' },
                    { value: 10, name: '模块局' },
                    { value: 10, name: '营业厅' },
                    { value: 10, name: '办公大楼' },
                    { value: 40, name: 'C网基站' }
                ],
                itemStyle: {
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ]
        ,
        color: ['#df4d4b', '#304552', '#52bbc8', 'rgb(224,134,105)', '#8dd5b4', '#5eb57d', '#d78d2f']
    };
    // 使用刚指定的配置项和数据显示图表。
    //pieChart.setOption(pieoption);


    // 基于准备好的dom，初始化echarts实例
    var lineChart = echarts.init(document.getElementById('linecontainer1'));
    // 指定图表的配置项和数据
    var lineoption = {
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            bottom: 'bottom',
            data: ['预算', '实际']
        },
        grid: {
            bottom: '8%',
            containLabel: true
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
        },
        yAxis: {
            type: 'value'
        },
        series: [
            {
                name: '预算',
                type: 'line',
                stack: '用电量',
                itemStyle: {
                    normal: {
                        color: "#fc0d1b",
                        lineStyle: {
                            color: "#fc0d1b"
                        }
                    }
                },
                data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 23.3, 18.3, 13.9, 9.6, 1]
            },
            {
                name: '实际',
                type: 'line',
                stack: '用电量',
                itemStyle: {
                    normal: {
                        color: '#344858',
                        lineStyle: {
                            color: '#344858'
                        }
                    }

                },
                data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
            }
        ]
    };
    // 使用刚指定的配置项和数据显示图表。
   
    //lineChart.setOption(lineoption);
   
    window.onresize = function (e) {
        e = e || window.event;
        console.log(1);
        if (!isupdate) {
            isupdate = true;
            pieChart.setOption(pieoption);
            lineChart.setOption(lineoption);
        }
        else {
            pieChart.resize(e);
            lineChart.resize(e);
        }
        
    }
    
});