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
}]);