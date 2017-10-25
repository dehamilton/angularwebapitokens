angular.module('userExpense', [
  'userExpense.home',
  'userExpense.about',
  'userExpense.login',
  'userExpense.logout',
  'userExpense.services',
  'angular-jwt',
  'angular-storage'
])
.config(['$urlRouterProvider', 'jwtInterceptorProvider', '$httpProvider', function myAppConfig($urlRouterProvider, jwtInterceptorProvider, $httpProvider) {
    $urlRouterProvider.otherwise('/');

    jwtInterceptorProvider.tokenGetter = function (store) {
        return store.get('jwt');
    }

    $httpProvider.interceptors.push('jwtInterceptor');
}])
.run(['$rootScope', '$state', 'store', 'jwtHelper', function ($rootScope, $state, store, jwtHelper) {
    $rootScope.$on('$stateChangeStart', function (e, to) {
        if (to.data && to.data.requiresLogin) {
            if (!store.get('jwt') || jwtHelper.isTokenExpired(store.get('jwt'))) {
                e.preventDefault();
                $state.go('login');
            }
        }
    });
}])
.controller('AppCtrl', ['$scope', function AppCtrl($scope) {
    $scope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        if (angular.isDefined(toState.data.pageTitle)) {
            $scope.pageTitle = toState.data.pageTitle + ' | User Expenses';
        }
        if (angular.isDefined(toState.data.pageHeader)) {
            $scope.pageHeader = toState.data.pageHeader;
        }
    });
}]);