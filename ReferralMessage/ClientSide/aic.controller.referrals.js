//grab model values from the view controller
aic.page.candidateId = $("#candidateId").val();
aic.page.jobId = $("#jobId").val();

(function () {
    'use strict';

    angular.module('aicApp')
            .controller('referralsController', ReferralsController);

    ReferralsController.$inject = ['$scope', '$baseController', '$referralsService', 'toastr', '$sce', '$jobService'];

    function ReferralsController($scope, $baseController, $referralsService, $toastr, $sce, $jobService) {
        var vm = this;
        vm.$scope = $scope;
        vm.$jobService = $jobService;
        vm.item = {};

        vm.$referralsService = $referralsService;

        //bindable members (hoisting)
        vm.onGetReferralSuccess = _onGetReferralSuccess;
        vm.onSubmitClick = _onSubmitClick;
        vm.onSubmitReferralSuccess = _onSubmitReferralSuccess;
        vm.resize = _resize;
        vm.handleTextAreaHeight = _handleTextAreaHeight;
        vm.getJobsSuccess = _getJobsSuccess;

        //this line ot simulate inheritance
        $baseController.merge(vm, $baseController);

        vm.notify = vm.$referralsService.getNotifier($scope);

        //the fold
        render();

        function render() {
            //check to make sure that the person is now referring themselves
            if (aic.page.personId === aic.page.candidateId) {
                //alert user and redirect
                $toastr.error("Sorry, you cannot write a referral for yourself, Redirecting...", { timeout: 5000 });
                window.location.href = "/Messagingng/Messages";
            } else {
                //defer data operations into an external service
                vm.$referralsService.getByIds(aic.page.personId, sabio.page.candidateId, sabio.page.jobId, vm.onGetReferralSuccess, vm.onError);
                vm.$jobService.getById(aic.page.jobId, vm.getJobsSuccess);

            };
        }

        function _onGetReferralSuccess(data) {
            console.log(data);
            //this receives the data and calls the special notify method that will trigger ng to refresh the UI
            vm.notify(function () {
                //grab the data and place in an object
                if (data.items != null) {
                    vm.item = data.items[0];
                } else {
                    //reroute user because the referral request no longer exists in the database
                    $toastr.error("The referral request has been removed from the system. Redirecting", { timeout: 5000 });
                    window.location.href = "/Messagingng/Messages";
                }
            });
        }

        function _onSubmitClick() {
            //must rewrite the the data object since not all parameters were passed in the form  (some were still in the uri/Model.Item)
            vm.item = {
                referrerId: aic.page.personId,
                candidateId: aic.page.candidateId,
                jobId: aic.page.jobId,
                message: vm.item.message,
                accepted: 1,
                candidateGuid: vm.item.candidateGuid
            };
            //check to make sure that the person is now referring themselves
            if (sabio.page.personId == aic.page.candidateId) {
                //alert user and redirect
                $toastr.error("Sorry, you cannot write a referral for yourself, Redirecting...", { timeout: 3000 });
                window.location.href = "/Messagingng/Messages";
            } else {
                //send the user input to the server to be updated with the referral message)
                vm.$referralsService.upsert(vm.item, vm.onSubmitReferralSuccess);
            };
        }

        function _onSubmitReferralSuccess(data) {
            //notify the user the message submitted successfully
            $toastr.success("Your referral was submitted. Redirecting...", { timeout: 3000 });
            //redirect user to their dashboard
            setTimeout(function () {
                window.location.href = "/ProfilePageNg/Landing";
            }, 2000);
        }

        function _resize() {
            $("textarea").height($("textarea")[0].scrollHeight);
        }; //_resize

        function _handleTextAreaHeight(e) {
            var element = e.target;

            element.style.overflow = 'hidden';
            element.style.height = 0;
            element.style.height = element.scrollHeight + 'px';
        }; //_handleTextAreaHeight

        function _getJobsSuccess(data, textStatus, jqXHR) {
            console.log(vm.item)
            console.log("Job Success Fired!!!")
            vm.notify(function () {
                vm.job = data.item;
            });
        }

        window.setTimeout(vm.resize, 1);

    }
})
();