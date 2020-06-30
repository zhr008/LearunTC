var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            // 打印
            $('.node').on('click', function () {
                var stockArea = $(this).attr("data-value");
                learun.layerForm({
                    id: 'form',
                    title:'A'+ stockArea+'仓位库存信息',
                    url: top.$.rootUrl + '/LR_CodeDemo/StockDemo/Stock?stockArea=' + stockArea,
                    width: 600,
                    height: 400
                });
            });
        }
    };
    page.init();
}
