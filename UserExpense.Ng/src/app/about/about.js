angular.module('userExpense.about', [
  'ui.router'
])
.config(['$stateProvider', function ($stateProvider) {
    $stateProvider.state('about', {
        url: '/about',
        controller: 'AboutCtrl',
        templateUrl: 'app/about/about.html',
        data: {
            pageTitle: 'About'
        }
    });
}]);

(function () {
    'use strict';
    var AboutCtrl = function ($scope, $http) {
        $scope.onNavLoad = function () {
            $('#navbar-main-strip ul li:first').addClass('active');
        };
    };

    AboutCtrl.$inject = ['$scope', '$http'];

    angular.module('userExpense.about').controller('AboutCtrl', AboutCtrl);
}());