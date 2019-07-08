
import app from '../../../app';

function controller($rootScope, $scope, $http, errHandlerSvc, toastr) {
    const vm = this;


    vm.$onInit = function () {
        vm.get();
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

    vm.get = function () {
        $http.get(`admin-module/api/roles/${vm.roleId}`)
            .then(function (resp) {
                vm.item = resp.data;
                
            }, errHandlerSvc.handle);
    };
}

controller.$inject = ['$rootScope', '$scope', '$http', 'errHandlerSvc', 'toastr'];
app.component('rolesViewComponent', {
    controller: controller,
    //templateUrl: '/admin/users/index?handler=indexpartial',
    templateUrl: ['$attrs', function ($attrs) {
        let roleId = $attrs.roleId.substr(1).slice(0, -1);
        let url = `/admin/roles/view/${roleId}/indexpartial`;

        return url;
    }],
    bindings: {
        roleId: '<'
    }
});