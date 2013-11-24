app.service("stockAnalysisService", ["$http", function ($http) {

    var stocks = [];
    var historicalData = [];
    var companySymbolKey = [];

    this.getStockList = function () {
        return stocks;
    };

    this.getCompanySymbolKey = function () {
        return companySymbolKey;
    };

    this.persistCompanySymbolKey = function (data) {
        companySymbolKey.push(data);
    };

    this.getHistoricalData = function () {
        return historicalData;
    };

    this.getHistoricalDataBySymbol = function (symbol) {
        for (var i = 0; i <= historicalData.length; i++) {
            if (historicalData[i][0].Symbol === symbol) {
                return historicalData[i];
            }
        }
        return null;
    };

    this.getStockData = function (symbol) {
        return $http.post("/Portfolio/MarketData", { "symbol": symbol })
            .success(function (data) {
                historicalData.push(data);
                stocks.push(data[data.length - 1]);
                return data.length;
            });
    };

    // Uses $index from ng-repeat to locate and splice element from array.
    this.removeStock = function (idx) {
        stocks.splice(idx, 1);
    };

}]);