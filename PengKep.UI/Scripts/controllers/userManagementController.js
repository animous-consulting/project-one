﻿app = angular.module('app', []);
app.controller('userManagementController', function ($scope, $http, $filter) {
    $scope.model = model;
    $scope.roles = roles;

    $scope.getRoleNames = function (roles) {
        var roleNames = '';
        roles.forEach(function (el, i) {
            var filteredRoles = $filter('filter')($scope.roles, { Id: el.RoleId }, true);
            if (filteredRoles) {
                roleNames += filteredRoles[0].Name;
                if (i != roles.length - 1) { roleNames += ', '; }
            }
        });
        return roleNames;
    };
    $scope.isChecked = function (item) {
        var target;
        if ($scope.addedModel) { target = $scope.addedModel; }
        if ($scope.editedModel) { target = $scope.editedModel; }
        if (target) {
            return target.Roles.some(function (e) {
                return e.RoleId == item.Id;
            });
        }
    };
    $scope.toggleSelection = function toggleSelection(item, event) {
        var idx = -1;
        var target;
        if ($scope.addedModel) { target = $scope.addedModel; }
        if ($scope.editedModel) { target = $scope.editedModel; }
        if (target.Roles == null) target.Roles = [];
        target.Roles.some(function (el, i) {
            if (el.RoleId == item.Id) {
                idx = i; return true;
            }
        });
        if (idx > -1) {
            target.Roles.splice(idx, 1);
        } else {
            target.Roles.push({ 'RoleId': item.Id });
        }
    };

    $scope.addItem = function () {
        $scope.reset();
        $scope.addedModel = angular.copy(newitem);
        $("#modal-create").modal({ backdrop: 'static', keyboard: false });
    };
    $scope.editItem = function (item) {
        $scope.reset();
        $scope.editedModel = angular.copy(item);
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
                var model = response.data.model;
                $scope.model.splice(0, 0, model);
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
                $scope.model.some(function (el, i) {
                    console.log(el);
                    if (el.UserId == model.UserId) {
                        $scope.model[i] = model; return true;
                    }
                })
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
    $scope.reset = function () {
        $scope.errorMessage = undefined;
        $scope.addedModel = undefined;
        $scope.editedModel = undefined;
    };
});
app.directive('ncgRequestVerificationToken', ['$http', function ($http) {
    return function (scope, element, attrs) {
        $http.defaults.headers.common['RequestVerificationToken'] = attrs.ncgRequestVerificationToken || "no request verification token";
    };
}]);