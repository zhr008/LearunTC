/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2019-03-14 15:17
 * 描  述：报表文件管理
 */

$(function () {
    var reportId = request('reportId');
    reportId = decodeURI(reportId);
    var viewer = GrapeCity.ActiveReports.Viewer(
        {
            element: '#viewerContainer',
            report: {
                id: "Reports/" + reportId
            },
            reportService: {
                url: top.$.rootUrl + '/WebService1.asmx'
            },
            uiType: 'desktop'
        });
});