angular.module('userExpense.login', [
  'ui.router',
  'angular-storage'
])
.config(['$stateProvider', function ($stateProvider) {
    $stateProvider.state('login', {
        url: '/login',
        controller: 'LoginCtrl',
        templateUrl: 'app/login/login.html',
        data: {
            pageTitle: 'Login'
        }
    });
}]);

(function () {
    'use strict';
    var LoginCtrl = function ($scope, $http, store, $state) {
        $scope.user = {};

        $scope.login = function () {
            $http({
                url: 'http://localhost:53415/oauth/token',
                method: 'POST',
                data: 'username=' + $scope.user.username + '&password=' + $scope.user.password + '&grant_type=password'
            }).then(function (response) {
                store.set('jwt', response.data.access_token);
                $state.go('home');
            }, function (error) {
                alert(error.data.error_description);
            });
        }
    };

    //pattern for DI issues
    LoginCtrl.$inject = ['$scope', '$http', 'store', '$state'];

    angular.module('userExpense.login').controller('LoginCtrl', LoginCtrl);
}());