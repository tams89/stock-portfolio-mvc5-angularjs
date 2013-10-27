app.factory("ajaxService", function ($http) {
    return {
        getSymbols: function (selected) {
            // Return symbols a.k.a the promise in angular lingo directly
            return $http.post("/Home/AutoComplete", { "symbol": selected })
                .then(function (response) {
                    // resolve promise as data.
                    return response.data;
                });
        }
    };
});