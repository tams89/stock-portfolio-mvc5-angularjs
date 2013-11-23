"use strict";

app.controller("OptionController", function ($scope, autocompleteService, toaster, optionAnalysisService) {

    //$scope.optionData = [];
    $scope.selected = undefined;
    //$scope.optionTable = undefined;
    
    //init();

    //function init() {
    //    $scope.optionData = optionAnalysisService.getOptionData();
    //}

    $scope.symbols = [];
    $scope.OnInputChange = function () {
        $scope.symbols = autocompleteService.getSymbols($scope.selected);
    };

    $scope.SelectSymbol = function() {
        var symbol = $scope.selected.Symbol;
        $scope.loading = true;
        var optionDataPromise = optionAnalysisService.getOptions(symbol);
        optionDataPromise.then(function(data) {
            $scope.items = data;
            console.log("Option promise forefilled data count: " + $scope.items.length);
            $scope.selected = undefined;
            $scope.loading = false;
        });
    };
    
});