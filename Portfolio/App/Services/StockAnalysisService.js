app.service("stockAnalysisService", function ($http) {

    var stocks = [];

    // Ajax call?
    this.getStocks = function () {
        return stocks;
    };

    this.addStock = function (symbol) {
        return $http.post("/Home/MarketData", { "symbol": symbol })
            .then(function (response) {
                stocks.push(response.data);
            });
    };

    // Uses $index from ng-repeat to locate and splice element from array.
    this.removeStock = function (idx) {
        stocks.splice(idx, 1);
    };
    
});