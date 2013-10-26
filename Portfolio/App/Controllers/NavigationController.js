// Setup navigation routes controller.
app.controller("NavigationController", function ($scope, $location) {
    $scope.getClass = function (path) {
        if ($location.path().substr(0, path.length) == path) {
            return true;
        } else {
            return false;
        }
    };
});

// Note $scope is bound per view.

app.controller("HomeController", function ($scope) {
});

app.controller("AboutController", function ($scope) {
});

app.controller("StockController", function ($scope, ajaxService) {
    $scope.Hello = "Tamesh says hello!";
    $scope.selected = undefined;
    $scope.symbols = [];

    $scope.OnInputChange = function() {
        $scope.symbols = [];
        $scope.symbols = ajaxService.getSymbols();
    };
});