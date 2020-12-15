//// Set new default font family and font color to mimic Bootstrap's default styling
//Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
//Chart.defaults.global.defaultFontColor = '#858796';

//// Pie Chart Example
//var url = '@Url.Action("PieChartTypes","charts")'
//var ctx = document.getElementById("myPieChart");
//var myPieChart = new Chart(ctx, {
//  type: 'doughnut',
//  data: {
//    labels: ["UI", "Database", "User Experience", "Back End", "Feature Request" ],
//    datasets: [{
//      data: [2, 5, 7, 6, 2],
//        backgroundColor: ['#0275d8', '#5cb85c', '#5bc0de', '#d9534f', '#292b2c'],
//        hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf', '#ab3330', '#0a0a0a'],
//      hoverBorderColor: "rgba(234, 236, 244, 1)",
//    }],
//  },
//  options: {
//    maintainAspectRatio: false,
//    tooltips: {
//      backgroundColor: "rgb(255,255,255)",
//      bodyFontColor: "#858796",
//      borderColor: '#dddfeb',
//      borderWidth: 1,
//      xPadding: 15,
//      yPadding: 15,
//      displayColors: false,
//      caretPadding: 10,
//    },
//    legend: {
//      display: false
//    },
//    cutoutPercentage: 80,
//  },
//});
