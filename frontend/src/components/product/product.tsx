import { Component, ReactNode } from "react";
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import './product.scss'
import IProduct from "../../types/IProduct";
import {RectButton, TextButton} from "../buttons/buttons";

import MyText from "../text";

type Props = {
    product: IProduct
}

type State = {
    qty: number 
};

export default class Product extends Component<Props, State> {
    state = {
        qty: 0
    }

    AddToCart() { this.setState({qty: 1}); }

    Plus() { 
        this.setState(prevState => {
            return { qty: prevState.qty + 1 }
        }); 
    }    

    Minus() { 
        if (this.state.qty > 0)
            this.setState(prevState => {
                return { qty: prevState.qty - 1 }
            }); 
    }   

    render() {
        return (
            <Card className="product border border-2 rounded-0" >
                <div className="symbol">
                    {this.props.product.symbol}
                </div>

                <Card.Body className="product-body">
                    <Card.Text style={{fontFamily: "consolas"}}>
                        {this.props.product.description}
                    </Card.Text>

                    <div className="sale">
                        <MyText>${this.props.product.sale}</MyText>
                    </div>

                    <div className="price">
                        <MyText>${this.props.product.price}</MyText>
                    </div>
                    
                    {this.state.qty === 0 && 
                        <RectButton text="+ Add to cart" onClick={() => this.AddToCart()}/>
                    }
                    
                    {this.state.qty > 0 && 
                        <var className="qty-changer">
                            <TextButton text="-" onClick={() => this.Minus()}/>
                            <var><MyText>{this.state.qty}</MyText></var>
                            <TextButton text="+" onClick={() => this.Plus()}/>
                        </var>
                    }

                </Card.Body>
            </Card>
        )
    }     
}