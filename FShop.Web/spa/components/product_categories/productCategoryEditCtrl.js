(function (app) {
    app.controller('productCategoryEditCtrl', productCategoryEditCtrl);

    productCategoryEditCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService'];
    function productCategoryEditCtrl($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.productCate = {
            CreateDate: new Date(),
            Status: true
        }
        $scope.UpdateProductCategory = UpdateProductCategory;
        $scope.GetSetTitle = GetSetTitle;
       
        angular.element(function () {
            loadParentCategory();
            loadProductCategoryDetail();
        });

        function GetSetTitle() {
            $scope.productCate.Alias = commonService.getSeoTitle($scope.productCate.Name);
        }
        function loadProductCategoryDetail() {
            apiService.get('/api/ProductCategory/GetById/' + $stateParams.id, null, function (result) {
                $scope.productCate = result.data.Data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function UpdateProductCategory() {
            apiService.post('/api/ProductCategory/Update', $scope.productCate, function () {
                notificationService.displaySuccess('Cập nhập thành công');
                $state.go('productCategory');
            }, function (error) {
                notificationService.displayError('Cập nhập thất bại');
            });
        }
        function loadParentCategory() {
            apiService.get('/api/ProductCategory/GetAllParents', null, function (result) {
                $scope.productCategory = result.data.Data;
            }, function () {
                console.log('Không tìm thấy dữ liệu !!!');
            });
        }

    }
})(angular.module('fshop.productCategory'));