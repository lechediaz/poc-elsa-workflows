import React, { Component } from 'react';
import { filter } from 'rxjs';

// Services
import { SessionService } from '../../services';

export class Home extends Component {
  static displayName = Home.name;

  state = { userSession: null };

  componentDidMount() {
    this.subscription = SessionService.userSession
      .pipe(filter((userSession) => userSession !== null))
      .subscribe((userSession) => {
        this.setState({ userSession });
      });
  }

  componentWillUnmount() {
    this.subscription.unsubscribe();
  }

  render() {
    return (
      <div>
        <h1>
          Hola, {this.state.userSession && this.state.userSession.name}.
          ¡Aprendamos juntos!
        </h1>
        <p>En este proyecto exploramos cómo integrar Elsa a una aplicación.</p>
        <h4>Variables de ambiente:</h4>
        <ul>
          <li>
            <strong>ELSA_SERVER</strong> {process.env.REACT_APP_ELSA_SERVER}
          </li>
          <li>
            <strong>API_SERVER</strong> {process.env.REACT_APP_API_SERVER}
          </li>
        </ul>
      </div>
    );
  }
}
