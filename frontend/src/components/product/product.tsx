import React from 'react';
import Card from 'react-bootstrap/Card';
import './product.scss'
import {RectButton, TextButton} from "../buttons/buttons";
import { useDeviceType, DeviceType } from '../../hooks/responsive';
import ProductDetailed from '../product-detailed/product-detailed';

import MyText from "../text";

import type { RootState } from '../../redux/store'
import { useSelector, useDispatch } from 'react-redux'
import { addProduct, removeByProductId } from '../../redux/cart-slice'

export default function Product(props: any) {
    // redux
    const cartItems = useSelector((state: RootState) => state.cart.items)
    const dispatch = useDispatch()

    const [modalShow, setModalShow] = React.useState(false);
    
    const deviceType = useDeviceType();

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

    const buttonBlock = (
        <>
            {Qty() === 0 && 
                <RectButton text="+ Add to cart" onClick={AddToCart} wrap={deviceType === DeviceType.Mobile}/>
            }
            {deviceType < 2 &&
                <div style={{height: "0", visibility: "hidden"}}>
                    <RectButton text="+ Add to cart" onClick={() => null} wrap={deviceType === DeviceType.Mobile}/>
                </div>
            }
            
            {Qty() > 0 && 
                <var className="qty-changer">
                    <TextButton text="-" onClick={Minus}/>
                    <var><MyText>{Qty()}</MyText></var>
                    <TextButton text="+" onClick={Plus}/>
                </var>
            }
        </>
    )

    return (
        <Card className={(deviceType < 2 ? "product-mobile" : "product") + " border border-2 rounded-0"}>
            {deviceType < 2 &&
                <ProductDetailed show={modalShow} onHide={() => setModalShow(false)} product={props.product}/>
            }

            <div className={deviceType < 2 ? "symbol-mobile" : "symbol"} onClick={() => setModalShow(true)}>
                <MyText>{props.product.symbol}</MyText>
            </div>

            <Card.Body className="product-body">
                {deviceType > 1 &&
                    <Card.Text className="description" style={{fontFamily: "consolas"}}>
                        {props.product.description}
                    </Card.Text>
                }

                <div className={deviceType < 2 ? "lower-block-mobile" : "lower-block"}>                        
                    {deviceType > 1 && buttonBlock}
                    <div className={deviceType < 2 ? "price-block-mobile" : "price-block"}>
                        <div className={deviceType < 2 ? "price-mobile" : "price"}>
                            <MyText>${props.product.price}</MyText>
                        </div>
                        
                        <div className="sale">
                            <MyText>${props.product.sale}</MyText>
                        </div>
                    </div>                        
                    {deviceType < 2 && buttonBlock}
                </div>
            </Card.Body>
        </Card>
    )  
}