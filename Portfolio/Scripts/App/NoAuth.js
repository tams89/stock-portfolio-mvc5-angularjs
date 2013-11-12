﻿var app = angular.module("NoAuth", ["ui.bootstrap", "ngRoute", "ngAnimate", "toaster"]);

// Route config
// Note the controller set here does not need to be explicitly set in the page itself.
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/Main/Home",
            {
                templateUrl: "/Main/Home"
            })
        .when()
        .otherwise({ redirectTo: "/Main/Home" });
}]);