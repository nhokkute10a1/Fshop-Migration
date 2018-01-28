(function (app) {
    app.controller('productCategoryListCtrl', productCategoryListCtrl);

    productCategoryListCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productCategoryListCtrl($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategory = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.search = search;
        $scope.getListProductCategory = getListProductCategory;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;

        this.$onInit = () => {
            getListProductCategory();
            $scope.ActiveClass(0);
        };

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                   checkedProductCategories: JSON.stringify(listId)
                }
            }
            apiService.del('/api/ProductCategory/DeleteMulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function (error) {
                notificationService.displayError('Xóa thất bại');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategory, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategory, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("productCategory", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có thật sự muốn xóa?').then(function () {
                var config = {
                    id: id,
                }
                apiService.post('/api/ProductCategory/Delete', config, function (result) {
                    if (result.data.Status) {
                        notificationService.displaySuccess('Xóa thành công');
                        search();
                    }
                }, function (error) {
                    notificationService.displayError('Xóa thất bại');
                });
            });
        }
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
                    page: page,
                    pageSize: 3
                }
            }
            apiService.get('/api/ProductCategory/GetAllPaging', config, function (result) {
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