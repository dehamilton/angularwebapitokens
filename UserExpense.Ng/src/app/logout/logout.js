angular.module('userExpense.logout', [
  'ui.router',
  'angular-storage',
  'angular-jwt'
])
.config(['$stateProvider', function ($stateProvider) {
    $stateProvider.state('logout', {
        url: '/logout',
        controller: 'LogoutCtrl',
        templateUrl: 'app/logout/logout.html',
        data: {
            pageTitle: 'Logout'
        }
    });
}]);

(function () {
    'use strict';
    var LogoutCtrl = function ($scope, $http, store, $state) {
        store.remove('jwt');
        $state.go('home');
    };

    LogoutCtrl.$inject = ['$scope', '$http', 'store', '$state'];

    angular.module('userExpense.logout').controller('LogoutCtrl', LogoutCtrl);
}());