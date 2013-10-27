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

//app.controller("StockController", function ($scope, ajaxService) {
//    $scope.selected = undefined;
//    $scope.symbols = [];
//    //$scope.symbols = function (inputVal) {
//    //    var res = ajaxService.getSymbols(inputVal);
//    //    $scope.symbols = res;
//    //};

//    $scope.OnInputChange = function () {

//        for (var i = 0; i < Math.floor((Math.random() * 10) + 1) ; i++) {
//            var x = "";
//            var y = "";
//            var value = { label: x, value: y };

//            for (var j = 0; j < i; j++) {
//                value.value += j;
//                value.label += x;
//            }
//            $scope.symbols.push(value);
//        }
//    };
//});

app.controller("StockController", function ($scope, $http) {
    $scope.selected = undefined;
    $scope.symbols = [];
    $scope.OnInputChange = function () {
        return $http.post("/Home/AutoComplete?symbol=" + $scope.selected)
            .then(function (response) {
                $scope.symbols = response.data;
            });
    };
});