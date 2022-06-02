import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { ProtectedRoute } from './components';
import {
  ChooseUser,
  Home,
  EditRequest,
  RawMaterials,
  Requests,
  ViewRequest,
} from './pages';

import { ROUTES } from './constants';

// Services
import { SessionService } from './services';

import './custom.css';

export default class App extends Component {
  static displayName = App.name;

  componentDidMount() {
    SessionService.getUserSession();
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
        <Route exact path={[ROUTES.NEW_REQUEST, ROUTES.EDIT_REQUEST]}>
          <ProtectedRoute>
            <EditRequest />
          </ProtectedRoute>
        </Route>
        <Route exact path={ROUTES.VIEW_REQUEST}>
          <ProtectedRoute>
            <ViewRequest />
          </ProtectedRoute>
        </Route>
        <Route exact path={ROUTES.CHOOSE_USER} component={ChooseUser} />
      </Switch>
    );
  }
}
