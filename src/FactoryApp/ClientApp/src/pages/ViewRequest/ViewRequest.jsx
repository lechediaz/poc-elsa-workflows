import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import { Table } from 'reactstrap';
import { ROUTES } from '../../constants';
import { ElsaService, RequestService } from '../../services';
import { getStatusName } from '../../utils';

export const ViewRequest = () => {
  const history = useHistory();
  let { id } = useParams();
  const [request, setRequest] = useState(null);

  useEffect(() => {
    RequestService.getRequestById(id)
      .then((response) => setRequest(response.data))
      .catch(() => console.log('error'));
  }, []);

  const onPublishClick = () => {
    const info = {
      RequestId: request.id,
      Author: {
        Name: request.createdBy.name,
        Email: request.createdBy.email,
      },
      Approver: {
        Name: request.receiver.name,
        Email: request.receiver.email,
      },
    };

    ElsaService.publishRequest(info)
      .then(() => {
        console.log('Good');
        history.push(ROUTES.REQUESTS);
      })
      .catch(() => console.log('error'));
  };

  const onEditClick = () => {
    const url = ROUTES.EDIT_REQUEST.replace(':id', id);
    history.push(url);
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
              <strong>Creado por:</strong> {request.createdBy.name}
            </div>
            <div className="col-4">
              <strong>Aprobador:</strong> {request.receiver.name}
            </div>
            <div className="col-4">
              <strong>Fecha de Creación:</strong> {request.createdAt}
            </div>
          </div>
          <div className="row">
            <div className="col-4">
              <strong>Estado:</strong> {getStatusName(request.status)}
            </div>
            {request.status === 2 && (
              <div className="col-4">
                <strong>Fecha de Aprobación:</strong> {request.approvedAt}
              </div>
            )}
            {request.status === 3 && (
              <div className="col-4">
                <strong>Fecha de Rechazo:</strong> {request.rejectedAt}
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

          {request.status === 0 && (
            <button className="btn btn-primary mr-2" onClick={onPublishClick}>
              Publicar
            </button>
          )}
          {request.status === 0 && (
            <button className="btn btn-warning" onClick={onEditClick}>
              Editar
            </button>
          )}
        </React.Fragment>
      )}
    </div>
  );
};

export default ViewRequest;
