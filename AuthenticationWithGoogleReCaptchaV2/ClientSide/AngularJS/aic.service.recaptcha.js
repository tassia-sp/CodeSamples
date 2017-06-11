sabio.services.recaptcha = sabio.services.recaptcha || {};

sabio.services.recaptcha.post = function (data, onSuccess, onError) {
    console.log("In sabio.services.recaptcha.post");

    sabio.page.sendAjax("captcha", "POST", data, onSuccess, onError);
}