import axios from 'axios';
import { API_ROUTES } from '../../constants';

class _RequestService {
  createRequest = (request) => axios.post(API_ROUTES.CREATE_REQUEST, request);
}

export const RequestService = new _RequestService();

export default RequestService;
