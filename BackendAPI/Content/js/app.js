var app = angular.module("application", []);

app.controller("testController", function($scope, $http) {


        $scope.myfunction = function (data) {
         return $http.get('/Abacate', {responseType: 'text'})

    };
})

    

