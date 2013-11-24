app.controller("RegistrationController", ["$scope", "authenticationService", "toaster", function ($scope, authenticationService, toaster) {

    $scope.antiForgeryToken = undefined;

    $scope.register = function () {
        if ($scope.registerForm.$valid) {
            var validatedPromise = authenticationService.Register($scope.registerModel, $scope.antiForgeryToken);
            validatedPromise.then(function (data) {
                if (data != undefined && data.success != undefined) {
                    toaster.pop("success", "Registration completed successfully.");
                    window.location.href = "/";
                    return true;
                }
                if (data != undefined) {
                    if (data.length > 1) {
                        for (var i = 0; i < data.length; i++) {
                            toaster.pop("error", data[i].toString());
                        }
                    } else {
                        toaster.pop("error", data.toString());
                    }
                }
                return false;
            });
        } else {
            toaster.pop("error", "Invalid Form");
        }
    };

}]);