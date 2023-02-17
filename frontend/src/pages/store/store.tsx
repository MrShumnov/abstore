import { Component } from "react";
import Product from "../../components/product/product";
import IProduct from "../../types/IProduct";
import { Col, Row, Container } from 'react-bootstrap';

type Props = {};

type State = {
    products: IProduct[]
};

export default class Store extends Component<Props, State> {
    state = {
        products: [
            {id: 1, symbol: "A", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 2, symbol: "a", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 3, symbol: "B", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 4, symbol: "b", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 5, symbol: "C", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 6, symbol: "c", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 7, symbol: "D", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 8, symbol: "d", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 9, symbol: "E", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 10, symbol: "e", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 11, symbol: "F", price: 1.99, sale: 0.99, description: "Description here. Wow!"},
            {id: 12, symbol: "f", price: 1.99, sale: 0.99, description: "Description here. Wow!"}
        ]
    }

    render() {
        // let productEx = {id: 1, symbol: "A", price: 1.99, sale: 0.99, description: "Description here. Wow!"};

        return (
            <Container fluid>
                <Row className="m-auto">
                    {this.state.products.map((product: IProduct) => (
                            <Col className="m-auto" key={product.id}>
                                <Product product={product} />
                            </Col>
                        ))                            
                    }
                </Row>
            </Container>

            /*<div>
                <Product product={productEx}/>
            </div>*/
        )
    }
}