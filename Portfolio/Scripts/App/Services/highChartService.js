app.service("highChartService", function (stockAnalysisService) {

    // Add series of historical data by symbol.
    this.addSeries = function (chart, data, seriesName) {
        if (chart.series == null) {
            this.clearSeries(chart);
        }

        if (!this.seriesExists(chart, seriesName)) {
            chart.series.push({
                name: seriesName,
                data: data
            });
        }
    };

    this.clearSeries = function (chart) {
        chart.series = [];
    };

    this.updateChart = function updateChart(chart, symbols) {
        this.clearSeries(chart);
        for (var i = 0; i <= symbols.length; i++) {
            if (symbols[i] != undefined) {
                var data = stockAnalysisService.getHistoricalDataBySymbol(symbols[i].Symbol);
                var closingValues = stockAnalysisService.flattenData("AdjClose", data);
                this.addSeries(chart, closingValues, symbols[i].Symbol);
            }
        }
        if (data != null) {
            var categories = stockAnalysisService.flattenData("Date", data);
            if (categories != null) {
                var dates = [];
                for (var j = 0; j <= categories.length; j++) {
                    if (categories[j] != null) {
                        var date = new Date(parseInt(categories[j].toString().substr(6, 13)));
                        dates.push(date);
                    }
                }

                chart.xAxis.categories.
                chart.xAxis.categories.push(dates);
                chart.xAxis.tickInterval = dates.length % 70;
            }
        }
    };

    this.seriesExists = function (chart, seriesName) {
        for (var i = 0; i <= chart.series.length; i++) {
            if (chart.series[i] != null && chart.series[i].name == seriesName) {
                return true;
            }
        }
        return false;
    };

    // Take in an array (can be nested to any level) and returns a flattened array of the property 
    // specified values.
    this.flattenData = function (flattenByProp, array) {
        return _.flatten(_.pluck(array, flattenByProp.toString()));
    };

});