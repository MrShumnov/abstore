import Product from "../../components/product/product";
import IProduct from "../../types/IProduct";
import { Container, Form } from 'react-bootstrap';
import ProductService from "../../services/product.service"; 
import Card from 'react-bootstrap/Card';
import "./store.scss"
import FilterGroup from "../../components/filter-group/filter-group";
import FlipMove from 'react-flip-move';
import { useState, useEffect } from 'react'
import { useSearchParams } from "react-router-dom";
import { useDeviceType, DeviceType } from '../../hooks/responsive';
 
type State = {
    products: IProduct[]
};

export default function Store(props: any) { 
    const [state, setState] = useState({
        products: []
    } as State);
    
    const [searchParams, setSearchParams] = useSearchParams();

    const deviceType = useDeviceType();
    
    useEffect(() => {
        let name = searchParams.get('name');
        let types = searchParams.get('type');
        let cases = searchParams.get('case');

        ProductService.getProducts(name ? name : undefined, types ? types : undefined, cases ? cases : undefined).then(response => {
            setState({ products: response.data.value });
        });
    }, [searchParams]);

    const colQty = () => {
        if (deviceType === DeviceType.Desktop)
            return 3;
        if (deviceType === DeviceType.Tablet)
            return 2;
        if (deviceType === DeviceType.BigMobile)
            return 2;
        if (deviceType === DeviceType.Mobile)
            return 2;

        return 1;
    }

    const addUrlParam = (name: string, value: any) => {
        searchParams.set(name, value);
        setSearchParams(searchParams);
    }

    const placeSpacers = (arr: any) => {
        // if (deviceType === DeviceType.Mobile)
        //     return arr;

        let cols = colQty();

        let idx = -1;
        for (let pos = arr.length - arr.length % cols; pos > 0; pos -= cols) {
            arr.splice(pos, 0, (
                <div className="col" key={idx-1} style={{minWidth: "100%", height: "0", margin: "0", padding: "0"}} />
            ));

            if (deviceType !== DeviceType.Mobile)
                arr.splice(pos, 0, (
                    <div className="col" key={idx} />
                ));

            idx -= 2;
        }

        return arr;
    }

    const queryValue = (name: string) => {
        let val = searchParams.get(name);
        return val ? val : "";
    }

    const checkQueryValue = (name: string, value: string) => {
        let values = searchParams.get(name)
        
        return (values && values.includes(value)) as boolean;
    };

    return (
        <div className={deviceType === DeviceType.Mobile ? "store-container-mobile" : "store-container"}>
            <Card className={(deviceType === DeviceType.Mobile ? "filter-mobile" : "filter") + " border border-2 rounded-0"}>
                <Card.Body className="filter-body">
                    <Card.Text style={{textAlign: "left", fontFamily: "consolas"}}>
                        Filter
                    </Card.Text>

                    <Form className="d-flex">
                        <Form.Control
                            placeholder="Search"
                            className="search-form me-2"
                            aria-label="Search"
                            value={queryValue("name")}
                            onChange={(e: any) => addUrlParam('name', e.target.value)}
                        />
                    </Form>

                    <div className={deviceType === DeviceType.Mobile ? "filter-group-mobile" : "filter-group"}>
                        <FilterGroup title="Type" 
                            boxes={[{boxName: "Letters", optionName: "letters", defaultValue: checkQueryValue('type', 'letters')}, 
                                    {boxName: "Other", optionName: "other", defaultValue: checkQueryValue('type', 'other')}]}
                            addUrlParam={(value: any) => addUrlParam('type', value)}/>
                    </div>

                    <div className={deviceType === DeviceType.Mobile ? "filter-group-mobile" : "filter-group"}>
                        <FilterGroup title="Case"
                            boxes={[{boxName: "Lowercase", optionName: "lower", defaultValue: checkQueryValue('case', 'lower')}, 
                                    {boxName: "Uppercase", optionName: "upper", defaultValue: checkQueryValue('case', 'upper')}]}
                            addUrlParam={(value: any) => addUrlParam('case', value)}/>
                    </div>
                </Card.Body>
            </Card>

            <Container fluid className="products-container">
                <FlipMove className={deviceType === DeviceType.Mobile ? "row-mobile row" : "rowp row"} maintainContainerHeight={false}>
                    {placeSpacers(state.products.map((product: IProduct, idx: number) => (
                            <div className={deviceType === DeviceType.Mobile ? "col-mobile col" : "colp col"} key={product.id}>
                                <Product product={product} />
                            </div>
                        )))                    
                    }
                </FlipMove>
            </Container>
        </div>
    )
}