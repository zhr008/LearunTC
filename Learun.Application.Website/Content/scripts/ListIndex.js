

$(function () {
    "use strict";

    new Swiper('.lr-site-swiper-container', {
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

    laypage({
        cont: "lr_page", //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
        pages: 10, //通过后台拿到的总页数
        curr: 0, //当前页
        groups: 5, //连续显示分页数
        skip: true, //是否开启跳页
        first: '首页', //若不显示，设置false即可
        last: '尾页', //若不显示，设置false即可
        jump: function (obj, first) { //触发分页后的回调
            return;

            if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                pageIndex = obj.curr;
                pageQuery(pageIndex);
                document.getElementById('newlist').innerHTML = newsDate(data.rows);
            }
        }
    });

});