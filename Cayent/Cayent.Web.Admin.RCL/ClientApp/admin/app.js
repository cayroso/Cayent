
import angular from 'angular';
import * as signalR from '@aspnet/signalr';

import '../common/index';

const app = angular.module('app', ['common', 'ngAnimate', 'toastr', 'ui.bootstrap']);

function controller($rootScope, $scope, $http, $uibModal, errHandlerSvc, toastr) {
    
    const vm = this;
    vm.$onInit = function () {
        //vm.notificationHub = new signalR.HubConnectionBuilder()
        //    .withUrl('/notificationHub')
        //    .build();

        //var start = vm.notificationHub.start();

        //start.then(function () {
        //    //toastr.success('Connected to Notification Hub');

        //    vm.notificationHub.on('receive', function (notification) {
        //        toastr.info(notification.content, notification.subject);
        //    });
        //});

        //start.catch(function (err) {
        //    toastr.error(JSON.stringify(err));
        //});


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




    $rootScope.send = function (message) {
        //debugger;
        vm.notificationHub.send('sendToAllAsync', { subject: message });
    };

}

controller.$inject = ['$rootScope', '$scope', '$http', '$uibModal', 'errHandlerSvc', 'toastr'];

app.component('appComponent', {
    controller: controller
});


app.controller('navbarComponent',
    ['$scope', '$http', '$uibModal', 'errHandlerSvc', 'toastr',
        function ($scope, $http, $uibModal, errHandlerSvc, toastr) {
            var vm = this;

            vm.data = null;
            vm.$onInit = function () {
                $http.get(`admin-module/api/admin/navbar`)
                    .then((resp) => {
                        vm.data = resp.data;
                    }, errHandlerSvc.handle);
            };

            vm.viewNotification = function (notificationReceiverId) {

                $http.get(`api/notification/${notificationReceiverId}`)
                    .then((resp) => {

                        let templateUrl = `/administrator/notifications/index/?handler=viewpartial`;

                        let modal = $uibModal.open({
                            animation: true,
                            controllerAs: '$ctrl',
                            controller: ['$http', '$uibModalInstance', 'item', function ($http, $uibModalInstance, item) {
                                var _vm = this;

                                _vm.item = item;

                                _vm.ok = function () {
                                    $uibModalInstance.close(false);
                                };

                                _vm.markAsRead = function () {
                                    $http.post(`api/notification/markAsRead/${notificationReceiverId}`)
                                        .then(() => {
                                            $uibModalInstance.close(true);
                                        }, errHandlerSvc.handle);
                                };

                                _vm.removeNotification = function () {
                                    $http.post(`api/notification/remove/?id=${notificationReceiverId}`)
                                        .then(() => {
                                            $uibModalInstance.close(true);
                                        }, errHandlerSvc.handle);
                                };
                            }],
                            templateUrl: templateUrl,
                            size: 'lg',
                            backdrop: 'static',
                            resolve: {
                                item: function () {
                                    
                                    return resp.data;
                                }
                            }
                        });

                        modal.result.then((resp) => {
                            if (resp) {
                                vm.$onInit();
                            }
                        }, angular.noop);

                    }, errHandlerSvc.handle);
            };

            vm.viewMessage = function (chatId, chatReceiverId) {
                $http.get(`api/chat/${chatId}`)
                    .then((resp) => {

                        let templateUrl = `/administrator/messages/index/?handler=viewpartial`;

                        let modal = $uibModal.open({
                            animation: true,
                            controllerAs: '$ctrl',
                            controller: ['$http', '$uibModalInstance', 'item', function ($http, $uibModalInstance, item) {
                                var _vm = this;

                                _vm.item = item;

                                _vm.send = function () {

                                    var payload = {
                                        chatId: _vm.item.chatId,
                                        content: _vm.content
                                    };

                                    $http.post(`api/chat/add`, payload)
                                        .then((resp) => {
                                            toastr.success('chat sent', 'chat', { timeOut: 0, onHidden: function () { _vm.$onInit(); } });
                                        }, errHandlerSvc.handle);
                                };

                                _vm.ok = function () {
                                    $uibModalInstance.close(false);
                                };

                                _vm.markAsRead = function () {
                                    $http.post(`api/message/markAsRead/${chatReceiverId}`)
                                        .then(() => {
                                            $uibModalInstance.close(true);
                                        }, errHandlerSvc.handle);
                                };

                                _vm.removeNotification = function () {
                                    $http.post(`api/message/remove/?id=${chatReceiverId}`)
                                        .then(() => {
                                            $uibModalInstance.close(true);
                                        }, errHandlerSvc.handle);
                                };
                            }],
                            templateUrl: templateUrl,
                            size: 'lg',
                            backdrop: 'static',
                            resolve: {
                                item: function () { 
                                    return resp.data;
                                }
                            }
                        });

                        modal.result.then((resp) => {
                            if (resp) {
                                vm.$onInit();
                            }
                        }, angular.noop);

                    }, errHandlerSvc.handle);
            };
        }]);

app.controller('sidebarComponent',
    ['$scope', '$http', '$uibModal', 'errHandlerSvc', 'toastr',
        function ($scope, $http, $uibModal, errHandlerSvc, toastr) {
            var vm = this;

            vm.$onInit = function () {
                $http.get(`admin-module/api/admin/sidebar`)
                    .then((resp) => {
                        vm.data = resp.data;
                    }, errHandlerSvc.handle);
            };

        }]);

export default app;
