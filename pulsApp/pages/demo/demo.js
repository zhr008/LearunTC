/*页面js模板,必须有init方法*/

(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            $('#demolist .lr-nav-right').on('tap', function () {
                var $this = $(this);
                var path = 'demo/' + $this.attr('data-value');
				 var title = $this.text();
				if(path == 'demo/amap'){
					if(learun.platform != 'browser'){
						learun.createWithoutTitle('/puls/maps_map.html', {
							titleNView:{
								type: 'float',
								backgroundColor: '#0C86D8',
								titleText: title,
								titleColor: '#ffffff',
								autoBackButton: true
							}
						});
					}
					else{
						learun.layer.toast('需要在真机上体验');
					}
					
				}
				else{
					learun.nav.go({ path: path, title: title, type: 'right' });
				}
               
            });
        }
    };
    return page;
})();
