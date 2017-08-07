必须有submit
$("#loginForm").ajaxForm();
    
       
必须有submit
            $("#loginForm").submit(function () {
                $(this).ajaxSubmit();

                return false;
            })

可以没有submit
            $("#myButton").click(function () {
                $("#loginForm").ajaxSubmit();
                return false;
            });

ajaxForm不能主动提交form，函数只是为提交表单做准备需要以submit来触发提交。
而ajaxSubmit会主动提交表单，同时可以在点击其他按钮时也可以触发提交，不一定是submit按钮

想要阻止自动提交，必须return false；

var object= {
             url:url,　　　　　　//form提交数据的地址
　　　　　　  type:type,　　　  //form提交的方式(method:post/get)
　　　　　　  target:target,　　//服务器返回的响应数据显示的元素(Id)号
             beforeSerialize:function(){} //序列化提交数据之前的回调函数
　　　　　　  beforeSubmit:function(){},　　//提交前执行的回调函数
　　　　　　  success:function(){},　　　　   //提交成功后执行的回调函数
             error:function(){},             //提交失败执行的函数
　　　　　　  dataType:null,　　　　　　　//服务器返回数据类型
　　　　　　  clearForm:true,　　　　　　 //提交成功后是否清空表单中的字段值
　　　　　　  restForm:true,　　　　　　  //提交成功后是否重置表单中的字段值，即恢复到页面加载时的状态
　　　　　　  timeout:6000 　　　　　 　 //设置请求时间，超过该时间后，自动退出请求，单位(毫秒)。　　

}



beforeSubmit: validate  
function validate(formData, jqForm, options) { //在这里对表单进行验证，如果不符合规则，将返回false来阻止表单提交，直到符合规则为止  
   //方式一：利用formData参数  
   for (var i=0; i < formData.length; i++) {  
       if (!formData[i].value) {  
            alert('用户名,地址和自我介绍都不能为空!');  
            return false;  
        }  
    }   
  
   //方式二：利用jqForm对象  
   var form = jqForm[0]; //把表单转化为dom对象  
      if (!form.name.value || !form.address.value) {  
            alert('用户名和地址不能为空，自我介绍可以为空！');  
            return false;  
      }  
  
   //方式三：利用fieldValue()方法，fieldValue 是表单插件的一个方法，它能找出表单中的元素的值，返回一个集合。  
   var usernameValue = $('input[name=name]').fieldValue();  
   var addressValue = $('input[name=address]').fieldValue();  
   if (!usernameValue[0] || !addressValue[0]) {  
      alert('用户名和地址不能为空，自我介绍可以为空！');  
      return false;  
   }  
  
    var queryString = $.param(formData); //组装数据  
    //alert(queryString); //类似 ： name=1&add=2    
    return true;  
}  

var queryString = $('#loginForm').formSerialize();
var queryString1 = $('#loginForm').serialize();
 var queryString3 = $('#loginForm :text').fieldSerialize();

form.js 和 validate.js 集合使用


http://www.cnblogs.com/bjlhx/p/6692058.html
http://blog.csdn.net/huang100qi/article/details/52453970
https://jqueryvalidation.org/
https://jqueryvalidation.org/documentation/
https://jqueryvalidation.org/files/demo/
https://github.com/jquery-validation/jquery-validation/blob/master/demo/index.html
http://blog.csdn.net/eson_15/article/details/51497533

