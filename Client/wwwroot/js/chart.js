$(document).ready(function () {
  chart.render();
  chart2.render();
});

const dataSeries = [];
const dataLabel = [];

var dataProp = $.ajax({
  type: 'GET',
  url: 'https://localhost:5001/api/employees/chart',
  success: function (data) {
    $.each(data, function (index, value) {
      dataSeries.push(value.count);
      dataLabel.push(value.degree);
		})
	}
})

// ApexChart
var options = {
  chart: {
    type: 'pie'
  },
  series: dataSeries,
  labels: dataLabel
}

var chart = new ApexCharts(document.querySelector("#chart"), options);

// ApexChart
var options = {
  chart: {
    type: 'donut'
  },
  series: [5,2],
  labels: ['Laki-Laki', 'Perempuan']
}

var chart2 = new ApexCharts(document.querySelector("#chart2"), options);
