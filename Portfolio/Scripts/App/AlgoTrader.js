var app = angular.module("AlgoTrader", ["ui.bootstrap", "ngRoute", "ngAnimate", "toaster", "highcharts-ng", "ngResource", "ngGrid"]);

// Route config
// Note the controller set here does not need to be explicitly set in the page itself.
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when('/Main/Home', // This URL is just what the links href is set to can be anything, angular will hijack this.
            {
                templateUrl: "/Main/Home" // actual URL triggering MVC controller.
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