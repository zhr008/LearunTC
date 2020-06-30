/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：成员添加
 */
var objectId = request('objectId');
var category = request('category');

var companyId = request('companyId');
var departmentId;

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 部门
            $('#department_tree').lrtree({
                nodeClick: function (item) {
                    departmentId = item.id;
                }
            });
            // 公司
            $('#company_select').lrCompanySelect({ isLocal: true }).bind('change', function () {
                companyId = $(this).lrselectGet();
                $('#department_tree').lrtreeSet('refresh', {
                    url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',
                    // 访问数据接口参数
                    param: { companyId: companyId },
                });
            });
        },
        initData: function () {
            if (!!companyId) {
                $('#company_select').lrselectSet(companyId);
            }
        }
    };
    // 保存数据
    acceptClick = function () {
        if (departmentId != null && departmentId != undefined && departmentId != '') {
            $.lrSaveForm(top.$.rootUrl + '/LR_AuthorizeModule/UserRelation/SaveForms', { objectId: objectId, category: category, companyId: companyId, departmentId: departmentId }, function (res) { });
            return true;
        }
        else {
            learun.alert.warning("请选择部门！");
            return false;
        }

       
    };
    page.init();
}