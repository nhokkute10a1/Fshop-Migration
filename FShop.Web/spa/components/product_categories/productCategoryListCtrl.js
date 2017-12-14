(function (app) {
    app.controller('productCategoryListCtrl', productCategoryListCtrl);

    productCategoryListCtrl.$inject = ['$scope', 'apiService'];

    function productCategoryListCtrl($scope, apiService) {
        // $scope.productCategory = [];
        angular.element(function () {
            getListProductCategory();
        });
        function getListProductCategory() {
            apiService.get('/api/ProductCategory/GetAllParents', null, function (result) {
                if (result.data.Status) {
                    $scope.productCategory = result.data.Data;
                }
                else {
                    $scope.Message = result.data.Message;
                }
            }, function () {
                console.log('Không tìm thấy dữ liệu !!!');
            });
        }
        
    }
})(angular.module('fshop.productCategory'));