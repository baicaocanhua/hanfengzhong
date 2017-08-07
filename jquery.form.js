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