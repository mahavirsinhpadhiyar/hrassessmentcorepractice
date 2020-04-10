hrApp.controller("UserLoginController", function ($scope, UserLoginService) {
    $scope.IsError = false;
    $scope.UserLogin = UserLogin;
    
    function UserLogin() {
        UserLoginService.UserLogin($scope.username, $scope.password, $scope.rememberme, function (response) {
            if (response != null) {
                window.location.href = "https://localhost:44388/home/index";
            } else {
                $scope.IsError = true;
                $scope.ErrorMessage = "Credentials do not match, please try again.";
            }
        }, function error(response) {
            debugger;
            if (response.status != 200) {
                $scope.IsError = true;
                $scope.ErrorMessage = "Credentials do not match, please try again.";
            }
        });
    };
});