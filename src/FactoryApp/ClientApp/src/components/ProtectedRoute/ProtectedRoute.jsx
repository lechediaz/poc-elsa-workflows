import React, { useEffect } from 'react';
import { useHistory } from 'react-router';
import { Layout } from '..';

// Services
import { UserSessionService } from '../../services';

export const ProtectedRoute = (props) => {
  const history = useHistory();

  useEffect(() => {
    const subscription = UserSessionService.userSession.subscribe(
      (userSession) => {
        if (userSession === null) {
          history.push('/choose-user');
        }
      }
    );

    return () => {
      subscription.unsubscribe();
    };
  }, []);

  return <Layout>{props.children}</Layout>;
};
