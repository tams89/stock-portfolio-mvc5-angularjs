var app = angular.module("AlgoTrader", ["ui.bootstrap", "ngRoute", "ngAnimate"]);

// Route config
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when('/',
            {
                controller: "HomeController",
                templateUrl: "/App/Views/Main/Home.html"
            })
        .when("/About",
            {
                controller: "AboutController",
                templateUrl: "/App/Views/Main/About.html"
            })
        .otherwise({ redirectTo: "/" });
}]);