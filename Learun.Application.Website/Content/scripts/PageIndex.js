

$(function () {
    "use strict";

    var pageId = request('id');
    var menuId = request('menuid');
    var moduleName = unescape(request('module'));

    var pageType;
    var activeItem = null;

    var navList = [];
    // 判断下页面数据是否是全的，如果是不全就转到主页
    $('.lr-site-banner-default .img').attr('src', $.rootUrl + '/home/GetPageImg?keyValue=' + pageId);

    $('body').on('click', '.site-menu-li', function () {
        var $this = $(this);
        var data = $this[0].data;

        if (data.F_UrlType == 1) {// 内部页面
            window.location.href = $.rootUrl + '/Home/ChildIndex?id=' + data.F_Url + '&menuid=' + data.F_Id;
        }
        else {// 外部页面
            window.open(data.F_Url);
        }

        return false;
    });

    $('body').on('click', '.lr-site-article-list-item', function () {
        var data = $(this)[0].data;
        window.open($.rootUrl + '/Home/DetailIndex?id=' + data);
        return false;
    });

    $('.homePage').on('click', function () {
        window.location.href = $.rootUrl + '/Home/Index';
    });

    // 渲染顶部菜单
    var renderTopMenu = function (_topMenuList) {
        var $topUl = $('.lr-site-nav-ul');
        $('.lr-site-sub-nav-ul').remove();
        $.each(_topMenuList["0"] || [], function (_index, _item) {
            var $item = $('<li class="lr-site-nav-li site-menu-li childPage">\
                               <a href="javascript:void(0);" class="lr-site-nav-item"><span class="text">' + _item.F_Name + '</span></a>\
                           </li>');
            $item[0].data = _item;
            $topUl.append($item);

            if (_item.F_Id == menuId) {
                $item.addClass('active');
                navList.push(_item);
            }

            // 加载子菜单
            if (_topMenuList[_item.F_Id]) {
                var $subList = $('<div class="lr-site-sub-nav-ul" data-value="' + _item.F_Id + '" ><div class="lr-site-content"><ul class="lr-site-sub-nav-menu"></ul></div></div>');
                var $subUl = $subList.find('ul');
                $.each(_topMenuList[_item.F_Id], function (_jindex, _jitem) {
                    var $jitem = $('<li class="lr-site-sub-nav-li site-menu-li"><a href="javascript:void(0);" class="lr-site-sub-nav-item">' + _jitem.F_Name + '</a></li>');
                    $jitem[0].data = _jitem;
                    if (_jitem.F_Id == menuId) {
                        $item.addClass('active');
                        navList.push(_item);
                        navList.push(_jitem);
                    }
                    // 加载三级子菜单
                    if (_topMenuList[_jitem.F_Id]) {
                        var $ul = $('<ul class="lr-site-three-nav-menu"></ul>');
                        $.each(_topMenuList[_jitem.F_Id], function (_mindex, _mitem) {
                            var $mitem = $('<li class="lr-site-three-nav-li site-menu-li"><a href="javascript:void(0);" class="lr-site-three-nav-item">' + _mitem.F_Name + '</a></li>');
                            $mitem[0].data = _mitem;
                            $ul.append($mitem);
                            if (_mitem.F_Id == menuId) {
                                $item.addClass('active');
                                navList.push(_item);
                                navList.push(_jitem);
                                navList.push(_mitem);
                            }
                        });
                        $jitem.append($ul);
                    }
                    $subUl.append($jitem);
                });
                $('body').append($subList);
            }
        });


        $topUl.find('.childPage').hover(function () {
            var $this = $(this);
            var data = $this[0].data;
            $('.lr-site-sub-nav-ul').hide();
            var $subList = $('.lr-site-sub-nav-ul[data-value="' + data.F_Id + '"]');
            if ($subList.length > 0) {
                $subList[0].isShow = false;
                $subList.show();
            }

        }, function () {
            var $this = $(this);
            var data = $this[0].data;
            setTimeout(function () {
                var $subList = $('.lr-site-sub-nav-ul[data-value="' + data.F_Id + '"]');
                if ($subList.length > 0) {
                    if (!$subList[0].isShow) {
                        $subList.hide();
                    }
                }
            }, 100);
        });
        $('.lr-site-sub-nav-ul').hover(function () {
            $(this)[0].isShow = true;
            $(this).show();
        }, function () {
            $(this)[0].isShow = false;
            $(this).hide();
        });


        // 显示导航信息
        if (menuId) {
            var $bannerTitle = $('.lr-site-banner-title .lr-site-content');
            $.each(navList, function (_index, _item) {
                var $item = $('<span class="lr-herf-item">' + _item.F_Name + '</span>');
                $item[0].data = _item;
                $bannerTitle.append($item);
                if (_index != navList.length - 1) {
                    $item.addClass('site-menu-li');
                    $bannerTitle.append('<span>></span>');
                }
            });
        }

    }

    // 渲染底部菜单
    var renderBottomMenu = function (_bottomMenuList) {
        var $list = $('.lr-site-footer-nav-menu');
        $list.html('');
        $.each(_bottomMenuList, function (_index, _item) {
            var $item = $(' <li class="lr-site-footer-nav-li site-menu-li"><a href="javascript:void(0);" class="lr-site-footer-nav-item">' + _item.F_Name + '</a></li>');
            $item[0].data = _item;
            $list.append($item);

            if (menuId == _item.F_Id) {
                $('.homePage').addClass('active');
                var $bannerTitle = $('.lr-site-banner-title .lr-site-content');
                var $item = $('<span class="lr-herf-item">首页</span>');
                $bannerTitle.append($item);
                $bannerTitle.append('<span>></span><span class="lr-herf-item">' + _item.F_Name + '</span>');
                $item.on('click', function () {
                    window.location.href = $.rootUrl + '/Home/Index';
                });
            }

        });
    }


    if (!menuId) {
        $('.homePage').addClass('active');
        var $bannerTitle = $('.lr-site-banner-title .lr-site-content');
        var $item = $('<span class="lr-herf-item">首页</span>');
        $bannerTitle.append($item);
        $bannerTitle.append('<span>></span><span class="lr-herf-item">' + moduleName + '</span>');
        $item.on('click', function () {
            window.location.href = $.rootUrl + '/Home/Index';
        });
    }


    var formatDate = function (v, format) {
        if (!v) return "";
        var d = v;
        if (typeof v === 'string') {
            if (v.indexOf("/Date(") > -1)
                d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
            else
                d = new Date(Date.parse(v.replace(/-/g, "/").replace("T", " ").split(".")[0]));//.split(".")[0] 用来处理出现毫秒的情况，截取掉.xxx，否则会出错
        }
        var o = {
            "M+": d.getMonth() + 1,  //month
            "d+": d.getDate(),       //day
            "h+": d.getHours(),      //hour
            "m+": d.getMinutes(),    //minute
            "s+": d.getSeconds(),    //second
            "q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
            "S": d.getMilliseconds() //millisecond
        };
        if (/(y+)/.test(format)) {
            format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return format;
    };

    // 获取页面公共部分数据
    httpGet($.rootUrl + '/Home/GetAllList', function (res) {
        var topMenuList = {};
        var bottomMenuList = [];
        if (res && res.code == 200) {
            $.each(res.data, function (_index, _item) {
                switch (_item.F_Type) {
                    case '1':// 顶部文字
                        $('#lr_site_top_text').text(_item.F_Name);
                        break;
                    case '2':// 底部文字
                        $('.lr-site-bottom').text(_item.F_Name);
                        break;
                    case '3':// 底部地址
                        $('.lr-site-contactInfo').text(_item.F_Name);
                        break;
                  
                    case '6':// 顶部菜单
                        topMenuList[_item.F_ParentId] = topMenuList[_item.F_ParentId] || [];
                        topMenuList[_item.F_ParentId].push(_item);
                        break;
                    case '7':// 底部菜单
                        bottomMenuList.push(_item);
                        break;
                    case '11':
                        $('.lr-site-footer-wechat .text').text(_item.F_Name);
                        break;
                }
            });

            renderTopMenu(topMenuList);
            renderBottomMenu(bottomMenuList);

        }

    });


    function loadPage(pageIndex) {
        var pagination = {
            rows: 10,                // 每页行数      
            page: pageIndex,         // 当前页
            sidx: 'F_PushDate',      // 排序列
            sord: 'DESC',            // 排序类型
            records: 0,              // 总记录数
            total: 0                 // 总页数
        };
        httpGet($.rootUrl + '/Home/GetArticlePageList?pagination=' + JSON.stringify(pagination) + '&queryJson=' + JSON.stringify({ F_Category: activeItem.category }), function (res) {
            var $list = $('#lr_body_cotent');
            $list.html('');
            if (res && res.code == 200) {
                var data = res.data;
                if (pageType == '1') {
                    $.each(data.rows, function (_index, _item) {
                        var $item = $('<div class="lr-site-body-list-item lr-site-article-list-item">\
                                        <div class="text">' + _item.F_Title + '</div>\
                                        <div class="date">' + formatDate(_item.F_PushDate, 'yyyy-MM-dd') + '</div>\
                                    </div>');
                        $item[0].data = _item.F_Id;
                        $list.append($item);
                    });
                }
                else {
                    $.each(data.rows, function (_index, _item) {
                        var $item = $('<div class="col-md-4 col-sm-6 lr-site-img-item lr-site-article-list-item">\
                                    <div class="lr-site-img-content2">\
                                        <img class="img" src="' + top.$.rootUrl + '/Home/GetArticleImg?keyValue=' + _item.F_Id + '" />\
                                        <div class="text" title="' + _item.F_Title + '" >' + _item.F_Title + '</div>\
                                    </div>\
                                </div>');
                        $item[0].data = _item.F_Id;
                        $list.append($item);
                    });
                }

                resetPage(data);
            }
        });
    }

    //重置分页(跳转分页)
    function resetPage(data) {
        laypage({
            cont: "lr_page", //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
            pages: data.total, //通过后台拿到的总页数
            curr: data.page, //当前页
            groups: 5, //连续显示分页数
            skip: true, //是否开启跳页
            first: '首页', //若不显示，设置false即可
            last: '尾页', //若不显示，设置false即可
            jump: function (obj, first) { //触发分页后的回调
                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                    loadPage(obj.curr);
                }
            }
        });
    }

    $('.lr-site-body-container').on('click', '.lr-site-title-item', function () {
        var $this = $(this);
        if (!$this.hasClass('active')) {
            $this.parent().find('.active').removeClass('active');
            $this.addClass('active');
            var data = $this[0].data;

            $('.right-title').text(data.name);

            activeItem = data;

            if (pageType == '3') {
                httpGet($.rootUrl + '/Home/GetArticle?keyValue=' + activeItem.article, function (res) {
                    if (res && res.code == 200) {
                        var $list = $('#lr_body_cotent');
                        // 图片地址切换
                        var _content = res.data.F_Content.replace(/\/ueditor\/upload\/image/g, $.rootUrl + '/Home/GetUeditorImg?id=');
                        $list.html(_content);
                    }
                });
            }
            else {
                loadPage(1);
            }
        }

    });

    // 获取具体页面数据
    httpGet($.rootUrl + '/Home/GetPageData?keyValue=' + pageId, function (res) {
        if (res && res.code == 200) {
            var data = res.data;
            var shceme = JSON.parse(data.F_Scheme);
            var $container = $('.lr-site-body-container');
            var $left = $('<div class="lr-site-body-left">\
                                       <div class="lr-site-title">' + shceme.title + '</div>\
                                       <div class="lr-site-body-cotent"></div>\
                                   </div>');
            var $leftContent = $left.find('.lr-site-body-cotent');
            var $firstItem = null;
            $.each(shceme.list, function (_index, _item) {
                var $item = $('<div class="lr-site-title-item">' + _item.name + '</div>');
                $item[0].data = _item;
                $leftContent.append($item);

                if (_index == 0) {
                    $firstItem =  $item;
                }
            });
            $container.append($left);
            pageType = data.F_Type;
            switch (data.F_Type) {
                case '1': // 文章列表
                    $container.append('<div class="lr-site-body-right">\
                                           <div class="lr-site-title right-title">未选择分类项</div>\
                                           <div class="lr-site-body-cotent">\
                                               <div class="lr-site-body-list" id="lr_body_cotent">\
                                               </div>\
                                               <div class="lr-site-body-page" id="lr_page"></div>\
                                           </div>\
                                       </div>');
                    break;
                case '2': // 图片列表
                    $container.append('<div class="lr-site-body-right">\
                                           <div class="lr-site-title right-title">未选择分类项</div>\
                                           <div class="lr-site-body-cotent">\
                                               <div class="row" id="lr_body_cotent">\
                                               </div>\
                                               <div class="lr-site-body-page" id="lr_page"></div>\
                                           </div>\
                                       </div>');
                    break;
                case '3': // 详细列表
                    $container.append('<div class="lr-site-body-right">\
                                           <div class="lr-site-title right-title">未选择分类项</div>\
                                           <div class="lr-site-body-cotent">\
                                               <div class="lr-site-body-list" id="lr_body_cotent">\
                                               </div>\
                                           </div>\
                                       </div>');
                    break;

            }

            if ($firstItem) {
                $firstItem.trigger('click');
                $firstItem = null;
            }
        }

    });
});