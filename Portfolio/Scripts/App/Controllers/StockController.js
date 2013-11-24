app.controller("StockController", function ($scope, utilitiesService, autocompleteService, stockAnalysisService, highStockService, toaster) {

    var lastSymbol;
    $scope.stockList = []; // for stock watch table list
    $scope.selected = undefined; // input selection property
    $scope.isChartEmpty = true;
    $scope.symbols = [];
    $scope.companySymbol = [];

    init();
    function init() {
        $scope.stockList = stockAnalysisService.getStockList();
        $scope.companySymbol = stockAnalysisService.getCompanySymbolKey();
        if (!highStockService.isChartEmpty()) {
            $scope.isChartEmpty = false;
            highStockService.createChart();
        }
    }

    $scope.OnInputChange = function () {
        $scope.symbols = autocompleteService.getSymbols($scope.selected);
    };

    $scope.SelectSymbol = function () {
        var symbol = $scope.selected.Symbol;
        var companyName = $scope.selected.Name;
        if (symbol === undefined || symbol === null || companyName === null) return;
        lastSymbol = symbol;
        addToList(symbol, companyName);
        $scope.selected = undefined;
    };

    function addToList(symbol, companyName) {
        if (utilitiesService.isItemInArrayProp($scope.stockList, "Symbol", symbol) === false) {
            stockAnalysisService.persistCompanySymbolKey({ name: companyName, symbol: symbol });
            var dataLength = stockAnalysisService.getStockData(symbol); // Get historical market data.
            if (dataLength === 0) {
                toaster.pop("information", "No data obtained", "No stock data was found for the selected company.");
            }
        } else {
            toaster.pop("warning", "", "Symbol already in watch-list");
            $scope.selected = undefined;
        }
    }

    $scope.companyName = function (symbol) {
        for (var i = 0; i <= $scope.companySymbol.length; i++) {
            if ($scope.companySymbol[i].symbol === symbol) {
                return $scope.companySymbol[i].name;
            }
        }
    };

    $scope.RemoveFromList = function (idx, symbol) {
        stockAnalysisService.removeStock(idx);
        $scope.updateChart(symbol, false);
    };

    $scope.isStockListEmpty = function () {
        if ($scope.stockList.length > 0) return true;
        return false;
    };

    $scope.updateChart = function (symbol, checked) {
        if (symbol === null) return;
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