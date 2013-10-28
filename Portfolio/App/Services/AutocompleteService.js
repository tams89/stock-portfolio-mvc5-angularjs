app.service("autocompleteService", function ($http) {
    return {
        getSymbols: function (selected) {
            return $http.post("/Home/AutoComplete", { "symbol": selected })
                .then(function (response) { // return promise.
                    return response.data; // data only return, json in this case.
                });
        }
    };
});