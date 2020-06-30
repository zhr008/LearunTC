(function (a) {
    if (!a.browser) {
        a.browser = {};
        a.browser.mozilla = !1;
        a.browser.webkit = !1;
        a.browser.opera = !1;
        a.browser.msie = !1;
        var b = navigator.userAgent;
        a.browser.name = navigator.appName;
        a.browser.fullVersion = "" + parseFloat(navigator.appVersion);
        a.browser.majorVersion = parseInt(navigator.appVersion, 10);
        var f, c; - 1 != (c = b.indexOf("Opera")) ? (a.browser.opera = !0, a.browser.name = "Opera", a.browser.fullVersion = b.substring(c + 6), -1 != (c = b.indexOf("Version")) && (a.browser.fullVersion = b.substring(c + 8))) : -1 != (c = b.indexOf("MSIE")) ? (a.browser.msie = !0, a.browser.name = "Microsoft Internet Explorer", a.browser.fullVersion = b.substring(c + 5)) : -1 != (c = b.indexOf("Chrome")) ? (a.browser.webkit = !0, a.browser.name = "Chrome", a.browser.fullVersion = b.substring(c + 7)) : -1 != (c = b.indexOf("Safari")) ? (a.browser.webkit = !0, a.browser.name = "Safari", a.browser.fullVersion = b.substring(c + 7), -1 != (c = b.indexOf("Version")) && (a.browser.fullVersion = b.substring(c + 8))) : -1 != (c = b.indexOf("Firefox")) ? (a.browser.mozilla = !0, a.browser.name = "Firefox", a.browser.fullVersion = b.substring(c + 8)) : (f = b.lastIndexOf(" ") + 1) < (c = b.lastIndexOf("/")) && (a.browser.name = b.substring(f, c), a.browser.fullVersion = b.substring(c + 1), a.browser.name.toLowerCase() == a.browser.name.toUpperCase() && (a.browser.name = navigator.appName)); - 1 != (b = a.browser.fullVersion.indexOf(";")) && (a.browser.fullVersion = a.browser.fullVersion.substring(0, b)); - 1 != (b = a.browser.fullVersion.indexOf(" ")) && (a.browser.fullVersion = a.browser.fullVersion.substring(0, b));
        a.browser.majorVersion = parseInt("" + a.browser.fullVersion, 10);
        isNaN(a.browser.majorVersion) && (a.browser.fullVersion = "" + parseFloat(navigator.appVersion), a.browser.majorVersion = parseInt(navigator.appVersion, 10));
        a.browser.version = a.browser.majorVersion
    }
})(jQuery);
(function (a) {
    var b;
    a.fn.jqprint = function (f) {
        b = a.extend({}, a.fn.jqprint.defaults, f);
        f = this instanceof jQuery ? this : a(this);
        if (b.operaSupport && a.browser.opera) {
            var c = window.open("", "jqPrint-preview");
            c.document.open();
            var d = c.document
        } else {
            var e = a("<iframe  />");
            b.debug || e.css({
                position: "absolute",
                width: "0px",
                height: "0px",
                left: "-600px",
                top: "-600px"
            });
            e.appendTo("body");
            d = e[0].contentWindow.document
        }
        d.write("<title></title><style>@page{size: auto;margin: 0mm;}html{background-color: #FFFFFF;margin: 0px;}body{border: solid 1px blue;margin: 10mm 15mm 10mm 15mm;}</style >");
        b.importCSS && (0 < a("link[media=print]").length ? a("link[media=print]").each(function () {
            d.write("<link type='text/css' rel='stylesheet' href='" + a(this).attr("href") + "' media='print' />")
        }) : a("link").each(function () {
            if (a(this).attr("href") != '/favicon.ico') {
                d.write("<link type='text/css' rel='stylesheet' href='" + a(this).attr("href") + "' />")
            }
        }), d.write("<style>" + a("style").html() + "</style>"));

        b.printContainer ? d.write(f.outer()) : f.each(function () {
           d.write(a(this).html())
        });

        d.close();
        (b.operaSupport && a.browser.opera ? c : e[0].contentWindow).focus();
        setTimeout(function () {
            (b.operaSupport && a.browser.opera ? c : e[0].contentWindow).print();
            c && c.close()
        }, 1E3)
    };
    a.fn.outer = function () {
        for (var b = this.clone(), c = a(this), b = a(b), d = 0; 100 > d; d++) {
            var e = c.parent().prop("nodeName").toLowerCase(),
                b = a("<" + e + "></" + e + ">").attr("style", c.parent().attr("style")).addClass(c.parent().attr("class")).html(b.clone());
            if ("body" == e) break;
            c = c.parent()
        }
        a(this).find('input').each(function () {
            var $this = $(this);
            var type = $this.attr('type');
            var _id = $(this).attr('id');
            if (type == 'radio') {
                if ($this.is(':checked')) {
                    var _name = $(this).attr('name');
                    var _value = $(this).attr('value');
                    b.find('[name="' + _name + '"][value="' + _value + '"]').attr('checked', 'checked');
                }
            }
            else {
                b.find('#' + _id).attr('value', $(this).val());
            }
        });


        return a(a("<div></div>").html(b)).html()
    }

    // 力软信息扩展
    a.fn.jqprintTable = function (f) {
        b = a.extend({}, a.fn.jqprint.defaults, f);
        f = this instanceof jQuery ? this : a(this);
        if (b.operaSupport && a.browser.opera) {
            var c = window.open("", "jqPrint-preview");
            c.document.open();
            var d = c.document
        } else {
            var e = a("<iframe  />");
            b.debug || e.css({
                position: "absolute",
                width: "0px",
                height: "0px",
                left: "-600px",
                top: "-6000px"
            });
            e.appendTo("body");
            d = e[0].contentWindow.document
        }
        d.write("<title></title><style>@page{size: auto;margin: 0mm;}html{background-color: #FFFFFF;margin: 0px;}body{border: solid 1px blue;margin: 10mm 15mm 10mm 15mm;}</style >");

        d.write('<style>');
        d.write('table{margin: 30px;border-collapse: collapse;width: 100%;border-left: 1px solid #ccc;}');
        d.write('th{border-top: 1px solid #ccc;border-bottom: 1px solid #ccc;border-right: 1px solid #ccc;padding-top:5px;padding-bottom:5px;text-overflow:ellipsis;word-break:keep-all;overflow:hidden;font-weight:bold;padding-left:5px;padding-right:5px;font-size: 12px;}');
        d.write('td{border-bottom: 1px solid #ccc;border-right: 1px solid #ccc;height:25px;line-height:25px;word-break: break-all;padding-left:5px;padding-right:5px;font-size: 12px;}');
        d.write('</style>');

        b.importCSS && (0 < a("link[media=print]").length ? a("link[media=print]").each(function () {
            d.write("<link type='text/css' rel='stylesheet' href='" + a(this).attr("href") + "' media='print' />")
        }) : a("link").each(function () {
            d.write("<link type='text/css' rel='stylesheet' href='" + a(this).attr("href") + "' />")
            }),
            d.write("<style>" + a("style").html() + "</style>"));
        b.printContainer ? d.write(f.outerTable(b)) : f.each(function () {
            d.write(a(this).html())
        });
        d.close();
        (b.operaSupport && a.browser.opera ? c : e[0].contentWindow).focus();
        setTimeout(function () {
            (b.operaSupport && a.browser.opera ? c : e[0].contentWindow).print();
            c && c.close()
        }, 1E3)
    };

    a.fn.outerTable = function (b) {
        var $div = a('<div style="-moz-box-sizing: border-box;-webkit-box-sizing: border-box;box-sizing: border-box;padding:5px;width:100%;position:relative;"></div>');

        if (!!b.title)
        {
            $div.html('<div style="text-align:center;font-size: 22px;color: #444;width:100%;">' + b.title+'</div>');
        }

        $div.append(a(this).jfGridPrint());
        return a('<div></div>').html($div).html();
    };

    //// 打印表单
    a.jqprintForm = function (data) {
        b = a.fn.jqprint.defaults;
        if (b.operaSupport && a.browser.opera) {
            var c = window.open("", "jqPrint-preview");
            c.document.open();
            var d = c.document
        } else {
            var e = a("<iframe  />");
            b.debug || e.css({
                position: "absolute",
                width: "0px",
                height: "0px",
                left: "-600px",
                top: "-600px"
            });
            e.appendTo("body");
            d = e[0].contentWindow.document
        }
        d.write("<title></title><style>@page{size: auto;margin: 0mm;}html{background-color: #FFFFFF;margin: 0px;}body{border: solid 1px blue;margin: 10mm 15mm 10mm 15mm;}</style >");
        d.write("<link type='text/css' rel='stylesheet' href='" + top.$.rootUrl + "/Content/jquery/plugin/jqprint/jqprint.css' />");
        var $table = a('<table></table>');
        var num = 24;
        var $tr = a('<tr></tr>');
        $.each(data, function (_index, _item) {
            if (_item.type == 'gird' && _item.data.length > 0) {
                if (num > 0 && num < 24) {
                    $table.append($tr);
                    $tr = a('<tr></tr>');
                    num = 24;
                }
                var _colp = parseInt(24 / _item.gridHead.length);
                var _colp_last = 24 - ((_item.gridHead.length - 1) * _colp);
                console.log(_colp_last);
                $.each(_item.gridHead, function (_i, _h) {

                    if (_i == _item.gridHead.length - 1) {
                        $tr.append('<th colspan="' + _colp_last + '">' + _h.label + '</th>');
                    }
                    else {
                        $tr.append('<th colspan="' + _colp + '">' + _h.label + '</th>');
                    }
                });
                $table.append($tr);
                $.each(_item.data, function (_j, _b) {
                    $tr = a('<tr></tr>');
                    $table.append($tr);
                    $.each(_item.gridHead, function (_i, _h) {
                        if (_i == _item.gridHead.length - 1) {
                            $tr.append('<td colspan="' + _colp_last + '">' + _b[_h.name] + '</td>');
                        }
                        else {
                            $tr.append('<td colspan="' + _colp + '">' + _b[_h.name] + '</td>');
                        }
                    });
                });

                $tr = a('<tr></tr>');
            }
            else if (_item.type == 'input') {
                var colspan = _item.col * 2 - 1;
                var _text = _item.text;
                if (_item.textList) {
                    _text = String(_item.textList);
                }

                var $td = a('<td class="input" >' + _text + '</td>');
                $td.attr('colspan', colspan);
                $tr.append('<td class="input inputname" >' + _item.name + ':</td>');
                $tr.append($td);
                num = num - colspan - 1;
                if (num <= 0) {
                    num = 24;
                    $table.append($tr);
                    $tr = a('<tr></tr>');
                }

            }
            else if (_item.type == 'anodeinfo'){
                if (num >= 0 && num < 24) {
                    $table.append($tr);
                    $tr = a('<tr></tr>');
                    num = 24;
                }
                

                $tr.append('<td class="input inputname" >' + _item.title + ':</td>');
                var __$td = a('<td class="anodeinfo" colspan="23" ><div class="anodeinfod" >' + _item.content + '</div></td>');
                var _$div = __$td.find('div');

                _$div.append('<div class="tuser" >' + _item.user + '</div>');
                _$div.append('<div class="tdate" >' + _item.date + '</div>');
                if (_item.signImg) {
                    _$div.append(_item.signImg);
                }
                if (_item.stampImg) {
                    _$div.append(_item.stampImg );
                }

                $tr.append(__$td);

                $table.append($tr);
                $tr = a('<tr></tr>');
            }
            else {
                var _colspan = _item.col * 2;
                var _$td = a('<td class="titletd" >' + _item.text + '</td>');
                _$td.attr('colspan', _colspan);
                $tr.append(_$td);
                num = num - _colspan;
                if (num <= 0) {
                    num = 24;
                    $table.append($tr);
                    $tr = a('<tr></tr>');
                }
            }


        });

        if (num > 0 && num < 24) {
            $table.append($tr);
        }
        d.write(a('<div></div>').html($table).html());
        console.log(d);
        d.close();
        (b.operaSupport && a.browser.opera ? c : e[0].contentWindow).focus();
        setTimeout(function () {
            (b.operaSupport && a.browser.opera ? c : e[0].contentWindow).print();
            c && c.close()
        }, 1E3)
    }



    a.fn.jqprint.defaults = {
        debug: !1,
        importCSS: !0,
        printContainer: !0,
        operaSupport: !0
    };
})(jQuery);