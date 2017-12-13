//Khai báo module
var myApp = angular.module('myModule', []);
//myApp.controller("tên controller", func controller);
myApp.controller("myController", myController);
myApp.directive("teduShopDirective", teduShopDirective);
myApp.directive("fshopDirective", fshopDirective);


myApp.service('Validator', Validator);

myController.$inject = ['$scope', 'Validator'];
//$inject:  tiêm giá vào giá trị $scope
//Khởi tạo controller
function myController($scope, Validator) {
    //sử dụng button
    $scope.checkNumber = function () {
        $scope.Msg = Validator.checkNumber($scope.num);
    }
    $scope.num = 1;
}
//khai báo service
function Validator($window) {
    return {
        checkNumber: checkNumber
    }
    function checkNumber(input) {
        if (input % 2 == 0) {
            return 'đây là chẵn';
        }
        else
            return 'đây là lẽ';
    }
}
//khai báo directives
function teduShopDirective() {
    return {
        template: "<h1>custom directive teduShopDirective</h1>"
    }
}

function fshopDirective() {
    return {
        template: "<h1>custom directive fshopDirective</h1>"
    }
}