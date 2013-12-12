app.service("highStockService", ["utilitiesService", function (utilitiesService) {

    var chart,
        seriesOptions = [],
        yAxisOptions = [],
        names = [];

    // Takes in JSON containing a companies historical market data and parses it into a valid HighStock series.
    this.addHistoricalDataSeries = function (data) {
        try {
            if (data == undefined) return;
            var dataNAdjClose = [];
            for (var i = 0; i <= data.length; i++) {
                if (data[i] !== undefined && data[i].Date !== null && data[i].AdjClose !== null) {
                    // Converted to string then all non-numeric chars removed and then parsed to JSON object to ensure
                    // date is valid, else the HighStock graph will not render the series. 
                    var j = JSON.parse(data[i].Date.toString().replace(/\D/g, ''));
                    var point = [j, data[i].AdjClose];
                    dataNAdjClose[i] = point;
                }
            }
            names.push(data[0].Symbol);
            seriesOptions.push({ name: data[0].Symbol, data: dataNAdjClose, color: randomColorGen() });
        } catch (e) {
            console.log(e);
        }
    };

    // Generates the code to generate the HighStock chart object. 
    this.createChart = function () {
        // create the chart when all data is ready.
        chart = $("#container").highcharts("StockChart", {
            chart: {
                backgroundColor: 'rgba(40,40,40,0.40)'
            },
            rangeSelector: {
                selected: 4
            },
            xAxis: {
                type: "datetime"
            },
            yAxis: {
                labels: {
                    formatter: function () {
                        return (this.value > 0 ? "+" : "") + this.value + "%";
                    }
                },
                plotLines: [{
                    value: 0,
                    width: 2,
                }]
            },
            plotOptions: {
                series: {
                    compare: "percent",
                }
            },
            tooltip: {
                pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.change}%)<br/>',
                valueDecimals: 2
            },
            series: seriesOptions
        });
    };

    // Returns random colour from array.
    function randomColorGen() {
        var colours = ['#D200D6', '#D60C00', '#5BD600', '#000000', '#00C5FF', '#0052FF', '#F09600'];
        var random = Math.floor((Math.random() * 7) + 1); // random number 1-7
        return colours[random];
    }

    // Check for if the series already exists using the same symbol.
    this.duplicatedSeries = function (symbol) {
        return utilitiesService.isItemInArrayByProperty(seriesOptions, "name", symbol);
    };

    // Remove Series by name i.e. the symbol.
    this.removeSeries = function (name) {
        for (var i = 0; i <= seriesOptions.length; i++) {
            if (seriesOptions[i] !== undefined && seriesOptions[i].name === name) {
                seriesOptions.splice(i, 1);
            }
        }
    };

    // Is the chart empty i.e. does it contain any series?
    this.isChartEmpty = function () {
        if (seriesOptions.length === 0) return true;
        return false;
    };

    // Remove all series from the graph by setting the series object to an empty array.
    this.clearSeries = function () {
        seriesOptions = [];
    };

}]);