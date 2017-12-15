(function (app) {
    app.controller('productCategoryListCtrl', productCategoryListCtrl);

    productCategoryListCtrl.$inject = ['$scope', 'apiService'];

    function productCategoryListCtrl($scope, apiService) {
        $scope.productCategory = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        //$scope.search = search;
        $scope.getListProductCategory = getListProductCategory;
        
        angular.element(function () {
            getListProductCategory();
        });
        //function search() {
        //    getListProductCategory();
        //}
        function getListProductCategory(page) {
            page = page || 0;
            /*===Phân trang cách 2==*/
            //let ArrPagination = [];
            var config = {
                params: {
                    page: page,
                    pageSize: 1
                }
            }
            apiService.get('/api/ProductCategory/GetAllPaging', config , function (result) {
                if (result.data.Status) {
                    $scope.productCategory = result.data.Data.Items;
                    $scope.page = result.data.Data.Page;
                    $scope.pagesCount = result.data.Data.TotalPages;
                    $scope.totalCount = result.data.Data.TotalCount;

                    /*===Phân trang cách 2==*/
                    //for (var i = 1; i <= result.data.Data.TotalPages; i++) {
                    //    ArrPagination.push(i);
                    //}
                    //$scope.Pagination = ArrPagination;
                }
                else {
                    $scope.Message = result.data.Message;
                }
            }, function () {
                console.log('Không tìm thấy dữ liệu !!!');
            });
        }
        /*===Phân trang cách 2==*/
        //$scope.PaggingProduct = (page) => {
        //    getListProductCategory(page);
        //};
        //$scope.ActiveClass = (item) => {
        //    $scope.selected = item;
        //};
        //$scope.IsActiveClass = (item) => {
        //    return $scope.selected === item;
        //};
        //$scope.getListProductCategory();
    }
})(angular.module('fshop.productCategory'));