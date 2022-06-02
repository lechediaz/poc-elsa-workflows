import React, { useEffect } from 'react';
import { useHistory } from 'react-router';
import { Layout } from '..';
import { ROUTES } from '../../constants';

// Services
import { UserSessionService } from '../../services';

export const ProtectedRoute = (props) => {
  const history = useHistory();

  useEffect(() => {
    const subscription = UserSessionService.userSession.subscribe(
      (userSession) => {
        if (userSession === null) {
          history.push(ROUTES.CHOOSE_USER);
        }
      }
    );

    return () => {
      subscription.unsubscribe();
    };
  }, []);

  return <Layout>{props.children}</Layout>;
};
