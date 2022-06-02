import React, { useEffect, useState } from 'react';
import { Alert } from 'reactstrap';
import { RawMaterialsListService } from '../../services';

export const NewRequest = () => {
  const [details, setDetails] = useState({});
  const [request, setRequest] = useState({});
  const [rawMaterialIdSelected, setRawMaterialIdSelected] = useState('');
  const [quantity, setQuantity] = useState(0);
  const [rawMaterials, setRawMaterials] = useState([]);
  const [errorMessage, setErrorMessage] = useState('');

  useEffect(() => {
    RawMaterialsListService.getRawMaterialsList()
      .then((_rawMaterialsList) => setRawMaterials(_rawMaterialsList))
      .catch(() => console.log('error'));
  }, []);

  const onRawMaterialSelected = (event) => {
    const selectedId = event.target.value;
    setRawMaterialIdSelected(selectedId);
  };

  const onQuantityChanged = (event) => {
    const quantityString = event.target.value;
    setQuantity(parseFloat(quantityString));
  };

  const onAddClick = () => {
    const newDetails = {
      ...details,
      [rawMaterialIdSelected]: {
        RawMaterialId: rawMaterialIdSelected,
        Quantity: quantity,
      },
    };

    const newRawMaterials = rawMaterials.filter(
      (m) => m.id != rawMaterialIdSelected
    );

    setDetails(newDetails);
    setRawMaterials(newRawMaterials);
    setRawMaterialIdSelected('');
    setQuantity(0);
  };

  return (
    <div>
      <h2>Crear nueva solicitud</h2>
      <p>
        Seleccione la materia prima que desea solicitar junto con la cantidad
        necesaria y haga click en el botón '<em>Agregar</em>' para agregarlo a
        los detalles. Una vez finalizado, guarde la solicitud haciendo click en
        el botón '<strong>Guardar solicitud</strong>' para dejarla en estado de{' '}
        <em>borrador</em>.
      </p>
      {errorMessage && <Alert color="danger">{errorMessage}</Alert>}
      <div className="d-flex flex-column align-items-center mt-2 mb-3">
        <div className="input-group w-75">
          <div className="input-group-prepend">
            <span className="input-group-text">Materia prima</span>
          </div>

          <select
            className="form-control"
            onChange={onRawMaterialSelected}
            value={rawMaterialIdSelected}
          >
            <option value="">Seleccione la materia prima</option>
            {rawMaterials &&
              rawMaterials.map((material) => (
                <option key={`material-${material.id}`} value={material.id}>
                  {material.name}
                </option>
              ))}
          </select>

          <div className="input-group-prepend">
            <span className="input-group-text">Cantidad</span>
          </div>

          <input
            min="0"
            type="number"
            value={quantity}
            onChange={onQuantityChanged}
          />

          <div className="input-group-append">
            <button
              disabled={rawMaterialIdSelected.length === 0 || quantity <= 0}
              className="btn btn-success"
              onClick={onAddClick}
            >
              Agregar
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default NewRequest;
