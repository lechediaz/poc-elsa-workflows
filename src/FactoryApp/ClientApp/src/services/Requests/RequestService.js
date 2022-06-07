import axios from 'axios';
import { API_ROUTES } from '../../constants';

class _RequestService {
  createRequest = (requestCreation) =>
    axios.post(API_ROUTES.CREATE_REQUEST, requestCreation);

  editRequest = (requestEdition) =>
    axios.put(API_ROUTES.EDIT_REQUEST, requestEdition);

  deleteRequest = (requestId) => {
    const url = API_ROUTES.DELETE_REQUEST.replace('{requestId}', requestId);
    return axios.delete(url);
  };

  getREquestToComplete = () => axios.get(API_ROUTES.REQUESTS_TO_COMPLETE);

  getUserRequests = (userId) => {
    const url = API_ROUTES.USER_REQUESTS.replace('{userId}', userId);
    return axios.get(url);
  };

  getRequestById = (requestId) => {
    const url = API_ROUTES.REQUEST_BY_ID.replace('{requestId}', requestId);
    return axios.get(url);
  };
}

export const RequestService = new _RequestService();

export default RequestService;
