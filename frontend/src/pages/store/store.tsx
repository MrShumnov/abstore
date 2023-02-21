import { Component } from "react";
import Product from "../../components/product/product";
import IProduct from "../../types/IProduct";
import { Col, Row, Container, Button, Form } from 'react-bootstrap';
import ProductService from "../../services/product.service"; 
import Card from 'react-bootstrap/Card';
import "./store.scss"
import WithSearchParams from "../../hooks/with-search-params";
import FilterGroup from "../../components/filter-group/filter-group";
import FlipMove from 'react-flip-move';

type State = {
    colQty: number,
    products: IProduct[]
};

class Store extends Component<any, State> {    
    state = {
        colQty: 3,
        products: []
    }

    constructor(props: any) {
        super(props);

        this.updateProducts();
    }

    updateProducts() {
        let name = this.props.searchParams.get('name');
        let types = this.props.searchParams.get('type');
        let cases = this.props.searchParams.get('case');

        ProductService.getProducts(name ? name : undefined, types ? types : undefined, cases ? cases : undefined).then(response => {
            this.setState({products: response.data.value});
        });
    }

    addUrlParam(name: string, value: any) {
        this.props.searchParams.set(name, value);
        this.props.setSearchParams(this.props.searchParams);
            
        this.updateProducts();
    }

    spacer = (id: number) => (
        <div className="col" key={id}>
        </div>
    )

    placeSpacers(arr: any) {
        arr.splice(arr.length, 0, this.spacer(-1));

        if (arr.length > this.state.colQty) {
            let pos = this.state.colQty;
            for (let i = 0; i < Math.floor(arr.length / this.state.colQty); i++) {
                arr.splice(pos + i, 0, this.spacer(-i - 2));
                pos += this.state.colQty;
            }
        }

        return arr;
    }

    queryValue = (name: string) => {
        let val = this.props.searchParams.get(name);
        return val ? val : "";
    }

    checkQueryValue = (name: string, value: string) => {
        let values = this.props.searchParams.get(name)
        
        return values && values.includes(value);
    }

    render() {
        return (
            <div className="store-container">
                <Card className="filter border border-2 rounded-0">
                    <Card.Body className="filter-body">
                        <Card.Text style={{textAlign: "left", fontFamily: "consolas"}}>
                            Filter
                        </Card.Text>

                        <Form className="d-flex">
                            <Form.Control
                                placeholder="Search"
                                className="search-form me-2"
                                aria-label="Search"
                                value={this.queryValue("name")}
                                onChange={(e: any) => this.addUrlParam('name', e.target.value)}
                            />
                        </Form>

                        <FilterGroup title="Type" 
                            boxes={[{boxName: "Letters", optionName: "letters", defaultValue: this.checkQueryValue('type', 'letters')}, 
                                    {boxName: "Other", optionName: "other", defaultValue: this.checkQueryValue('type', 'other')}]}
                            addUrlParam={(value: any) => this.addUrlParam('type', value)}/>

                        <FilterGroup title="Case"
                            boxes={[{boxName: "Lowercase", optionName: "lower", defaultValue: this.checkQueryValue('case', 'lower')}, 
                                    {boxName: "Uppercase", optionName: "upper", defaultValue: this.checkQueryValue('case', 'upper')}]}
                            addUrlParam={(value: any) => this.addUrlParam('case', value)}/>
                    </Card.Body>
                </Card>

                <Container fluid className="products-container">
                    <FlipMove className="row" maintainContainerHeight={false}>
                        {this.placeSpacers(this.state.products.map((product: IProduct, idx: number) => (
                                <div className="col" key={product.id}>
                                    <Product product={product} />
                                </div>
                            )))                      
                        }
                    </FlipMove>
                </Container>
            </div>
        )
    }
}

export default WithSearchParams(Store);