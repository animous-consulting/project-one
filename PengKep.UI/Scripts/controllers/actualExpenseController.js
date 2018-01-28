var chartData;
var cellheight = 25;
var scrollspaceheight = 20;
var app = angular.module('app', ['chart.js', 'darthwade.dwLoading']);
app.controller('actualExpenseController', function ($scope, $http, $filter, $loading) {
    $scope.expenseCategories = expenseCategories;
    $scope.organizationUnitId = organizationUnitId;
    $scope.year = year;
    $scope.model = model;
    $scope.isVar1Shown = true;
    $scope.isVar2Shown = true;
    $scope.isVar3Shown = true;
    $scope.detailViewBy = 'C';

    $scope.getOrganizationUnitName = function (id) {
        var filteredOrganizationUnit = $filter('filter')(organizationUnits, { 'OrganizationUnitID': id });
        if (filteredOrganizationUnit[0]) { return filteredOrganizationUnit[0].OrganizationUnitName; } else { return ''; }
    };
    $scope.sheet1 = {
        settings: {
            manualColumnResize: true
        },
        rowHeaders: false,
        height: 250,
        minSpareRows: 0,
        maxRows: 1
    };
    $scope.sheet2 = {
        settings: {
            manualColumnResize: true
        },
        rowHeaders: false,
        height: 250,
        minSpareRows: 0,
        maxRows: 1
    };
    $scope.getActualExpenseTotal = function (category, month) {
        var total = 0;
        var filteredModel;
        if (category) {
            filteredModel = $filter('filter')($scope.model.ActualExpense, { ExpenseCategoryID: category }, true);
        } else {
            filteredModel = $scope.model.ActualExpense;
        }
        for (i = 0; i < filteredModel.length; i++) {
            if (month == 1 || month == null) total += filteredModel[i].Jan;
            if (month == 2 || month == null) total += filteredModel[i].Feb;
            if (month == 3 || month == null) total += filteredModel[i].Mar;
            if (month == 4 || month == null) total += filteredModel[i].Apr;
            if (month == 5 || month == null) total += filteredModel[i].May;
            if (month == 6 || month == null) total += filteredModel[i].Jun;
            if (month == 7 || month == null) total += filteredModel[i].Jul;
            if (month == 8 || month == null) total += filteredModel[i].Aug;
            if (month == 9 || month == null) total += filteredModel[i].Sep;
            if (month == 10 || month == null) total += filteredModel[i].Oct;
            if (month == 11 || month == null) total += filteredModel[i].Nov;
            if (month == 12 || month == null) total += filteredModel[i].Dec;
        }
        return total;
    };
    $scope.getCumulativeActualExpenseTotal = function (category, month) {
        var total = 0;
        if (month == null) month = 12;
        for (var imonth = 1; imonth <= month; imonth++) {
            total += $scope.getActualExpenseTotal(category, imonth);
        }
        return total;
    };
    $scope.reset = function () {
        $scope.editedActualExpenseCollection = undefined;
        $scope.isEditingActualExpense = undefined;
        $scope.isSaving = undefined;
        $scope.errorMessage = undefined;
    };
    $scope.getChartData = function (organizationUnitId, year) {
        $scope.isChartLoading = true;
        if (organizationUnitId != '' && year != '') {
            $http({
                method: 'post',
                url: urlGetChartData,
                data: { 'organizationUnitId': organizationUnitId, 'year': year }
            }).then(function successCallback(response) {
                if (response.status == 200) {
                    if (response.data.status == "OK") {
                        $scope.chartData = response.data.data;
                    }
                }
            }, function errorCallback(e) {
                console.log(e);
            }).finally(function () {
                $scope.isChartLoading = false;
            });
        } else {
            $scope.isChartLoading = false;
        }
    };
    $scope.chartLabels = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    $scope.chartSeries = ['Series A', 'Series B', 'Series C'];
    $scope.chartOptions = {
        scales: {
            yAxes: [
              {
                  id: 'y-axis-1',
                  type: 'linear',
                  display: true,
                  position: 'left',
                  ticks: {
                      beginAtZero: true,
                  },
                  stacked: true,
                  scaleLabel: {
                      display: true,
                      labelString: 'x1000 USD'
                  }
              },
              {
                  id: 'y-axis-2',
                  type: 'linear',
                  display: true,
                  position: 'right',
              }
            ],
            xAxes: [{ stacked: true }]
        },
        legend: {
            display: true,
            position: 'bottom'
        },
        title: {
            display: true,
            text: organizationUnitName + ' ' + year + ' ACTUAL EXPENSE'
        },
    };

    $scope.datasetOverride = [
        {
            yAxisID: 'y-axis-2',
            label: "CUMULATIVE COST",
            borderWidth: 3,
            borderColor: "rgba(226, 24,54, 0.7)",
            backgroundColor: 'transparent',
            pointBackgroundColor: "rgba(226, 24,54, 0.7)",
            pointBorderColor: "rgba(226, 24,54, 0.7)",
            pointHoverBackgroundColor: "rgba(226, 24,54, 0.7)",
            pointHoverBorderColor: "rgba(226, 24,54, 0.7)",
            pointBorderWidth: 1,
            type: 'line',
            tension: 0
        },
        {
            yAxisID: 'y-axis-1',
            label: "ROUTINE COST",
            borderWidth: 2,
            type: 'bar',
            backgroundColor: 'rgba(0, 102, 178, 0.4)',
            borderColor: 'rgba(0, 102, 178, 0.6)',
        },
        {
            yAxisID: 'y-axis-1',
            label: "NON ROUTINE COST",
            borderWidth: 2,
            type: 'bar',
            backgroundColor: 'rgba(8, 51, 85, 0.5)',
            borderColor: 'rgba(8, 51, 85, 0.7)',
        }
    ];
    $scope.dw = {
        loadingoptions: {
            spinner: false,
            text: ''
        }
    };
});
app.directive('ncgRequestVerificationToken', ['$http', function ($http) {
    return function (scope, element, attrs) {
        $http.defaults.headers.common['RequestVerificationToken'] = attrs.ncgRequestVerificationToken || "no request verification token";
    };
}]);
app.filter("jsonDate", function () {
    return function (x) {
        return new Date(parseInt(x.substr(6)));
    };
});
