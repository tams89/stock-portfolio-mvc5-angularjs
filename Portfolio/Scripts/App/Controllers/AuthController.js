app.controller("AuthController", function ($scope, authenticationService, toaster) {

    $scope.formData = {};
    $scope.antiForgeryToken = undefined;

    $scope.login = function () {
        var validatedPromise = authenticationService.Login($scope.formData, $scope.antiForgeryToken);
        validatedPromise.then(function (data) {
            if (data != undefined) {
                window.location.href = "/Main";
            }
            toaster.pop("error", data);
            return false;
        });
    };

    $scope.logOut = function () {
        var promise = authenticationService.LogOut($scope.antiForgeryToken);
        promise.then(function () {
            window.location.href = "/Main";
        });
    };

});