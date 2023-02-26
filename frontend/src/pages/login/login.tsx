import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import styles from "./login.module.css"
import {useState} from "react";
import { useNavigate } from "react-router-dom";
import Card from 'react-bootstrap/Card';

import type { RootState } from '../../redux/store'
import { useSelector, useDispatch } from 'react-redux'
import { addToken, addUserInfo } from '../../redux/user-slice'

import AuthService from "../../services/auth.service";

type State = {
    username: string,
    password: string,
    loading: boolean,
    message: string
};

export default function Login(props: any) {
    // redux
    const user = useSelector((state: RootState) => state.user.user)
    const dispatch = useDispatch()

    const [state, setState] = useState({
        username: "",
        password: "",
        loading: false,
        message: ""
    } as State);
    
    const navigate = useNavigate();

    if (user) 
        navigate("/store");

    const validationSchema = () => {
        return Yup.object().shape({
        username: Yup.string().required("This field is required!"),
        password: Yup.string().required("This field is required!"),
        });
    }

    const handleLogin = (formValue: { username: string; password: string }) => {
        let { username, password } = formValue;

        setState({
            message: "",
            loading: true,
            username: state.username,
            password: state.password
        });

        AuthService.login(username, password).then(response => {
            if (response.data.value) {
                dispatch(addToken(response.data.value));

                AuthService.getUserData(response.data.value).then(response => {
                    dispatch(addUserInfo({id: response.data.value.id, username: response.data.value.username}));
                    navigate("/store");
                })
            }
            if (response.data.statusCode === 401) {
                setState({
                    loading: false,
                    message: "Invalid password or username...",
                    username: state.username,
                    password: state.password
                });
            }

        },
        error => {
            console.log(error);
            const resMessage =
            (error.response &&
                error.response.data &&
                error.response.data.message) ||
            error.message ||
            error.toString();

            setState({
                loading: false,
                message: resMessage,
                username: state.username,
                password: state.password
            });
        });
    }

    const { loading, message } = state;

    const initialValues = {
        username: "",
        password: "",
    };

    return (
        <div className={`${styles.page}`}>
            <Card className={`${styles.card} border border-2 rounded-0`} >
                <Formik
                    initialValues={initialValues}
                    validationSchema={validationSchema}
                    onSubmit={handleLogin}
                >
                    <Form>
                    <div className={styles.formGroup}>
                        <label htmlFor="username">Username</label>
                        <Field name="username" type="text" className="search-form border border-2 rounded-0" />
                        <ErrorMessage
                        name="username"
                        component="div"
                        className={styles.alert}
                        />
                    </div>

                    <div className={styles.formGroup}>
                        <label htmlFor="password">Password</label>
                        <Field name="password" type="password" className="search-form border border-2 rounded-0" />
                        <ErrorMessage
                        name="password"
                        component="div"
                        className={styles.alert}
                        />
                    </div>

                    <div>
                        <button type="submit" className={"rect-button"} disabled={loading}>
                            {loading && (
                                <span className="spinner-border spinner-border-sm"></span>
                            )}
                        <span>Login</span>
                        </button>
                    </div>

                    {message && (
                        <div className="form-group">
                        <div className="border border-2 rounded-0 mt-4" role="alert">
                            {message}
                        </div>
                        </div>
                    )}
                    </Form>
                </Formik>
            </Card>
        </div>
    );
}
