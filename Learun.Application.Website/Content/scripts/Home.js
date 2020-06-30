

$(function () {
    "use strict";
    
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

    ////banner 轮播
    var bannerSwiper = new Swiper('.lr-site-banner-swiper-container', {
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
            // 加载子菜单
            if (_topMenuList[_item.F_Id]) {
                var $subList = $('<div class="lr-site-sub-nav-ul" data-value="' + _item.F_Id + '" ><div class="lr-site-content"><ul class="lr-site-sub-nav-menu"></ul></div></div>');
                var $subUl = $subList.find('ul');
                $.each(_topMenuList[_item.F_Id], function (_jindex, _jitem) {
                    var $jitem = $('<li class="lr-site-sub-nav-li site-menu-li"><a href="javascript:void(0);" class="lr-site-sub-nav-item">' + _jitem.F_Name + '</a></li>');
                    $jitem[0].data = _jitem;

                    // 加载三级子菜单
                    if (_topMenuList[_jitem.F_Id]) {
                        var $ul = $('<ul class="lr-site-three-nav-menu"></ul>');
                        $.each(_topMenuList[_jitem.F_Id], function (_mindex, _mitem) {
                            var $mitem = $('<li class="lr-site-three-nav-li site-menu-li"><a href="javascript:void(0);" class="lr-site-three-nav-item">' + _mitem.F_Name + '</a></li>');
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
    var renderBottomMenu = function (_bottomMenuList) {
        var $list = $('.lr-site-footer-nav-menu');
        $list.html('');
        $.each(_bottomMenuList, function (_index, _item) {
            var $item = $('<li class="lr-site-footer-nav-li site-menu-li"><a href="javascript:void(0);" class="lr-site-footer-nav-item">' + _item.F_Name + '</a></li>');
            $item[0].data = _item;
            $list.append($item);

        });
    }

    // 轮播图片
    var renderPicture = function (_pictureList) {
        var $swrapper = $('.lr-site-banner-swiper-container .swiper-wrapper');
        $swrapper.html("");
        if (_pictureList.length > 0) {
            $('.lr-site-banner-swiper-container').show();
            $('.lr-site-banner-default').hide();

            $.each(_pictureList, function (_index, _item) {
                var src = _item.src || ($.rootUrl + '/Home/GetImg2?keyValue=' + _item.F_Id);

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

    httpGet($.rootUrl + '/Home/GetAllList', function (res) {
        console.log(res);
        var topMenuList = {};
        var bottomMenuList = [];
        var pictureList = [];
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
                    case '9':
                        break;
                    case '11':
                        $('.lr-site-footer-wechat .text').text(_item.F_Name);
                        break;
                }
            });

            renderTopMenu(topMenuList);
            renderBottomMenu(bottomMenuList);
            renderPicture(pictureList);

        }

    });


    
   
});