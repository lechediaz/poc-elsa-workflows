import axios from 'axios';
import { SERVER } from '../../constants';

class _UsersListService {
  async getUsersList() {
    const response = await axios.get(`${SERVER.API}/api/user/list`);
    const usersList = response.data;
    return usersList;
  }
}

export const UsersListService = new _UsersListService();

export default UsersListService;
