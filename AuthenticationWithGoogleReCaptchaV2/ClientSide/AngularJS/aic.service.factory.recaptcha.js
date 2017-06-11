(function () {
    'use strict';
    //implement service as a factory
    angular.module('aicApp').factory('$recaptchaService', recaptchaServiceFactory);
    //manually identify dependencies for injection
    //$aic is a reference for aic.page object.
    recaptchaServiceFactory.$inject = ['$baseService', '$aic'];

    function recaptchaServiceFactory($baseService, $aic) {
        //aic.page has been injected as $aic so we can reference anything
        var aAicServiceObject = aic.services.recaptcha;

        //merge the jQuery object with the angular base service to simulate inheritance
        var newService = $baseService.merge(true, {}, aAicServiceObject, $baseService);

        return newService;
    }

})();