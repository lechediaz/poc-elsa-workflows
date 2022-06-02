import { BehaviorSubject } from 'rxjs';
import { STORAGE } from '../../constants';

class _UserSessionService {
  userSession = new BehaviorSubject(null);

  getUserSession() {
    const userSessionString = localStorage.getItem(STORAGE.USER_SESION);

    if (userSessionString !== null) {
      const userSession = JSON.parse(userSessionString);

      this.userSession.next(userSession);
    }
  }

  setUserSession(userSession) {
    if (typeof userSession === 'object') {
      const userSessionString = JSON.stringify(userSession);

      localStorage.setItem(STORAGE.USER_SESION, userSessionString);

      this.userSession.next(userSession);
    }
  }

  logOut() {
    localStorage.removeItem(STORAGE.USER_SESION);
    this.userSession.next(null);
  }
}

export const UserSessionService = new _UserSessionService();

export default UserSessionService;
