import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { ProtectedRoute } from './components';
import { ChooseUser, Home, NewRequest, RawMaterials, Requests } from './pages';
import { ROUTES } from './constants';

// Services
import UserSessionService from './services/UserSesionService/UserSessionService';

import './custom.css';

export default class App extends Component {
  static displayName = App.name;

  componentDidMount() {
    UserSessionService.getUserSession();
  }

  render() {
    return (
      <Switch>
        <Route exact path={ROUTES.HOME}>
          <ProtectedRoute>
            <Home />
          </ProtectedRoute>
        </Route>
        <Route exact path={ROUTES.RAW_MATERIALS}>
          <ProtectedRoute>
            <RawMaterials />
          </ProtectedRoute>
        </Route>
        <Route exact path={ROUTES.REQUESTS}>
          <ProtectedRoute>
            <Requests />
          </ProtectedRoute>
        </Route>
        <Route exact path={ROUTES.NEW_REQUESTS}>
          <ProtectedRoute>
            <NewRequest />
          </ProtectedRoute>
        </Route>
        <Route exact path={ROUTES.CHOOSE_USER} component={ChooseUser} />
      </Switch>
    );
  }
}
