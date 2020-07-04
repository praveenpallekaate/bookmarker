const STATICURLS_CONST = {
  local: 'https://localhost:44324/api/',
  environment: 'http://13.69.27.216:99' + '/api/'
};

const COMMONURLS_CONST = {
  apibaseurl: STATICURLS_CONST.environment,
  authurl: 'https://login.microsoftonline.com/87d70b0f-5efc-4991-a065-e205bc3db308/oauth2/v2.0/authorize?'
};

const COMMON_CONST = {
  clientId: 'b2a53e19-f878-46f1-88fd-73d68c822ce7',
  redirectUrl: window.location.origin + '/services/home',
  responseType: 'code id_token',
  scope: 'openid profile offline_access User.Read.All',
  responseMode: 'form_post',
  catalogueType: 'bookmarker'
};

export {
  STATICURLS_CONST,
  COMMON_CONST,
  COMMONURLS_CONST
};