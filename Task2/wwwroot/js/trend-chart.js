$(function () {
    // Retrieve temperature chart data and create chart
    $.getJSON(temperatureChartDataUrl, function (data) {
        var temperatureData = {
            labels: [],
            datasets: [{
                label: 'Temperature (°C)',
                data: [],
                borderColor: 'red',
                borderWidth: 1,
                fill: false
            }]
        };

        $.each(data, function (i, item) {
            temperatureData.labels.push(item.Timestamp);
            temperatureData.datasets[0].data.push(item.Temperature);
        });

        var temperatureChart = new Chart($('#temperature-chart-container'), {
            type: 'line',
            data: temperatureData,
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    xAxes: [{
                        ticks: {
                            autoSkip: true,
                            maxTicksLimit: 10
                        }
                    }]
                }
            }
        });
    });

    // Retrieve wind speed chart data and create chart
    $.getJSON(windSpeedChartDataUrl, function (data) {
        var windSpeedData = {
        labels: [],
        datasets: [{
            label: 'Wind Speed (m/s)',
            data: [],
            borderColor: 'blue',
            borderWidth: 1,
            fill: false
        }]
    };

    $.each(data, function (i, item) {
        windSpeedData.labels.push(item.Timestamp);
        windSpeedData.datasets[0].data.push(item.WindSpeed);
    });

    var windSpeedChart = new Chart($('#wind-speed-chart-container'), {
        type: 'line',
        data: windSpeedData,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                xAxes: [{
                    ticks: {
                        autoSkip: true,
                        maxTicksLimit: 10
                    }
                }]
            }
        }
    });
});

    // Reload data every minute
    setInterval(function () {
        temperatureChart.destroy();
        windSpeedChart.destroy();

        $.getJSON(temperatureChartDataUrl, function (data) {
            var temperatureData = {
                labels: [],
                datasets: [{
                    label: 'Temperature (°C)',
                    data: [],
                    borderColor: 'red',
                    borderWidth: 1,
                    fill: false
                }]
            };

            $.each(data, function (i, item) {
                temperatureData.labels.push(item.Timestamp);
                temperatureData.datasets[0].data.push(item.Temperature);
            });

            temperatureChart = new Chart($('#temperature-chart-container'), {
                type: 'line',
                data: temperatureData,
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        xAxes: [{
                            ticks: {
                                autoSkip: true,
                                maxTicksLimit: 10
                            }
                        }]
                    }
                }
            });
        });

        $.getJSON(windSpeedChartDataUrl, function (data) {
            var windSpeedData = {
                labels: [],
                datasets: [{
                    label: 'Wind Speed (m/s)',
                    data: [],
                    borderColor: 'blue',
                    borderWidth: 1,
                    fill: false
                }]
            };

            $.each(data, function (i, item) {
                windSpeedData.labels.push(item.Timestamp);
                windSpeedData.datasets[0].data.push(item.WindSpeed);
            });

            windSpeedChart = new Chart($('#wind-speed-chart-container'), {
                type: 'line',
                data: windSpeedData,
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        xAxes: [{
                            ticks: {
                                autoSkip: true,
                                maxTicksLimit: 10
                            }
                        }]
                    }
                }
            });
        });
    }, 60000);
});
