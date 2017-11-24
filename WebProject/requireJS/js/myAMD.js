; (function (factory) {
    if (typeof define === "function" && define.amd) {
        define(factory);
    } else {
        window.maimai = factory();
    }
})(function () {
    var fun1 = function () {
        // $("#dd").html("dddddd");
        alert("it works1111111111");
    }
    var maimai = {};
    maimai.fun1 = fun1;
    return maimai;

});


! function (a) {
    "function" == typeof define && define.amd ? define(["jquery", "jquery.validate.min"], a) : a(jQuery)
}(function (a) {
    var icon = "<i class='fa fa-times-circle'></i>  ";

});


;(
    
    function (factory) {
    if (typeof define === "function" && define.amd) {
        // AMD模式
        define([ "jquery" ], factory);
    } else {
        // 全局模式
        factory(jQuery);
    }
}(function ($) {
    $.fn.jqueryPlugin = function () {
        //插件代码
    };
})
);

懒加载
define(function(){
    console.log('main');
    document.onclick = function(){
        require(['a'],function(a){
            a.test();
        });
    }
});

http://www.cnblogs.com/xiaohuochai/p/6847942.html

http://www.css88.com/archives/4826 jQuery 对AMD的支持（Require.js中如何使用jQuery）

http://www.ruanyifeng.com/blog/2012/11/require_js.html


http://requirejs.org/docs/api.html#modulenotes

http://www.jb51.net/article/78661.htm 一篇文章掌握RequireJS常用知识  Require加载


https://www.tuicool.com/articles/jam2Anv 快速理解RequireJs

http://blog.csdn.net/bluesky1215/article/details/71079667 requirejs入门到精通

http://blog.csdn.net/sanxian_li/article/details/39394097 RequireJS API中文版