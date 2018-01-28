(function (app) {
    app.directive('paggingBoostrap', () => {
        return {
            restrict: "E",
            scope: {
                page: "@",
                totalPage: "=",
                callBackMethod: "&goToPage"
            },
            controller: ($scope) => {
                angular.element(() => {
                    $scope.ActiveClass(1);
                });
                $scope.Pagging = (p) => {
                    if (p !== null) {
                        $scope.callBackMethod({
                            page: p
                        });
                    }
                    else {
                        alert("Vui lòng truyền vào số trang");
                    }
                };
                $scope.ActiveClass = (item) => {
                    $scope.selected = item;
                };
                $scope.IsActiveClass = (item) => {
                    return $scope.selected === item;
                };
            },
            templateUrl: '/spa/shared/directives/pagination.html',
        };
    })
})(angular.module('fshop.common'));