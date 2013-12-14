var app = angular.module("NoAuth", ["ui.bootstrap", "ngRoute", "ngAnimate", "toaster"]);


// Route config
// Note the controller set here does not need to be explicitly set in the page itself.
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/Home",
            {
                templateUrl: "Main/Home"
            })
        .when("/About",
            {
                templateUrl: "Main/About"
            })
        .when("/Login",
            {
                templateUrl: "Main/Login",
                controller: "LoginController"
            })
        .when("/Register",
            {
                templateUrl: "Main/Register",
                controller: "RegistrationController"
            })
        .otherwise({ redirectTo: "/Home" });
}]);

app.config(["$locationProvider", function ($locationProvider) {
    $locationProvider.html5Mode(false);
}]);﻿app.service("authenticationService", ["$http", "$q", function ($http, $q) {

    this.Login = function (formData, token) {
        var deferred = $q.defer();
        $http({
            method: "POST",
            url: "Account/JsonLogin",
            data: formData,
            headers: { "RequestVerificationToken": token },
        }).success(function (data) {
            deferred.resolve(data);
        });
        return deferred.promise;
    };

    this.Register = function (formData, token) {
        var deferred = $q.defer();
        $http({
            method: "POST",
            url: "Account/JsonRegister",
            data: formData,
            headers: { "RequestVerificationToken": token },
        }).success(function (data) {
            deferred.resolve(data);
        });
        return deferred.promise;
    };

    this.LogOut = function (token) {
        var deferred = $q.defer();
        $http({
            method: "POST",
            url: "Account/LogOff",
            headers: { "RequestVerificationToken": token }
        }).success(function (data) {
            deferred.resolve(data);
        });;
        return deferred.promise;
    };

}]);﻿app.service("authenticationService", ["$http", "$q", function ($http, $q) {

    this.Login = function (formData, token) {
        var deferred = $q.defer();
        $http({
            method: "POST",
            url: "Account/JsonLogin",
            data: formData,
            headers: { "RequestVerificationToken": token },
        }).success(function (data) {
            deferred.resolve(data);
        });
        return deferred.promise;
    };

    this.Register = function (formData, token) {
        var deferred = $q.defer();
        $http({
            method: "POST",
            url: "Account/JsonRegister",
            data: formData,
            headers: { "RequestVerificationToken": token },
        }).success(function (data) {
            deferred.resolve(data);
        });
        return deferred.promise;
    };

    this.LogOut = function (token) {
        var deferred = $q.defer();
        $http({
            method: "POST",
            url: "Account/LogOff",
            headers: { "RequestVerificationToken": token }
        }).success(function (data) {
            deferred.resolve(data);
        });;
        return deferred.promise;
    };

}]);﻿// Setup navigation routes controller sets navbar items to active and so highlights when on page.
app.controller("NavigationController", ["$scope", "$location", function ($scope, $location) {
    $scope.getClass = function (path) {
        if ($location.path().substr(0, path.length) == path) {
            return true;
        } else {
            return false;
        }
    };
}]);﻿app.controller("LoginController", ["$scope", "authenticationService", "toaster", function ($scope, authenticationService, toaster) {

    $scope.antiForgeryToken = undefined;

    $scope.login = function () {
        if ($scope.loginForm.$valid) {
            var validatedPromise = authenticationService.Login($scope.loginModel, $scope.antiForgeryToken);
            validatedPromise.then(function (data) {
                if (data == "true") {
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

}]);﻿app.controller("RegistrationController", ["$scope", "authenticationService", "toaster", function ($scope, authenticationService, toaster) {

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