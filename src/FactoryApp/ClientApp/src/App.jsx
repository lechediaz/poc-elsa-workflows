import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { ProtectedRoute } from './components';
import { ChooseUser, Home } from './pages';

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
        <Route exact path="/">
          <ProtectedRoute>
            <Home />
          </ProtectedRoute>
        </Route>
        <Route exact path="/choose-user" component={ChooseUser} />
      </Switch>
    );
  }
}
