

$(function () {
    "use strict";

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

    /*模块3标签页切换*/
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
    $('.lr-site-modules').on('click', '.lr-site-more', function () {
        var $this = $(this);
        var data = $this.parents('.lr-site-module')[0].data;

        if (data.F_Url) {
            window.location.href = $.rootUrl + '/Home/ChildIndex?id=' + data.F_Url + '&module=' + escape(data.F_Name);
        }
    });
    $('.lr-site-modules').on('click', '.lr-site-article-list-item', function () {
        var data = $(this)[0].data;
        window.open($.rootUrl + '/Home/DetailIndex?id=' + data.F_Id);        
        return false;
    });


    var renderModule = function (data) {
        var schemeObj = JSON.parse(data.F_Scheme);
        var $item;
        var $list = $('.lr-site-modules');
        switch (schemeObj.type) {
            case '1':// 风格一
                $item = $('<div class="lr-site-module module1">\
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
                               </div>\
                           </div>');
                $item.find('.content').SiteCarousel();
                var $ul = $item.find('ul');
                $ul.html('');
                $.each(schemeObj.list, function (_index, _item) {
                    var _$li = $('<li><a href="javascript:void(0);" class="lr-text-item lr-site-article-list-item">' + _item.F_Title + '</a></li>');
                    _$li[0].data = _item;
                    $ul.append(_$li);
                });
                $list.append($item);
                break;
            case '2':
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
                               </div>\
                           </div>');
                $list.append($item);

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
                $item.css({ width: parseFloat(schemeObj.prop) * 100 + '%' });
                // 加载图片
                if (schemeObj.list1.length == 0) {
                    $item.addClass('noimg');
                }
                else {
                    $item.removeClass('noimg');
                    var $swrapper = $item.find('.swiper-wrapper');
                    $swrapper.html('');
                    $.each(schemeObj.list1, function (_index, _item) {
                        var $li = $('<div class="swiper-slide lr-site-article-list-item">\
                                   <img class="img"  src="' + top.$.rootUrl + '/Home/GetArticleImg?keyValue=' + _item.F_Id + '" />\
                               </div>');

                        $li[0].data = _item;
                        $swrapper.append($li);
                    });

                    $item[0].swiper.update();
                }
                // 加载文字列表
                var $textList = $item.find('.lr-site-module-list');
                $textList.html('');
                $.each(schemeObj.list2, function (_index, _item) {
                    var $li = $('<div class="lr-site-module-item lr-site-article-list-item">\
                                    <div class="text">'+ _item.F_Title + '</div>\
                                    <div class="date">' + formatDate(_item.F_PushDate, 'yyyy-MM-dd') + '</div>\
                                </div>');

                    $li[0].data = _item;
                    $textList.append($li);
                });
                break;
            case '3':
                $item = $('<div class="lr-site-module module3" data-value="' + data.F_Id + '" >\
                               <div class="lr-site-box">\
                                   <div class="lr-site-tabs"></div>\
                                   <div class="lr-site-body">\
                                    </div>\
                               </div>\
                           </div>');
                $list.append($item);
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
                        var _$jli = $('<div class="lr-site-tab-content-item lr-site-article-list-item">' + _jitem.F_Title + '</div>');
                        _$jli[0].data = _jitem;

                        $tabContent.append(_$jli);
                    });

                    tabBody.append($tabContent);
                    if (_index == 0) {
                        $li.trigger('click');
                    }
                });
                break;
            case '4':
                $item = $('<div class="lr-site-module module4" data-value="' + data.F_Id + '" >\
                               <div class="lr-site-box">\
                                   <div class="lr-site-title"><span class="name" >' + data.F_Name + '</span><div class="lr-site-more">' + (data.F_Url ? '更多' : '') + '</div></div>\
                                   <div class="lr-site-body">\
                                    </div>\
                               </div>\
                           </div>');
                $list.append($item);
                var $body = $item.find('.lr-site-body');
                $body.html('');
                $.each(schemeObj.list, function (_index, _item) {
                    var _$li = $('<div class="lr-site-img-item lr-site-article-list-item">\
                                        <div class="lr-site-img-item-content">\
                                            <img class="img"  src="' + $.rootUrl + '/Home/GetArticleImg?keyValue=' + _item.F_Id + '" />\
                                            <div class="text">' + _item.F_Title + '</div>\
                                        </div>\
                                    </div>');
                    _$li[0].data = _item;
                    $body.append(_$li);
                });
                break;
            case '5':
                $item = $('<div class="lr-site-module module5" data-value="' + data.F_Id + '" >\
                               <div class="lr-site-box lr-site-article-list-item">\
                                   <div class="lr-site-title"><span class="name" >' + data.F_Name + '</span></div>\
                                   <div class="lr-site-body">\
                                        <div class="lr-site-article-pic">\
                                            <img class="img" src="' + $.rootUrl + '/Home/GetArticleImg?keyValue=' + schemeObj.article + '" />\
                                        </div>\
                                        <div class="text-content">\
                                            <div class="text" data-text="' + schemeObj.article + '" ></div>\
                                        </div>\
                                    </div>\
                               </div>\
                           </div>');
                $list.append($item);

                $item.find('.lr-site-box')[0].data = { F_Id: schemeObj.article };
                $item.css({ width: parseFloat(schemeObj.prop) * 100 + '%' });

                httpGet($.rootUrl + '/Home/GetArticle?keyValue=' + schemeObj.article, function (res) {
                    if (res && res.code == 200) {
                        var data = res.data;

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

    httpGet($.rootUrl + '/Home/GetList?type=9', function (res) {
        if (res && res.code == 200) {
            $.each(res.data, function (_index, _item) {
                renderModule(_item);
            });
        }
    });
    
    
});