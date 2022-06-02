import { API } from '../server/server';

// Request
export const USER_REQUESTS = `${API}/api/request/all-from-user/{userId}`;
export const CREATE_REQUEST = `${API}/api/request/create`;
export const REQUEST_BY_ID = `${API}/api/request/{requestId}`;

// Raw material
export const RAW_MATERIALS_LIST = `${API}/api/raw-material/list`;

// User
export const USERS_LIST = `${API}/api/user/list`;
