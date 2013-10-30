app.service("highChartService", function () {

    // Add series of historical data by symbol.
    this.addSeries = function (chart, data, seriesName) {
        if (chart.series == null) {
            chart.series = [];
        }
        chart.series.push({
            name: seriesName,
            data: data
        });
    };

    this.removeRandomSeries = function () {
        var seriesArray = $scope.chart.series;
        var rndIdx = Math.floor(Math.random() * seriesArray.length);
        seriesArray.splice(rndIdx, 1);
    };

    this.flattenData = function (flattenByProp, array) {
        return _.flatten(_.pluck(array, flattenByProp.toString()));
    };

});