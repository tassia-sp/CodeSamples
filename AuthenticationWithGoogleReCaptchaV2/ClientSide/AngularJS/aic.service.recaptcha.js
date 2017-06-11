aic.services.recaptcha = aic.services.recaptcha || {};

aic.services.recaptcha.post = function (data, onSuccess, onError) {
    aic.page.sendAjax("captcha", "POST", data, onSuccess, onError);
}