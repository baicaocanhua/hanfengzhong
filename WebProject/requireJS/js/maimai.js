define(['jquery'], function ($) {
    var fun1 = function () {
        $("#dd").html("dddddd");
        alert("it works2222222222,先走config，再走js路径，不区分大小写");
    }

    var maimai = {};
    maimai.fun1 = fun1;
    return maimai;
})