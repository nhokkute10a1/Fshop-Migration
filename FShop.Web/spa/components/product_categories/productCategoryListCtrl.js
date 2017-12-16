(function (app) {
    app.controller('productCategoryListCtrl', productCategoryListCtrl);

    productCategoryListCtrl.$inject = ['$scope', 'apiService','notificationService'];

    function productCategoryListCtrl($scope, apiService, notificationService) {
        $scope.productCategory = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.search = search;
        $scope.getListProductCategory = getListProductCategory;
        
        angular.element(function () {
            getListProductCategory();
            $scope.ActiveClass(0);
        });
        
        function search() {
            getListProductCategory();
        }
        function getListProductCategory(page) {
            page = page || 0;
            /*===Phân trang cách 2==*/
            let ArrPagination = [];
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page ,
                    pageSize: 3
                }
            }
            apiService.get('/api/ProductCategory/GetAllPaging', config , function (result) {
                if (result.data.Status) {
                    if (result.data.Data.TotalCount == 0) {
                        notificationService.displayWarning('Không có giá trị nào');
                    }
                    else {
                        $scope.productCategory = result.data.Data.Items;
                        $scope.page = result.data.Data.Page;
                        $scope.pagesCount = result.data.Data.TotalPages;
                        $scope.totalCount = result.data.Data.TotalCount;

                        /*===Phân trang cách 2==*/
                        for (var i = 0; i <= result.data.Data.TotalPages - 1; i++) {
                            ArrPagination.push(i);
                        }
                        $scope.Pagination = ArrPagination;
                    }
                }
                else {
                    $scope.Message = result.data.Message;
                }
            }, function () {
                console.log('Không tìm thấy dữ liệu !!!');
            });
        }
        /*===Phân trang cách 2==*/
        $scope.PaggingProduct = (page) => {
            getListProductCategory(page);
        };
        $scope.ActiveClass = (item) => {
            $scope.selected = item;
        };
        $scope.IsActiveClass = (item) => {
            return $scope.selected === item;
        };
        //$scope.getListProductCategory();
        //domain.com/abc
    }
})(angular.module('fshop.productCategory'));