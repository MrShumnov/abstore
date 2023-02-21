import React from 'react';
import {
    useSearchParams,
    useNavigate
} from "react-router-dom";

export default function WithSearchParams (Component: any) {
    return function WrappedComponent(props: any) {
        const [searchParams, setSearchParams] = useSearchParams();
        const navigate = useNavigate();

        return <Component navigate={navigate} searchParams={searchParams} setSearchParams={setSearchParams} {...props} />;
    }
};