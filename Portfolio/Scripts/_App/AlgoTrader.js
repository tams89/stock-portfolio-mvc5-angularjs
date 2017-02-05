var app = angular.module("AlgoTrader", ["ui.bootstrap", "ngRoute", "ngAnimate", "toaster", "highcharts-ng", "ngResource", "ngSanitize", "ngGrid"]);

// Route config
// Note the controller set here does not need to be explicitly set in the page itself.
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when('/Home', // This URL is just what the links href is set to can be anything, angular will hijack this.
            {
                templateUrl: "Main/Home" // actual URL triggering MVC controller.
            })
        .when("/About",
            {
                templateUrl: "Main/About"
            })
        .when("/Stocks",
            {
                controller: "StockController",
                templateUrl: "Portfolio/Stocks"
            })
        .when("/Options",
            {
                controller: "OptionController",
                templateUrl: "Portfolio/Options"
            })
        .when("/Sample",
            {
                templateUrl: "Sample/Angular2"
            })
        .otherwise({ redirectTo: "/Home" });
}]);