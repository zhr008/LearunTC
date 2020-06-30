/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-12-25 15:44
 * 描  述：门户网站首页配置
 */
var titleName = "";
var renderTopMenu;
var renderBottomMenu;
var renderPicture;
var renderModule;
var addModule;

var bannerSwiper;
var currentModule;
var bootstrap = function ($, learun) {
    "use strict";
    function uploadImg1() {
        var f = document.getElementById('uploadFile1').files[0]
        var src = window.URL.createObjectURL(f);
        document.getElementById('uploadPreview1').src = src;


        learun.loading(true, '正在保存...');
        $.ajaxFileUpload({
            url: top.$.rootUrl + "/LR_PortalSite/HomeConfig/UploadFile?type=4",
            secureuri: false,
            fileElementId: 'uploadFile1',
            dataType: 'json',
            success: function (data) {
                learun.loading(false);
            }
        });
    };
    function uploadImg2() {
        var f = document.getElementById('uploadFile2').files[0]
        var src = window.URL.createObjectURL(f);
        document.getElementById('uploadPreview2').src = src;


        learun.loading(true, '正在保存...');
        $.ajaxFileUpload({
            url: top.$.rootUrl + "/LR_PortalSite/HomeConfig/UploadFile?type=10",
            secureuri: false,
            fileElementId: 'uploadFile2',
            dataType: 'json',
            success: function (data) {
                learun.loading(false);
            }
        });

    };
    function uploadImg3() {
        var f = document.getElementById('uploadFile3').files[0]
        var src = window.URL.createObjectURL(f);
        document.getElementById('uploadPreview3').src = src;

        learun.loading(true, '正在保存...');
        $.ajaxFileUpload({
            url: top.$.rootUrl + "/LR_PortalSite/HomeConfig/UploadFile?type=5",
            secureuri: false,
            fileElementId: 'uploadFile3',
            dataType: 'json',
            success: function (data) {
                learun.loading(false);
            }
        });

    };

    $.fn.SiteCarousel = function (options) {
        //默认配置
        var defaults = {
            speed: 4000,  //滚动速度,值越大速度越慢
            rowHeight: 47 //每行的高度
        };

        var opts = $.extend({}, defaults, options), intId;

        function marquee(obj, step, callback) {
            obj.find("ul").animate({
                marginTop: '-=' + step
            }, 300, function () {
                $(this).find("li").slice(0, 1).appendTo($(this));
                $(this).css("margin-top", 0);
                callback();
            });
        }


        this.each(function (i) {
            var sh = opts["rowHeight"], speed = opts["speed"], _this = $(this);
            var _fn = function (flag) {
                if (flag) {
                    clearInterval(intId);
                    intId = setTimeout(_fn, speed);
                }
                else {
                    if (_this.find("ul").height() > _this.height()) {
                        marquee(_this, sh, function () {
                            clearInterval(intId);
                            intId = setTimeout(_fn, speed);
                        });
                    }
                }
            };
            _fn(true);
            _this.hover(function () {
                if (intId) {
                    clearInterval(intId);
                }
            }, function () {
                _fn(true);
            });
        });
    };

    var page = {
        init: function () {
            ////banner 轮播
            bannerSwiper = new Swiper('.lr-site-banner-swiper-container', {
                direction: 'horizontal',
                autoplay: true,
                loop: true,
                speed: 600,
                // 分页器
                pagination: {
                    el: '.swiper-pagination',
                    clickable: true
                },
            });

            page.bind();
            page.initData();
        },
        bind: function () {
            // 顶部文字设置
            $('#lr_site_top_text').on('click', function () {
                var $this = $(this);
                titleName = $this.find('span').text();
                learun.layerForm({
                    id: 'SetTextForm',
                    title: '设置',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/SetTextForm?type=1',
                    width: 700,
                    height: 140,
                    callBack: function (id) {
                        return top[id].acceptClick(function (name) {
                            $this.find('span').text(name);
                        });
                    }
                });
            });
            // 底部地址
            $('#lr_site_footer_text').on('click', function () {
                var $this = $(this);
                titleName = $this.find('span').text();
                learun.layerForm({
                    id: 'SetTextForm',
                    title: '设置',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/SetTextForm?type=3',
                    width: 700,
                    height: 140,
                    callBack: function (id) {
                        return top[id].acceptClick(function (name) {
                            $this.find('span').text(name);
                        });
                    }
                });
            });
            // 底部文字
            $('#lr_site_bottom_text').on('click', function () {
                var $this = $(this);
                titleName = $this.find('span').text();
                learun.layerForm({
                    id: 'SetTextForm',
                    title: '设置',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/SetTextForm?type=2',
                    width: 700,
                    height: 140,
                    callBack: function (id) {
                        return top[id].acceptClick(function (name) {
                            $this.find('span').text(name);
                        });
                    }
                });
            });
            // 微信文字
            $('#wechat_text').on('click', function () {
                var $this = $(this);
                titleName = $this.find('span').text();
                learun.layerForm({
                    id: 'SetTextForm',
                    title: '设置',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/SetTextForm?type=11',
                    width: 300,
                    height: 140,
                    callBack: function (id) {
                        return top[id].acceptClick(function (name) {
                            $this.find('span').text(name);
                        });
                    }
                });
            });
            

            // 顶部logo上传
            $('#uploadFile1').on('change', uploadImg1);
            $('#top_logo').prepend('<img id="uploadPreview1"  src="' + top.$.rootUrl + '/LR_PortalSite/HomeConfig/GetImg?type=4" >');

            // 底部logo上传
            $('#uploadFile2').on('change', uploadImg2);
            $('#bottom_logo').prepend('<img id="uploadPreview2"  src="' + top.$.rootUrl + '/LR_PortalSite/HomeConfig/GetImg?type=10" >');

            // 微信图片
            $('#uploadFile3').on('change', uploadImg3);
            $('#wechat_img').prepend('<img id="uploadPreview3"  src="' + top.$.rootUrl + '/LR_PortalSite/HomeConfig/GetImg?type=5" >');

            // 顶部菜单
            $('#top_menu').on('click', function () {
                learun.layerForm({
                    id: 'TopMenuIndex',
                    title: '顶部菜单配置',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/TopMenuIndex',
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: null
                });
            });

            // 底部菜单
            $('#bottom_menu').on('click', function () {
                learun.layerForm({
                    id: 'BottomMenuIndex',
                    title: '底部菜单配置',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/BottomMenuIndex',
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: null
                });
            });

            // 配置轮播图
            $('#banner_site').on('click', function () {
                learun.layerForm({
                    id: 'PictureForm',
                    title: '轮播图配置',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/PictureForm',
                    width: 400,
                    height: 500,
                    maxmin: true,
                    btn: null
                });
            });

            // 新增模块
            $('#moduleAddBtn').on('click', function () {
                currentModule = null;
                learun.layerForm({
                    id: 'SelectModuleForm',
                    title: '模块风格选择',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/SelectModuleForm',
                    width: 600,
                    height: 500,
                    maxmin: true,
                    btn: null
                });
            });

            // 模块编辑(上移，下移，删除)
            $('.lr-site-modules').on('click', '.edit-btn>span', function () {
                var $this = $(this);
                var v = $this.attr('data-value');
                var $item = $this.parents('.lr-site-module');
                var data = $item[0].data;
                var $next;
                var _index;
                switch (v) {
                    case 'up':
                        $next = $item.prev();
                        if ($next.length > 0) {
                            _index = $next[0].data.F_Sort;
                            $next[0].data.F_Sort = $item[0].data.F_Sort;
                            $item[0].data.F_Sort = _index;

                            learun.httpAsync('post', top.$.rootUrl + '/LR_PortalSite/HomeConfig/UpdateForm', { keyValue1: $item[0].data.F_Id, keyValue2: $next[0].data.F_Id }, function (data) {

                            });

                            $next.before($item);
                        }
                        break;
                    case 'down':
                        $next = $item.next();
                        if (!$next.hasClass('lr-site-module-add')) {
                            _index = $next[0].data.F_Sort;
                            $next[0].data.F_Sort = $item[0].data.F_Sort;
                            $item[0].data.F_Sort = _index;

                            learun.httpAsync('post', top.$.rootUrl + '/LR_PortalSite/HomeConfig/UpdateForm', { keyValue1: $item[0].data.F_Id, keyValue2: $next[0].data.F_Id }, function (data) {

                            });

                            $next.after($item);
                        }
                        break;
                    case 'edit':
                        var schemeObj = JSON.parse(data.F_Scheme);
                        currentModule = data;
                        addModule(schemeObj.type, data.F_Sort);
                        break;
                    case 'delete':
                        learun.layerConfirm('是否确认删除该模块?', function (res) {
                            if (res) {
                                learun.deleteForm(top.$.rootUrl + '/LR_PortalSite/HomeConfig/DeleteForm', { keyValue: data.F_Id }, function () {
                                    $item.remove();
                                });
                            }
                        });
                        break;
                }

                return false;
            });

            
            $('.lr-site-modules').on('click', '.lr-site-tab', function () {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    var $parent = $this.parent();
                    $parent.find('.active').removeClass('active');
                    $this.addClass('active');

                    var value = $this.attr('data-value');
                    $parent.next().find('.active').removeClass('active');
                    $parent.next().find('[data-value="' + value + '"]').addClass('active');
                }
            });


        },
        initData: function () {
            var topMenuList = {};
            var bottomMenuList = [];
            var pictureList = [];

            learun.httpAsync('GET', top.$.rootUrl + '/LR_PortalSite/HomeConfig/GetAllList', {}, function (data) {
                // 数据渲染
                $.each(data, function (_index,_item) {
                    switch (_item.F_Type) {
                        case '1':// 顶部文字
                            $('#lr_site_top_text span').text(_item.F_Name);
                            break;
                        case '2':// 底部文字
                            $('#lr_site_bottom_text span').text(_item.F_Name);
                            break;
                        case '3':// 底部地址
                            $('#lr_site_bottom_text span').text(_item.F_Name);
                            break;
                        case '4':// logo图片
                            break;
                        case '5':// 微信图片
                            break;
                        case '6':// 顶部菜单
                            topMenuList[_item.F_ParentId] = topMenuList[_item.F_ParentId] || [];
                            topMenuList[_item.F_ParentId].push(_item);
                            break;
                        case '7':// 底部菜单
                            bottomMenuList.push(_item);
                            break;
                        case '8':
                            pictureList.push(_item);
                            break;
                        case '11':
                            $('#wechat_text span').text(_item.F_Name);
                            break;
                    }
                });
                // 加载顶部菜单
                renderTopMenu(topMenuList);
                renderBottomMenu(bottomMenuList);
                renderPicture(pictureList);
            });
            learun.httpAsync('GET', top.$.rootUrl + '/LR_PortalSite/HomeConfig/GetList', { type: 9 }, function (data) {
                $.each(data, function (_index, _item) {
                    renderModule(_item);
                });
            });
        }
    };

    // 渲染顶部菜单
    renderTopMenu = function (_topMenuList) {
        var $topUl = $('#top_menu ul');
        $topUl.find('.childPage').remove();
        $('.lr-site-sub-nav-ul').remove();
        $.each(_topMenuList["0"] || [], function (_index, _item) {
            var $item = $('<li class="lr-site-nav-li childPage">\
                               <a href="javascript:void(0);" class="lr-site-nav-item"><span class="text">' + _item.F_Name + '</span></a>\
                           </li>');
            $item[0].data = _item;
            $topUl.append($item);
            // 加载子菜单
            if (_topMenuList[_item.F_Id]) {
                var $subList = $('<div class="lr-site-sub-nav-ul" data-value="' + _item.F_Id + '" ><div class="lr-site-content"><ul class="lr-site-sub-nav-menu"></ul></div></div>');
                var $subUl = $subList.find('ul');
                $.each(_topMenuList[_item.F_Id], function (_jindex, _jitem) {
                    var $jitem = $('<li class="lr-site-sub-nav-li"><a href="javascript:void(0);" class="lr-site-sub-nav-item">'+_jitem.F_Name+'</a></li>');
                    $jitem[0].data = _jitem;

                    // 加载三级子菜单
                    if (_topMenuList[_jitem.F_Id]) {
                        var $ul = $('<ul class="lr-site-three-nav-menu"></ul>');
                        $.each(_topMenuList[_jitem.F_Id], function (_mindex, _mitem) {
                            var $mitem = $('<li class="lr-site-three-nav-li"><a href="javascript:void(0);" class="lr-site-three-nav-item">'+_mitem.F_Name+'</a></li>');
                            $mitem[0].data = _mitem;
                            $ul.append($mitem);
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
    }

    // 渲染底部菜单
    renderBottomMenu = function (_bottomMenuList) {
        var $list = $('#bottom_menu ul');
        $list.html('');
        $.each(_bottomMenuList, function (_index, _item) {
            var $item = $(' <li class="lr-site-footer-nav-li"><a href="javascript:void(0);" class="lr-site-footer-nav-item">' + _item.F_Name + '</a></li>');
            $list.append($item);
        });
    }

    // 轮播图片
    renderPicture = function (_pictureList) {
        var $swrapper = $('.lr-site-banner-swiper-container .swiper-wrapper');
        $swrapper.html("");
        if (_pictureList.length > 0) {
            $('.lr-site-banner-swiper-container').show();
            $('.lr-site-banner-default').hide();
           
            $.each(_pictureList, function (_index, _item) {
                var src = _item.src || (top.$.rootUrl + '/LR_PortalSite/HomeConfig/GetImg2?keyValue=' + _item.F_Id);


                var $item = $('<div class="swiper-slide">\
                                   <img class="img"  src="' + src + '" />\
                               </div>');
                $swrapper.append($item);
            });
        }
        else {
            $('.lr-site-banner-swiper-container').hide();
            $('.lr-site-banner-default').show();
        }
        bannerSwiper.update();
    }

    // 设置模块
    addModule = function (type, sort) {
        if (!sort) {
            var $module = $('#moduleAddBtn').parent().prev();
            if ($module.length > 0) {
                sort = parseInt($module[0].data.F_Sort) + 1;
            }
            else {
                sort = 1;
            }
        }


        switch (type) {
            case '1':
                learun.layerForm({
                    id: 'ModuleForm1',
                    title: '添加模块(风格一)',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/ModuleForm1?sort=' + sort,
                    height: 700,
                    width: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(renderModule);
                    }
                });
                break;
            case '2':
                learun.layerForm({
                    id: 'ModuleForm2',
                    title: '添加模块(风格二)',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/ModuleForm2?sort=' + sort,
                    height: 700,
                    width: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(renderModule);
                    }
                });
                break;
            case '3':
                learun.layerForm({
                    id: 'ModuleForm3',
                    title: '添加模块(风格三)',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/ModuleForm3?sort=' + sort,
                    height: 700,
                    width: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(renderModule);
                    }
                });
                break;
            case '4':
                learun.layerForm({
                    id: 'ModuleForm4',
                    title: '添加模块(风格四)',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/ModuleForm4?sort=' + sort,
                    height: 700,
                    width: 800,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(renderModule);
                    }
                });
                break;
            case '5':
                learun.layerForm({
                    id: 'ModuleForm5',
                    title: '添加模块(风格五)',
                    url: top.$.rootUrl + '/LR_PortalSite/HomeConfig/ModuleForm5?sort=' + sort,
                    width: 400,
                    height: 300,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(renderModule);
                    }
                });
                break;
        }
    }

    renderModule = function (data) {
        var schemeObj = JSON.parse(data.F_Scheme);

        var $btn = $('#moduleAddBtn').parent();
        var $item = $('.lr-site-module[data-value="' + data.F_Id + '"]');
        switch (schemeObj.type) {
            case '1':// 风格一
                if ($item.length == 0) {
                    $item = $('<div class="lr-site-module module1" data-value="' + data.F_Id + '" >\
                               <div class="lr-site-box">\
                                   <div class="title">\
                                       <i class="fa fa-volume-down"></i>\
                                       <span class="name" >' + data.F_Name + '</span>\
                                       <span class="arrow"></span>\
                                   </div>\
                                   <div class="content">\
                                       <ul>\
                                       </ul>\
                                   </div>\
                                   <div class="lr-site-more">' + (data.F_Url ? '更多' : '') + '</div>\
                                   <div class="edit-btn"><span data-value="up" >上移</span><span data-value="down"  >下移</span><span data-value="edit"  >编辑</span><span data-value="delete"  >删除</span></div>\
                               </div>\
                           </div>');
                    $item.find('.content').SiteCarousel();
                    $btn.before($item);
                }
                else {
                    $item.find('.title .name').text(data.F_Name);

                    if (data.F_Url && data.F_Url != '&nbsp;') {
                        $item.find('.lr-site-more').text('更多');
                    }
                    else {
                        $item.find('.lr-site-more').text('');
                    }
                }
                var $ul = $item.find('ul');
                $ul.html('');
                $.each(schemeObj.list, function (_index,_item) {
                    $ul.append('<li><a href="javascript:void(0);" class="lr-text-item">' + _item.F_Title + '</a></li>');
                });
                break;
            case '2':
                if ($item.length == 0) {
                    $item = $('<div class="lr-site-module module2" data-value="' + data.F_Id + '" >\
                               <div class="lr-site-box">\
                                   <div class="lr-site-title"><span class="name" >' + data.F_Name + '</span><div class="lr-site-more">' + (data.F_Url ? '更多' : '') + '</div></div>\
                                   <div class="lr-site-body">\
                                        <div class="lr-site-module-pic">\
                                            <div class="lr-site-module-swiper-container swiper-container">\
                                                <div class="swiper-wrapper">\
                                                </div>\
                                                <div class="swiper-pagination"></div>\
                                            </div>\
                                        </div>\
                                        <div class="lr-site-module-list">\
                                        </div>\
                                    </div>\
                                   <div class="edit-btn"><span data-value="up" >上移</span><span data-value="down"  >下移</span><span data-value="edit"  >编辑</span><span data-value="delete"  >删除</span></div>\
                               </div>\
                           </div>');
                    $btn.before($item);

                    $item[0].swiper = new Swiper('[data-value="' + data.F_Id + '"] .lr-site-module-swiper-container', {
                        direction: 'horizontal',
                        autoplay: true,
                        loop: true,
                        speed: 600,
                        // 分页器
                        pagination: {
                            el: '.swiper-pagination',
                            clickable: true
                        },
                    });
                }
                else {
                    $item.find('.title .name').text(data.F_Name);

                    if (data.F_Url && data.F_Url != '&nbsp;') {
                        $item.find('.lr-site-more').text('更多');
                    }
                    else {
                        $item.find('.lr-site-more').text('');
                    }
                }
                $item.css({ width: parseFloat(schemeObj.prop) * 100 + '%' });
                // 加载图片
                if (schemeObj.list1.length == 0) {
                    $item.addClass('noimg');
                }
                else {
                    $item.removeClass('noimg');
                    var $swrapper = $item.find('.swiper-wrapper');
                    $swrapper.html('');
                    $.each(schemeObj.list1, function (_index,_item) {
                        var $li = $('<div class="swiper-slide">\
                                   <img class="img"  src="' + top.$.rootUrl + '/LR_PortalSite/Article/GetImg?keyValue=' + _item.F_Id + '" />\
                               </div>');
                        $swrapper.append($li);
                    });

                    $item[0].swiper.update();
                }
                // 加载文字列表
                var $textList = $item.find('.lr-site-module-list');
                $textList.html('');
                $.each(schemeObj.list2, function (_index, _item) {
                    var $li = $('<div class="lr-site-module-item">\
                                    <div class="text">'+ _item.F_Title + '</div>\
                                    <div class="date">' + learun.formatDate(_item.F_PushDate, 'yyyy-MM-dd') + '</div>\
                                </div>');
                    $textList.append($li);
                });
                break;
            case '3':
                if ($item.length == 0) {
                    $item = $('<div class="lr-site-module module3" data-value="' + data.F_Id + '" >\
                               <div class="lr-site-box">\
                                   <div class="lr-site-tabs"></div>\
                                   <div class="lr-site-body">\
                                    </div>\
                                   <div class="edit-btn"><span data-value="up" >上移</span><span data-value="down"  >下移</span><span data-value="edit"  >编辑</span><span data-value="delete"  >删除</span></div>\
                               </div>\
                           </div>');
                    $btn.before($item);
                }
                $item.css({ width: parseFloat(schemeObj.prop) * 100 + '%' });

                var tabList = $item.find('.lr-site-tabs');
                var tabBody = $item.find('.lr-site-body');
                tabList.html('');
                tabBody.html('');
                $.each(schemeObj.list, function (_index, _item) {
                    var $li = $('<div class="lr-site-tab" data-value="' + _index + '">' + _item.name + '</div>');
                    tabList.append($li);
                    var $tabContent = $('<div class="lr-site-tab-content" data-value="' + _index + '"></div>')
                    $.each(_item.list, function (_jindex, _jitem) {
                        $tabContent.append('<div class="lr-site-tab-content-item">' + _jitem.F_Title + '</div>');
                    });

                    tabBody.append($tabContent);
                    if (_index == 0) {
                        $li.trigger('click');
                    }
                });
                break;
            case '4':
                if ($item.length == 0) {
                    $item = $('<div class="lr-site-module module4" data-value="' + data.F_Id + '" >\
                               <div class="lr-site-box">\
                                   <div class="lr-site-title"><span class="name" >' + data.F_Name + '</span><div class="lr-site-more">' + (data.F_Url ? '更多' : '') + '</div></div>\
                                   <div class="lr-site-body">\
                                    </div>\
                                   <div class="edit-btn"><span data-value="up" >上移</span><span data-value="down"  >下移</span><span data-value="edit"  >编辑</span><span data-value="delete"  >删除</span></div>\
                               </div>\
                           </div>');
                    $btn.before($item);
                }
                else {
                    $item.find('.title .name').text(data.F_Name);
                    if (data.F_Url && data.F_Url != '&nbsp;') {
                        $item.find('.lr-site-more').text('更多');
                    }
                    else {
                        $item.find('.lr-site-more').text('');
                    }
                }
                var $body = $item.find('.lr-site-body');
                $body.html('');
                $.each(schemeObj.list, function (_index, _item) {
                    $body.append(' <div class="lr-site-img-item">\
                                        <div class="lr-site-img-item-content">\
                                            <img class="img"  src="' + top.$.rootUrl + '/LR_PortalSite/Article/GetImg?keyValue=' + _item.F_Id + '" />\
                                            <div class="text">' + _item.F_Title + '</div>\
                                        </div>\
                                    </div>');
                });

                break;
            case '5':
                if ($item.length == 0) {
                    $item = $('<div class="lr-site-module module5" data-value="' + data.F_Id + '" >\
                               <div class="lr-site-box">\
                                   <div class="lr-site-title"><span class="name" >' + data.F_Name + '</span></div>\
                                   <div class="lr-site-body">\
                                        <div class="lr-site-article-pic">\
                                            <img class="img" src="' + top.$.rootUrl + '/LR_PortalSite/Article/GetImg?keyValue=' + schemeObj.article + '" />\
                                        </div>\
                                        <div class="text-content">\
                                            <div class="text" data-text="' + schemeObj.article + '" ></div>\
                                        </div>\
                                    </div>\
                                   <div class="edit-btn"><span data-value="up" >上移</span><span data-value="down"  >下移</span><span data-value="edit"  >编辑</span><span data-value="delete"  >删除</span></div>\
                               </div>\
                           </div>');
                    $btn.before($item);
                }
                else {
                    $item.find('.title .name').text(data.F_Name);
                    $item.find('.img').attr('src', top.$.rootUrl + '/LR_PortalSite/Article/GetImg?keyValue=' + schemeObj.article);
                }
                $item.css({ width: parseFloat(schemeObj.prop) * 100 + '%' });

                learun.httpAsync('GET', top.$.rootUrl + '/LR_PortalSite/Article/GetFormData', { keyValue: schemeObj.article }, function (data) {
                    if (data) {
                        var text = $('<div></div>').html(data.F_Content).text().substring(0, 106) || '';
                        if (text.length == 106) {
                            text = text.substring(0, 105) + '...';
                        }
                        $('.module5 [data-text="' + data.F_Id + '"]').text(text);
                    }
                });

                break;

        }
        $item[0].data = data;
    }

    page.init();
}
