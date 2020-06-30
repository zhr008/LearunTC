/*
 * 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2020 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.12.11
 * 描 述：工作流操作界面 （会向系统表单页面传递create,againCreate,agree,disagree,sign,signAudit,自定义按钮编码）
 */
var processId = request('processId');      // 流程实例主键
var nodeId = request('nodeId');            // 流程节点

var bootstrap = function ($, learun) {
    "use strict";
    // 表单页面对象集合
    var formIframesMap = {};
    var formIframes = [];
    var formIframesHave = {};

    // 自定义表单
    var custmerForm = {
        loadForm: function (formList, isLoadData, isLook) {
            var $ul = $('#form_list_tabs');
            var $iframes = $('#form_list_iframes');
            $.each(formList, function (_index, _item) {
                $ul.append('<li><a data-value="' + _index + '" >' + _item.name + '</a></li>');
                if (_item.type == '1') {// 自定义表单
                    $iframes.append('<div id="wfFormContainer' + _index + '" class="form-list-container" data-value="' + _index + '" ></div>');
                    _item._index = _index;
                    custmerForm.init(_item, $iframes.find('#wfFormContainer' + _index), isLoadData, isLook);

                }
                else {// 系统表单
                    $iframes.append('<iframe id="wfFormIframe' + _index + '" class="form-list-iframe" data-value="' + _index + '" frameborder="0" ></iframe>');
                    page.iframeLoad("wfFormIframe" + _index, _item.url, function (iframeObj, formData) {
                        // 设置字段权限
                        iframeObj.setAuthorize && iframeObj.setAuthorize(formData.authorize, isLook);
                        iframeObj.setFormData && iframeObj.setFormData(processId);
                    }, _item);
                }
                if (_index == 0) {
                    $ul.find('a').trigger('click');
                }
            });
        },
        init: function (formInfo, $container, isLoadData, isLook) {
            custmerForm.getScheme(formInfo.formId, function (data) {
                if (data) {
                    var formScheme = JSON.parse(data.schemeEntity.F_Scheme);
                    formInfo.formScheme = formScheme;

                    // 编辑表格权限
                    var girdMap = {};
                    var _flag = false;
                    $.each(formInfo.authorize || [], function (_field, _item) {
                        var _ids = _field.split('|');
                        if (_ids.length > 1) {
                            if (_item.isLook != 1 || _item.isEdit != 1) {
                                girdMap[_ids[0]] = girdMap[_ids[0]] || {};
                                girdMap[_ids[0]][_ids[1]] = _item;
                                _flag = true;
                            }
                        }
                    });
                    if (_flag) {
                        $.each(formScheme.data, function (_i, _item) {
                            $.each(_item.componts, function (_j, _jitem) {
                                if ((_jitem.type == 'girdtable' || _jitem.type == 'gridtable') && !!girdMap[_jitem.id]) {
                                    var _gird = girdMap[_jitem.id];
                                    var _fieldsData = [];
                                    $.each(_jitem.fieldsData, function (_m, _mitem) {
                                        if (!_gird[_mitem.id] || _gird[_mitem.id].isLook == 1) {
                                            _fieldsData.push(_mitem);
                                            if (_gird[_mitem.id] && _gird[_mitem.id].isEdit != 1) {
                                                _mitem._isEdit = 1;
                                            }
                                        }
                                    });
                                    _jitem.fieldsData = _fieldsData;
                                }
                            });
                        });
                    }

                    formInfo.girdCompontMap = $container.lrCustmerFormRender(formScheme.data);

                    // 表单组件权限
                    $.each(formInfo.authorize || [], function (_field, _item) {
                        var _ids = _field.split('|');
                        if (_ids.length == 1) {
                            if (_item.isLook != 1) {// 如果没有查看权限就直接移除
                                $('#' + _ids[0]).parent().remove();
                                $('[name="' + _ids[0] + '"]').parents('.lr-form-item').attr('disabled', 'disabled');
                            }
                            else {
                                if (_item.isEdit != 1) {
                                    $('#' + _ids[0]).attr('disabled', 'disabled');
                                    if ($('#' + _ids[0]).hasClass('lrUploader-wrap')) {
                                        $('#' + _ids[0]).css({ 'padding-right': '58px' });
                                        $('#' + _ids[0]).find('.btn-success').remove();
                                    }
                                }
                            }
                        }
                    });

                    if (isLook) {// 当前是查看状态
                        $('.lrUploader-wrap').css({ 'padding-right': '58px' }).find('.btn-success').remove();
                    }

                    // 获取表单数据
                    if (isLoadData) {
                        custmerForm.getFormData(formInfo, processId, formInfo.field, function (formData, _formInfo) {
                            $.each(formData, function (id, item) {
                                if (_formInfo.girdCompontMap[id]) {
                                    var fieldMap = {};
                                    $.each(_formInfo.girdCompontMap[id].fieldsData, function (id, girdFiled) {
                                        if (girdFiled.field) {
                                            fieldMap[girdFiled.field.toLowerCase()] = girdFiled.field;
                                        }
                                    });
                                    var rowDatas = [];
                                    for (var i = 0, l = item.length; i < l; i++) {
                                        var _point = {};
                                        for (var _field in item[i]) {
                                            _point[fieldMap[_field]] = item[i][_field];
                                        }
                                        rowDatas.push(_point);
                                    }
                                    if (rowDatas.length > 0) {
                                        _formInfo.isUpdate = true;
                                    }
                                    $('#' + _formInfo.girdCompontMap[id].id).jfGridSet('refreshdata', { rowdatas: rowDatas });
                                }
                                else {
                                    if (item[0]) {
                                        _formInfo.isUpdate = true;
                                        $('#wfFormContainer' + _formInfo._index).lrSetCustmerformData(item[0], id);
                                    }
                                }

                            });

                            $.each(_formInfo.authorize || [], function (_field, _item) {
                                if (_item.isLook == 1 && _item.isEdit != 1) {// 如果没有查看权限就直接移除
                                    $('[name="' + _field + '"]').attr('disabled', 'disabled');
                                }
                            });
                        });
                    }

                }
            });
        },
        // 获取数据
        getScheme: function (formId, callback) {
            learun.httpAsync('GET', top.$.rootUrl + '/LR_FormModule/Custmerform/GetFormData', { keyValue: formId }, function (data) {
                callback && callback(data);
            });
        },
        getFormData: function (formInfo, processId, processIdName, callback) {
            learun.httpAsync('GET', top.$.rootUrl + '/LR_FormModule/Custmerform/GetInstanceForm', { schemeInfoId: formInfo.formId, keyValue: processId, processIdName: processIdName }, function (data) {
                callback && callback(data, formInfo);
            });
        }
    };

    var page = {
        init: function () {
            page.bind();

            // 获取表单节点
            var nodeInfo;
            var schemeObj = top['lrwfschemeObj'];

            $.each(schemeObj.nodes, function (_index, _node) {
                if (_node.id == nodeId) {
                    nodeInfo = _node;
                    return false;
                }
            });

            custmerForm.loadForm(nodeInfo.wfForms, true, true);

        },
        bind: function () {
            // 表单选项卡点击事件
            $('#form_list_tabs').on('click', 'a', function () {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    $this.parents('ul').find('.active').removeClass('active');
                    $this.parent().addClass('active');

                    var value = $this.attr('data-value');
                    var $iframes = $('#form_list_iframes');
                    $iframes.find('.active').removeClass('active');
                    $iframes.find('[data-value="' + value + '"]').addClass('active');
                }
            });
            // 打印表单
            $('#print').on('click', function () {
                var $iframes = $('#form_list_iframes');
                var iframeId = $iframes.find('.form-list-iframe.active').attr('id');
                if (iframeId) {
                    var $iframe = learun.iframe(iframeId, frames);
                    $iframe.$('.lr-form-wrap:visible').jqprint();
                }
                else {
                    $iframes.find('.form-list-container.active').find('.lr-form-wrap:visible').jqprint();
                }
            });
            $('#print').show();
        },
        // iframe 加载
        iframeLoad: function (iframeId, url, callback, formData) {
            var _iframe = document.getElementById(iframeId);
            var _iframeLoaded = function () {
                var iframeObj = learun.iframe(iframeId, frames);

                if (formIframesMap[iframeId] != undefined) {
                    formIframes[formIframesMap[iframeId]] = iframeObj;
                }
                else {
                    formIframesMap[iframeId] = formIframes.length;
                    formIframes.push(iframeObj);
                }

                if (!!iframeObj.$) {
                    callback(iframeObj, formData);
                }
            };

            if (_iframe.attachEvent) {
                _iframe.attachEvent("onload", _iframeLoaded);
            } else {
                _iframe.onload = _iframeLoaded;
            }
            $('#' + iframeId).attr('src', top.$.rootUrl + url);
        }
    };

    page.init();
}