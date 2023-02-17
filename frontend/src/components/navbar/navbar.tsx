import {
  Button,
  Container,
  Nav,
  Navbar as NavbarContainer,
} from 'react-bootstrap';
import { Component } from "react";
import { NavLink } from 'react-router-dom';
import MyText from "../text";
import "./navbar.scss"

import authService from '../../services/auth.service';

type Props = {};

type State = {
};

export default class Navbar extends Component<Props, State> {
  render() {
    let loginBlock;

    if (authService.getCurrentUser() === null)
      loginBlock = (
        <Nav className="ms-auto">
          <Nav.Link to="/login" as={NavLink}>
            <MyText>Login</MyText>
          </Nav.Link>
          <Nav.Link to="/register" as={NavLink}>
            <MyText>Register</MyText>
          </Nav.Link>
        </Nav>
      )
    else
      loginBlock = (
        <Nav className="ms-auto">
          <Nav.Link to="/logout" as={NavLink}>
            <MyText>Logout</MyText>
          </Nav.Link>
        </Nav>
    )
    

    return (
      <NavbarContainer sticky="top" className="navbar bg-white shadow-sm mb-3">
        <Container>
          <Nav className="me-auto">
            <Nav.Link to="/" as={NavLink}>
              <MyText>Home</MyText>
            </Nav.Link>
            <Nav.Link to="/store" as={NavLink}>
              <MyText>Store</MyText>
            </Nav.Link>
          </Nav>

          {loginBlock}
        </Container>
      </NavbarContainer>
    );
  }
}
  