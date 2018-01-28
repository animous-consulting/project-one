var app = angular.module('organizationUnitAndMonthSelectorApp', ['angularBootstrapNavTree', 'ae-datetimepicker']);
app.controller('organizationUnitAndMonthSelectorController', function ($scope, $http, $timeout, $filter) {
    var timer;
    $scope.isTreeInit = false;
    $scope.organizationUnitTreeData = [];
    $scope.organizationUnits = organizationUnits;
    $scope.selectedOrganizationUnit = selectedOrganizationUnit;
    $scope.organizationUnitTreeData = organizationUnitTreeData;
    $scope.clickable = clickable;
    $scope.date = new Date(date).toISOString();
    $scope.getOrganizationUnitName = function (id) {
        var filteredOrganizationUnits = $filter('filter')($scope.organizationUnits, { 'OrganizationUnitID': id }, true);
        if (filteredOrganizationUnits[0] != null) { return filteredOrganizationUnits[0].OrganizationUnitName.trim(); } else { return ''; }
    };
    $scope.edit = function () {
        if (!$scope.isEdited && !$scope.isLoading) $scope.isEdited = true;
        $timeout.cancel(timer);
    };
    $scope.doneEdit = function () {
        timer = $timeout(function () {
            $scope.isEdited = false;
        }, 1000);
    };
    $scope.setSelectedBranch = function (branch) {
        if ($scope.selectedOrganizationUnit != branch.data) {
            $scope.selectedOrganizationUnit = branch.data;
            $scope.setClickable(true);
        }
    };
    $scope.openOrganizationUnitPicker = function () {
        $("#modal_organizationunit_picker").modal();
    };
    $scope.setClickable = function (b) {
        $scope.clickable = b;
        if (b == true) { $("#ng-app").hide(); }
    };
    $scope.getDisplayDate = function (d) {
        return moment(d).format('MMM YYYY');
    };
    $scope.datePickerOption = {
        format: 'MMM YYYY'
    };
    $scope.$watch('date', function (oldVal, newVal) {
        if (Date.parse(newVal) !== Date.parse(oldVal)) $scope.setClickable(true);
    })
});
