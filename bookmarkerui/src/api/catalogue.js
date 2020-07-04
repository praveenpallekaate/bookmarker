import {COMMONURLS_CONST} from '../constants/index';
import {axiosInstance} from './axioshelp';

const jsonHeader = {'Content-Type': 'application/json'};

const getCatalogues = (type) => {
  const apiUrl = COMMONURLS_CONST.apibaseurl + 'catalogue/' + type;

  return axiosInstance.get(apiUrl)
    .then((response) => {
      return response;
    })
    .catch((error) => {
      console.log(error);
      throw error;
    });
}

const addCatalogue = (catalogue) => {
  const apiUrl = COMMONURLS_CONST.apibaseurl + 'catalogue';

  return axiosInstance.post(apiUrl, catalogue, jsonHeader)
    .then((response) => {
        return response;
    })
    .catch((error) => {
      console.log(error);
      throw error;
    });
}

const editCatalogue = (catalogue) => {
  const apiUrl = COMMONURLS_CONST.apibaseurl + 'catalogue';

  return axiosInstance.put(apiUrl, catalogue, jsonHeader)
    .then((response) => {
        return response;
    })
    .catch((error) => {
      console.log(error);
      throw error;
    });
}

const removeCatalogue = (type, id) => {
  const apiUrl = COMMONURLS_CONST.apibaseurl + 'catalogue/' + type + '/' + id;

  return axiosInstance.get(apiUrl)
    .then((response) => {
      return response;
    })
    .catch((error) => {
      console.log(error);
      throw error;
    });
}

export default {
  getCatalogues,
  addCatalogue,
  editCatalogue,
  removeCatalogue
};