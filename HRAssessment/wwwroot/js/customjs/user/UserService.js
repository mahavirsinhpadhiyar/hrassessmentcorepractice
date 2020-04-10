hrApp.service("UserLoginService", function ($http) {

    var apiUrl = "https://localhost:44392/";
    var service = {};

    service.UserLogin = UserLogin;

    return service;

    function UserLogin(username, password, rememberme, successFuncCall, errorFuncCall) {

        $http.post(apiUrl + 'api/User', { UserName: username, Password: password, RememberMe: rememberme })
            .success(function (response) {
                debugger;
                successFuncCall(response);
            })
            .error(function (response) {
                debugger;
                errorFuncCall(response);
            });
    }
});
