import axios from 'axios';
import { API_ROUTES } from '../../constants';

class _RawMaterialsListService {
  async getRawMaterialsList() {
    const response = await axios.get(API_ROUTES.RAW_MATERIALS_LIST);
    const rawMaterialsList = response.data;
    return rawMaterialsList;
  }
}

export const RawMaterialsListService = new _RawMaterialsListService();

export default RawMaterialsListService;
