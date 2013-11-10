app.controller("StockController", function ($scope, utilitiesService, autocompleteService, stockAnalysisService, highStockService, toaster) {

    var lastSymbol;
    $scope.stockList = []; // for stock watch table list
    $scope.selected = undefined; // input selection property
    $scope.isChartEmpty = true;

    init();
    function init() {
        $scope.stockList = stockAnalysisService.getStockList();
        if (!highStockService.isChartEmpty()) {
            $scope.isChartEmpty = false;
            highStockService.createChart();
        }
    }

    $scope.symbols = [];
    $scope.OnInputChange = function () {
        $scope.symbols = autocompleteService.getSymbols($scope.selected);
    };

    $scope.SelectSymbol = function () {
        var symbol = $scope.selected.Symbol;
        if (symbol === undefined || symbol === null) return;
        lastSymbol = symbol;
        addToList(symbol);
        $scope.selected = undefined;
    };

    function addToList(symbol) {
        if (utilitiesService.isItemInArrayProp($scope.stockList, "Symbol", symbol) === false) {
            stockAnalysisService.getStockData(symbol);
        } else {
            toaster.pop("warning", "", "Symbol already in watch-list");
            $scope.selected = undefined;
        }
    }

    $scope.RemoveFromList = function (idx, symbol) {
        stockAnalysisService.removeStock(idx);
        $scope.updateChart(symbol, false);
    };

    $scope.isStockListEmpty = function () {
        if ($scope.stockList.length > 0) return true;
        return false;
    };

    $scope.updateChart = function (symbol, checked) {
        if (symbol == null) return;
        if (checked && highStockService.duplicatedSeries(symbol) === false) {
            var h = stockAnalysisService.getHistoricalDataBySymbol(symbol); // get series data from local store.
            highStockService.addHistoricalDataSeries(h); // Add series to chart
        } else {
            highStockService.removeSeries(symbol); // Remove series from chart
        }
        if (!highStockService.isChartEmpty()) $scope.isChartEmpty = false;
        else $scope.isChartEmpty = true;
        highStockService.createChart(); // create chart after series options set.
    };

});