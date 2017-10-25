(function () {
    'use strict';
    var NavbarCtrl = function ($scope, $http, $state) {
        $http.get('http://localhost:53415/api/accounts/currentuser').then(function(data) {
            $scope.fullName = data.data.fullName;
        });

        $scope.logout = function () {
            $state.go('logout');
        };
    };

    NavbarCtrl.$inject = ['$scope', '$http', '$state'];

    angular.module('userExpense').controller('NavbarCtrl', NavbarCtrl);
}());