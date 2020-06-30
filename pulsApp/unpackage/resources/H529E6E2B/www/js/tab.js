/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 底部选项卡导航（框架核心部分）
 * 修改日志：2018-02-02 修正go方法无法跳转页面的问题
 */
(function ($, learun, fui, window) {
    "use strict";

    learun.tab = {
        init: function (tabdata) {
            // 首先加载tab数据
            var $tab = $('.lr-tabbar');
            var _html = '';
            $.each(tabdata, function (_index, item) {
                if (item.icon) {
                    _html += '\
                    <a class="lr-tab-button" data-value="'+ item.page + '" href="#">\
                        <i class="unselected iconfont '+ item.icon + '"></i>\
                        <i class="selected iconfont '+ item.fillicon + '"></i>\
                        <span class="lr-tab-button-text">'+ item.text + '</span>\
                    </a>';
                }
                else {
                    _html += '\
                    <a class="lr-tab-button" data-value="'+ item.page + '" href="#">\
                        <img class="unselected" src="'+ item.img + '">\
                        <img class="selected" src="'+ item.fillimg +'">\
                        <span class="lr-tab-button-text">'+ item.text + '</span>\
                    </a>';
                }
               
            });
            $tab.html(_html);
            $tab.on("tap", ".lr-tab-button", function (e) {
                var $this = $(this);
                if ($this.hasClass('active')) {
                    return false;
                }
                var page = $this.attr('data-value');
                var title = $this.find('span').text();

                learun.nav.go({ path: page, title: title, isBack: false, isTab: true });

                $this.parent().find('.active').removeClass('active');
                $this.addClass('active');
            });
        },
        go: function (page) {
            $('.lr-tabbar [data-value="' + page + '"]').removeClass('active');
            $('.lr-tabbar [data-value="' + page + '"]').trigger('tap');
        }
    };

})(window.jQuery, window.lrmui, window.fui, window);