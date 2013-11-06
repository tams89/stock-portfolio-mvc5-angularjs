var app = angular.module("AlgoTrader", ["ui.bootstrap", "ngRoute", "ngAnimate", "toaster", "highcharts-ng"]);

// Route config
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when('/Main/Home',
            {
                templateUrl: "/App/Views/Main/Home.html"
            })
        .when("/Main/About",
            {
                templateUrl: "/App/Views/Main/About.html"
            })
        .when("/Portfolio/Stocks",
            {
                controller: "StockController",
                templateUrl: "/App/Views/Portfolio/Stocks.html"
            })
        .when("/Portfolio/Options",
            {
                controller: "OptionController",
                templateUrl: "/App/Views/Portfolio/Options.html"
            })
        .otherwise({ redirectTo: "/Main/Home" });
}]);