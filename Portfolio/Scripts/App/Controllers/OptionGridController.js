app.controller("OptionGridController", function ($scope) {

    $scope.optionGrid = {
        data: "optionData",
        multiSelect: false,
        selectedItemns: $scope.selectedOptions,
        columnDefs: [
            { field: "Symbol", width: "28%" },
            { field: "Type" },
            { field: "StrikePrice", displayName: "Strike" },
            { field: "LastPrice", displayName: "Last" },
            { field: "Change" },
            { field: "Bid" },
            { field: "Ask" },
            { field: "Vol" },
            { field: "OpenInt" }
        ]
    };
    
});