import styles from "./shopping-cart.module.scss"
import React, { useState } from 'react';
import Offcanvas from 'react-bootstrap/Offcanvas';
import MyText from "../text";
import ICartProduct from "../../types/ICartProduct";
import { Col, Row, Container, Button, Form } from 'react-bootstrap';
import CartItem from "../cart-item/cart-item";
import FlipMove from 'react-flip-move';
import { useNavigate } from "react-router-dom";

import type { RootState } from '../../redux/store'
import { useSelector, useDispatch } from 'react-redux'
import {clear } from '../../redux/cart-slice'
import { RectButton } from "../buttons/buttons";

import OrderService from "../../services/order.service";

export default function ShoppingCart(props: any) {
    // redux
    const cartItems = useSelector((state: RootState) => state.cart.items);
    const user = useSelector((state: RootState) => state.user.user);
    const dispatch = useDispatch();
    
    const navigate = useNavigate();

    const handleClose = () => { 
        props.onClose();
    }

    const calcPrice = (mapper: (val: any) => number) => {
        return Math.round(cartItems.map(mapper).reduce((a, b) => a + b, 0) * 100) / 100
    }

    const checkout = () => {
        let itemsIds = cartItems.map((val: any) => val.id)

        if (user) {
            OrderService.createOrder(user.id, itemsIds);
            dispatch(clear());
        }
        else {
            props.onClose();
            navigate("/login");
        }
    }

    return (
        <Offcanvas show={props.show} onHide={handleClose} placement="end">
            <Offcanvas.Header closeButton>
            <Offcanvas.Title><MyText>Cart</MyText></Offcanvas.Title>
            </Offcanvas.Header>
            <Offcanvas.Body className={styles.main}>
                <Container fluid className={styles.productsContainer}>
                    <FlipMove className={styles.col}>
                        {cartItems.map((value: ICartProduct) => (
                            <div key={value.index} className={styles.item}>
                                <CartItem product={value}/>
                            </div>
                        ))}
                    </FlipMove>
                </Container>
                
                <div className={styles.checkoutBlock}>
                    <div style={{fontSize: "1.1em"}}><MyText>Final text:</MyText></div>
                    <div style={{marginLeft: "5%"}}><MyText>{cartItems.map((val: any) => val.symbol).join('')}</MyText></div>
                    
                    <div style={{fontSize: "1.1em", marginTop: "5%"}}><MyText>Total:</MyText></div>
                    <div className={styles.priceBlock}>
                        <div className={styles.sale}>
                            <MyText>${calcPrice((val: any) => val.sale)}</MyText>
                        </div>
                        <div className={styles.price}>
                            <MyText>${calcPrice((val: any) => val.price)}</MyText>
                        </div>
                    </div>

                    <RectButton text="Checkout" onClick={checkout}/>
                </div>
            </Offcanvas.Body>
        </Offcanvas>
    );
}
