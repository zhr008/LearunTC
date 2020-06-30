var url = "http://192.168.0.98:8055/";

//ajax发送请求
function httpAsync(type, url, param, callback) {
    $.ajax({
        url: url,
        data: param,
        type: type,
        dataType: "json",
        async: true,
        cache: false,
        success: function (res) {
            callback(res);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        },
        beforeSend: function (xhr) {
            //xhr.setRequestHeader("Test", "testheadervalue");
        },
        complete: function () {
        }
    });
}
//分类的中文，英文
Vue.filter('Chinese', function (value) {
    if (!value) return ''
    value = value.toString();
    return value.substring(0, value.indexOf('/'));
})
Vue.filter('English', function (value) {
    if (!value) return ''
    value = value.toString();
    return value.substring(value.indexOf('/') + 1, value.length);
})
//时间过滤出年月日
Vue.filter("data", function (input) {
    var d = new Date(input);
    var year = d.getFullYear();
    var month = d.getMonth() + 1;
    var day = d.getDate() < 10 ? '0' + d.getDate() : '' + d.getDate();
    return year + '-' + month + '-' + day;
})
//* 获取url传参数
//* @name {string} 参数名称
//* @return null {null} 
var request=function (name) {
    var search = location.search.slice(1);
    var arr = search.split("&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == name) {
            if (unescape(ar[1]) === 'undefined' || unescape(ar[1]) === 'null') {
                return "";
            } else {
                return (ar[1]);
            }
        }
    }
    return "";
}


//更改文本编辑器填写内容中的所有图片路劲地址
function changeUeditorUploadImage(content) {
    if (content != null || content != "") {
        if (JSON.stringify(content).indexOf("/ueditor/upload/image/") != -1) {
            return content.replace("/ueditor/upload/image/", url + "learun/adms/home/getdetailimg?data=/ueditor/upload/image/");
        }
        else {
            return content;
        }
    }
}