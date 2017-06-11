aic.services.referrals = aic.services.referrals || {};

aic.services.referrals.getByIds = function (referrerId, candidateId, jobId, onSuccess, onError) {

    aic.page.sendAjax("referral/" + referrerId + "/" + candidateId + "/" + jobId, "GET", null, onSuccess, onError);
}

aic.services.referrals.upsert = function (data, onSuccess, onError) {

    aic.page.sendAjax("referral", "PUT", data, onSuccess, onError);
}