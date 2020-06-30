(function () {
    var $header = null;
    var page = {
        isScroll: true,
        init: function ($page, param) {
            // 添加头部按钮列表
            var _html = '\
                <div class="lr-form-header-cancel" style="display:block;" >取消</div>\
                <div class="lr-form-header-submit" style="display:block;" >提交</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            // 添加头部按钮事件
            // 取消
            $header.find('.lr-form-header-cancel').on('tap', function () {
                learun.nav.closeCurrent();
            });
            // 提交
            $header.find('.lr-form-header-submit').on('tap', function () {
                if (!$page.find('.lr-form-container').lrformValid()) {
                    return false;
                }
                var formdata = $page.find('.lr-form-container').lrformGet();
                // 获取审核人员
                var auditers = {};
                $page.find('.nodeId').each(function () {
                    var $this = $(this);
                    var id = $this.attr('id');

                    if (formdata[id] && formdata[id] !== 'undefined' && formdata[id] !== undefined) {
                        auditers[id] = formdata[id];
                    }
                });

                var data = {
                    level: formdata.F_Level
                };
                if (param.node.isTitle == '1') {
                    data.title = formdata.F_Title;
                }

                learun.nav.closeCurrent();
                setTimeout(function () {
                    var prepage = learun.nav.getpage("nworkflow/draft");
                    prepage.create(data, auditers);
                }, 300);
            });

            $page.find('#F_Level').lrpicker({
                placeholder: '请选择(必填)',
                data: [
                    { value: '0', text: '普通' },
                    { value: '1', text: '重要' },
                    { value: '2', text: '紧急' }
                ]
            });


            if (param.node.isTitle != '1') {
                $page.find('#F_Title').parent().remove();
            }

            //加载下一节点审核者
            if (param.node.isNext == '1') {
                var nodeMap = {};
                // 节点信息
                $.each(param.nodelist, function (_index, _item) {
                    nodeMap[_item.id] = _item;
                });
                var req = {
                    code: param.schemeCode,
                    processId: param.processId,
                    nodeId: param.node.id,
                    operationCode: 'agree'
                };
                learun.httpget(config.webapi + "learun/adms/newwf/auditer", req, function (data) {
                    if (data) {
                        var $item = $page.find('#F_Level').parent();
                        $.each(data, function (_id, _list) {
                            if (_list.length > 1) {
                                $item.after('<div class="lr-form-row"><label>' + nodeMap[_id].name + '</label><div id="' + _id + '"  class="nodeId" ></div></div>');
                                $page.find('#' + _id).lrpicker({ data: _list, itext: 'Name', ivalue: 'Id' });
                            }
                        });
                    }
                });

            }
        },
        destroy: function (pageinfo) {
            $header = null;
        }
    };
    return page;
})();