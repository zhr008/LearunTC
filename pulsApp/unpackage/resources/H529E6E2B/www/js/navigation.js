/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 导航路由方法（框架核心部分）
 */
(function ($, learun, fui, window) {
    "use strict";

    learun.nav = {
        data: {// 加载过的页面数据|开发的时候请改完代码保存后刷新下页面
        },
        getpage: function (name) {
            var page = this.data[name];
            var pageobj = null;
            if (page) {
                pageobj = page.jsObj;
            }
            return pageobj;
        },
        go: function (op) {
            var dfop = {
                path: '',
                title: '',
                type: '',// 'right','bottom', 不填为淡入淡出
                backbtn: '<i class="iconfont icon-back_light"></i>',
                isBack: true,
                isTab: false,
                isHead: true,
                fclass: '',
                param: {}// 传递参数
            };
            $.extend(dfop, op || {}); 
            if (dfop.path === '') {
                fui.dialog({ msg: '力软信息提示：不知道页面路径！' });
                return false;
            }

            dfop.start = function (pageinfo) {
                // 页面开始
                if (!pageinfo.op.isTab) {
                    $('.lr-tabbar').css('z-index', 2);
                }
            };

            dfop.end = function (pageinfo) {
                var $tab = $('.lr-tabbar');
                $tab.css('z-index', 10);
                // 页面动画结束
                if (pageinfo.op.isTab) {
                    $tab.show();
                }
                else {
                    $tab.hide();
                }
                var $pagebody = pageinfo.$page.find('.f-page-body');
                var page = learun.nav.data[pageinfo.op.id];

                if (!pageinfo.op.isBack || !pageinfo.op.isHead) {
                    // 如果跳转的页面没有返回的按钮，就把之前带返回按钮的页面删掉
                    var pageid = "";
                    var _pageinfo = pageinfo;
                    for (var i = 0; i < 100; i++) {
                        pageid = _pageinfo.preid;
                        if (pageid !== "" && pageinfo.id !== pageid) {
                            _pageinfo = fui.nav.page.data[pageid];
                            if (!_pageinfo.op.isBack || !_pageinfo.op.isHead) {
                                break;
                            }
                            else {
                                fui.nav.close(pageid);
                            }
                        }
                        else {
                            break;
                        }
                    }
                    pageinfo.preid = "";
                }

                if (!pageinfo.hasLoad) {
                    loadhtml(page, $pagebody, pageinfo.op);
                }
                else {// 如果页面已经加载过了,如果页面有reload方法就加载一次
                    page.jsObj && page.jsObj.reload && page.jsObj.reload($pagebody, pageinfo);
                }
            };

            dfop.bfdestroy = function (pageinfo) {
                // 页面销毁之前执行
                var page = learun.nav.data[pageinfo.op.id];
                if (!!page.jsObj && !!page.jsObj.beforedestroy) {
                    return page.jsObj.beforedestroy();
                }
                return true;
            };
            dfop.destroy = function (pageinfo) {
                // 页面销毁
                var page = learun.nav.data[pageinfo.op.id];
                if (!!page.jsObj && !!page.jsObj.destroy) {
                    page.jsObj.destroy();
                }
            };

            // 去加载页面数据
            load(dfop.path);

            // 渲染页面
            if (!dfop.isBack) {
                dfop.fclass = "lr-page-no-back";
            }

            if (!dfop.isHead) {
                dfop.fclass = dfop.fclass + " lr-page-no-head";
            }



            if (dfop.isTab) {
                dfop.fclass = dfop.fclass + " lr-page-have-tab";
            }
            dfop.id = dfop.path;
            fui.nav.go(dfop);

        },
        closeCurrent: function () {
            fui.nav.closeCurrent();
        },
        close: function (id) {
            fui.nav.close(id);
        }
    };
    function load(path) {
        if (!learun.nav.data[path]) {// 判断下当前页面是否已经加载了
            learun.nav.data[path] = {
                //cssLoaded: false,
                jsLoaded: false,
                htmlLoaded: false,
                strhtml: ''
            };
            var paths = path.split('/');
            var filename = paths[paths.length - 1];
            // 加载页面css代码
            learun.http.webget('pages/' + path + '/' + filename + '.css', {}, function (res) {
                if (res !== null) {
                    fui.includeCss(res);
                }
            }, 'text');
            //fui.loadCss('pages/' + path + '/' + path + '.css');
            // 加载页面html代码
            learun.http.webget('pages/' + path + '/' + filename + '.html', {}, function (res) {
                if (res === null) {
                    fui.dialog({ msg: '力软信息提示：找不到页面！' });
                }
                learun.nav.data[path].strhtml = res || '';
                learun.nav.data[path].htmlLoaded = true;
            }, 'text');
            // 加载页面js代码
            learun.http.webget('pages/' + path + '/' + filename + '.js', {}, function (res) {
                var strjs = '(function ($,learun) {learun.nav.data["' + path + '"].jsObj = ' + (res || 'null;') + '})(window.jQuery,window.lrmui);';
                fui.includeJs(strjs);
                learun.nav.data[path].jsLoaded = true;
            }, 'text');
        }
    }


    // 加载js代码
    function loadjs(_page, _$pagebody, _op) {
        if (_page.jsLoaded) {
            if (_page.jsObj) {
                if (_page.jsObj.isScroll) {
                    _$pagebody.scroll();
                }
                setTimeout(function () {
                    _page.jsObj.init && _page.jsObj.init(_$pagebody, _op.param);
                }, 100);
            }
        }
        else {
            setTimeout(function () {
                loadjs(_page, _$pagebody, _op);
            }, 200);
        }
    }
    // 加载html代码
    function loadhtml(_page, _$pagebody, _op) {
        if (_page.htmlLoaded) {
            _$pagebody.html(_page.strhtml);
            loadjs(_page, _$pagebody, _op);
        }
        else {
            setTimeout(function () {
                loadhtml(_page, _$pagebody, _op);
            }, 200);
        }
    }
})(window.jQuery, window.lrmui, window.fui, window);