var app = angular.module('organizationUnitAndYearSelectorApp', ['angularBootstrapNavTree']);
app.controller('organizationUnitAndYearSelectorController', function ($scope, $http, $timeout, $filter) {
    var timer;
    $scope.isTreeInit = false;
    $scope.organizationUnitTreeData = [];
    $scope.organizationUnits = organizationUnits;
    $scope.years = years;
    $scope.selectedOrganizationUnit = selectedOrganizationUnit;
    $scope.organizationUnitTreeData = organizationUnitTreeData;
    $scope.selectedYear = selectedYear;
    $scope.clickable = clickable;
    $scope.model = model;
    $scope.getOrganizationUnitName = function (id) {
        var filteredOrganizationUnits = $filter('filter')($scope.organizationUnits, { OrganizationUnitID: id }, true); 
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
});
app.directive('ncgRequestVerificationToken', ['$http', function ($http) {
    return function (scope, element, attrs) {
        $http.defaults.headers.common['RequestVerificationToken'] = attrs.ncgRequestVerificationToken || "no request verification token";
    };
}]);