app.controller("StockController", function ($scope, autocompleteService, stockAnalysisService, highStockService, toaster) {

    var lastSymbol;
    $scope.stockList = [];
    $scope.selected = undefined;

    init();
    function init() {
        $scope.stockList = stockAnalysisService.getStockList();
    }

    $scope.symbols = [];
    $scope.OnInputChange = function () {
        $scope.symbols = autocompleteService.getSymbols($scope.selected);
    };

    $scope.SelectSymbol = function () {
        var symbol = $scope.selected.Symbol;
        if (symbol === undefined || symbol == null) return;
        lastSymbol = symbol;
        addToList(symbol);
        $scope.selected = undefined;
    };

    function addToList(symbol) {
        if (doesExist(symbol) === false) {
            stockAnalysisService.getStockData(symbol);
        } else {
            toaster.pop("warning", "", "Symbol already in watch-list");
            $scope.selected = undefined;
        }
    }

    //$scope.$watchCollection("stockList", function () {
    //    if (lastSymbol != null) {
    //        highChartService.updateChart($scope.chart, $scope.stockList);
    //    }
    //});

    $scope.RemoveFromList = function (idx) {
        stockAnalysisService.removeStock(idx);
        //highChartService.updateChart($scope.chart, $scope.stockList);
    };

    var doesExist = function (symbol) {
        for (var i = 0; i < $scope.stockList.length; i++) {
            if ($scope.stockList[i].Symbol == symbol) {
                return true;
            }
        }
        return false;
    };

    $scope.isStockListEmpty = function () {
        if ($scope.stockList.length > 0) {
            return true;
        } else {
            return false;
        }
    };

    $scope.updateChart = function (symbol) {
        if (highStockService.duplicatedSeries(symbol) === false) {
            var historicalData = stockAnalysisService.getHistoricalDataBySymbol(symbol); // Get stored historical data.
            if (historicalData != null) {
                highStockService.parseHistoricalData(historicalData); // Pass historical data for chart prep.
                highStockService.createChart();
            }
        }
    };

    $scope.testChart = function() {
        highStockService.alternateChart();
    };

});