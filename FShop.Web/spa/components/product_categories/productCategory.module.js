//<reference path="../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('fshop.productCategory', ['fshop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('productCategory', {
            url: "/product-category",
            templateUrl: "/spa/components/product_categories/productCategoryListView.html",
            controller: "productCategoryListCtrl"
        });
    }
})();