(function () {
            "use strict";

            aic.ng.app.module.controller('userController', UserController);
		//vcRecaptchaService is a third party plugin for using Recaptcha with AngularJs
            UserController.$inject = ['$scope', '$baseController', '$recaptchaService', '$userService', 'toastr', '$timeout', 'vcRecaptchaService'];

            function UserController(
                $scope
                , $baseController
                , $recaptchaService
                , $userService
                , $toastr
                , $timeout
                , vcRecaptchaService) {

                //controllerAs with vm syntax: https://github.com/johnpapa/angular-styleguide#style-y032
                var vm = this;//this points to a new {}

                vm.$scope = $scope;
                vm.$userService = $userService;
                vm.$recaptchaService = $recaptchaService;
                vm.$RecaptchaService = vcRecaptchaService;
                vm.response = null;
                vm.widgetId = null;
                vm.disabled = false;
                vm.tokenId = $("#registrationCode").val();

                //bindable members (functions) always go up top while the "meat"
                //of the functions go below: https://github.com/johnpapa/angular-styleguide#style-y033
                vm.item = null;

                //vm.getPressItems = _getPressItems;
                vm.register = _register;
                vm.onRegisterSuccess = _onRegisterSuccess;
                vm.onRegisterError = _onRegisterError;
                vm.getResponse = _getResponse;
                vm.getWidget = _getWidget;
                vm.captcha = _captcha;
                vm.captchaSuccess = _captchaSuccess;
                vm.captchaError = _captchaError;

                //--this line to simulate inheritance
                $baseController.merge(vm, $baseController);

                //this is a wrapper for our small dependency on $scope
                vm.notify = vm.$userService.getNotifier($scope);
                vm.notify2 = vm.$recaptchaService.getNotifier($scope);

                //this is like the sabio.startUp function
                render();

                function render() {
                }

                //get response value from google captcha
                function _getResponse(response) {
                    vm.response = response;
                }

                //get widgetId from google captcha
                function _getWidget(widgetId) {
                    vm.widgetId = widgetId;
                }

                function _captcha() {
                    console.log("In the captcha function!");
                    var captcha_data = {
                        'response': vm.response
                    };
                    //send data off to api controller
                    vm.$recaptchaService.post(captcha_data, vm.captchaSuccess, vm.captchaError);
                }

                function _captchaSuccess(response) {
                    console.log(response);
                    //POST registration
                    _register();
                }

                function _captchaError(jqXHR, data) {
                    $toastr.error("The recaptcha verification failed.", "Sorry", { timeout: 5000 });
                }

                function _register() {
                    vm.disabled = true;
                    if (vm.tokenId) {
                        vm.$userService.registerReferral(vm.item, vm.onRegisterSuccess, vm.onRegisterError);
                    }
                    else {
                        vm.$userService.register(vm.item, vm.onRegisterSuccess, vm.onRegisterError);
                    }
                }

                function _onRegisterSuccess(data) {
                    if (vm.tokenId) {
                        $toastr.success("Your email has been registered. Please Login.", { timeOut: 5000 });
                        $timeout(function () {
                            self.location = '/Users/Login';
                        }, 2000);
                    }
                    else {
                        $toastr.success("Your email has been registered. Please check your email for email confirmation.", { timeOut: 5000 });
                        $timeout(function () {
                            self.location = '/Home/Index';
                        }, 2000);
                    }
                };

                function _onRegisterError() {
                    $toastr.error("That email is already registered.", { timeOut: 5000 })
                    $timeout(function () {
                        vm.disabled = false;
                    }, 2000);


                }

                //if captcha expires, console logs error and expire statement and refreshes widget
                function _expire() {
                    $toastr.error("Please solve the recaptcha again.", "Validation Expired", { timeout: 5000 });
                    vm.$RecaptchaService.reload(vm.widgetId);
                }
            }
        })();