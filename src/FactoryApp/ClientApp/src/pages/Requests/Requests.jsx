import React from 'react';
import { useHistory } from 'react-router-dom';
import { ROUTES } from '../../constants';

export const Requests = () => {
  const history = useHistory();

  const onNewRequestClick = () => {
    history.push(ROUTES.NEW_REQUESTS);
  };

  return (
    <div>
      <h2>Solicitudes</h2>
      <p>
        A continuación podrá observar el estado de las diferentes solicitudes en
        las que usted se encuentre relacionado. Para crear una nueva solicitud,
        haga click en el botón '<strong>Nueva solicitud</strong>'.
      </p>
      <button className="btn btn-info mb-3" onClick={onNewRequestClick}>
        Nueva solicitud
      </button>
    </div>
  );
};

export default Requests;
