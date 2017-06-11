(function () {
    'use strict';

    var app = angular.module('aicApp');
    app.factory('$referralsService', referralsServiceFactory); //service is implemeneted as a factory.

    //manually identify dependencies for injection. $aic is a reference aic.page.object. aic.page.is created in aic.js
    referralsServiceFactory.$inject = ['$baseService', '$aic'];

    function referralsServiceFactory($baseService, $aic) {
        //sabio.page has been injected as $sabio so we can reference anything that is attached to sabio.page here.
        var anAicServiceObject = aic.services.referrals;
        //merge the jQuery object with the angular base service to simulate inheritance.
        var newService = $baseService.merge(true, {}, aAicServiceObject, $baseService);

        return newService;
    }
})();
