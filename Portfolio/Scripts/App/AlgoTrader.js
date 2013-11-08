var app = angular.module("AlgoTrader", ["ui.bootstrap", "ngRoute", "ngAnimate", "toaster", "highcharts-ng", "ngResource", "ngGrid"]);

// Route config
app.config(["$routeProvider", function($routeProvider) {
    $routeProvider
        .when('/Main/Home',
            {
                templateUrl: "/Main/Home"
            })
        .when("/Main/About",
            {
                templateUrl: "/Main/About"
            })
        .when("/Portfolio/Stocks",
            {
                controller: "StockController",
                templateUrl: "/Portfolio/Stocks"
            })
        .when("/Portfolio/Options",
            {
                controller: "OptionController",
                templateUrl: "/Portfolio/Options"
            })
        .otherwise({ redirectTo: "/Main/Home" });
}]);