import axios from 'axios';
import { API_ROUTES } from '../../constants';

class _UsersListService {
  async getUsersList() {
    const response = await axios.get(API_ROUTES.USERS_LIST);
    const usersList = response.data;
    return usersList;
  }
}

export const UsersListService = new _UsersListService();

export default UsersListService;
