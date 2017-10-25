angular.module('userExpense.services', ['ngResource'])

.service('expenseService', ['$resource', function ($resource) {
    return $resource('http://localhost:53415/api/expenses/:id');
}]);

