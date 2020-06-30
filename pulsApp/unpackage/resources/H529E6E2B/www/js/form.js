/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 表单方法插件
 */
(function ($, learun, fui, window) {
    "use strict";
    // 表单验证
    $.fn.lrformValid = function () {
        var validateflag = true;
        var validHelper = fui.validator;
        $(this).find("[isvalid=yes]").each(function () {

            var $this = $(this);
            var checkexpession = $(this).attr("checkexpession");
            var checkfn = validHelper['is' + checkexpession];
            if (!checkexpession || !checkfn) { return false; }
            var errormsg = $(this).attr("errormsg") || "";
            // 获取数据值
            var value;
            if ($this.hasClass('lrtextarea')) {
                value = $this.text();
            }
            else {
                var type = $this.attr('type');
                if (type === 'lrpicker') {
                    value = $this.lrpickerGet();
                }
                else if (type === 'lrdate') {
                    value = $this.lrdateGet();
                }
                else if (type === 'lrcheckbox') {
                    value = $this.lrcheckboxGet();
                }
                else if (type === 'lrselect') {
                    value = $this.lrselectGet();
                }
                else {
                    value = $this.val();
                }
            }


            var r = { code: true, msg: '' };
            if (checkexpession === 'LenNum' || checkexpession === 'LenNumOrNull' || checkexpession === 'LenStr' || checkexpession === 'LenStrOrNull') {
                var len = $this.attr("length");
                r = checkfn(value, len);
            } else {
                r = checkfn(value);
            }
            if (!r.code) {
                validateflag = false;
                fui.dialog({ msg: errormsg + r.msg });
                return false;
            }
        });
        return validateflag;
    };
    // 获取表单数据
    $.fn.lrformGet = function (keyValue) {
        var resdata = {};
        $(this).find('input,textarea,.lr-picker,.lr-date,.f-switch,.lrtextarea,.lr-checkbox,.lr-select,.lr-imagepicker').each(function (r) {
            var $this = $(this);
            var id = $this.attr('id');
            if (id) {
                var type = $this.attr('type');
                switch (type) {
                    case "lrpicker":
                        resdata[id] = $this.lrpickerGet() || '';
                        break;
                    case "lrdate":
                        resdata[id] = $this.lrdateGet() || '';
                        break;
                    case "lrswitch":
                        resdata[id] = $this.lrswitchGet();
                        break;
                    case "lrcheckbox":
                        resdata[id] = $this.lrcheckboxGet();
                        break;
                    case "lrselect":
                        resdata[id] = $this.lrselectGet();
                        break;
                    case "lrimagepicker":
                        resdata[id] = $this.imagepickerGet();
                        break;
                    default:
                        if ($this.hasClass('lrtextarea')) {
                            var value1 = $this.text() || '';
                            resdata[id] = $.trim(value1);
                        }
                        else {
                            var value2 = $this.val() || '';
                            resdata[id] = $.trim(value2);
                        }
                        break;
                }
                resdata[id] += '';
                if (resdata[id] === '') {
                    resdata[id] = '&nbsp;';
                }
                if (resdata[id] === '&nbsp;' && !keyValue) {
                    resdata[id] = '';
                }
            }
        });
        return resdata;
    };
    // 设置表单数据
    $.fn.lrformSet = function (data) {
        var $this = $(this);
        for (var id in data) {
            var value = data[id];
            var $obj = $this.find('#' + id);
            if ($obj.length >= 0 && value !== null) {
                var type = $obj.attr('type');
                switch (type) {
                    case "lrpicker":
                        $obj.lrpickerSet(value);
                        break;
                    case "lrdate":
                        $obj.lrdateSet(value);
                        break;
                    case "lrswitch":
                        $obj.lrswitchSet(value);
                        break;
                    case "lrcheckbox":
                        $obj.lrcheckboxSet(value);
                        break;
                    case "lrselect":
                        $obj.lrselectSet(value);
                        break;
                    case "lrimagepicker":
                        resdata[id] = $this.imagepickerSet(value);
                        break;
                    default:
                        if ($obj.hasClass('lrtextarea')) {
                            $obj.text(value);
                        }
                        else {
                            $obj.val(value);
                        }
                        break;
                }
            }
        }
    };

    learun.formblur = function () {// 是输入框失去焦点,隐藏输入键盘
        // 失去焦点
        var pageid = learun.pageid();
        $('#' + pageid).find('input[type="password"]:focus,input[type="text"]:focus,textarea:focus,.lrtextarea:focus').blur();
        if (learun.isreal) {// 真机调试下隐藏键盘
			plus.key.hideSoftKeybord();
        }
    };

})(window.jQuery, window.lrmui, window.fui, window);


