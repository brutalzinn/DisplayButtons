var app = angular.module("application", []);

app.controller("testController", function($scope, $http) {
$scope.$on('$viewContentLoaded', function() {
novoObservable(): Observable<string> {
    return new Observable<string>(observador => {
      setTimeout(() => {
        observador.next("Primeiro timeout");
      }, 2000);

      setTimeout(() => {
        observador.next("Segundo timeout");
       
      }, 3000);

      setTimeout(() => {
        observador.next("Terceiro timeout");
      }, 5000);

      setTimeout(() => {
        observador.next("Quarto timeout");
      }, 4000);
    });
  }
     
         });

    
})

    

