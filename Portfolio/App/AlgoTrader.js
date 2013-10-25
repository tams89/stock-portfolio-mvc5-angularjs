var app = angular.module("AlgoTrader", ["ui.bootstrap"]);

// Route config
app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when('/Home',
            {
                controller: "HomeController",
                templateUrl: "/App/Views/Main/Home.html"
            })
        .when("/About",
            {
                controller: "AboutController",
                templateUrl: "/App/Views/Main/About.html"
            })
        .otherwise({ redirectTo: "/Home" });
}]);