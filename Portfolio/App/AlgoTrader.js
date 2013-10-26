var app = angular.module("AlgoTrader", ["ui.bootstrap", "ngRoute", "ngAnimate"]);

// Route config
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when('/Main/Home',
            {
                controller: "HomeController",
                templateUrl: "/App/Views/Main/Home.html"
            })
        .when("/Main/About",
            {
                controller: "AboutController",
                templateUrl: "/App/Views/Main/About.html"
            })
        .when("/Portfolio/Stocks",
            {
                controller: "StockController",
                templateUrl: "/App/Views/Portfolio/Stocks.html"
            })
        .otherwise({ redirectTo: "/Main/Home" });
}]);