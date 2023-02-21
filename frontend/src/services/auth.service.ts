import axios from "axios";

import { IAuthDto } from "../types/dto/request/IAuthDto";
import IUser from "../types/IUser";
import authHeader from "./auth-header";
import host from "./host";

const API_URL = host() + "/api/";

class AuthService {
  async login(login: string, password: string) {
    return axios
      .post(API_URL + "auth", {
        login,
        password
      } as IAuthDto);
  }

  register(username: string, login: string, password: string) {
    return axios.post(API_URL + "users", {
      username,
      login,
      password
    });
  }

  getUserData(token: string) {
    return axios
      .get(API_URL + "users/current", {
        headers: { Authorization: 'Bearer ' + token, 'Access-Control-Allow-Origin': '*' }
      });
  }
}

export default new AuthService();
