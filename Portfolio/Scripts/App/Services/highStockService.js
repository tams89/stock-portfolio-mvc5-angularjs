app.service("highStockService", function () {

    var seriesOptions = [],
        yAxisOptions = [],
        seriesCounter = 0,
        names = [],
        colors = Highcharts.getOptions().colors;

    this.parseHistoricalData = function (data) {
        if (data == undefined) return;
        var dataNAdjClose = [];
        for (var i = 0; i <= data.length; i++) {
            if (data[i] != undefined && data[i].Date != null && data[i].AdjClose != null) {
                try {
                    var j = JSON.parse(data[i].Date.toString().substr(6, 13)); // re-parse to JSON to ensure validity.
                    var point = [j, data[i].AdjClose];
                    dataNAdjClose[i] = point;
                } catch (e) {
                    alert(e);
                }

            }
        }
        names.push(data[0].symbol);
        seriesOptions.push({ name: data[0].Symbol, data: dataNAdjClose });
        seriesCounter++;
    };

    this.createChart = function () {
        // create the chart when all data is ready.
        $("#container").highcharts("StockChart", {
            chart: {
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
                    color: "silver"
                }]
            },
            plotOptions: {
                series: {
                    compare: "percent"
                }
            },
            tooltip: {
                pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.change}%)<br/>',
                valueDecimals: 2
            },
            series: seriesOptions
        });
    };

    this.duplicatedSeries = function (symbol) {
        for (var i = 0; i < names.length; i++) {
            if (names[i] === symbol) {
                return true;
            }
        }
        return false;
    };

    this.alternateChart = function () {
        var seriesOptions = [],
            yAxisOptions = [],
            seriesCounter = 0,
            names = ['MSFT', 'AAPL', 'GOOG'],
            colors = Highcharts.getOptions().colors;
        $.each(names, function (i, name) {
            $.getJSON('http://www.highcharts.com/samples/data/jsonp.php?filename=' + name.toLowerCase() + '-c.json&callback=?', function (data) {
                seriesOptions[i] = {
                    name: name,
                    data: data
                };
                // As we're loading the data asynchronously, we don't know what order it will arrive. So
                // we keep a counter and create the chart when all the data is loaded.
                seriesCounter++;
                if (seriesCounter == names.length) {
                    createChart();
                }
            });
        });
        // create the chart when all data is loaded
        function createChart() {
            $('#container').highcharts('StockChart', {
                chart: {
                },
                rangeSelector: {
                    selected: 4
                },
                yAxis: {
                    labels: {
                        formatter: function () {
                            return (this.value > 0 ? '+' : '') + this.value + '%';
                        }
                    },
                    plotLines: [{
                        value: 0,
                        width: 2,
                        color: 'silver'
                    }]
                },
                plotOptions: {
                    series: {
                        compare: 'percent'
                    }
                },
                tooltip: {
                    pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b> ({point.change}%)<br/>',
                    valueDecimals: 2
                },
                series: seriesOptions
            });
        }
    };

});