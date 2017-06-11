(function () {
    'use strict';
    //implement service as a factory
    angular.module('sabioApp').factory('$recaptchaService', recaptchaServiceFactory);
    //manually identify dependencies for injection
    //$sabio is a reference for sabio.page object. sabio.page is created in sabio.js
    recaptchaServiceFactory.$inject = ['$baseService', '$sabio'];

    function recaptchaServiceFactory($baseService, $sabio) {
        //sabio.page has been injected as $sabio so we can reference anything
        var aSabioServiceObject = sabio.services.recaptcha;

        //merge the jQuery object with the angular base service to simulate inheritance
        var newService = $baseService.merge(true, {}, aSabioServiceObject, $baseService);

        return newService;
    }

})();