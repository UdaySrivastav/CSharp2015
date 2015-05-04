 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('app_Transaction');

    angular.module('app_Transaction',['ngRoute'])
    .controller('Transaction_list', ['$scope', '$http', function($scope, $http){

        $http.get('/Api/Transaction/')
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('Transaction_details', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams){

        $http.get('/Api/Transaction/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('Transaction_create', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $scope.data = {};
                $http.get('/Api/Account/')
        .then(function(response){$scope.AccountId_options = response.data;});
        
        $scope.save = function(){
            $http.post('/Api/Transaction/', $scope.data)
            .then(function(response){ $location.path("Transaction"); });
        }

    }])
    .controller('Transaction_edit', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/Transaction/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

                $http.get('/Api/Account/')
        .then(function(response){$scope.AccountId_options = response.data;});
        
        $scope.save = function(){
            $http.put('/Api/Transaction/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("Transaction"); });
        }

    }])
    .controller('Transaction_delete', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/Transaction/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});
        $scope.save = function(){
            $http.delete('/Api/Transaction/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("Transaction"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
            .when('/Transaction', {
                title: 'Transaction - List',
                templateUrl: '/Static/Transaction_List',
                controller: 'Transaction_list'
            })
            .when('/Transaction/Create', {
                title: 'Transaction - Create',
                templateUrl: '/Static/Transaction_Edit',
                controller: 'Transaction_create'
            })
            .when('/Transaction/Edit/:id', {
                title: 'Transaction - Edit',
                templateUrl: '/Static/Transaction_Edit',
                controller: 'Transaction_edit'
            })
            .when('/Transaction/Delete/:id', {
                title: 'Transaction - Delete',
                templateUrl: '/Static/Transaction_Delete',
                controller: 'Transaction_delete'
            })
            .when('/Transaction/:id', {
                title: 'Transaction - Details',
                templateUrl: '/Static/Transaction_Details',
                controller: 'Transaction_details'
            })
    }])
;

})();
