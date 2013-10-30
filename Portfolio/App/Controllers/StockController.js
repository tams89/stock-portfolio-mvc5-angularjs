app.controller("StockController", function ($scope, autocompleteService, stockAnalysisService, toaster, highChartService) {

    var lastSymbol;
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
        var symbol = $scope.selected.Symbol;
        lastSymbol = symbol;
        addToList(symbol);
        $scope.selected = undefined;
    };

    function addToList(symbol) {
        if (doesExist(symbol) === false) {
            stockAnalysisService.addStock(symbol);
        }
    }

    function updateChart(symbol) {
        var data = stockAnalysisService.getHistoricalDataBySymbol(symbol);
        var flattened = stockAnalysisService.flattenData("AdjClose", data);
        highChartService.addSeries($scope.chart, flattened, symbol);
    }

    $scope.$watchCollection("selectedSymbols", function () {
        if (lastSymbol != null) {
            updateChart(lastSymbol);
        }
    });

    $scope.RemoveFromList = function (idx) {
        stockAnalysisService.removeStock(idx);
    };

    var doesExist = function (symbol) {
        for (var i = 0; i < $scope.selectedSymbols.length; i++) {
            if ($scope.selectedSymbols[i].Symbol == symbol) {
                $scope.selected = undefined;
                toaster.pop("warning", "", "Duplicate!");
                return true;
            }
        }
        return false;
    };

    $scope.chart = {
        options: {
            chart: {
                type: 'line'
            }
        },
        title: {
            text: 'Historical Trend (Adjusted Close)'
        },
        xAxis: {
          title: { text: "Price ($)" }  
        },
        loading: false
    };

});