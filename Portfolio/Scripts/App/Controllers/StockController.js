"use strict";

app.controller("StockController", function ($scope, autocompleteService, stockAnalysisService, toaster, highChartService) {

    var lastSymbol;
    $scope.stockList = [];
    $scope.selected = undefined;

    init();
    function init() {
        $scope.stockList = stockAnalysisService.getStocks();
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

    $scope.$watchCollection("stockList", function () {
        if (lastSymbol != null) {
            highChartService.updateChart($scope.chart, $scope.stockList);
        }
    });

    $scope.RemoveFromList = function (idx) {
        stockAnalysisService.removeStock(idx);
        highChartService.updateChart($scope.chart, $scope.stockList);
    };

    var doesExist = function (symbol) {
        for (var i = 0; i < $scope.stockList.length; i++) {
            if ($scope.stockList[i].Symbol == symbol) {
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
            title: { text: "Date" }
        },
        yAxis: {
            title: { text: "Value ($)" }
        },
        loading: false
    };

    $scope.isStockListEmpty = function() {
        if ($scope.stockList.length > 0) {
            return true;
        } else {
            return false;
        }
    };

});