app.controller("StockController", function ($scope, autocompleteService, stockAnalysisService, toaster) {
    $scope.selectedSymbols = [];
    $scope.selected = undefined;

    init();
    function init() {
        $scope.selectedSymbols = stockAnalysisService.getStocks();
    }

    $scope.symbols = [];
    $scope.OnInputChange = function () {
        $scope.symbols = autocompleteService.getSymbols($scope.selected);
    };

    $scope.OnEnterIfSelected = function () {
        if (doesExist($scope.selected.Symbol) === false) {
            stockAnalysisService.addStock($scope.selected.Symbol);
            $scope.selected = undefined;
        }
    };

    $scope.RemoveFromList = function (idx) {
        stockAnalysisService.removeStock(idx);
    };

    function doesExist(symbol) {
        for (var i = 0; i < $scope.selectedSymbols.length; i++) {
            if ($scope.selectedSymbols[i].Symbol == symbol) {
                $scope.selected = undefined;
                toaster.pop("warning", "", "Duplicate!");
                return true;
            }
        }
        return false;
    }

});