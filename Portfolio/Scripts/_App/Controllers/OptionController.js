"use strict";

app.controller("OptionController", ["$scope", "autocompleteService", "toaster", "optionAnalysisService", function ($scope, autocompleteService, toaster, optionAnalysisService) {

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
            console.log("Option promise forefilled data count: " + data.length);
            $scope.selected = undefined;
            $scope.loading = false;
            if (data.length === 0) {
                toaster.pop("information", "No options found.", "Please try another symbol/company as not all companies have option listings");
            }
        });

    };
    
    $scope.filterOptions = {
        filterText: ''
    };
    

    
    $scope.gridOptions = {
        data: "options",
        filterOptions: $scope.filterOptions,
        enablePinning: true,
        enableColumnResize: true,
        columnDefs: [
            {
                field: "StrikePrice",
                displayName: "Strike",
                width: "10%",
                cellTemplate:
                    '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}">' +
                        '<div class="ngCellText">{{row.getProperty(col.field)}}</div' +
                        '></div>'
            },
            { field: "Symbol", width: "18%" },
            { field: "LastPrice", displayName: "Last", width: "5%" },
            { field: "Change", width: "10%" },
            { field: "Bid", width: "5%" },
            { field: "Ask", width: "5%" },
            { field: "Vol", width: "5%" },
            { field: "OpenInt", width: "8%" },
            { field: "DaysToExpiry", width: "5%", displayName: "DoE" },
            { field: "BlackScholes", displayName: "Black Scholes", width: "15%" }
        ]
    };

    // Example - custom filter by name    
    //$scope.filterNephi = function () {
    //    var filterText = 'name:Nephi';
    //    if ($scope.filterOptions.filterText === '') {
    //        $scope.filterOptions.filterText = filterText;
    //    } else if ($scope.filterOptions.filterText === filterText) {
    //        $scope.filterOptions.filterText = '';
    //    }
    //};

}]);