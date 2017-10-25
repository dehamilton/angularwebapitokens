angular.module('userExpense.home', [
  'ui.router',
  'angular-storage',
  'angular-jwt',
  'ui.grid',
  'ui.bootstrap'
])
.config(['$stateProvider', function ($stateProvider) {
    $stateProvider.state('home', {
        url: '/',
        controller: 'HomeCtrl',
        templateUrl: 'app/home/home.html',
        data: {
            pageTitle: 'Home',
            requiresLogin: true
        }
    });
}]);

(function () {
    'use strict';
    var HomeCtrl = function ($scope, $http, $modal, expenses) {
        $scope.getExpenses = function () {
            expenses.query(function (data) {
                $scope.expenses = data;
                $scope.expensesCache = $scope.expenses;
            });
        };

        $scope.$watch(function (scope) {
            return scope.gridFilter;
        },
        function (newVal) {
            if (!$scope.expensesCache) return;

            if (newVal === '') {
                $scope.expenses = $scope.expensesCache;
                return;
            }

            $scope.expenses = [];
            $scope.expensesCache.forEach(function (e, i) {
                if ((e.Description.toLowerCase().indexOf(newVal.toLowerCase()) >= 0) || (e.Category.toLowerCase().indexOf(newVal.toLowerCase()) >= 0)) {
                    $scope.expenses.push(e);
                }
            });
        });

        $scope.addNewExpense = function (expense) {
            expenses.save(expense).$promise.then(function (data) {
                $scope.getExpenses();
            });
        };

        $scope.getExpenses();

        $scope.gridOptions = {
            enableSorting: true,
            columnDefs: [
                { field: 'ExpenseDate', displayName: 'Date', cellFilter: 'date:\'MM/dd/yyyy\'' },
                { field: 'Description' },
                { field: 'Category' },
                { field: 'Amount', cellFilter: 'currency' }
            ],
            data: 'expenses'
        };

        $scope.openModal = function (size) {
            var modalInstance = $modal.open({
                templateUrl: 'newExpense.html',
                controller: 'ModalController',
                size: size
            });

            modalInstance.result.then(function (expense) {
                $scope.addNewExpense(expense);
            });
        };
    };

    HomeCtrl.$inject = ['$scope', '$http', '$modal', 'expenseService'];

    angular.module('userExpense.home').controller('HomeCtrl', HomeCtrl);

}());