/* * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-08-20 15:04
 * 描  述：其他页面配置
 */
var acceptClick;
var keyValue = request('keyValue');
var type = request('type');

var titleName = '';
var categoryData = null;
var activeItem = null;

var bootstrap = function ($, learun) {
    "use strict";

    function uploadImg() {
        var f = document.getElementById('uploadFile').files[0]
        var src = window.URL.createObjectURL(f);
        document.getElementById('uploadPreview').src = src;
    };

    function loadPage(pageIndex) {
        var pagination = {
            rows: 10,                // 每页行数      
            page: pageIndex,         // 当前页
            sidx: 'F_PushDate',      // 排序列
            sord: 'DESC',            // 排序类型
            records: 0,              // 总记录数
            total: 0                 // 总页数
        };
        var param = {
            pagination: JSON.stringify(pagination),
            queryJson: JSON.stringify({ F_Category: activeItem.category })
        };
        learun.httpAsync('GET', top.$.rootUrl + '/LR_PortalSite/Article/GetPageList', param, function (data) {
            var $list = $('#lr_body_cotent');
            $list.html('');
            if (type == '1') {
                $.each(data.rows, function (_index, _item) {
                    var $item = $('<div class="lr-site-body-list-item">\
                                        <div class="text">' + _item.F_Title + '</div>\
                                        <div class="date">' + learun.formatDate(_item.F_PushDate, 'yyyy-MM-dd') + '</div>\
                                    </div>');
                    $list.append($item);
                });
            }
            else {
                $.each(data.rows, function (_index, _item) {
                    var $item = $('<div class="col-md-4 col-sm-6 lr-site-img-item">\
                                    <div class="lr-site-img-content2">\
                                        <img class="img" src="' + top.$.rootUrl + '/LR_PortalSite/Article/GetImg?keyValue=' + _item.F_Id + '" />\
                                        <div class="text" title="' + _item.F_Title + '" >' + _item.F_Title + '</div>\
                                    </div>\
                                </div>');
                    $list.append($item);
                });
            }

            resetPage(data);
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

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 图片上传
            $('#uploadFile').on('change', uploadImg);
            $('.file').prepend('<img id="uploadPreview"  src="' + top.$.rootUrl + '/LR_PortalSite/Page/GetImg?keyValue=' + keyValue + '" >');

            // 设置左右侧标题
            $('.lr-site-body-left .lr-site-title').on('click', function () {
                var $this = $(this);
                titleName = $this.find('span').text();
                learun.layerForm({
                    id: 'settingTitle',
                    title: '设置名称',
                    url: top.$.rootUrl + '/LR_PortalSite/Page/SetNameForm',
                    width: 350,
                    height: 140,
                    callBack: function (id) {
                        return top[id].acceptClick(function (name) {
                            $('.left-title').find('span').text(name);
                        });
                    }
                });
            });

            // 添加分类项
            $('#add_item').on('click', function () {
                categoryData = null;
                learun.layerForm({
                    id: 'SetCategoryForm',
                    title: '添加分类项',
                    url: top.$.rootUrl + '/LR_PortalSite/Page/SetCategoryForm?type=' + type,
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (_data) {
                            var $item = $('<div class="lr-site-title-item value-item"><div class="btn-list" ><span>上移</span><span>下移</span><span>编辑</span><span>删除</span></div><span class="itemname" >' + _data.F_Name + '</span></div>');
                            $item[0].data = {
                                name: _data.F_Name,
                                category: _data.F_Category,
                                article: _data.F_Article
                            }
                            $('#add_item').before($item);
                            $item.trigger('click');
                        });
                    }
                });
            });
            // 编辑分类项
            $('.lr-site-body-left').on('click', '.btn-list>span', function () {
                var $this = $(this);
                var $item = $this.parents('.lr-site-title-item');
                var text = $this.text();
                switch (text) {
                    case '上移':
                        $item.prev().before($item);
                        break;
                    case '下移':
                        if (!$item.next().hasClass('active2')) {
                            $item.next().after($item);
                        }
                        break;
                    case '编辑':
                        categoryData = $item[0].data;
                        learun.layerForm({
                            id: 'SetCategoryForm',
                            title: '编辑分类项',
                            url: top.$.rootUrl + '/LR_PortalSite/Page/SetCategoryForm?type=' + type,
                            width: 400,
                            height: 300,
                            callBack: function (id) {
                                return top[id].acceptClick(function (_data) {
                                    $item.find('.itemname').text(_data.F_Name);
                                    $item[0].data.name = _data.F_Name;
                                    $item[0].data.category = _data.F_Category;
                                    $item[0].data.article = _data.F_Article;
                                });

                                $item.trigger('click');
                            }
                        });
                        break;
                    case '删除':
                        $item.remove();
                        break;
                }

                return false;
            });

            $('.lr-site-body-left').on('click', '.value-item', function () {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    $this.parent().find('.active').removeClass('active');
                    $this.addClass('active');
                    var data = $this[0].data;

                    $('.right-title>span').text(data.name);

                    activeItem = data;

                    if (type == '3') {
                        learun.httpAsync('GET', top.$.rootUrl + '/LR_PortalSite/Article/GetFormData', { keyValue: activeItem.article }, function (data) {
                            var $list = $('#lr_body_cotent');
                            $list.html(data.F_Content);
                        });
                    }
                    else {
                        loadPage(1);
                    }
                }

            });

            switch (type) {
                case '1':// 列表
                    $('#lr_body_cotent').addClass('lr-site-body-list');
                    break;
                case '2':// 图片列表
                    $('#lr_body_cotent').addClass('row');
                    break;
                case '3':// 详情页
                    $('#lr_body_cotent').addClass('lr-site-body-list');
                    break;
            }

            $('#save').on('click', function () {
                if (!$('.container-top').lrValidform()) {
                    return false;
                }
                var formData = $('.container-top').lrGetFormData();

                // 获取左侧分类数据
                var scheme = {
                    title:$('.left-title>span').text(),
                    list:[]
                };

                $('.lr-site-body-left .value-item').each(function(){
                    var item = $(this)[0].data;
                    scheme.list.push(item);
                });

                var postData = {
                    F_Img: formData.F_Img,
                    F_Title: formData.F_Title,
                    F_Type: type,
                    F_Scheme: JSON.stringify(scheme).replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;' }[c]; })
                }
                learun.loading(true, '正在保存...');
                $.ajaxFileUpload({
                    data: postData,
                    url: top.$.rootUrl + "/LR_PortalSite/Page/UploadFile?keyValue=" + keyValue,
                    secureuri: false,
                    fileElementId: 'uploadFile',
                    dataType: 'json',
                    success: function (data) {
                        learun.loading(false);
                        if (data.code = "200") {
                            // 保存成功后才回调
                            learun.frameTab.parentIframe().refreshGirdData();//index页列表刷新
                            learun.frameTab.close(learun.frameTab.iframeId);//关闭新增界面
                        }
                    }
                });

            });
        },
        initData: function () {
            if (keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_PortalSite/Page/GetFormData?keyValue=' + keyValue, function (data) {
                    $('.container-top').lrSetFormData(data);
                    var _scheme = JSON.parse(data.F_Scheme);

                    $('.left-title>span').text(_scheme.title);

                    var $add = $('#add_item');
                    $.each(_scheme.list, function (_index, _item) {
                        var $item = $('<div class="lr-site-title-item value-item"><div class="btn-list" ><span>上移</span><span>下移</span><span>编辑</span><span>删除</span></div><span class="itemname" >' + _item.name + '</span></div>');
                        $item[0].data = _item;
                        $add.before($item);

                        if (_index == 0) {
                            $item.trigger('click');
                        }
                    });
                });
            }
        }
    };
    page.init();
}
