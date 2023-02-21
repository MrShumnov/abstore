import { Component, ReactNode } from "react";
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import styles from './cart-item.module.scss'
import ICartProduct from "../../types/ICartProduct";
import {RectButton, TextButton} from "../buttons/buttons";

import type { RootState } from '../../redux/store'
import { useSelector, useDispatch } from 'react-redux'
import { moveProduct, removeByItemIndex } from '../../redux/cart-slice'

import MyText from "../text";

type Props = {
    product: ICartProduct
}

export default function CartItem(props: Props) {
    // redux
    const cartItems = useSelector((state: RootState) => state.cart.items)
    const dispatch = useDispatch()

    if (props.product.symbol === " ")
        props.product.symbol = "_"

    const moveUp = () => {
        dispatch(moveProduct({itemIndex: props.product.index, shift: -1}));
    }

    const moveDown = () => {
        dispatch(moveProduct({itemIndex: props.product.index, shift: 1}));
    }

    const remove = () => {
        dispatch(removeByItemIndex(props.product.index));
    }

    return (
        <Card className={`${styles.product} border border-2 rounded-0`} >
            <div className={styles.productBody}>  
                <div className={styles.movingBlock}>
                    <TextButton text="▲" onClick={moveUp}/>
                    <TextButton text="▼" onClick={moveDown}/>
                </div>

                <div className={styles.symbol}>
                    <MyText>{props.product.symbol}</MyText>
                </div>

                <div className={styles.priceBlock}>
                    <div className={styles.price}>
                        <MyText>${props.product.price}</MyText>
                    </div>
                    
                    <div className={styles.sale}>
                        <MyText>${props.product.sale}</MyText>
                    </div>
                </div>
                
                <TextButton text="✖" onClick={remove}/>
            </div>
        </Card>
    ) 
}