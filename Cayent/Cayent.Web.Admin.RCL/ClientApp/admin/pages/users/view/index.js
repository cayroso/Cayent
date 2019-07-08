
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
        $http.get(`admin-module/api/users/${vm.userId}`)
            .then(function (resp) {
                vm.item = resp.data;
                
            }, errHandlerSvc.handle);
    };
}

controller.$inject = ['$rootScope', '$scope', '$http', 'errHandlerSvc', 'toastr'];
app.component('usersViewComponent', {
    controller: controller,
    //templateUrl: '/admin/users/index?handler=indexpartial',
    templateUrl: ['$attrs', function ($attrs) {
        let userId = $attrs.userId.substr(1).slice(0, -1);
        let url = `/admin/users/view/${userId}/indexpartial`;

        return url;
    }],
    bindings: {
        userId: '<'
    }
});