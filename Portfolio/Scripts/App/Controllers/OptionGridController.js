app.controller("OptionGridController", function ($scope) {

    $scope.optionGrid = {
        data: "optionData",
        multiSelect: false,
        selectedItems: [],
        columnDefs: [
            { field: "Symbol", width: "27%" },
            { field: "StrikePrice", displayName: "Strike", width: "10%" },
            { field: "LastPrice", displayName: "Last", width: "10%" },
            { field: "Change", width: "10%" },
            { field: "Bid", width: "10%" },
            { field: "Ask", width: "10%" },
            { field: "Vol", width: "10%" },
            { field: "OpenInt", width: "10%" }
        ]
    };

});