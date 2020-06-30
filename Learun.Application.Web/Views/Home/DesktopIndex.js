/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.04.28
 * 描 述：热门功能
 */
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            // 自定义表单
            $('#formDesign').on('click', function () {//表单设计
                top.learun.frameTab.open({ F_ModuleId: 'a57993fa-5a94-44a8-a330-89196515c1d9', F_FullName: '表单设计', F_UrlAddress: '/LR_FormModule/Custmerform/Index' });
            });
            // 代码生成器
            $('#code').on('click', function () {//代码生成器
                top.learun.frameTab.open({ F_ModuleId: '1f98c46c-72f1-4abb-b750-af1620d33c79', F_FullName: '代码生成器', F_UrlAddress: '/LR_CodeGeneratorModule/TemplatePC/Index' });
            });
            // BI大屏，看板设计
            $('#displayBoard').on('click', function () {//代码生成器
                top.learun.frameTab.open({ F_ModuleId: 'bef57647-25d1-47f7-98e8-f007dc5f3451', F_FullName: '看板设计', F_UrlAddress: '/LR_DisplayBoard/LR_KBKanBanInfo/Index' });
            });
            // 流程设计
            $('#workflow').on('click', function () {//代码生成器
                top.learun.frameTab.open({ F_ModuleId: '1e89a1bf-32fc-4cc9-a7e7-42ae3f066e3f', F_FullName: '流程设计', F_UrlAddress: '/LR_NewWorkFlow/NWFScheme/Index' });
            });
            // 报表展示
            $('#arReport').on('click', function () {//报表展示
                top.learun.frameTab.open({ F_ModuleId: 'd3b56ca9-64bd-4907-b320-c4b6ac7a26b9', F_FullName: '报表应用', F_UrlAddress: '/LR_ReportModule/RptManage/Index' });
            });
            // 甘特图
            $('#gantt').on('click', function () {//甘特图
                top.learun.frameTab.open({ F_ModuleId: 'bb7d6b50-6bd3-4f90-8151-3e3fc0c9b5a1', F_FullName: '甘特图', F_UrlAddress: '/LR_CodeDemo/GanttDemo/Index4' });
            });



            // 任务调度
            $('#taskScheduling').on('click', function () {//甘特图
                top.learun.frameTab.open({ F_ModuleId: '3d8a1b4d-e650-4a5a-9ca3-31361755cfe2', F_FullName: '任务调度', F_UrlAddress: '/LR_TaskScheduling/TSScheme/Index' });
            });
            // 首页展示
            $('#desktopShow').on('click', function () {//首页展示
                //top.learun.frameTab.open({ F_ModuleId: '4efd37bf-e3ef-4ced-8248-58eba046d78b', F_FullName: '数据字典', F_UrlAddress: '/LR_SystemModule/DataItem/Index' });
                top.learun.frameTab.open({ F_ModuleId: 'AdminDesktopTemp1', F_Icon: 'fa fa-desktop', F_FullName: '首页', F_UrlAddress: '/Home/AdminDesktopTemp' }, true);
            });
            // 数据源
            $('#dataSource').on('click', function () {//数据源
                top.learun.frameTab.open({ F_ModuleId: 'd967ce5c-1bdf-4bbf-967b-876abc3ea245', F_FullName: '数据源', F_UrlAddress: '/LR_SystemModule/DataSource/Index' });
            });
            // 多语言
            $('#lg').on('click', function () {//数据源
                top.learun.frameTab.open({ F_ModuleId: '5ed58504-62f7-4884-9108-f96d732fc84e', F_FullName: '多语言', F_UrlAddress: '/LR_LGManager/LGMap/Index' });
            });
            // excel配置
            $('#excel').on('click', function () {//excel配置
                top.learun.frameTab.open({ F_ModuleId: '23801918-10c6-4fe3-9f1c-554548834f16', F_FullName: 'excel配置', F_UrlAddress: '/LR_SystemModule/ExcelImport/Index' });
            });
            // 代码模板
            $('#codeSchema').on('click', function () {//代码模板
                top.learun.frameTab.open({ F_ModuleId: '634df40d-53fd-4bb4-b15f-ef5a6aca6f44', F_FullName: '代码模板', F_UrlAddress: '/LR_CodeGeneratorModule/CodeSchema/Index' });
            });
            // 首页配置
            $('#desktop').on('click', function () {//首页配置
                top.learun.frameTab.open({ F_ModuleId: 'f68d920f-dd86-4020-945f-f86b47b4f5d8', F_FullName: '首页配置', F_UrlAddress: '/LR_Desktop/DTSetting/PcIndex' });
            });
            // 数据可视化示例
            $('#imgDemo').on('click', function () {//数据可视化示例
                top.learun.frameTab.open({ F_ModuleId: '3bb29a7f-ac7d-4e81-aba3-8d8ccb6ee02e', F_FullName: '数据可视化', F_UrlAddress: '/LR_CodeDemo/ImgShow/Demo3' });
            });
        }
    };
    page.init();
}