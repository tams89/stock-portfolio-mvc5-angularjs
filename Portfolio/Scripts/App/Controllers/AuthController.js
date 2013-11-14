app.controller("AuthController", function ($scope, authenticationService, toaster) {

    $scope.formData = {};
    $scope.antiForgeryToken = undefined;
    $scope.isLoggedIn = false;

    $scope.login = function () {
        var validatedPromise = authenticationService.Login(this.formData, this.antiForgeryToken);
        validatedPromise.then(function (data) {
            if (data != undefined && data.success != undefined) return true;
            toaster.pop("error", data);
            return false;
        });
    };

    $scope.register = function () {
        // Sign up...
    };

});