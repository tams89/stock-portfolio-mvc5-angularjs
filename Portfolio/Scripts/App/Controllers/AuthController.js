app.controller("AuthController", function ($scope, authenticationService) {

    $scope.formData = {};
    $scope.antiForgeryToken = undefined;
    $scope.isLoggedIn = false;

    $scope.login = function () {
        var validated = authenticationService.Login(this.formData, this.antiForgeryToken);
        return validated;
    };

    $scope.register = function () {
        // Sign up...
    };

});