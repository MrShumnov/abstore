import axios from "axios";

import { IAuthDto } from "../types/dto/request/IAuthDto";
import IUser from "../types/IUser";

const API_URL = "https://localhost:7264/api/";

class AuthService {
  login(login: string, password: string) {
    return axios
      .post(API_URL + "auth", {
        login,
        password
      } as IAuthDto)
      .then(response => {
        if (response.data.accessToken) {
          let user = {token: response.data.accessToken} as IUser
          localStorage.setItem("user", JSON.stringify(user));
          
          this.fillUserData(user);
          
        }
        return response.data;
      });
  }

  logout() {
    localStorage.removeItem("user");
  }

  register(username: string, login: string, password: string) {
    return axios.post(API_URL + "users", {
      username,
      login,
      password
    });
  }

  fillUserData(user: IUser) {
    return axios
      .get(API_URL + "users/current")
      .then(response => {
        user.id = response.data.id;
        user.username = response.data.username;
      });
  }

  getCurrentUser() : IUser | null {
    const userStr = localStorage.getItem("user");
    if (userStr) return JSON.parse(userStr);

    return null;
  }
}

export default new AuthService();
