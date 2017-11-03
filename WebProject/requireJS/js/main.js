// require.config({
//     paths: {
//         "maimai": "maimai"
//     }
// });
// require(["maimai"], function (maimai) {
//     maimai.fun1();
// });

//config中没有，最后执行
require(['config'],function(){
    require(['MAIMAI','logger'],function (maimai,logger) {
         maimai.fun1();
         //logger.logInfo();
    });
});

require(['config'],function(){
    require(['maimai','logger'],function (maimai,logger) {
         maimai.fun1();
         //logger.logInfo();
    });
});