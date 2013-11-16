app.controller("AuthController", function ($scope, authenticationService, toaster) {

    $scope.registerModel = {};
    $scope.antiForgeryToken = undefined;

    $scope.login = function () {
        var validatedPromise = authenticationService.Login($scope.registerModel, $scope.antiForgeryToken);
        validatedPromise.then(function (data) {
            if (data == "true") {
                window.location.href = "/";
                return true;
            } else {
                toaster.pop("error", data);
                return false;
            }
        });
    };

    $scope.logOut = function () {
        var promise = authenticationService.LogOut($scope.antiForgeryToken);
        promise.then(function () {
            window.location.href = "/";
        });
    };

});