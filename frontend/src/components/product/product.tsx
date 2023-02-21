import React, { useState } from 'react';
import Card from 'react-bootstrap/Card';
import './product.scss'
import IProduct from "../../types/IProduct";
import {RectButton, TextButton} from "../buttons/buttons";


import MyText from "../text";

import type { RootState } from '../../redux/store'
import { useSelector, useDispatch } from 'react-redux'
import { addProduct, removeByProductId } from '../../redux/cart-slice'

export default function Product(props: any) {
    // redux
    const cartItems = useSelector((state: RootState) => state.cart.items)
    const dispatch = useDispatch()

    if (props.product.symbol === " ")
        props.product.symbol = "_";

    const Qty = () => {
        return cartItems.filter((val: any) => val.id === props.product.id).length
    }

    const AddToCart = () => {
        dispatch(addProduct(props.product));
    }

    const Plus = () => {         
        dispatch(addProduct(props.product));
    }    

    const Minus = () => { 
        if (Qty() > 0)
            dispatch(removeByProductId(props.product.id));
    }   

    return (
        <Card className="product border border-2 rounded-0" >
            <div className="symbol">
                <MyText>{props.product.symbol}</MyText>
            </div>

            <Card.Body className="product-body">
                <Card.Text className="description" style={{fontFamily: "consolas"}}>
                    {props.product.description}
                </Card.Text>

                <div className="lower-block">                        
                    {Qty() === 0 && 
                        <RectButton text="+ Add to cart" onClick={AddToCart}/>
                    }
                    
                    {Qty() > 0 && 
                        <var className="qty-changer">
                            <TextButton text="-" onClick={Minus}/>
                            <var><MyText>{Qty()}</MyText></var>
                            <TextButton text="+" onClick={Plus}/>
                        </var>
                    }

                    <div className="price-block">
                        <div className="price">
                            <MyText>${props.product.price}</MyText>
                        </div>
                        
                        <div className="sale">
                            <MyText>${props.product.sale}</MyText>
                        </div>
                    </div>
                </div>
            </Card.Body>
        </Card>
    )  
}