app.controller("LoginController", ["$scope", "authenticationService", "toaster", function ($scope, authenticationService, toaster) {

    $scope.antiForgeryToken = undefined;

    $scope.login = function () {
        if ($scope.loginForm.$valid) {
            var validatedPromise = authenticationService.Login($scope.loginModel, $scope.antiForgeryToken);
            validatedPromise.then(function (data) {
                if (data === "true") {
                    window.location.href = "/";
                    return true;
                } else {
                    toaster.pop("error", data.toString());
                    return false;
                }
            });
        } else {
            toaster.pop("error", "Invalid Form");
        }
    };

    $scope.logOut = function () {
        var promise = authenticationService.LogOut($scope.antiForgeryToken);
        promise.then(function () {
            window.location.href = "/";
        });
    };

}]);