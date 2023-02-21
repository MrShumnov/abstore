import axios from "axios";
import authHeader from "./auth-header";

import { IOrderRequestDto } from "../types/dto/request/IOrderRequestDto";

const API_URL = "https://localhost:7264/api/";

class OrderService {
  async createOrder(userId: number, itemsIds: number[]) {
    console.log(authHeader());
    return axios
      .post(API_URL + "orders", {userId, itemsIds} as IOrderRequestDto, { headers: authHeader() })
      .then(response => {
        return response.data;
      });
  }
}

export default new OrderService();
