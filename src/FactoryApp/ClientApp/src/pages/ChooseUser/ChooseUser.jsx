import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { ROUTES } from '../../constants';

// Services
import { SessionService, UsersListService } from '../../services';

import './ChooseUser.css';

export const ChooseUser = () => {
  const history = useHistory();
  const [usersList, setUsersList] = useState([]);
  const [userIdSelected, setUserIdSelected] = useState('');

  useEffect(() => {
    const subscription = SessionService.userSession.subscribe((userSession) => {
      if (userSession !== null) {
        goToHome();
      }
    });

    UsersListService.getUsersList()
      .then((_usersList) => setUsersList(_usersList))
      .catch(() => console.log('error'));

    return () => {
      subscription.unsubscribe();
    };
  }, []);

  const goToHome = () => history.push(ROUTES.HOME);

  const onUserSelected = (event) => {
    const selectedId = event.target.value;
    setUserIdSelected(selectedId);
  };

  const onStartSessionClick = () => {
    const selectedUser = usersList.find((u) => u.id == userIdSelected);
    SessionService.setUserSession(selectedUser);
    goToHome();
  };

  return (
    <div className="bg-dark text-light d-flex flex-column justify-content-center vh-100">
      <p className="text-center">
        Por favor escoja un usuario de la siguiente lista y presione en el botón
        'Empezar sesión' para simular la sesión
      </p>
      <div className="d-flex flex-column align-items-center">
        <div className="col-5 input-group">
          <select
            className="form-control"
            onChange={onUserSelected}
            value={userIdSelected}
          >
            <option value="">Seleccione un usuario</option>
            {usersList &&
              usersList.map((user, i) => (
                <option key={`user-${i}`} value={user.id}>
                  {user.name}
                </option>
              ))}
          </select>
          <div className="input-group-append">
            <button
              disabled={userIdSelected.length === 0}
              className="btn btn-success"
              onClick={onStartSessionClick}
            >
              Empezar sesión
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
