"use strict";

app.controller("OptionController", ["$scope", "autocompleteService", "toaster", "optionAnalysisService", function ($scope, autocompleteService, toaster, optionAnalysisService) {

    $scope.selected = undefined;
    $scope.options = [];

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
            $scope.volatility = data[0].Volatility;
            $scope.filteredOptions = data;
            console.log("Option promise forefilled data count: " + data.length);
            $scope.selected = undefined;
            $scope.loading = false;
            if (data.length === 0) {
                toaster.pop("information", "No options found.", "Please try another symbol/company as not all companies have option listings");
            }
        });

    };

    $scope.filterByInTheMoney = function () {
        if ($scope.inTheMoney) {
            $scope.filteredOptions = [];
            for (var i = 0; i < $scope.options.length; i++) {
                if ($scope.options[i].InTheMoney) {
                    $scope.filteredOptions.push($scope.options[i]);
                }
            }
        }
        if (!$scope.inTheMoney) {
            $scope.filteredOptions = $scope.options;
        }
    };

    $scope.filterOptions = {
        filterText: ''
    };

    $scope.gridOptions = {
        data: "filteredOptions",
        filterOptions: $scope.filterOptions,
        enablePinning: true,
        enableColumnResize: true,
        columnDefs: [
            {
                field: "StrikePrice", displayName: "Strike", width: "10%",
                cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            },
            {
                field: "Symbol", width: "18%", cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            },
            {
                field: "LastPrice", displayName: "Last", width: "5%",
                cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            },
            {
                field: "Change", width: "7%",
                cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            },
            {
                field: "Bid", width: "5%",
                cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            },
            {
                field: "Ask", width: "5%",
                cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            },
            {
                field: "Vol", width: "5%",
                cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            },
            {
                field: "OpenInt", width: "8%",
                cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            },
            {
                field: "DaysToExpiry", width: "4%", displayName: "DTE",
                cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            },
            {
                field: "BlackScholes", displayName: "Black Scholes", width: "11%",
                cellTemplate: '<div ng-class="{optionGridRowInTheMoney: row.getProperty(\'InTheMoney\'), optionGridRowAtTheMoney: row.getProperty(\'AtTheMoney\')}"><div class="ngCellText">{{row.getProperty(col.field)}}</div></div>'
            }
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