/*
 * 版 本 Learun-Mobile V2.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.12.15
 * 描 述：力软移动端框架 条码扫描插件
 */

function lrearunscaned(t, r, f){
	console.log(r);
	lrearunscanedcallback && lrearunscanedcallback({status: 'success',msg: r});
}
var lrearunscanedcallback = null;

(function ($, learun, fui, window) {
    "use strict";
    learun.code = {
        scan: function (callback) {
            if (learun.isreal) {
				lrearunscanedcallback = callback;
				learun.createWithoutTitle('/puls/barcode_scan.html', {
					titleNView:{
						type: 'float',
						backgroundColor: 'rgba(215,75,40,0.3)',
						titleText: '扫一扫',
						titleColor: '#FFFFFF',
						autoBackButton: true
					}
				});
            }
            else {
                learun.layer.warning('浏览器环境不支持扫描', null, '力软提示', '好的');
            }
        },
        encode: function (op) {// id:div的id，text：需要生成的文本；width：宽度；height：高度；colorDark，colorLight；
            var qrcode = new QRCode(document.getElementById(op.id), {
                text: op.text || "http://www.learun.cn/",
                width: op.width || 128,
                height: op.height || 128,
                colorDark: op.colorDark || "#000000",
                colorLight: op.colorLight || "#ffffff",
                correctLevel: QRCode.CorrectLevel.H
            });
        }
    };


})(window.jQuery, window.lrmui, window.fui, window);