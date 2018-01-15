app = angular.module('app', []);
app.controller('userManagementController', function ($scope, $http) {
    $scope.model = model;
    $scope.roles = roles;
    
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
        $scope.editedModel.LastAccess = moment($scope.editedModel.LastAccess);
        $http({
            method: 'post',
            url: urlEdit,
            data: $scope.editedModel
        }).then(function successCallback(response) {
            if (response.data.result == "OK") {
                var model = response.data.model;
                $scope.model.some(function (el, i) {
                    if (el.UserID == model.UserID) {
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