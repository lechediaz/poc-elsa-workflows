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
    case 4:
      statusName = 'En negociación';
      break;
    case 5:
      statusName = 'En camino';
      break;
    case 6:
      statusName = 'Completada';
      break;

    default:
      statusName = 'Borrador';
      break;
  }

  return statusName;
};
