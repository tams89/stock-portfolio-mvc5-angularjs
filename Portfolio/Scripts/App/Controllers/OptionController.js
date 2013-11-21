"use strict";

app.controller("OptionController", function($scope, autocompleteService, toaster, optionAnalysisService) {

    $scope.optionData = [];
    $scope.selected = undefined;

    init();

    function init() {
        $scope.optionData = optionAnalysisService.getOptionData();
    }

    $scope.symbols = [];
    $scope.OnInputChange = function() {
        $scope.symbols = autocompleteService.getSymbols($scope.selected);
    };

    //$scope.SelectSymbol = function() {
    //    var symbol = $scope.selected.Symbol;
    //    $scope.loading = true;
    //    var optionDataPromise = optionAnalysisService.getOptions(symbol);
    //    optionDataPromise.then(function(data) {
    //        $scope.optionData = data;
    //        console.log("Option promise forefilled data count: " + $scope.optionData.length);
    //        $scope.loading = false;
    //    });
    //};
    
    $scope.SelectSymbol = function () {
        $scope.loading = true;
        optionAnalysisService.getOptionTableData($scope.selected.Symbol);
        $scope.loading = false;
    };

});