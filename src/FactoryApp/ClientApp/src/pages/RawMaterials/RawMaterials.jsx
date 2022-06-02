import React, { useEffect, useState } from 'react';
import { Table } from 'reactstrap';
import { RawMaterialsListService } from '../../services';

export const RawMaterials = () => {
  const [rawMaterials, setRawMaterials] = useState([]);

  useEffect(() => {
    RawMaterialsListService.getRawMaterialsList()
      .then((_rawMaterialsList) => setRawMaterials(_rawMaterialsList))
      .catch(() => console.log('error'));
  }, []);

  return (
    <div>
      <h2>Materias primas</h2>
      <p>
        A continuación podrá observar la cantidad de materia prima que se
        encuentra en bodega de la fábrica. Visita la opción del menú{' '}
        <em>Solicitudes</em> para conocer el estado de tus solicitudes o crear
        una nueva.
      </p>
      <Table striped>
        <thead>
          <tr>
            <th>Nombre</th>
            <th>Cantidad Disponible</th>
          </tr>
        </thead>
        <tbody>
          {rawMaterials &&
            rawMaterials.map((material) => (
              <tr key={`material-${material.id}`}>
                <td>{material.name}</td>
                <td>{material.stock}</td>
              </tr>
            ))}
          {rawMaterials.length === 0 && (
            <tr>
              <td colSpan="2">Sin registros</td>
            </tr>
          )}
        </tbody>
      </Table>
    </div>
  );
};

export default RawMaterials;
