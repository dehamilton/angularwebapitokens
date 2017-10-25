(function () {
    'use strict';
    var ModalController = function ($scope, $modalInstance, $http) {
        $scope.expense = {
            ExpenseDate: '',
            Description: '',
            Category: '',
            Amount: undefined
        };

        $scope.save = function () {
            $modalInstance.close($scope.expense);
        };

        $scope.blurValidator = function (control) {
            switch (control) {
                case 'date':
                    $scope.dateSkipped = true;
                    break;
                case 'desc':
                    $scope.descSkipped = true;
                    break;
                case 'cat':
                    $scope.catSkipped = true;
                    break;
                case 'amount':
                    $scope.amountSkipped = true;
                    break;
            }
        };

        $scope.dateIsInValid = function () {
            return ($scope.expense.ExpenseDate.toString().length == 0 || ($scope.expense.ExpenseDate.getFullYear() < 2000 || $scope.expense.ExpenseDate.getFullYear() > 2020));
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.openDateControl = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.opened = true;
        };

        $scope.getCategory = function (val) {
            return $http.get('http://localhost:53415/api/utils/categories/' + val).then(function (response) {
                return response.data;
            });
        };
    };

    ModalController.$inject = ['$scope', '$modalInstance', '$http'];

    angular.module('userExpense').controller('ModalController', ModalController);
}());