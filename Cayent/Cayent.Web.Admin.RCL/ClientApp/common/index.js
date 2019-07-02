
import angular from 'angular';

const app = angular.module('common', ['toastr']);

function controller($rootScope, $scope) {
    const vm = this;

    //vm.$onInit = function () {
    //    debugger;
    //};
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
}

controller.$inject = ['$rootScope', '$scope'];

app.component('commonComponent', {
    controller: controller
});


function errHandlerSvc(toastr) {

    return {
        handle: function (arg) {

            let msg = JSON.stringify(arg);

            if (arg.status === 404) {
                msg = 'Record not found';
            }

            toastr.error(msg, 'Unhandled Error', { timeOut: 0 });
        }
    };

}

errHandlerSvc.$inject = ['toastr'];

app.service('errHandlerSvc', errHandlerSvc);

app.constant('enumAccountType', {
    0: 'Unknown',
    1: 'Patient',
    2: 'Caregiver',
    3: 'Administrator'
});

app.constant('enumAccountVerifiedStatus', {
    0: 'Unknown',
    1: 'Verified',
    2: 'Unverified'
});

app.constant('enumAccountAvailability', {
    0: 'Unknown',
    1: 'Available',
    2: 'Unavailable'
});

app.constant('enumJobStatus', {
    0: 'Unknown',
    1: 'Draft',
    2: 'Paid',
    3: 'Assigned',
    4: 'Active',
    5: 'Complete',
    6: 'Cancelled'
});

app.constant('enumJobApplicantStatus', {
    0: 'Unknown',
    1: 'Accepted',
    2: 'Rejected'
});

app.constant('enumNotificationType', {
    0: 'Unknown',
    1: 'Primary',
    2: 'Secondary',
    3: 'Success',
    4: 'Info',
    5: 'Warning',
    6: 'Error'
});

app.component('commonPager', {
    controller: function () {
        
    },
    bindings: {
        pageIndex: '<',
        totalPages: '<',
        moveFirst: '&',
        movePrev: '&',
        moveNext: '&',
        moveLast: '&'
    },
    template: `
            <ul class="pagination pl-5 mb-0">
                <li class="page-item">
                    <button ng-click="$ctrl.moveFirst()" class="page-link" aria-label="First">
                        <span aria-hidden="true">&laquo;</span>
                    </button>
                </li>
                <li class="page-item">
                    <button ng-click="$ctrl.movePrev()" class="page-link">&lsaquo;</button>
                </li>
                <li class="page-item">
                    <button class="page-link">{{$ctrl.pageIndex}}/{{$ctrl.totalPages}}</button>
                </li>
                <li class="page-item">
                    <button ng-click="$ctrl.moveNext()" class="page-link">&rsaquo;</button>
                </li>
                <li class="page-item">
                    <button ng-click="$ctrl.moveLast()" class="page-link" href="#" aria-label="Last">
                        <span aria-hidden="true">&raquo;</span>
                    </button>
                </li>
            </ul>
`
});

export default app;
