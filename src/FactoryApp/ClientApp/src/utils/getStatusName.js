export const getStatusName = (status) => {
  let statusName = '';

  switch (status) {
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

  return statusName;
};
