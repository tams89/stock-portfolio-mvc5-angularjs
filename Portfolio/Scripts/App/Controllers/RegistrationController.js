app.controller("RegistrationController", function ($scope, authenticationService, toaster) {

    $scope.registerModel = {};
    $scope.antiForgeryToken = undefined;
    $scope.isLoggedIn = false;

    $scope.register = function () {
        var validatedPromise = authenticationService.Register($scope.registerModel, $scope.antiForgeryToken);
        validatedPromise.then(function (data) {
            if (data != undefined && data.success != undefined) {
                return true;
            }
            if (data != undefined) {
                if (data.length > 1) {
                    for (var i = 0; i < data.length; i++) {
                        toaster.pop("error", data[i]);
                    }
                } else {
                    toaster.pop("error", data.val());
                }
            }
            return false;
        });
    };

});