app.service("stockAnalysisService", function ($http, highChartService) {

    var stocks = [];
    var historicalData = [];

    this.getStocks = function () {
        return stocks;
    };

    this.getHistoricalDataBySymbol = function (symbol) {
        for (var i = 0; i <= historicalData.length; i++) {
            if (historicalData[i][0].Symbol === symbol) {
                return historicalData[i];
            }
        }
        return null;
    };

    this.addStock = function (symbol) {
        return $http.post("/Home/MarketData", { "symbol": symbol })
            .success(function (data) {
                historicalData.push(data);
                stocks.push(data[0]);
            });
    };

    // Uses $index from ng-repeat to locate and splice element from array.
    this.removeStock = function (idx) {
        stocks.splice(idx, 1);
    };

    this.flattenData = function (flattenByProp, array) {
        return _.flatten(_.pluck(array, flattenByProp.toString()));
    };

});