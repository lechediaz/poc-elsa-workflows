import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Hola, estoy aprendiendo Elsa!</h1>
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
