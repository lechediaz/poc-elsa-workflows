import axios from 'axios';
import { ELSA_ROUTES } from '../constants';

class _ElsaService {
  publishRequest = (info) => axios.post(ELSA_ROUTES.PUBLISH_REQUEST, info);

  negotiateRequest = (info) => axios.post(ELSA_ROUTES.NEGOTIATE_REQUEST, info);
}

export const ElsaService = new _ElsaService();

export default ElsaService;
