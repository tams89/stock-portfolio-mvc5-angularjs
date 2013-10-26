app.factory("ajaxService", function($http) {
    return {
        getSymbols: function() {
            // Return symbols a.k.a the promise n angular lingo directly
            return $http.get("/Home/AutoComplete")
                .then(function() {
                    // resolve promise as data.
                    return result.data;
                });
        }
    };
});