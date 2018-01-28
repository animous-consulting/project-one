var app = angular.module('app', ['monospaced.elastic']);
app.controller('organizationUnitController', function ($scope, $http, $filter) {
    $scope.model = model;
    $scope.getOrganizationUnitName = function (id) {
        var organizationUnits = $filter('filter')(model, { 'OrganizationUnitID': id }, true);
        if (organizationUnits[0] != null) { return organizationUnits[0].OrganizationUnitName.trim(); }
        return '';
    };
    $scope.addItem = function () {
        $scope.reset();
        $scope.addedModel = angular.copy(newitem);
        $("#modal-create").modal({ backdrop: 'static', keyboard: false });
    };
    $scope.editItem = function (item) {
        $scope.reset();
        $scope.editedModel = angular.copy(item);
        $scope.editedModel.OrganizationUnitName = $scope.editedModel.OrganizationUnitName.trim();
        $("#modal-edit").modal({ backdrop: 'static', keyboard: false });
    };
    $scope.save = function () {
        $scope.isSaving = true;
        $http({
            method: 'post',
            url: urlCreate,
            data: $scope.addedModel
        }).then(function successCallback(response) {
            if (response.data.result == "OK") {
                $scope.model = response.data.model;
                $("#modal-create").modal('hide');
            } else {
                $scope.errorMessage = response.data.result;
            }
        }, function errorCallback(response) {
            $scope.errorMessage = response.statusText;
        })["finally"](function () {
            $scope.isSaving = undefined;
        });
    };
    $scope.update = function () {
        $scope.isSaving = true;
        $http({
            method: 'post',
            url: urlEdit,
            data: $scope.editedModel
        }).then(function successCallback(response) {
            if (response.data.result == "OK") {
                var model = response.data.model;
                if (Array.isArray(model)) {
                    $scope.model = model;
                } else {
                    var idx = -1;
                    $scope.model.some(function (el, i) {
                        if (el.OrganizationUnitID == model.OrganizationUnitID) { idx = i; return true; }
                    });
                    $scope.model[idx] = model;
                }
                $("#modal-edit").modal('hide');
            } else {
                $scope.errorMessage = response.data.result;
            }
        }, function errorCallback(response) {
            $scope.errorMessage = response.statusText;
        })["finally"](function () {
            $scope.isSaving = undefined;
        });
    };
    $scope.isChecked = function (id) {
        var ischecked = false;
        if ($scope.addedModel) {
            if ($scope.addedModel.CompanyTag) {
                if ($scope.addedModel.CompanyTag.indexOf(id) > -1) {
                    ischecked = true;
                }
            }
        }
        if ($scope.editedModel) {
            if ($scope.editedModel.CompanyTag) {
                if ($scope.editedModel.CompanyTag.indexOf(id) > -1) {
                    ischecked = true;
                }
            }
        }
        return ischecked;
    }
    $scope.reset = function () {
        $scope.errorMessage = undefined;
        $scope.addedModel = undefined;
        $scope.editedModel = undefined;
        $scope.deletedModel = undefined;
    };
    $("#modal-create").on('hidden.bs.modal', function () { $scope.reset(); });
    $("#modal-edit").on('hidden.bs.modal', function () { $scope.reset(); });
});
app.directive('ncgRequestVerificationToken', ['$http', function ($http) {
    return function (scope, element, attrs) {
        $http.defaults.headers.common['RequestVerificationToken'] = attrs.ncgRequestVerificationToken || "no request verification token";
    };
}]);