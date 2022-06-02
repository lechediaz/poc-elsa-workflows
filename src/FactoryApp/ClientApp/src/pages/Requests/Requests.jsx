import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import { Table } from 'reactstrap';
import { filter } from 'rxjs';
import { ROUTES } from '../../constants';
import { RequestService, UserSessionService } from '../../services';

export const Requests = () => {
  const history = useHistory();
  const [requests, setRequests] = useState([]);

  useEffect(() => {
    const subscription = UserSessionService.userSession
      .pipe(filter((_userSession) => _userSession !== null))
      .subscribe((_userSession) => {
        RequestService.getUserRequests(String(_userSession.id))
          .then((response) => {
            const _request = response.data.map((r) => {
              let statusName = '';

              switch (r.status) {
                case 1:
                  statusName = 'Publicada';
                  break;
                case 2:
                  statusName = 'Aprobada';
                  break;
                case 3:
                  statusName = 'Rechazada';
                  break;

                default:
                  statusName = 'Borrador';
                  break;
              }

              const newRequest = { ...r, statusName };

              return newRequest;
            });

            setRequests(_request);
          })
          .catch(() => console.log('error'));
      });

    return () => {
      subscription.unsubscribe();
    };
  }, []);

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

      <Table striped>
        <thead>
          <tr>
            <th>Id</th>
            <th>Creada por</th>
            <th>Aprovador</th>
            <th>Fecha de Creación</th>
            <th>Estado</th>
            <th>Total de Elementos</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {requests &&
            requests.map((request) => (
              <tr key={`request-${request.id}`}>
                <td>{request.id}</td>
                <td>{request.createdBy}</td>
                <td>{request.approver}</td>
                <td>{request.createdAt}</td>
                <td>{request.statusName}</td>
                <td>{request.totalItems}</td>
                <td>
                  <button className="btn btn-primary">Ver</button>
                </td>
              </tr>
            ))}
        </tbody>
      </Table>
    </div>
  );
};

export default Requests;
