app.service("autocompleteService", ["$http", function ($http) {
    return {
        getSymbols: function (selected) {
            return $http.post("/Portfolio/AutoComplete", { "symbol": selected })
                .then(function (response) { // return promise.
                    return response.data; // data only return, json in this case.
                });
        }
    };
}]);