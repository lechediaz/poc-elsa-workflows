import axios from 'axios';
import { API_ROUTES } from '../../constants';

class _RequestService {
  createRequest = (request) => axios.post(API_ROUTES.CREATE_REQUEST, request);
  getUserRequests = (userId) => {
    const url = API_ROUTES.USER_REQUESTS.replace('{userId}', userId);
    return axios.get(url);
  };
}

export const RequestService = new _RequestService();

export default RequestService;
