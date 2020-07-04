import axios from 'axios';
import {COMMON_CONST, COMMONURLS_CONST} from '../constants/index';

const axiosInstance = axios.create();

const requestHandler = (request) => {
  return request
};

axiosInstance.interceptors.request.use(
  request => requestHandler(request)
);

const getredirectUrl = () => {
  return COMMONURLS_CONST.authurl + 
  'client_id=' + COMMONURLS_CONST.clientId + '&' +
  'redirect_uri=' + COMMONURLS_CONST.redirectUrl + '&' +
  'response_type=' + COMMONURLS_CONST.responseType + '&' +
  'scope=' + COMMONURLS_CONST.scope + '&' +
  'response_mode=' + COMMONURLS_CONST.responseMode
};

const errorHandler = (error) => {
  console.log({'errorHandler error':error});
  //NOTE: To be developed..
  //Redirect only when there is a auth issue. If not redirect to error page (status error page i.e error.html)
  //Possible of infinite redirecting if the issue is in the server.
  //redirect only when login needed
  if(error.response === undefined){
    //window.location.href = getredirectUrl();
  }
  return Promise.reject(error);
};

const successHandler = (response) => {
  return response
};

axiosInstance.interceptors.response.use(
  response => successHandler(response),
  error => errorHandler(error)
);

export {
  axiosInstance
};