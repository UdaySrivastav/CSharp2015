 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('app_Account');

    angular.module('app_Account',['ngRoute'])
    .controller('Account_list', ['$scope', '$http', function($scope, $http){

        $http.get('/Api/Account/')
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('Account_details', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams){

        $http.get('/Api/Account/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('Account_create', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $scope.data = {};
        
        $scope.save = function(){
            $http.post('/Api/Account/', $scope.data)
            .then(function(response){ $location.path("Account"); });
        }

    }])
    .controller('Account_edit', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/Account/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

        
        $scope.save = function(){
            $http.put('/Api/Account/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("Account"); });
        }

    }])
    .controller('Account_delete', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/Account/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});
        $scope.save = function(){
            $http.delete('/Api/Account/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("Account"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
            .when('/Account', {
                title: 'Account - List',
                templateUrl: '/Static/Account_List',
                controller: 'Account_list'
            })
            .when('/Account/Create', {
                title: 'Account - Create',
                templateUrl: '/Static/Account_Edit',
                controller: 'Account_create'
            })
            .when('/Account/Edit/:id', {
                title: 'Account - Edit',
                templateUrl: '/Static/Account_Edit',
                controller: 'Account_edit'
            })
            .when('/Account/Delete/:id', {
                title: 'Account - Delete',
                templateUrl: '/Static/Account_Delete',
                controller: 'Account_delete'
            })
            .when('/Account/:id', {
                title: 'Account - Details',
                templateUrl: '/Static/Account_Details',
                controller: 'Account_details'
            })
    }])
;

})();
