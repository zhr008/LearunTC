/*
 * 
 * 
 * 创建人：上海力软信息技术有限公司
 * 日 期：2018.04.28
 * 描 述：自定义表单预览
 */
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var formData;
    var page = {
        init: function () {
            if (!!keyValue) {
                formData = top[keyValue];
                $('body').lrCustmerFormRender(formData);
            }
        }
    };
    page.init();
}