import {
  Button,
  Container,
  Nav,
  Navbar as NavbarContainer,
} from 'react-bootstrap';
import React from 'react';
import { NavLink } from 'react-router-dom';
import MyText from "../text";
import "./navbar.scss"
import {RectButton, TextButton, RoundButton} from "../buttons/buttons";
import ShoppingCart from '../shopping-cart/shopping-cart';
import {useState} from "react";

import authService from '../../services/auth.service';

import type { RootState } from '../../redux/store'
import { useSelector, useDispatch } from 'react-redux'
import { addToken, addUserInfo, remove } from '../../redux/user-slice'

type Props = {};

type State = {
    showCart: boolean
};

export default function Navbar(props: any) {
    // redux
    const user = useSelector((state: RootState) => state.user.user)
    const dispatch = useDispatch()

    const [state, setState] = useState({
        showCart: false
    });

    const showCart = (show: boolean) => {
        setState({showCart: show})
    }

    const logout = () => {
        dispatch(remove());
    }

    const cartButton = (
    <RoundButton onClick={() => showCart(true)} className="cart-button">
        <img style={{alignSelf: "center", maxWidth: "100%", maxHeight: "100%", margin: "0"}} src={require("../../cart.svg").default} />
    </RoundButton>
    )

    return (
    <div>
        <ShoppingCart show={state.showCart} onClose={() => showCart(false)}/>
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

            {!user && 
            <Nav className="ms-auto">
                <Nav.Link to="/login" as={NavLink}>
                <MyText>Login</MyText>
                </Nav.Link>
                <Nav.Link to="/register" as={NavLink}>
                <MyText>Register</MyText>
                </Nav.Link>
                {cartButton}
            </Nav>
            }
            {user &&
            <Nav className="ms-auto">
                <div style={{alignSelf: "center", paddingRight: "0.5em", marginRight: "0.5em", borderRight: "2px solid"}}>
                    <MyText>{user.username}</MyText>
                </div>
                <Nav.Link onClick={logout} style={{marginLeft:"0",paddingLeft: "0", marginRight: "1em"}}>
                <MyText>Logout</MyText>
                </Nav.Link>
                {cartButton}
            </Nav>
            }
        </Container>
        </NavbarContainer>
    </div>
    );
}
  