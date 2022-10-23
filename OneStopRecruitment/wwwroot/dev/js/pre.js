/* Chart JS */
function getColorForChart(index) {
    var colors = [
        "#f44336", // RED
        "#ffeb3b", // YELLOW
        "#4caf50", // GREEN
        "#2196f3", // CYAN
        "#3f51b5", // BLUE
        "#673ab7", // PURPLE
        "#9c27b0", // VIOLET
        "#f06292", // PINK
        "#e91e63", // MAGENTA
        "#ff5722", // ORANGE
    ]

    return colors[index % 10];
}

var barOptions = {
    scales: {
        xAxes: [{
            ticks: {
                beginAtZero: true,
                max: 100
            }
        }]
    },
    legend: {
        display: false
    },
    tooltips: {
        enabled: false
    },
    plugins: {
        datalabels: {
            formatter: (value, ctx) => {
                let sum = 0;
                let dataArr = ctx.chart.data.datasets[0].data;
                dataArr.map(data => {
                    sum += data;
                });
                let percentage = value + " (" + (value * 100 / sum).toFixed(2) + "%" + ")";
                return percentage;
            },
            color: '#000000',
        }
    }
}

var doughnutOptions = {
    legend: {
        display: true,
        position: 'right',
        labels: {
            fontSize: 14,
            fontColor: '#000000'
        }
    },
    tooltips: {
        enabled: false
    },
    plugins: {
        datalabels: {
            formatter: (value, ctx) => {
                let sum = 0;
                let dataArr = ctx.chart.data.datasets[0].data;
                dataArr.map(data => {
                    sum += data;
                });
                let percentage = value + " (" + (value * 100 / sum).toFixed(2) + "%" + ")";
                return percentage;
            },
            color: '#000000',
        }
    }
}