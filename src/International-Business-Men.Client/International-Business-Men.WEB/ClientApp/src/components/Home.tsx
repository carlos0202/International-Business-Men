import * as React from 'react';
import { connect } from 'react-redux';
import { Container, Jumbotron } from 'reactstrap';

const Home = () => (
    <div>
        <Jumbotron fluid>
            <Container fluid>
                <h1 className="display-3">International Business Man App!</h1>
                <p className="lead">Bienvenido a su app m&aacute;s completa para la gesti&oacute; de divisas!!!</p>
            </Container>
        </Jumbotron>
  </div>
);

export default connect()(Home);