function demo() {

    var login_username = document.getElementById("login_username").value;
    var login_password = document.getElementById("login_password").value;
    alert(login_username + login_password);
                $.ajax({
            url: "/login", // to get the right path to controller from TableRoutes of Asp.Net MVC
                dataType: "json", //to work with json format
                type: "POST", //to do a post request
                contentType: 'application/json; charset=utf-8', //define a contentType of your request
                    cache: false, //avoid caching results
                    data: {
                        login_username: login_username,
                        login_password: login_password
                    }, // here you can pass arguments to your request if you need
                success: function (data) {
                     // data is your result from controller
                    if (data.success) {
            alert("đăng nhập thành công");
                    }
                },
                error: function (xhr) {
            alert('error');
    }
});
          
}
function demo1()
    {
    var options = {};
    options.url = "/login";
    options.type = "POST";

    var obj = {};
    obj.login_username = $("#login_username").val();
    obj.login_password = $("#login_password").val();
    options.data = JSON.stringify(obj);
    options.contentType = "application/json";
    options.dataType = "json";

    options.beforeSend = function (xhr) {
        xhr.setRequestHeader("MY-XSRF-TOKEN",
            $('input:hidden[name="__RequestVerificationToken"]').val());
    };
    options.success = function (msg) {
        $("#msg").html(msg);
    };
    options.error = function () {
        $("#msg").html("Error while making Ajax call!");
    };
    $.ajax(options);
}