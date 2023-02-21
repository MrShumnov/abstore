import { Component } from "react";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import Card from 'react-bootstrap/Card';
import styles from "./register.module.css"

import AuthService from "../../services/auth.service";

type Props = {};

type State = {
  username: string,
  login: string,
  password: string,
  successful: boolean,
  message: string
};

export default class Register extends Component<Props, State> {
  constructor(props: Props) {
    super(props);
    this.handleRegister = this.handleRegister.bind(this);

    this.state = {
      username: "",
      login: "",
      password: "",
      successful: false,
      message: ""
    };
  }

  validationSchema() {
    return Yup.object().shape({
      username: Yup.string()
        .test(
          "len",
          "The username must be between 3 and 20 characters.",
          (val: any) =>
            val &&
            val.toString().length >= 3 &&
            val.toString().length <= 20
        )
        .required("This field is required!"),
      login: Yup.string()
        .required("This field is required!"),
      password: Yup.string()
        .test(
          "len",
          "The password must be between 6 and 40 characters.",
          (val: any) =>
            val &&
            val.toString().length >= 6 &&
            val.toString().length <= 40
        )
        .required("This field is required!"),
    });
  }

  handleRegister(formValue: { username: string; login: string; password: string }) {
    const { username, login, password } = formValue;

    this.setState({
      message: "",
      successful: false
    });

    AuthService.register(
      username,
      login,
      password
    ).then(
      response => {
        this.setState({
          message: response.data.message,
          successful: true
        });
      },
      error => {
        const resMessage =
          (error.response &&
            error.response.data &&
            error.response.data.message) ||
          error.message ||
          error.toString();

        this.setState({
          successful: false,
          message: resMessage
        });
      }
    );
  }

  render() {
    const { successful, message } = this.state;

    const initialValues = {
      username: "",
      login: "",
      password: "",
    };

    return (
      <div className={`${styles.page}`}>
        <Card className={`${styles.card} border border-2 rounded-0`} >
          <Formik
            initialValues={initialValues}
            validationSchema={this.validationSchema}
            onSubmit={this.handleRegister}
          >
            <Form>
              {!successful && (
                <div>
                  <div className={styles.formGroup}>
                    <label htmlFor="username"> Username </label>
                    <Field name="username" type="text" className="search-form" />
                    <ErrorMessage
                      name="username"
                      component="div"
                      className={styles.alert}
                    />
                  </div>

                  <div className={styles.formGroup}>
                    <label htmlFor="login"> Login </label>
                    <Field name="login" type="login" className="search-form" />
                    <ErrorMessage
                      name="login"
                      component="div"
                      className={styles.alert}
                    />
                  </div>

                  <div className={styles.formGroup}>
                    <label htmlFor="password"> Password </label>
                    <Field
                      name="password"
                      type="password"
                      className="search-form"
                    />
                    <ErrorMessage
                      name="password"
                      component="div"
                      className={styles.alert}
                    />
                  </div>

                  <div>
                    <button type="submit" className={"rect-button"}>Sign Up</button>
                  </div>
                </div>
              )}

              {message && (
                <div className="form-group">
                  <div
                    className={
                      successful ? "alert alert-success" : "alert alert-danger"
                    }
                    role="alert"
                  >
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
}
