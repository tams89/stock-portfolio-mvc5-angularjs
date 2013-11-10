﻿app.service("highStockService", function(utilitiesService) {

    var chart,
        seriesOptions = [],
        yAxisOptions = [],
        names = [],
        colors = Highcharts.getOptions().colors;

    this.addHistoricalDataSeries = function(data) {
        try {
            if (data == undefined) return;
            var dataNAdjClose = [];
            for (var i = 0; i <= data.length; i++) {
                if (data[i] != undefined && data[i].Date != null && data[i].AdjClose != null) {
                    var j = JSON.parse(data[i].Date.toString().substr(6, 13)); // re-parse to JSON to ensure validity.
                    var point = [j, data[i].AdjClose];
                    dataNAdjClose[i] = point;
                }
            }
            names.push(data[0].symbol);
            seriesOptions.push({ name: data[0].Symbol, data: dataNAdjClose, color: randomColorGen() });
        } catch(e) {
            console.log(e);
        }
    };

    this.createChart = function() {
        // create the chart when all data is ready.
        chart = $("#container").highcharts("StockChart", {
            chart: {
                reflow: true
            },
            rangeSelector: {
                selected: 4
            },
            xAxis: {
                type: "datetime"
            },
            yAxis: {
                labels: {
                    formatter: function() {
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

    this.duplicatedSeries = function(symbol) {
        return utilitiesService.isItemInArrayProp(seriesOptions, "name", symbol);
    };

    // Remove Series by name i.e. the symbol.
    this.removeSeries = function(name) {
        for (var i = 0; i <= seriesOptions.length; i++) {
            if (seriesOptions[i] != undefined && seriesOptions[i].name == name) {
                seriesOptions.splice(i, 1);
            }
        }
    };

    this.isChartEmpty = function() {
        if (seriesOptions.length == 0) return true;
        return false;
    };

    this.clearSeries = function() {
        seriesOptions = [];
    };

});