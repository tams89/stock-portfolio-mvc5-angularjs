app.controller("RegistrationController", function (authenticationService, toaster) {

    $scope.formData = {};
    $scope.antiForgeryToken = undefined;
    $scope.isLoggedIn = false;

    $scope.register = function () {
        var validatedPromise = authenticationService.Register($scope.formData, $scope.antiForgeryToken);
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