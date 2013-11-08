app.controller("OptionGridController", function($scope) {

    $scope.optionGrid = {
        data: "optionData",
        multiSelect: false,
        selectedItemns: $scope.selectedOptions,
        columnDefs: [
            { field: "Symbol", width: "30%" },
            { field: "StrikePrice", displayName: "Strike", width: "22%" },
            { field: "Ask", width: "22%" },
            { field: "Bid", width: "22%" }
        ]
    };

});