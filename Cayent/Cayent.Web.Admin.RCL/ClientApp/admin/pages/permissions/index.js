
import app from '../../app';

function controller($rootScope, $scope, $http, errHandlerSvc, toastr) {
    const vm = this;

    vm.data = {
        criteria: '',
        page: 1,
        pageSize: 10
    };

    vm.$onInit = function () {
        vm.search();
    };
    //vm.postLink = function () {
    //    debugger;
    //};
    //vm.$onChanges = function (changes) {
    //    debugger;
    //};
    //vm.$doCheck = function () {
    //    debugger;
    //};
    //vm.onDestroy = function () {
    //    debugger;
    //};

    vm.search = function () {
        $http.get(`admin-module/api/permissions/?criteria=${vm.data.criteria}&page=${vm.data.page}&pageSize=${vm.data.pageSize}`)
            .then((resp) => {
                angular.extend(vm.data, resp.data);
                
            }, errHandlerSvc.handle);
        
    };

    vm.first = function () {
        
        if (vm.data.page > 1) {
            vm.data.page = 1;
            vm.search();
        }
    };

    vm.previous = function () {
        if (vm.data.page > 1) {
            vm.data.page--;
            vm.search();
        }
    };

    vm.next = function () {
        if (vm.data.page < vm.data.pageCount) {
            vm.data.page++;
            vm.search();
        }
    };

    vm.last = function () {
        if (vm.data.page < vm.data.pageCount) {
            vm.data.page++;
            vm.search();
        }
    };
}

controller.$inject = ['$rootScope', '$scope', '$http', 'errHandlerSvc', 'toastr'];

app.component('permissionsComponent', {
    controller: controller,
    templateUrl: '/admin/permissions/index?handler=indexpartial'
});