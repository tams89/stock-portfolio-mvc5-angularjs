app.service("optionAnalysisService", function($http, $q) {

    var optionData = [];

    this.getOptionData = function() {
        return optionData;
    };

    this.getOptionDataBySymbol = function(symbol) {
        for (var i = 0; i <= optionData.length; i++) {
            if (optionData[i][0].Symbol === symbol) {
                return optionData[i];
            }
        }
        return null;
    };

    this.getOptions = function(symbol) {
        var deferred = $q.defer();
        $http.post("/Portfolio/OptionData", { "symbol": symbol }).success(function(data) {
            deferred.resolve(data);
        });
        return deferred.promise;
    };
   
    // TODO
    this.postSelectedOption = function (symbol) {
        $http.get("/Portfolio/Options", { "symbol": symbol });
    };

});