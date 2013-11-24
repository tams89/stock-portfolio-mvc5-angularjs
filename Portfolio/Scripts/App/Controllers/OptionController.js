"use strict";

app.controller("OptionController", function ($scope, autocompleteService, toaster, optionAnalysisService) {

    $scope.currentPage = 0;
    $scope.pageSize = 10;
    $scope.options = [];
    $scope.selected = undefined;

    init();

    function init() {
        $scope.options = optionAnalysisService.getOptionData();
    }

    $scope.symbols = [];
    $scope.OnInputChange = function () {
        $scope.symbols = autocompleteService.getSymbols($scope.selected);
    };

    $scope.SelectSymbol = function () {
        var symbol = $scope.selected.Symbol;
        $scope.loading = true;
        var optionDataPromise = optionAnalysisService.getOptions(symbol);
        optionDataPromise.then(function (data) {
            $scope.options = data;
            console.log("Option promise forefilled data count: " + $scope.options.length);
            $scope.selected = undefined;
            $scope.loading = false;
            if ($scope.options.length === 0) {
                toaster.pop("information", "No options found.", "Please try another symbol/company as not all companies have option listings");
            }
        });
    };

    $scope.numberOfPages = function () {
        return Math.ceil($scope.options.length / $scope.pageSize);
    };
    
});

app.filter("startFrom", function() {
    return function(input, start) {
        start = +start;
        return input.slice(start);
    };
});