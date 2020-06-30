/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.01.08
 * 描 述：力软移动端框架 图片选择插件（支持拍照和从相册中选择）
 */
(function ($, learun, fui, window) {
    "use strict";
    var _ft = null;

    // 选择图片从相册中
    learun.imagePick = function (callback, op) {
        if (learun.isreal) {
			// 从相册中选择图
			plus.gallery.pick(function(e){				
				!!callback && callback(e.files);
			}, function(e){
				!!callback && callback([]);
				//outSet('取消选择图片');
			},{filter:'image',multiple:true,maximum:op._maxCount,system:false,onmaxed:function(){
				plus.nativeUI.alert('最多能选择'+op._maxCount+'张图片');
			}});// 最多选择3张图片
        }
        else {
            learun.layer.warning('浏览器环境不支持图片选择', null, '力软提示', '好的');
        }
    };
    // 拍照获取图片
    learun.camera = function (callback) {
        if (learun.isreal) {
			var cmr = plus.camera.getCamera();
			cmr.captureImage(function(p){
				console.log(p);
				plus.gallery.save(p, function(e){
					//outLine('保存成功');
					callback(plus.io.convertLocalFileSystemURL(p));
				}, function(e){
					//outSet('保存失败: '+JSON.stringify(e));
				});
			}, function(e){
				console.log('失败：'+e.message);
			}, {filename:'_doc/gallery/',index:1});
        }
        else {
            learun.layer.warning('浏览器环境不支持拍照', null, '力软提示', '好的');
        }
    };
	
	

    var _imagepicker = {
        addImage: function (url, op, _fileId) {
            var $imagePicker = $('#' + op.id);
            var $imageHandle = $imagePicker.find('.lr-imagepicker-handle').parent();
            var _html = '\
                <div class="lr-imagepicker-item" >\
                    <img src="'+ url + '" />\
                    <div class="lr-imagepicker-remove" data-value="'+ _fileId + '" ><i class="iconfont icon-roundclosefill"></i><div></div></div>\
                </div>';
            $imageHandle.before(_html);
            $imageHandle = null;
			op._maxCount--;
        },
        upload: function (url, op, callback) {// 上传图片文件
            if (op.uploadUrl) {
                if (op.getParams) {
                    op.params = op.getParams();
                }
                op.params = op.params || {};
                op.params.data = op.value;
                _ft.upload(url, encodeURI(op.uploadUrl),
                    function (r) {
                        var _res = JSON.parse(r.response);
                        if (_res.code === 200) {
                            callback(true, url, op, _res.data);
                        }
                        else {
                            callback(false, url, op);
                        }
                    },
                    function (error) {
                        callback(false, url, op);
                    },
                    {
                        chunkedMode: false,
                        params: op.params || {}
                    }
                );
            }
            else {
                callback(true, url, op, learun.guid());
            }
        },
        uploads: function (data, index, op, callback, callback2) {
            if (data.length > index) {
                _imagepicker.upload(data[index], op, function (isOk, _url, _op, _fileId) {
                    callback(isOk, _url, _op, _fileId);
                    _imagepicker.uploads(data, index + 1, op, callback, callback2);
                });
            }
            else {
                callback2();
            }
        },
        down: function (imgUrl) {
			plus.downloader.createDownload(url, options, completedCB);
			
			
            cordova.plugins.photoLibrary.requestAuthorization(
                function () {
                    // User gave us permission to his library, retry reading it! 
                    cordova.plugins.photoLibrary.getLibrary(
                        function () {
                            //var url = 'file:///...'; // file or remote URL. url can also be dataURL, but giving it a file path is much faster 
                            var album = 'learunADMSApp';
                            cordova.plugins.photoLibrary.saveImage(imgUrl, album,
                                function (libraryItem) {
                                    learun.layer.toast('保存成功');
                                }, function (err) {
                                    learun.layer.toast('保存失败' + err);
                                });
                        },
                        function (err) {
                            if (err.startsWith('Permission')) {
                                // call requestAuthorization, and retry 
                            }
                            // Handle error - it's not permission-related 
                            console.log('权限' + err);

                        }
                    );
                },
                function (err) {
                    learun.layer.toast('用户拒绝访问' + + err);
                    // User denied the access 
                }, // if options not provided, defaults to {read: true}. 
                {
                    read: true,
                    write: true
                }
            );
        }
    };

    $.fn.imagepicker = function (op) {
        var dfop = {
            maxCount: 9,
            isOnlyCamera: false,
            params: {}
        };
        $.extend(dfop, op || {});
        var $this = $(this);
        $this[0].op = dfop;
        
        var id = $this.attr('id');
        if (!id) {
            id = learun.guid();
            $this.attr('id', id);
        }
        dfop.id = id;
        dfop.value = learun.guid();

		dfop._maxCount = dfop.maxCount;
       

        $this.addClass('lr-imagepicker');
        $this.attr('type','lrimagepicker');
        $this.html('\
            <div class="lr-imagepicker-item">\
                <div class="lr-imagepicker-handle" ><i class="iconfont icon-add1"></i></div >\
            </div>');
		
        $this.find('.lr-imagepicker-handle').on('tap', function () {
            var $this = $(this);
            if ($this.attr('readonly') || $this.parents('.lr-form-row').attr('readonly')) {
                return false;
            }

            var $imagePicker = $this.parents('.lr-imagepicker');
            var op = $imagePicker[0].op;
			
			if(op._maxCount <= 0){
				learun.layer.toast('最多能选择'+op.maxCount+'张图片');
				return;
			}
			

            if (dfop.isOnlyCamera) {
                learun.camera(function (res) {
                    learun.layer.loading(true, '正在上传...');
                    // 上传文件
                    _imagepicker.upload(res, op, function (isOk, _url, _op, _fileId) {
                        learun.layer.loading(false);
                        if (isOk) {
                            _imagepicker.addImage(_url, _op, _fileId);
                        }
                    });
                });
            } else {
                var _data = [{
                    text: '拍照',
                    event: function () {
                        learun.camera(function (res) {
                            learun.layer.loading(true, '正在上传...');
                            // 上传文件
                            _imagepicker.upload(res, op, function (isOk, _url, _op, _fileId) {
                                learun.layer.loading(false);
                                if (isOk) {
                                    _imagepicker.addImage(_url, _op, _fileId);
                                }
                            });
                        });
                    }
                }, {
                    text: '从手机相册选择',
                    event: function () {
                        learun.imagePick(function (res) {
                            learun.layer.loading(true, '正在上传...');
                            _imagepicker.uploads(res, 0, op, function (isOk, _url, _op, _fileId) {
                                if (isOk) {
                                    _imagepicker.addImage(_url, _op, _fileId);
                                }
                            }, function () {
                                learun.layer.loading(false);
                            });
                        },op);
                    }
                }];

                learun.actionsheet({
                    id: 'lrimagepicker',
                    data: _data
                });
            }

        });

        $this.delegate('.lr-imagepicker-remove>i', 'tap', { op: dfop }, function (e) {
            var op = e.data.op;
            var fileId = $(this).parent().attr('data-value');
            var $imapge = $(this).parent().parent();
            $imapge.remove();
            $imapge = null;
			
			op._maxCount++;

            op.deleteImg && op.deleteImg(fileId);
            return false;
        });

        $this.delegate('img', 'tap', { op: dfop }, function (e) {
            var op = e.data.op;

            var data = [];
            var fileId = $(this).parent().find('.lr-imagepicker-remove').attr('data-value');
            var _index = 0;
            $(this).parent().parent().find('img').each(function () {
                var _fileId = $(this).parent().find('.lr-imagepicker-remove').attr('data-value');
                var src = $(this).attr('src');
                if (_fileId === fileId) {
                    _index = data.length;
                }
                data.push(src);
            });
            fui.imagePreview({
                data: data, gotonum: _index,
                holdEvent: function (_src) {
					return;
					
                    learun.actionsheet({
                        id: 'lrimagepicker2',
                        data: [{
                        text: '保存图片',
                        event: function () {
                            if (_src.indexOf('http://') !== -1) {
                                _imagepicker.down(_src, op);
                            }
                            else {
                                learun.layer.toast('本地图片无需保存');
                            }
                        }
                    }]
                    });
                }
            });
            return false;
        });

        return $this;
    };

    $.fn.imagepickerGet = function () {
        var $this = $(this);
        if ($this.hasClass('lr-imagepicker')) {
            var _op = $this[0].op;
            $this = null;
            return _op.value;
        }
        $this = null;
        return '';
    }

    $.fn.imagepickerSet = function (value) {
        if (value != undefined && value != null && value != '' && value != 'undefined' && value != 'null') {
            var $this = $(this);
            if ($this.hasClass('lr-imagepicker')) {
                var op = $this[0].op;
                op.value = value;
                op.downFile(value, function (data) {
                    $.each(data, function (index, _item) {
                        _imagepicker.addImage(op.downUrl + _item.name, op, _item.id);
                    });
                });
            }
        }
    }

})(window.jQuery, window.lrmui, window.fui, window);