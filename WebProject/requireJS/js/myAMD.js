; (function (factory) {
    if (typeof define === "function" && define.amd) {
        define(factory);
    } else {
        window.maimai = factory();
    }
})(function () {
    var fun1 = function () {
        // $("#dd").html("dddddd");
        alert("it works");
    }
    var maimai = {};
    maimai.fun1 = fun1;
    return maimai;

});