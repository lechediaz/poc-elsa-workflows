import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { Table } from 'reactstrap';
import { filter } from 'rxjs';
import swal from 'sweetalert';

// Contants
import { ROLES, ROUTES } from '../../constants';

// Services
import { ElsaService, RequestService, SessionService } from '../../services';

// Utils
import { getStatusName } from '../../utils';

export const ViewRequest = () => {
  const history = useHistory();
  let { id } = useParams();
  const [request, setRequest] = useState(null);
  const [userSession, setUserSession] = useState(null);

  useEffect(() => {
    RequestService.getRequestById(id)
      .then((response) => setRequest(response.data))
      .catch(() => console.log('error'));

    const subscription = SessionService.userSession
      .pipe(filter((_userSession) => _userSession !== null))
      .subscribe((_userSession) => setUserSession(_userSession));

    return () => {
      subscription.unsubscribe();
    };
  }, []);

  const onPublishClick = () => {
    swal({
      icon: 'warning',
      text: 'Por favor confirme que desea publicar esta solicitud. Esto iniciará el flujo de aprobación.',
      buttons: {
        cancel: 'Cancelar',
        confirm: 'Aceptar',
      },
    }).then((confirmed) => {
      if (confirmed) {
        const info = {
          RequestId: request.id,
          Author: {
            Name: request.author.name,
            Email: request.author.email,
          },
          Approver: {
            Name: request.approver.name,
            Email: request.approver.email,
          },
        };

        ElsaService.publishRequest(info)
          .then(() =>
            swal({
              icon: 'success',
              text: 'Solicitud publicada correctamente',
              timer: 3000,
            })
          )
          .then(() => {
            history.push(ROUTES.REQUESTS);
          })
          .catch(() => console.log('error'));
      }
    });
  };

  const onNegotiateClick = () => {
    swal({
      icon: 'warning',
      text: 'Por favor confirme que desea iniciar negociaciones a esta solicitud.',
      buttons: {
        cancel: 'Cancelar',
        confirm: 'Aceptar',
      },
    }).then((confirmed) => {
      if (confirmed) {
        const info = {
          RequestId: request.id,
          Author: {
            Name: request.author.name,
            Email: request.author.email,
          },
          DealMaker: {
            Name: userSession.name,
            Email: userSession.email,
          },
        };

        ElsaService.negotiateRequest(info)
          .then(() =>
            swal({
              icon: 'success',
              text: 'Negociación iniciada correctamente',
              timer: 3000,
            })
          )
          .then(() => {
            history.push(ROUTES.REQUESTS);
          })
          .catch(() => console.log('error'));
      }
    });
  };

  const onEditClick = () => {
    const url = ROUTES.EDIT_REQUEST.replace(':id', id);
    history.push(url);
  };

  const onDeleteClick = () => {
    swal({
      icon: 'warning',
      text: 'Por favor confirme que desea eliminar esta solicitud. ¡No se podrá recuperar!.',
      buttons: {
        cancel: 'Cancelar',
        confirm: 'Aceptar',
      },
    }).then((confirmed) => {
      if (confirmed) {
        RequestService.deleteRequest(id)
          .then(() =>
            swal({
              icon: 'success',
              text: 'Solicitud eliminada correctamente',
              timer: 3000,
            })
          )
          .then(() => {
            console.log('Good');
            history.push(ROUTES.REQUESTS);
          })
          .catch(() => console.log('error'));
      }
    });
  };

  return (
    <div>
      <h2>Solicitud {id}</h2>

      <p className="mb-3">
        A continuación se presenta informaciòn acerca de esta solicitud:
      </p>

      {request && (
        <React.Fragment>
          <div className="row">
            <div className="col-4">
              <strong>Creado por:</strong> {request.author.name}
            </div>
            <div className="col-4">
              <strong>Aprobador:</strong> {request.approver.name}
            </div>
            <div className="col-4">
              <strong>Fecha de Creación:</strong> {request.createdAt}
            </div>
          </div>

          <div className="row">
            <div className="col-4">
              <strong>Estado:</strong> {getStatusName(request.status)}
            </div>
            {(request.status === 2 || request.status > 3) && (
              <div className="col-4">
                <strong>Fecha de Aprobación:</strong> {request.approvedAt}
              </div>
            )}
            {request.status === 3 && (
              <div className="col-4">
                <strong>Fecha de Rechazo:</strong> {request.rejectedAt}
              </div>
            )}
            {request.status > 3 && (
              <div className="col-4">
                <strong>Fecha de inicio negociación:</strong>{' '}
                {request.inNegociationAt}
              </div>
            )}
          </div>

          <div className="row">
            {request.status > 4 && (
              <div className="col-4">
                <strong>Fecha de envio:</strong> {request.inShipmentAt}
              </div>
            )}
            {request.status > 5 && (
              <div className="col-4">
                <strong>Fecha de Completado:</strong> {request.completedAt}
              </div>
            )}
          </div>

          <h4 className="mt-3 mb-3">Detalles</h4>

          <Table striped>
            <thead>
              <tr>
                <th>Nombre</th>
                <th>Cantidad a solicitar</th>
              </tr>
            </thead>
            <tbody>
              {request.details &&
                request.details.map((material) => (
                  <tr key={`material-${material.name}`}>
                    <td>{material.name}</td>
                    <td>{material.quantity}</td>
                  </tr>
                ))}
            </tbody>
          </Table>

          {userSession && (
            <React.Fragment>
              {userSession.id === request.author.id && request.status === 0 && (
                <React.Fragment>
                  <button
                    className="btn btn-primary mr-2"
                    onClick={onPublishClick}
                  >
                    Publicar
                  </button>

                  <button
                    className="btn btn-warning mr-2"
                    onClick={onEditClick}
                  >
                    Editar
                  </button>

                  <button className="btn btn-danger" onClick={onDeleteClick}>
                    Eliminar
                  </button>
                </React.Fragment>
              )}

              {userSession.role === ROLES.LOGISTICS && request.status === 2 && (
                <React.Fragment>
                  <button
                    className="btn btn-primary mr-2"
                    onClick={onNegotiateClick}
                  >
                    Iniciar negociaciones
                  </button>
                </React.Fragment>
              )}
            </React.Fragment>
          )}
        </React.Fragment>
      )}
    </div>
  );
};

export default ViewRequest;
