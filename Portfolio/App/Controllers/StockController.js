app.controller("StockController", function ($scope, autocompleteService, stockAnalysisService) {
    $scope.selectedSymbols = [];
    
    init();
    function init() {
        $scope.selectedSymbols = stockAnalysisService.getStocks();
    }

    $scope.selected = undefined;
    $scope.symbols = [];
    
    $scope.OnInputChange = function () {
        $scope.symbols = autocompleteService.getSymbols($scope.selected);
    };
    
    $scope.OnAdd = function () {
        stockAnalysisService.addStock($scope.selected.Symbol);
        $scope.selected = undefined;
    };
});