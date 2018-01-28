 //<reference path="../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('fshop',
        [
            'fshop.productCategory',
            'fshop.common'
        ])
        .config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            cache: false,
            templateUrl: "/spa/components/home/homeView.html",
            controller: "homeCtrl"
        });
        $urlRouterProvider.otherwise('/admin');
    }
})(); 